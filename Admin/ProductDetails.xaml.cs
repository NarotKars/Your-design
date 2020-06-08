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
using DataAccess.Models;
using DataAccess;
using System.Data.SqlClient;

namespace Admin
{
    /// <summary>
    /// Interaction logic for ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : Window
    {
        private long imageId;
        private int categoryId;
        private List<Company> companies = new List<Company>();
        private bool isCanceled;
        public ProductDetails(long _imageId, int _categoryId)
        {
            InitializeComponent();
            imageId = _imageId;
            categoryId = _categoryId;
            Companies comp = new Companies();
            companies=comp.GetCompanies();
            DataContext = companies;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a company!!");
                return;
            }
            decimal BUYINGpRICE, SELLINGpRICE;
            if(!decimal.TryParse(buyingPrice.Text, out BUYINGpRICE))
            {
                MessageBox.Show("Invalid input in Buying Price");
                return;
            }
            if(!decimal.TryParse(sellingPrice.Text, out SELLINGpRICE))
            {
                MessageBox.Show("Invalid input in Selling Price");
                return;
            }

            Company company = companies[comboBox.SelectedIndex];
            DataAccess.Models.Product product = new DataAccess.Models.Product();
            product.SellingPrice = SELLINGpRICE;
            product.BuyingPrice = BUYINGpRICE;
            product.CategoryId = categoryId;
            product.ImageId = imageId;
            product.CompanyId = company.Id;

            Products products = new Products();
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                products.InsertProduct(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            isCanceled = true;
            this.Close();

        }
        public bool canceled
        {
            get { return isCanceled; }
        }
    }
}
