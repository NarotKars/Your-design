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
using System.Data.SqlClient;
using DataAccess.Models;
using DataAccess;


namespace Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Dashboard();
        }
        private void PagesButton_Click(object sender, RoutedEventArgs e)
        {
            pages.Items.Clear();
            List<Category> categories = new List<Category>();
            Categories category = new Categories();
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                categories = category.GetCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MenuItem homePage = new MenuItem();
            homePage.Tag = "Homepage";
            homePage.Header = "Homepage";
            homePage.Click += new RoutedEventHandler(item_Click);
            this.pages.Items.Add(homePage);

            for (int i=0;i<categories.Count;i++)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Tag= categories[i].Id.ToString();
                menuItem.Header = categories[i].Name;
                menuItem.Click += new RoutedEventHandler(item_Click);
                this.pages.Items.Add(menuItem);
            }
            MenuItem addCategory = new MenuItem();
            addCategory.Tag = "addCategory";
            addCategory.Header = "Add Category";
            addCategory.Click += new RoutedEventHandler(item_Click);
            this.pages.Items.Add(addCategory);
            pages.IsOpen = true;
        }
        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Dashboard();
        }

        private void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;
            if (mi.Tag.ToString() == "Homepage")
            {
                Main.Content = new HomePage();
            }
            else if(mi.Tag.ToString()== "addCategory")
            {
                AddCategory addCategory = new AddCategory();
                addCategory.ShowDialog();
                if(addCategory.ret.Id!=0)
                {
                    pages.Items.RemoveAt(pages.Items.Count - 1);
                    MenuItem newCategory = new MenuItem();
                    newCategory.Tag= addCategory.ret.Id.ToString();
                    newCategory.Header = "Add Category";
                    newCategory.Click += new RoutedEventHandler(item_Click);
                    this.pages.Items.Add(newCategory);

                    MenuItem addNewCategory = new MenuItem();
                    addNewCategory.Tag = "addCategory";
                    addNewCategory.Header = "Add Category";
                    addNewCategory.Click += new RoutedEventHandler(item_Click);
                    this.pages.Items.Add(addNewCategory);

                }
            }
            else
            {
                Main.Content = new CategoriesPage(mi.Tag.ToString());
            }
        }

        private void CompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new CompaniesPage();
        }
    }
}
