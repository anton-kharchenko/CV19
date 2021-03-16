﻿using CV19.Models.Decanat;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

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
            if (group.Description != null && group.Description.Contains(filter_text)) return;

            e.Accepted = false;
        }

        private void OnGroupFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var text_box = (TextBox)sender;
            var collection = (CollectionViewSource)text_box.FindResource("GroupsCollection");
            collection.View.Refresh();
        }
    }
}