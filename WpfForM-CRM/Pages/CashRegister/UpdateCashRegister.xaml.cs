using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.CashRegister
{
    /// <summary>
    /// Interaction logic for UpdateCashRegister.xaml
    /// </summary>
    public partial class UpdateCashRegister : Window
    {
        public UpdateCashRegister(ShopsPage shopsPage, string cashRegisterNameOld, Guid cashRegisterId)
        {
            
            InitializeComponent();
            this.shopsPage = shopsPage;
            this.cashRegisterId = cashRegisterId;
            cashRegisterName.Text = cashRegisterNameOld;
        }

        private Guid cashRegisterId;
        ShopsPage shopsPage;

        private void AddTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var pattern = @"^[а-яА-Яa-zA-Z0-9]+$";
            var regex = new Regex(pattern);

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }


        private void UpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var appDbContext = new AppDbContext();
            var cashRegister = appDbContext.CashRegisters.First(c => c.Id == cashRegisterId);
            if (cashRegisterName.Text.Length == 0)
            {

                MessageBox.Show("Значение не указано", "",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            //if (appDbContext.Shops.Any(sh => sh.Name == shopname.Text))
            //{
            //    MessageBox.Show("Это имя уже существует");
            //    return;
            //}

            cashRegister.Name = cashRegisterName.Text;
            appDbContext.CashRegisters.Update(cashRegister);
            appDbContext.SaveChanges();
            shopsPage.ReadCashRegisters();
            Close();
        }
    }
}
