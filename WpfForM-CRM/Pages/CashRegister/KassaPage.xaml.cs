using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

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
        TabTechLoad();
        TabClothesLoad();
    }

    public ShopsPage shopsPage;
    MainWindow mainWindow;
    public void TabFoodLoad()
    {
        tabFood.Items.Clear();
        var plusBtn = new PlusButton(mainWindow, shopsPage, this, tab1.Header.ToString()!);
        tabFood.Items.Add(plusBtn);
        var db = new AppDbContext();
        var products = db.Products.Where(p => p.ShopId == shopsPage.ShopId && p.TabName == tab1.Header.ToString()).ToList();
        //var cashRegister = db.CashRegisters.First(c => c.Id == shopsPage.KassaId);

        foreach (var product in products)
        {
            var tab = new TabProductControl(this);
            tab.ProductSellingPrice = product.SellingPrice!;
            tab.ProductId = product.Id;
            tab.ProductName = product.Name;
            tabFood.Items.Add(tab);
        }
    }

    public void TabClothesLoad()
    {
        tabClothes.Items.Clear();
        var plusBtn = new PlusButton(mainWindow, shopsPage, this, tab2.Header.ToString()!);
        tabClothes.Items.Add(plusBtn);
        var db = new AppDbContext();
        var products = db.Products.Where(p => p.ShopId == shopsPage.ShopId && p.TabName == tab2.Header.ToString()).OrderByDescending(p => p.CreatedDate).ToList();


        foreach (var product in products)
        {
            var tab = new TabProductControl(this);
            tab.ProductSellingPrice = product.SellingPrice!;
            tab.ProductId = product.Id;
            tab.ProductName = product.Name;
            tabClothes.Items.Add(tab);
        }
    }
    public void TabTechLoad()
    {
        tabTexnika.Items.Clear();
        var plusBtn = new PlusButton(mainWindow, shopsPage, this, tab3.Header.ToString()!);
        tabTexnika.Items.Add(plusBtn);
        var db = new AppDbContext();
        var products = db.Products.Where(p => p.ShopId == shopsPage.ShopId && p.TabName == tab3.Header.ToString()).ToList();

        foreach (var product in products)
        {
            var tab = new TabProductControl(this);
            tab.ProductSellingPrice = product.SellingPrice!;
            tab.ProductId = product.Id;
            tab.ProductName = product.Name;
            tabTexnika.Items.Add(tab);
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


    private void KassaPage_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var db = new AppDbContext();
        if (e.XButton1 == MouseButtonState.Pressed)
        {
            shopsPage.Window.mainframe.Margin = new Thickness(0, 25, 0, 0);
            shopsPage.Window.DockPanel.Visibility = Visibility.Visible;
            shopsPage.Window.exit_btn.Visibility = Visibility.Visible;
            shopsPage.Window.minimizeButton_Copy.Visibility = Visibility.Visible;
            shopsPage.Window.restoreBtn.Visibility = Visibility.Visible;

            foreach (var item in db.CashedProducts)
            {
                db.CashedProducts.Remove(item);
            }
            db.SaveChanges();
        }
    }

    private void Tab2_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        TabClothesLoad();
    }

    private void Tab3_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        TabTechLoad();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var checkWindow = new CheckWindow(mainWindow, this);
        checkWindow.ShowDialog();
    }

    private void BackBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var db = new AppDbContext();
        var cashes = db.CashedProducts.ToList();
        db.CashedProducts.RemoveRange(cashes);
        db.SaveChanges();
        shopsPage.Window.mainframe.Margin = new Thickness(0, 25, 0, 0);
        shopsPage.Window.DockPanel.Visibility = Visibility.Visible;
        shopsPage.Window.exit_btn.Visibility = Visibility.Visible;
        shopsPage.Window.minimizeButton_Copy.Visibility = Visibility.Visible;
        shopsPage.Window.restoreBtn.Visibility = Visibility.Visible;
        NavigationService!.GoBack();
    }
}

