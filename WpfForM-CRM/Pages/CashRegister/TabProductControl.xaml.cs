using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Entities;

namespace WpfForM_CRM.Pages.CashRegister;

/// <summary>
/// Interaction logic for TabProductControl.xaml
/// </summary>
public partial class TabProductControl : UserControl
{
    public TabProductControl(KassaPage kassaPage)
    {
        this.kassaPage = kassaPage;
        InitializeComponent();
    }

    public object ProductId
    {
        get => productId.Content;
        set => productId.Content = value;
    }

    public object ProductSellingPrice
    {
        get => productSellingPrice.Text;
        set => productSellingPrice.Text = value.ToString();
    }
    public object ProductName
    {
        get => productName.Text;
        set => productName.Text = value.ToString();
    }

    KassaPage kassaPage;
    private void AddProductIcon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var db = new AppDbContext();
        var product = db.Products.First(p => p.Id == (Guid)ProductId);
        var cashedProduct = new CashedProduct()
        {
            Id = product.Id,
            Count = product.Count,
            CategoryId = product.CategoryId,
            Barcode = product.Barcode,
            CreatedTime = product.CreatedDate,
            OriginalPrice = product.OriginalPrice,
            SellingPrice = product.SellingPrice,
            ShopId = product.ShopId,
            Name = product.Name,
            CashedTime = DateTime.UtcNow,
            TotalCount = 1
        };

        if (!db.CashedProducts.Any(p => p.Id == (Guid)ProductId))
        {
            db.CashedProducts.Add(cashedProduct);
            db.SaveChanges();
            //kassaPage.LoadCashedProducts();
        }
        else
        {
            var cashedproductThan = db.CashedProducts.First(p => p.Id == (Guid)ProductId);
            cashedproductThan.TotalCount += 1;
            db.CashedProducts.Update(cashedproductThan);
            db.SaveChanges();
            //kassaPage.LoadCashedProducts();
        }
    }
}