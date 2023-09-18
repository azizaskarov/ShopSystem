using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages;

public partial class ShopControl : UserControl 
{
    private AppDbContext appDbContext;

    public object Name
    {
        get { return shopName.Content; }
        set { shopName.Content = value; }
    }

    ShopsPage shopsPage;

    public ShopControl(ShopsPage shopsPage)
    {
        this.appDbContext = new AppDbContext();
        this.shopsPage = shopsPage;
        InitializeComponent();
    }

    private void Button_Delete(object sender, RoutedEventArgs e)
    {
        var shopNameContent = this.shopName.Content;


        var shop = appDbContext.Shops.FirstOrDefault(sh => sh.Name == shopNameContent);

        var deleteResultButton = MessageBox.Show("Вы уверены, что хотите удалить магазин?",
            "Удалит",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (deleteResultButton == MessageBoxResult.Yes)
        {
            if (shop != null)
            {
                appDbContext.Shops.Remove(shop);
                appDbContext.SaveChanges();
                shopsPage.Load();
            }
            else
                return;
        }
    }

    private void Button_Update(object sender, RoutedEventArgs e)
    {
        var shop = appDbContext.Shops.FirstOrDefault(sh => sh.Name == shopName.Content);
        if (shop != null)
        {
            var shopId = shop.Id;
            var update = new ShopUpdate(shopsPage, shopId);
            update.ShowDialog();
        }
    }

    private void DeleteImageIcon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Button_Delete(sender, e);
    }


    private void UpdateImageIcon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Button_Update(sender, e);
    }

 
}