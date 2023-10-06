using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.CashRegister
{
    /// <summary>
    /// Interaction logic for AddCashRegister.xaml
    /// </summary>
    public partial class AddCashRegister : Window
    {
        public AddCashRegister(ShopsPage shopsPage)
        {
            this.shopsPage = shopsPage;
            InitializeComponent();
            cashRegisterName.Focus();
        }

        private ShopsPage shopsPage;
        private void AddTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var pattern = @"^[а-яА-Яa-zA-Z0-9]+$";
            var regex = new Regex(pattern);

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void Addbtn_OnClick(object sender, RoutedEventArgs e)
        {
            var db = new AppDbContext();

            if (cashRegisterName.Text.Length == 0 )
            {
                MessageBox.Show("Значение не указано");
                return; 
            }


            var cashRegister = new Entities.CashRegister
            {
                Name = Helper.Helper.ToUpperNamesOneChar(cashRegisterName.Text),
                ShopId = shopsPage.ShopId,
            };

            db.CashRegisters.Add(cashRegister);
            db.SaveChanges();
            shopsPage.ReadCashRegisters();
            Close();
        }
    }
}
