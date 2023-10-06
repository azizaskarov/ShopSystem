using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.CashRegister
{
    /// <summary>
    /// Interaction logic for CashRegisterPage.xaml
    /// </summary>
    public partial class CashRegisterPage : Page
    {
        public CashRegisterPage(ShopsPage shopsPage)
        {
           
            InitializeComponent();
            ReadCashRegisterControls();
            this.shopsPage = shopsPage;
            shopName.Text = "Магазин: " + shopsPage.ShopName;
        }

        ShopsPage shopsPage;

        public void ReadCashRegisterControls()
        {
            var db = new AppDbContext();

            var cashRegisters = db.CashRegisters;

            var cashRegisterControls = new List<CashRegisterControl>();

            foreach (var cashRegister in cashRegisters)
            {
                var cashRegisterControl = new CashRegisterControl
                {
                    CashRegisterName = cashRegister.Name,
                    CashRegisterId = cashRegister.Id
                };
                cashRegisterControls.Add(cashRegisterControl);
            }

            cashRegisterFrame.ItemsSource = cashRegisterControls;
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            var addCashRegister = new AddCashRegister(shopsPage,this);
            addCashRegister.ShowDialog();
        }
    }
}
