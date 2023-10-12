using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.CashRegister
{
    /// <summary>
    /// Interaction logic for CashRegisterControl.xaml
    /// </summary>
    public partial class CashRegisterControl : UserControl
    {
        public CashRegisterControl( MainWindow mainWindow, ShopsPage shopsPage)
        {
            this.mainWindow = mainWindow;
            this.shopsPage = shopsPage;
            InitializeComponent();
        }

        private MainWindow mainWindow;
        ShopsPage shopsPage;
        
        public object CashRegisterName
        {
            get => cashRegisterName.Text;
            set => cashRegisterName.Text = (string)value;
        }
        public object CashRegisterId
        {
            get => cashRegisterId.Content;
            set => cashRegisterId.Content = value;
        }

        private void CashRegisterControl_OnMouseEnter(object sender, MouseEventArgs e)
        {
            DeleteImageIcon.Visibility = Visibility.Visible;
            UpdateImageIcon.Visibility = Visibility.Visible;
        }

        private void CashRegisterControl_OnMouseLeave(object sender, MouseEventArgs e)
        {
            DeleteImageIcon.Visibility = Visibility.Hidden;
            UpdateImageIcon.Visibility = Visibility.Hidden;
        }


        private void DeleteImageIcon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var appDbContext = new AppDbContext();
            var cashRegister = appDbContext.CashRegisters.FirstOrDefault(c => c.Id == (Guid)CashRegisterId);

            var deleteResultButton = MessageBox.Show("Вы уверены, что хотите удалить kassa?",
                "Удалит",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (deleteResultButton == MessageBoxResult.Yes)
            {
                if (cashRegister != null)
                {
                    appDbContext.CashRegisters.Remove(cashRegister);
                    appDbContext.SaveChanges();
                    shopsPage.ReadCashRegisters();
                }
            }
        }

        private void UpdateImageIcon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var cashRegisterUpdate = new UpdateCashRegister(shopsPage,(string)CashRegisterName, (Guid)CashRegisterId);
            cashRegisterUpdate.ShowDialog();
        }

        private void CashRegisterControl_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            shopsPage.KassName = (string)CashRegisterName;
            shopsPage.KassaId = (Guid)CashRegisterId;
            shopsPage.Window.mainframe.Navigate(new KassaPage(mainWindow:mainWindow, shopsPage));
            shopsPage.Window.DockPanel.Visibility = Visibility.Collapsed;
            shopsPage.Window.exit_btn.Visibility = Visibility.Collapsed;
            shopsPage.Window.minimizeButton_Copy.Visibility = Visibility.Collapsed;
            shopsPage.Window.restoreBtn.Visibility = Visibility.Collapsed;
            shopsPage.Window.mainframe.Margin = new Thickness(0,0,0,0);
        }
    }
}
