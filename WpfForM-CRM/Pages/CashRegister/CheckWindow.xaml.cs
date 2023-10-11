using System;
using System.Linq;
using System.Windows;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages.CashRegister;

/// <summary>
/// Interaction logic for CheckWindow.xaml
/// </summary>
public partial class CheckWindow : Window
{
    public CheckWindow(MainWindow window, KassaPage kassaPage)
    {
        this.window = window;
        this.kassaPage = kassaPage;
        InitializeComponent();
        kassirName.Content = kassaPage.shopsPage.UserName;
        datatime.Content = DateTime.Now;
        Load();
    }

    private MainWindow window;
    private KassaPage kassaPage;
    private double totalPrice;
    public void Load()
    {
        int i = 1;
        checkedProducts.Items.Clear();
        var db = new AppDbContext();
        var products = db.CashedProducts.OrderBy(p => p.CashedTime).ToList();
        foreach (var product in products)
        {
            var check = new CheckControl();
            check.Height = 120;
            check.Width = 280;
            check.ProductPrice = product.SellingPrice!;
            check.ProductCount = product.TotalCount!;
            check.ProductTotalPrice = product.TotalCount! * (double)product.SellingPrice!;
            check.BarCode = product.Barcode!;
            check.ProductName = product.Name;
            check.ProductNumber = i;
            totalPrice += ((double)product.TotalCount! * (double)product.SellingPrice)!;
            checkedProducts.Items.Add(check);
            ;
            i++;
        }

        total.Text = totalPrice!.ToString()!;
    }
}