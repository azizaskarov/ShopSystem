using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfForM_CRM.Pages.CashRegister
{
    /// <summary>
    /// Interaction logic for CashRegisterControl.xaml
    /// </summary>
    public partial class CashRegisterControl : UserControl
    {
        public CashRegisterControl()
        {
            InitializeComponent();
        }

        public string CashRegisterName
        {
            get => cashRegisterName.Text;
            set => cashRegisterName.Text = value;
        }
        public Guid CashRegisterId
        {
            get => (Guid)cashRegisterId.Content;
            set => cashRegisterId.Content = value;
        }

        private void CategoryNameDelete_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CategoryNameUpdateImage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CashRegisterControl_OnMouseEnter(object sender, MouseEventArgs e)
        {
            CategoryDeleteImage.Visibility = Visibility.Visible;
            CategoryNameUpdateImage.Visibility = Visibility.Visible;
        }

        private void CashRegisterControl_OnMouseLeave(object sender, MouseEventArgs e)
        {
            CategoryDeleteImage.Visibility = Visibility.Collapsed;
            CategoryNameUpdateImage.Visibility = Visibility.Collapsed;
        }
    }
}
