using System.Text.RegularExpressions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfForM_CRM.Pages.Stock;

/// <summary>
/// Interaction logic for AddProductStock.xaml
/// </summary>
public partial class AddProductStock : Window
{

    public AddProductStock()
    {
        InitializeComponent();
    }

    private void ProductName_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        string pattern = @"^[а-яА-Яa-zA-Z0-9]+$";

        // Create a regular expression object with the pattern
        Regex regex = new Regex(pattern);

        // Check if the entered text matches the pattern
        if (!regex.IsMatch(e.Text))
        {
            e.Handled = true; // Ignore the input
        }
    }

    private void ProductPrice_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {

        string pattern = @"^[0-9]+$";

        // Create a regular expression object with the pattern
        Regex regex = new Regex(pattern);

        // Check if the entered text matches the pattern
        if (!regex.IsMatch(e.Text))
        {
            e.Handled = true; // Ignore the input
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void category_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void subcategory_name_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (categoryNameComboBox.SelectedValue == null)
        {
            MessageBox.Show("Category not selected");
            return;
        }
    }
}


