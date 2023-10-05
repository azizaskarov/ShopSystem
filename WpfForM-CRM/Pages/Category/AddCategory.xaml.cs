using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.Category
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        public AddCategory(ShopsPage shopsPage)
        {
            InitializeComponent();
            this.db = new AppDbContext();
            this.shopsPage = shopsPage;
            CategoryName.Focus();
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

            var categories = db.Categories.ToList();

            if (categories.Any(category => category.Name == CategoryName.Text))
            {
                MessageBox.Show("Название магазина уже существует");
                return;
            }

            

            var category = new Entities.Category()
            {
                Name = Helper.Helper.ToUpperNamesOneChar(CategoryName.Text),
                ShopId = (Guid?)shopsPage.ShopId,
                UserId = shopsPage.UserId
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


        private void AddCategory_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonBase_OnClick(sender,e);
            }

            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
