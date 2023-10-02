using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.Product;

/// <summary>
/// Interaction logic for ProductControl.xaml
/// </summary>
public partial class ProductControl : UserControl
{
    public ProductControl(ShopsPage shopsPage)
    {
        this.shopsPage = shopsPage;
        InitializeComponent();
    }

    ShopsPage shopsPage;
    public Guid ProductId
    {
        get => (Guid)productId.Content;
        set => productId.Content = value;
    }

    public string ProductName
    {
        get => (string)productName.Text;
        set => productName.Text = value;
    }

    public long SellingProductPrice
    {
        get => long.Parse(productPrice.Text.Replace("UZS", "").Trim());
        set => productPrice.Text = value + " UZS";
    }

    public string OriginalProductPrice { get; set; }
    public string ProductCount { get; set; }


    private void DeleteProduct_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var db = new AppDbContext();
        var product = db.Products.First(p => p.Id.Equals(ProductId));
        var questionResult = MessageBox.Show("delete?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        if (questionResult == MessageBoxResult.OK)
        {
            db.Products.Remove(product);
            db.SaveChanges();
            shopsPage.ReadProducts();
        }
    }

    private void UpdateProduct_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {

        var product = new UpdateProduct(shopsPage, ProductId, ProductName, OriginalProductPrice,
                    SellingProductPrice.ToString(), ProductCount);
        product.ShowDialog();
    }
}