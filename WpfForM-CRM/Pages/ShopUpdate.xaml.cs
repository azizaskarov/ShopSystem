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
using System.Windows.Shapes;
using WpfForM_CRM.Context;
using WpfForM_CRM.Entities;

namespace WpfForM_CRM.Pages
{
    /// <summary>
    /// Interaction logic for ShopUpdate.xaml
    /// </summary>
    public partial class ShopUpdate : Window
    {
        ShopsPage shopsPage;
        private AppDbContext appDbContext = new AppDbContext();
        Guid shopId;
        public ShopUpdate(ShopsPage shopsPage, Guid shopId)
        {
            InitializeComponent();
            this.shopId = shopId;
            this.shopsPage = shopsPage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var shop = appDbContext.Shops.FirstOrDefault(sh => sh.Id == shopId);

            shop.Name = shopname.Text;

            appDbContext.Shops.Update(shop);
            appDbContext.SaveChanges();
            shopsPage.Load();
            Close();
        }
    }
}
