using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections;
using System.Globalization;
using System.Windows.Media;

namespace TVCust
{
    public class TreeViewControl2 : Control
    {
        static TreeViewControl2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeViewControl2), new FrameworkPropertyMetadata(typeof(TreeViewControl2)));
        }



        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(TreeViewControl2), new PropertyMetadata(null));



        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(TreeViewControl2), new PropertyMetadata(null));


        public TreeViewControl2()
        {

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var ctrl = GetTemplateChild("treebase") as TreeView;
            if (ctrl != null)
            {
                _treeView = ctrl;
            }
        }

        private TreeView _treeView;
    }

    public class LeftMarginMultiplierController : IValueConverter
    {
        public double Length { get; set; } = 19;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return null;

            var leftOffset = Length * item.GetDepth();
            var ret = new Thickness(leftOffset, 0, 0, 0);

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public static class TreeViewItemExtensions
    {
        public static int GetDepth(this TreeViewItem item)
        {
            TreeViewItem parent;
            while ((parent = GetParent(item)) != null)
            {
                return GetDepth(parent) + 1;
            }
            return 0;
        }
        private static TreeViewItem GetParent(TreeViewItem item)
        {
            var parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as TreeViewItem;
        }
    }
}
