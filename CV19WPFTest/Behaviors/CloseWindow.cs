using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace CV19WPFTest.Behaviors
{
    internal class CloseWindow : Behavior<Button>
    {
        protected override void OnAttached() => AssociatedObject.Click += OnClick;

        protected override void OnDetaching() => AssociatedObject.Click -= OnClick;

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var button = AssociatedObject;

            var window = FindVisualRoot(button) as Window;
            window?.Close();
        }

        private static DependencyObject FindVisualRoot(DependencyObject obj)
        {
            do
            {
                var parent = VisualTreeHelper.GetParent(obj);
                if (parent is null) return obj;
                obj = parent;
            } while (true);
        }
    }
}