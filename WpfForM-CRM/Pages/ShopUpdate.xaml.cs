using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages
{
    /// <summary>
    /// Interaction logic for ShopUpdate.xaml
    /// </summary>
    public partial class ShopUpdate : Window
    {
        ShopsPage shopsPage;
        private readonly AppDbContext appDbContext;
        Guid shopId;
        public ShopUpdate(ShopsPage shopsPage, Guid shopId)
        {
            InitializeComponent();
            this.shopId = shopId;
            this.appDbContext = new AppDbContext();
            this.shopsPage = shopsPage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var shop = appDbContext.Shops.FirstOrDefault(sh => sh.Id == shopId);

            if (appDbContext.Shops.Any(sh => sh.Name == shopname.Text))
            {
                MessageBox.Show( "Это имя уже существует");
                return;
            }

            if (shopname.Text.Length == 0)
            {

                MessageBox.Show("Значение не указано", "",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            shop.Name = shopname.Text;
            appDbContext.Shops.Update(shop);
            appDbContext.SaveChanges();
            shopsPage.Load();
            Close();
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
