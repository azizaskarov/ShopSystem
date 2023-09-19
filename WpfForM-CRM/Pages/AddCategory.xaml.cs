using System;
using System.Linq;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WpfForM_CRM.Context;
using WpfForM_CRM.Entities;

namespace WpfForM_CRM.Pages
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        ShopsPage shopsPage;
        private MainWindow mainWindow;
        private Guid shopId;
        public AddCategory(MainWindow mainWindow, ShopsPage shopsPage, Guid id)
        {
            this.shopsPage = shopsPage;
            shopId = id;
            InitializeComponent();
        }


         

        private void AddCategoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dbContext = new AppDbContext();
            var categories = dbContext.Categories.Where(c => c.ShopId == shopId).ToList();

            if (categories.Any(category => category.Title == CategoryName.Text))
            {
                MessageBox.Show("already exist");
                return;
            }
            var category = new Category()
            {
                Title = CategoryName.Text,
                CreatedDate = DateTime.UtcNow,
                ShopId = shopId
            };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            shopsPage.ReadCategories(shopId);
            Close();
        }
    }
}
