using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Easy.Toolkit.ViewModelLocator;

namespace Easy.Toolkit 
{
    /// <summary>
    /// container extensions
    /// </summary>
    public static partial class ViewRegisterExtensions
    { 
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        internal static IDictionary<string, ViewViewModelAware> viewNameAwares = new Dictionary<string, ViewViewModelAware>();


        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        internal static IDictionary<Type, ViewViewModelAware> viewTypeAwares = new Dictionary<Type, ViewViewModelAware>();


        /// <summary>
        /// regisger view into container
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="registry"></param>
        /// <param name="viewName"></param>
        /// <param name="autoWareViewModel"></param>
        /// <param name="throwExceptionWhenViewModelNotExist"></param>
        /// <exception cref="Exception"></exception>
        public static void RegisterView<TView>(this IContainerRegistry registry, string viewName = null, bool autoWareViewModel = true, bool throwExceptionWhenViewModelNotExist = false)
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
                ThrowExceptionWhenViewModelNotExist = throwExceptionWhenViewModelNotExist,
                ViewType = viewType,
                ViewName = viewName,
                ViewModelType = viewModelType
            };

            viewTypeAwares[aware.ViewType] = aware;
            viewNameAwares[aware.ViewName] = aware;

            registry.Register<TView>().AsSingleton();
        }

        /// <summary>
        ///  Register View
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="registry"></param>
        /// <param name="viewName"></param>
        public static void RegisterView<TView, TViewModel>(this IContainerRegistry registry, string viewName = null)
        {
            Type viewType = typeof(TView);

            viewName ??= viewType.Name;

            if (viewNameAwares.TryGetValue(viewName, out ViewViewModelAware viewViewModelAware) && viewViewModelAware != null)
            {
                throw new Exception($"view name : {viewName} already exists");
            }

            ViewViewModelAware aware = new ViewViewModelAware
            {
                AutoWareViewModel = false,
                ThrowExceptionWhenViewModelNotExist = false,
                ViewType = viewType,
                ViewName = viewName,
                ViewModelType = typeof(TViewModel)
            };

            viewTypeAwares[aware.ViewType] = aware;
            viewNameAwares[aware.ViewName] = aware;
             
            registry.Register<TView>().AsSingleton();
            registry.Register<TViewModel>().AsSingleton();
        }

        /// <summary>
        /// register dialog window container
        /// </summary>
        /// <typeparam name="TDialogWindow">target window container</typeparam>
        /// <param name="registry"><see cref="IContainerRegistry"/></param>
        public static void RegisterDialogWindow<TDialogWindow>(this IContainerRegistry registry) 
            where TDialogWindow:IDialogWindow
        {
            registry.Register<IDialogWindow,TDialogWindow>();
        }
    }

     
}
