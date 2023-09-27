using System;
using System.Windows.Controls;

namespace WpfForM_CRM.Pages.Product;

/// <summary>
/// Interaction logic for ProductControl.xaml
/// </summary>
public partial class ProductControl : UserControl
{
    public ProductControl()
    {
        InitializeComponent();
    }

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

    public decimal ProductPrice
    {
        get => decimal.Parse(productPrice.Text);
        set => productPrice.Text = value + " Сум";
    }
}