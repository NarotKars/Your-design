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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Admin
{
    /// <summary>
    /// Interaction logic for CompaniesPage.xaml
    /// </summary>
    public partial class CompaniesPage : Page
    {
        public CompaniesPage()
        {
            InitializeComponent();
            List<Company> companies = new List<Company>();
            Companies comp = new Companies();
            try
            {
                dataGrid.ItemsSource = comp.GetCompanies();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void seeProducts_Click(object sender, RoutedEventArgs e)
        {
            Company company = (Company)((Button)e.Source).DataContext;
            Media media = new Media(company.Id);
            media.ShowDialog();
        }

        private void deleteCompany_Click(object sender, RoutedEventArgs e)
        {
            Company company = (Company)((Button)e.Source).DataContext;
            Companies companies = new Companies();
            companies.DeleteCompany(company.Id);
            dataGrid.ItemsSource = companies.GetCompanies();
            dataGrid.Items.Refresh();
        }
    }
}
