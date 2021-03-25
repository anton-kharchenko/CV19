using System;
using System.Threading;
using System.Windows;

namespace CV19WPFTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            new Thread(ComputeValue).Start();
        }

        private void ComputeValue()
        {
            var resultBlockText = LongProcess(DateTime.Now);
            if (ResultBlock.Dispatcher.CheckAccess())
                ResultBlock.Text = resultBlockText;
            else
                ResultBlock.Dispatcher.BeginInvoke(new Action(() => ResultBlock.Text = resultBlockText));
            // ResultBlock.Dispatcher.Invoke(new Action(() => ResultBlock.Text = resultBlockText));
        }

        private static string LongProcess(DateTime time)
        {
            Thread.Sleep(7000);

            return $"Value: {time}";
        }
    }
}