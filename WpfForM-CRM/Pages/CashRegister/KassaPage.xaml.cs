using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;
using WpfForM_CRM.Pages.Stock;

namespace WpfForM_CRM.Pages.CashRegister;

/// <summary>
/// Interaction logic for KassaPage.xaml
/// </summary>
///
public partial class KassaPage : Page
{
    public KassaPage(MainWindow mainWindow, ShopsPage shopsPage)
    {
        InitializeComponent();
        this.mainWindow = mainWindow;
        this.shopsPage = shopsPage;
        //"Shop: Havas | Kassa: AliKassa | Kassir: Alijon"
        shopTitle.Text = $"Магазин: {shopsPage.ShopName},  Касса: {shopsPage.KassName},  Кассир: {shopsPage.UserName}";
        TabFoodLoad();
    }

    ShopsPage shopsPage;
    MainWindow mainWindow;

    public void TabFoodLoad()
    {
        tabFood.Items.Clear();
        var plusBtn = new PlusButton(mainWindow, shopsPage, this, tab1.Header.ToString()!);
        tabFood.Items.Add(plusBtn);
        var db = new AppDbContext();
        var products = db.Products.Where(p => p.TabName == tab1.Header.ToString()).ToList();

        foreach (var product in products)
        {
            var tab = new TabProductControl(this);
            tab.ProductSellingPrice = product.SellingPrice!;
            tab.ProductId = product.Id;
            tab.ProductName = product.Name;
            tabFood.Items.Add(tab);
        }
    }

    public void LoadCashedProducts()
    {
        cashed_products.Items.Clear();
        var db = new AppDbContext();
        var tabs = new List<CashedProductControl>();
        var products = db.CashedProducts.OrderBy(p => p.CashedTime).ToList();
        foreach (var product in products)
        {
            var cashed = new CashedProductControl(); 
            cashed.ProductCount = product.TotalCount!;
            cashed.Price = product.SellingPrice!;
            cashed.ProductName = product.Name!;
            cashed.TotalPrice = product.TotalCount! * product.SellingPrice!;
            cashed_products.Items.Add(cashed);
        }
    }

    private void Tab1_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        TabFoodLoad();
    }
}

