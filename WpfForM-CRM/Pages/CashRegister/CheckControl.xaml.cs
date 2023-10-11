using System.Windows.Controls;

namespace WpfForM_CRM.Pages.CashRegister;

/// <summary>
/// Interaction logic for CheckControl.xaml
/// </summary>
public partial class CheckControl : UserControl
{
    public CheckControl()
    {
        InitializeComponent();
    }

    public object ProductName
    {
        get => productName.Text;
        set => productName.Text = value.ToString();
    }
    public object ProductNumber
    {
        get => nomer.Text;
        set => nomer.Text = value.ToString();
    }
    public object ProductCount
    {
        get => productCount.Text;
        set => productCount.Text = value + " шт";
    }
    public object ProductPrice
    {
        get => productPrice.Text;
        set => productPrice.Text = value.ToString();
    } 
    public object ProductTotalPrice
    {
        get => totalPrice.Text;
        set => totalPrice.Text = value.ToString();
    } 
    public object BarCode
    {
        get => barCode.Text;
        set => barCode.Text = value.ToString();
    }
}