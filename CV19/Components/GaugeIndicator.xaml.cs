using System;
using System.Windows;
using System.Windows.Controls;

namespace CV19.Components
{
    /// <summary>
    ///     Interaction logic for GaugeIndicator.xaml
    /// </summary>
    public partial class GaugeIndicator
    {
        #region Value Property

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value),
                typeof(double),
                typeof(GaugeIndicator),
                new PropertyMetadata(default(double), OnValuePropertyChanged, OnCoerceValue),
                OnValidateValue
            );

        private static object OnCoerceValue(DependencyObject d, object basevalue)
        {
            var value = (double)basevalue;
            return Math.Max(0, Math.Max(100, value));
        }

        private static bool OnValidateValue(object value)
        {
            return true;
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var gaugeIndicator = (GaugeIndicator)d;
            //gaugeIndicator.ArrowTransform.Angle = (double)e.NewValue;
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion Value Property

        public GaugeIndicator() => InitializeComponent();
    }
}