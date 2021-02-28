﻿using System.Linq;
using System.Windows.Data;
using CV19.Models.Decanat;

namespace CV19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void GroupCollectionFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Group group)) return;
            if (group.Name == null) return;

            var filter_text = GroupNameFilterText.Text;

            if (filter_text.Length == 0) return;

            if (group.Name.Contains(filter_text)) return;
            if (group.Description.Contains(filter_text)) return;

            e.Accepted = false;
        }
    }
}