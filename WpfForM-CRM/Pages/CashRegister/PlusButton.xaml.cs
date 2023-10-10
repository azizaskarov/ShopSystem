using System.Windows.Controls;
using System.Windows.Input;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.CashRegister;

/// <summary>
/// Interaction logic for PlusButton.xaml
/// </summary>
public partial class PlusButton : UserControl
{
    public PlusButton(MainWindow mainWindow,ShopsPage shopsPage, KassaPage kassaPage, string header)
    {
        InitializeComponent();
        this.kassaPage = kassaPage;
        this.header = header;
        this.mainWindow = mainWindow;
        this.shopsPage = shopsPage;
    }

    private MainWindow mainWindow;
    private KassaPage kassaPage;
    ShopsPage shopsPage;
    private string header;


    private void PlusButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var productsWindow = new ProductsWindow(mainWindow, shopsPage, kassaPage, header);
        productsWindow.ShowDialog();
    }
}