using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Easy.Toolkit
{
    /// <summary>
    /// binding assist
    /// </summary>
    public static class BindAssist
    {


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static readonly ConcurrentDictionary<object, bool> bindingAssistCached = new();

        /// <summary>
        /// password
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        /// <summary>
        /// password
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// password
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string),
            typeof(BindAssist),
            new FrameworkPropertyMetadata("X{F2D5CFCD-4E95-4158-9A65-EBE51037FE31}", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordChanged));

        //when the buffer changed, upate the passwordBox's password
        private static void OnPasswordChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is PasswordBox passwordBox)
            {
                if (bindingAssistCached.TryGetValue(passwordBox, out bool result) == false)
                {
                    passwordBox.Unloaded += PasswordBox_Unloaded;
                    passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
                    bindingAssistCached[passwordBox] = true;
                }

                string newValue = e.NewValue?.ToString() ?? string.Empty;
                string oldValue = passwordBox.Password;

                if (newValue == oldValue)
                {
                    return;
                }

                passwordBox.Password = newValue;
            }

            void PasswordBox_Unloaded(object sender, RoutedEventArgs e)
            {
                passwordBox.Unloaded -= PasswordBox_Unloaded;
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                bindingAssistCached.TryRemove(passwordBox, out bool _);
            }
            void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
            {
                SetPassword(passwordBox, passwordBox.Password);
            }
        }
    }
}
