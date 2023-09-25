using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfForM_CRM.Pages.ChildCategory
{
    /// <summary>
    /// Interaction logic for ChildCategoryControl.xaml
    /// </summary>
    public partial class ChildCategoryControl : UserControl
    {
        public ChildCategoryControl()
        {
            InitializeComponent();
        }

        public object ChildCategoryId
        {
            get
            {
                return childCategoryId.Content;
            }
            set
            {
                childCategoryId.Content = value.ToString();
            }
        }

        public object ChildCategoryName
        {
            get
            {
                return childCategoryName.Text;
            }
            set
            {
                childCategoryName.Text = value.ToString();
            }
        }

        public Guid? ShopId { get; set; }
        public Guid? CategoryId { get; set; }
        private void ChildCategoryControl_OnMouseEnter(object sender, MouseEventArgs e)
        {
            childCategoryDeleteImage.Visibility = Visibility.Visible;
            childCategoryNameUpdateImage.Visibility = Visibility.Visible;
        }

        private void ChildCategoryControl_OnMouseLeave(object sender, MouseEventArgs e)
        {
            childCategoryDeleteImage.Visibility = Visibility.Hidden;
            childCategoryNameUpdateImage.Visibility = Visibility.Hidden;
        }

        private void ChildCategoryNameDelete_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChildCategoryNameUpdateImage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
