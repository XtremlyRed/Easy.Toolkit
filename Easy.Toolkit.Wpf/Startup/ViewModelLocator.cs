using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows;


namespace Easy.Toolkit
{
    /// <summary>
    /// Used to actively position ViewModel
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// automatically associate view model  when true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool? GetAutoAware(DependencyObject obj)
        {
            return (bool?)obj.GetValue(AutoAwareProperty);
        }

        /// <summary>
        /// automatically associate view model  when true
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetAutoAware(DependencyObject obj, bool? value)
        {
            obj.SetValue(AutoAwareProperty, value);
        }

        /// <summary>
        /// automatically associate view model when true
        /// </summary>
        public static readonly DependencyProperty AutoAwareProperty =
            DependencyProperty.RegisterAttached("AutoAware", typeof(bool?),
            typeof(ViewModelLocator),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) =>
            {
                if (e.NewValue is not true)
                {
                    return;
                }
                if (s is not FrameworkElement element)
                {
                    return;
                }
                Type viewType = s.GetType();
                Type viewModelType = viewModelType = defaultViewTypeToViewModelTypeResolver(viewType);

                if (viewModelType is null)
                {
                    return;
                }
                object viewModel = ContainerLocator.Container.Resolve(viewModelType);

                if (viewModel is null)
                {
                    return;
                }
                element.DataContext = viewModel;
            }));



        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        internal static Func<Type, Type> defaultViewTypeToViewModelTypeResolver =
          viewType =>
          {
              string viewName = viewType.FullName;
              viewName = viewName.Replace(".Views.", ".ViewModels.");
              string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
              string suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
              string viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}{1}, {2}", viewName, suffix, viewAssemblyName);
              return Type.GetType(viewModelName);
          };


        /// <summary>
        /// default view type to view model type resolver
        /// </summary>
        /// <param name="viewTypeToViewModelTypeResolver"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetDefaultViewTypeToViewModelTypeResolver(Func<Type, Type> viewTypeToViewModelTypeResolver)
        {
            if (viewTypeToViewModelTypeResolver is null)
            {
                return;
            }

            defaultViewTypeToViewModelTypeResolver = viewTypeToViewModelTypeResolver;
        }

         
        internal class ViewViewModelAware
        {
            public Type ViewType;

            public Type ViewModelType;

            public string ViewName;

            public bool AutoWareViewModel;

            public bool ThrowExceptionWhenViewModelNotExist;
        }



    }


    /// <summary>
    /// view model locator attribute
    /// </summary>
    public class ViewModelLocatorAttribute : Attribute
    {
        /// <summary>
        /// create a new view model locator attribute from <paramref name="viewModelType"/>
        /// </summary>
        /// <param name="viewModelType"></param>
        public ViewModelLocatorAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        /// <summary>
        /// view model type
        /// </summary>
        public Type ViewModelType { get; }
    }
}
