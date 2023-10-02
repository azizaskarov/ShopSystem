using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.Category;

/// <summary>
/// Interaction logic for CategoryControl.xaml
/// </summary>
public partial class CategoryControl : UserControl
{
    public CategoryControl(ShopsPage shopsPage)
    {
        this.shopsPage = shopsPage;
        InitializeComponent();
        this.appDbContext = new AppDbContext();
    }
    private AppDbContext appDbContext;
    private ShopsPage shopsPage;
    public object Name
    {
        get { return categoryName.Text; }
        set { categoryName.Text = value.ToString(); }
    }
    public object CategoryId
    {
        get { return categoryId.Content; }
        set { categoryId.Content = (Guid)value; }
    }

    public Guid? ShopId { get; set; }


    private void CategoryNameDelete_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var category = appDbContext.Categories
            .FirstOrDefault(category => category.Id == (Guid)CategoryId);
        var deleteResultButton = MessageBox.Show("Вы уверены, что хотите удалить категория?",
            "Удалит",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (deleteResultButton == MessageBoxResult.Yes)
        {

            appDbContext.Categories.Remove(category);
            appDbContext.SaveChanges();
            shopsPage.ReadCategories();
        }

    }

    private void CategoryNameUpdateImage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var categoryUpdate = new CategoryUpdate(shopsPage, (Guid)CategoryId, (string)Name, ShopId);
        categoryUpdate.ShowDialog();
    }

    private void ShopControl_OnMouseEnter(object sender, MouseEventArgs e)
    {
        CategoryDeleteImage.Visibility = Visibility.Visible;
        CategoryNameUpdateImage.Visibility = Visibility.Visible;
    }

    private void ShopControl_OnMouseLeave(object sender, MouseEventArgs e)
    {
        CategoryDeleteImage.Visibility = Visibility.Hidden;
        CategoryNameUpdateImage.Visibility = Visibility.Hidden;
    }

    private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        shopsPage.shopsFrame.Visibility = Visibility.Visible;
        shopsPage.CategoryId = (Guid)CategoryId;
        shopsPage.CategoryName = (string)Name;
        shopsPage.ReadChildCategories();
    }
}