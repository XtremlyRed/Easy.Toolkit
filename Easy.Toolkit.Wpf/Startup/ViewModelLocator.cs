using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows;


namespace Easy.Toolkit
{
    public static class ViewModelLocator
    {
        public static bool? GetAutoAware(DependencyObject obj)
        {
            return (bool?)obj.GetValue(AutoAwareProperty);
        }

        public static void SetAutoAware(DependencyObject obj, bool? value)
        {
            obj.SetValue(AutoAwareProperty, value);
        }


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


        public static void SetDefaultViewTypeToViewModelTypeResolver(Func<Type, Type> viewTypeToViewModelTypeResolver)
        {
            if (viewTypeToViewModelTypeResolver is null)
            {
                return;
            }

            defaultViewTypeToViewModelTypeResolver = viewTypeToViewModelTypeResolver;
        }




        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        internal static IDictionary<string, ViewViewModelAware> viewNameAwares = new Dictionary<string, ViewViewModelAware>();


        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        internal static IDictionary<Type, ViewViewModelAware> viewTypeAwares = new Dictionary<Type, ViewViewModelAware>();


        public static void RegisterView<TView>(this IContainerRegistry registry, string viewName = null, bool autoWareViewModel = true, bool notExistViewModelThrowException = false)
        {
            Type viewType = typeof(TView);

            viewName ??= viewType.Name;

            if (viewNameAwares.TryGetValue(viewName, out ViewViewModelAware viewViewModelAware) && viewViewModelAware != null)
            {
                throw new Exception($"view name : {viewName} already exists");
            }

            Type viewModelType = viewType.GetAttribute<ViewModelLocatorAttribute>()?.ViewModelType;

            ViewViewModelAware aware = new ViewViewModelAware
            {
                AutoWareViewModel = viewModelType is null && autoWareViewModel,
                NotExistViewModelThrowException = notExistViewModelThrowException,
                ViewType = viewType,
                ViewName = viewName,
                ViewModelType = viewModelType
            };

            viewTypeAwares[aware.ViewType] = aware;
            viewNameAwares[aware.ViewName] = aware;

            registry.Register<TView>().AsSingleton();
        }


        public static void RegisterView<TView, TViewModel>(this IContainerRegistry registry, string viewName = null)
        {
            Type viewType = typeof(TView);

            viewName ??= viewType.Name;

            RegisterView<TView>(registry, viewName, false, false);

            viewNameAwares[viewName].ViewModelType = typeof(TViewModel);

            registry.Register<TViewModel>().AsSingleton();
        }



        internal class ViewViewModelAware
        {
            public Type ViewType;

            public Type ViewModelType;

            public string ViewName;

            public bool AutoWareViewModel;

            public bool NotExistViewModelThrowException;
        }



    }



    public class ViewModelLocatorAttribute : Attribute
    {
        public ViewModelLocatorAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        public Type ViewModelType { get; }
    }
}
