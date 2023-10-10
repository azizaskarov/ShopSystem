using System.Windows.Controls;

namespace WpfForM_CRM.Pages.CashRegister;

/// <summary>
/// Interaction logic for CashedProductcontrol.xaml
/// </summary>
public partial class CashedProductControl : UserControl
{
    public CashedProductControl()
    {
        InitializeComponent();
    }

    public object ProductCount
    {
        get
        {
            return count_product.Content;
        }
        set
        {
            count_product.Content = value;
        }
    }

    public object ProductName
    {
        get
        {
            return product_name.Text;
        }
        set
        {
            product_name.Text = value.ToString();
        }
    }

    public object Price
    {
        get
        {
            return price_product.Text;
        }
        set
        {
            price_product.Text = value.ToString();
        }
    }

    public object TotalPrice
    {
        get
        {
            return total_price_product.Text;
        }
        set
        {
            total_price_product.Text = value.ToString();
        }
    }
}