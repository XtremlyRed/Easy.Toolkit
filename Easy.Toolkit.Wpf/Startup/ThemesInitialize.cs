using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
[assembly: XmlnsDefinition("http://easytoolkit.org/", "Easy.Toolkit")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

namespace Easy.Toolkit
{


    /// <summary>
    /// pack://Application:,,,/Easy.Toolkit.Wpf;component/Themes/Defaults.xaml
    /// </summary>
    [DisplayName("pack://Application:,,,/Easy.Toolkit.Wpf;component/Themes/Defaults.xaml")]
    public static class ThemesInitialize

    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static bool _initialized = false;

        public static void Initialize(Application application = null)
        {
            application ??= Application.Current;

            if (application is null)
            {
                return;
            }

            lock (application)
            {
                if (_initialized)
                {
                    return;
                }
                _initialized = true;
            }

            string path = $"pack://Application:,,,/Easy.Toolkit.Wpf;component/Themes/Defaults.xaml";

            ResourceDictionary resource = new()
            {
                Source = new Uri(path)
            };

            application.Resources?.MergedDictionaries?.Add(resource);
        }
    }
}
