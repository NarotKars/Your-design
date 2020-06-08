using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Admin
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        private Category category = new Category();
        public AddCategory()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Categories categories = new Categories();
            if (String.IsNullOrWhiteSpace(categoryName.Text))
            {
                MessageBox.Show("Please enter a category name");
                return;
            }
            category.Name = categoryName.Text;
            int retVal= categories.InsertCategory(categoryName.Text);
            category.Id = retVal;
            if (retVal == 0) MessageBox.Show("This category name already exists");
            else this.Close();
        }
        public Category ret
        {
            get { return category; }
        }
    }
}
