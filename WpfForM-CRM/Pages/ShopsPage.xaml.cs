using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages
{
    /// <summary>
    /// Interaction logic for ShopsPage.xaml
    /// </summary>
    public partial class ShopsPage : Page
    {
        public ShopsPage()
        {
            InitializeComponent();
        }

        public void Load()
        {
            var db = new AppDbContext();
            var shops = db.Shops.ToList();
            var list = new List<ShopControl>();

            foreach (var shop in shops)
            {
                var model = new ShopControl(this);
                model.Width = 200;
                model.Height = 40;
                model.Name = shop.Name;
                list.Add(model);
            }
            
            shopsFrame.ItemsSource = list;
        }

        private void Button_ReadShops(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Button_AddShop(object sender, RoutedEventArgs e)
        {
            Add add = new Add(this);
            add.ShowDialog();
        }

        private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTxt = SearchText.Text;

            var dbContext = new AppDbContext();

            var matchingShops = dbContext.Shops
                .Where(shop => shop.Name.Contains(searchTxt))
                .ToList();

            var list = new List<ShopControl>();

            foreach (var shop in matchingShops)
            {
                var model = new ShopControl(this);
                model.Width = 200;
                model.Height = 40;
                model.Name = shop.Name;
                list.Add(model);
            }

            shopsFrame.ItemsSource = list;
        }

    }


}