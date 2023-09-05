using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages;

public partial class ShopControl : UserControl
{
    private AppDbContext appDbContext = new AppDbContext();
    public object Name
    {
        get
        {
            return shopName.Content;
        }
        set
        {
            shopName.Content = value;
        }
    }

    ShopsPage shopsPage;
    public ShopControl(ShopsPage shopsPage)
    {
        this.shopsPage = shopsPage;
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var shopNameContent = this.shopName.Content;


        var shop = appDbContext.Shops.FirstOrDefault(sh => sh.Name == shopNameContent);
        if (shop != null)
        {
            appDbContext.Shops.Remove(shop);
            appDbContext.SaveChanges();
            MessageBox.Show("Success");
            shopsPage.Load();
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        var shop = appDbContext.Shops.FirstOrDefault(sh => sh.Name == shopName.Content);
        if (shop != null)
        {
            var shopId = shop.Id; 
            var update = new ShopUpdate(shopsPage, shopId);
            update.ShowDialog();
        }

    }
}