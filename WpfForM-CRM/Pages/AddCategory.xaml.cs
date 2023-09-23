using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfForM_CRM.Context;
using WpfForM_CRM.Entities;

namespace WpfForM_CRM.Pages
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        public AddCategory( ShopsPage shopsPage)
        {
            InitializeComponent();
            this.db = new AppDbContext();
            this.shopsPage = shopsPage;
        }

        private ShopsPage shopsPage;
        AppDbContext db = new AppDbContext();

        

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            if (CategoryName.Text.Length == 0)
            {

                MessageBox.Show("Значение не указано", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }

            var categories = db.Categories.Where(category => category.ShopId == shopsPage.ShopId).ToList();

            if (categories.Any(category => category.Name == CategoryName.Text))
            {
                MessageBox.Show("Название магазина уже существует");
                return;
            }

            var category = new Category()
            {
                Name = CategoryName.Text,
                ShopId = (Guid)shopsPage.ShopId,
            };

            db.Categories.Add(category);
            var shop = db.Shops.FirstOrDefault(shop => shop.Id == shopsPage.ShopId);
            shop.Categories.Add(category);
            db.Shops.Update(shop);

            db.SaveChanges();
            shopsPage.ReadCategories();
            Close();
        }

        private void CategoryName_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var pattern = @"^[а-яА-Яa-zA-Z0-9]+$";
            var regex = new Regex(pattern);

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        
    }
}
