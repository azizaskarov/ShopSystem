using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages
{
    /// <summary>
    /// Interaction logic for ShopUpdate.xaml
    /// </summary>
    public partial class ShopUpdate : Window
    {
        ShopsPage shopsPage;
        Guid shopId;
        public ShopUpdate(ShopsPage shopsPage, Guid shopId, string currentShopName)
        {
            InitializeComponent();
            this.shopId = shopId;
            shopname.Text = currentShopName;
            this.shopsPage = shopsPage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var appDbContext = new AppDbContext();
            var shop = appDbContext.Shops.First(sh => sh.Id == shopId);
            if (shopname.Text.Length == 0)
            {

                MessageBox.Show("Значение не указано", "",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (appDbContext.Shops.Any(sh => sh.Name == shopname.Text))
            {
                MessageBox.Show( "Это имя уже существует");
                return;
            }

            shop.Name = shopname.Text;
            appDbContext.Shops.Update(shop);
            appDbContext.SaveChanges();
            shopsPage.Load();
            Close();
            this.Close();
        }

        private void addtxt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string pattern = @"^[а-яА-Яa-zA-Z0-9]+$";

            // Create a regular expression object with the pattern
            Regex regex = new Regex(pattern);

            // Check if the entered text matches the pattern
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Ignore the input
            }
        }


        private void ShopUpdate_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender,e);
            }
        }
    }
}
