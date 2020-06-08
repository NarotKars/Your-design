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
    /// Interaction logic for SeeProductDetails.xaml
    /// </summary>
    public partial class SeeProductDetails : Window
    {
        public SeeProductDetails(long imageId)
        {
            InitializeComponent();
            List<DataAccess.Models.Product> products = new List<DataAccess.Models.Product>();
            Products prod = new Products();
            try
            {
                products = prod.GetProductDetails(imageId);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            companyName.Text = products[0].CompanyName;
            buyingPrice.Text = products[0].BuyingPrice.ToString();
            sellingPrice.Text = products[0].SellingPrice.ToString();

        }
    }
}
