using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace CV19WPFTest.Behaviors
{
    internal class DragInCanvas : Behavior<UIElement>
    {
        private Point _startPoint;
        private Canvas _canvas;

        #region PositionX : double - Вертикальное положение

        /// <summary>Вертикальное положение</summary>
        public static readonly DependencyProperty PositionXProperty =
            DependencyProperty.Register(
                nameof(PositionX),
                typeof(double),
                typeof(DragInCanvas),
                new PropertyMetadata(default(double)));

        /// <summary>Вертикальное положение</summary>
        //[Category("")]
        [Description("Вертикальное положение")]
        public double PositionX
        {
            get => (double)GetValue(PositionXProperty);
            set => SetValue(PositionXProperty, value);
        }

        #endregion PositionX : double - Вертикальное положение

        #region PositionY : double - Горизонтальное положение

        /// <summary>Вертикальное положение</summary>
        public static readonly DependencyProperty PositionYProperty =
            DependencyProperty.Register(
                nameof(PositionY),
                typeof(double),
                typeof(DragInCanvas),
                new PropertyMetadata(default(double)));

        /// <summary>Вертикальное положение</summary>
        //[Category("")]
        [Description("Вертикальное положение")]
        public double PositionY
        {
            get => (double)GetValue(PositionYProperty);
            set => SetValue(PositionYProperty, value);
        }

        #endregion PositionY : double - Горизонтальное положение

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
        }

        private void OnButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((_canvas ??= VisualTreeHelper.GetParent(AssociatedObject) as Canvas) is null)
                return;

            _startPoint = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseUp += OnMouseUp;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var obj = AssociatedObject;
            var currentPos = e.GetPosition(obj);

            var delta = currentPos - _startPoint;

            obj.SetValue(Canvas.LeftProperty, delta.X);
            obj.SetValue(Canvas.TopProperty, delta.Y);

            PositionX = delta.X;
            PositionY = delta.Y;
        }
    }
}