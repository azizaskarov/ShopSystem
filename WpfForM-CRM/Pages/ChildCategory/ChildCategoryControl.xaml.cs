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
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.ChildCategory
{
    /// <summary>
    /// Interaction logic for ChildCategoryControl.xaml
    /// </summary>
    public partial class ChildCategoryControl : UserControl
    {
        public ChildCategoryControl(ShopsPage shopsPage)
        {
            this.shopsPage = shopsPage;
            this.appDbContext = new AppDbContext();
            InitializeComponent();
        }

        private ShopsPage shopsPage;
        private AppDbContext appDbContext;
        public object ChildCategoryId
        {
            get
            {
                return childCategoryId.Content;
            }
            set
            {
                childCategoryId.Content = value;
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


            var childCategory = appDbContext.ChildCategories
                .FirstOrDefault(ch => ch.Id == (Guid?)ChildCategoryId);
            var deleteResultButton = MessageBox.Show("Вы уверены, что хотите удалить под категория?",
                "Удалит",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (deleteResultButton == MessageBoxResult.Yes)
            {

                appDbContext.ChildCategories.Remove(childCategory);
                appDbContext.SaveChanges();
                shopsPage.ReadChildCategories();
            }

        }

        private void ChildCategoryNameUpdateImage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var updateChildCategory =
                new UpdateChildCategory(shopsPage, (Guid)ChildCategoryId, (string)ChildCategoryName, CategoryId);
            updateChildCategory.ShowDialog();
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            shopsPage.shopsFrame.Visibility = Visibility.Visible;
            shopsPage.ChildCategoryId = (Guid?)ChildCategoryId;
            shopsPage.ChildCategoryName = (string)ChildCategoryName;
            shopsPage.ReadProducts();

        }
    }
}
