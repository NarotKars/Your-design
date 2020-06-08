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
using DataAccess.Models;
using DataAccess;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.DataVisualization.Charting;
using System.Data.SqlClient;

namespace Admin
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadPieChartData();
            InitializeSearchByDate();
            
            ScrollViewer myScrollViewer = new ScrollViewer();
            myScrollViewer.Content = grid;
            myScrollViewer.Height = 605;
            this.Content = myScrollViewer;
            InitializeDataGrid();

            Statistics statistics = new Statistics();
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                profit.Text = statistics.GetProfit().ToString();
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                   "Message: " + ex.Errors[i].Message + "\n" +
                   "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                   "Source: " + ex.Errors[i].Source + "\n" +
                   "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString());
            }



        }
        private void InitializeSearchByDate()
        {
            Label yearLabel = new Label();
            yearLabel.Content = "year";
            TextBox year = new TextBox();
            year.Name = "year";
            year.Width = 70;
            Label monthLabel = new Label();
            monthLabel.Content = "month";
            TextBox month = new TextBox();
            month.Name = "month";
            month.Width = 70;
            Label dayLabel = new Label();
            dayLabel.Content = "day";
            TextBox day = new TextBox();
            day.Width = 70;
            day.Name = "day";
            Button search = new Button();
            search.Content = "search";
            search.Margin = new Thickness(10, 0, 0, 0);
            search.Click += new RoutedEventHandler(Search_Click);
            StackPanel searchByDate = new StackPanel();
            searchByDate.Children.Add(dayLabel);
            searchByDate.Children.Add(day);
            searchByDate.Children.Add(monthLabel);
            searchByDate.Children.Add(month);
            searchByDate.Children.Add(yearLabel);
            searchByDate.Children.Add(year);
            searchByDate.Children.Add(search);
            searchByDate.Orientation = Orientation.Horizontal;
            searchByDate.Margin = new Thickness(0, 0, 0, 20);
            orderFeedbackPanel.Children.Add(searchByDate);
        }
        private void InitializeDataGrid()
        {
            var rowStyle = new Style { TargetType = typeof(DataGridRow) };
            rowStyle.Setters.Add(new Setter(ForegroundProperty, Brushes.Black));
            rowStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.AliceBlue));

            var cellStyle = new Style { TargetType = typeof(DataGridCell) };
            cellStyle.Setters.Add(new Setter(WidthProperty, 128d));
            List<Order> orders = new List<Order>();
            Orders order = new Orders();
            orders = order.GetOrders();
            DataGrid dataGrid = new DataGrid();
            dataGrid.Width = 650;
            dataGrid.RowStyle = rowStyle;
            dataGrid.CellStyle = cellStyle;
            
            dataGrid.IsReadOnly = true;

            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Address";
            c1.Binding = new Binding("Address");
            //c1.Width = 110;
            dataGrid.Columns.Add(c1);

            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Date";
            c2.Binding = new Binding("Date");
            //c2.Width = 110;
            dataGrid.Columns.Add(c2);

            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "Amount";
            c3.Binding = new Binding("Amount");
            //c3.Width = 110;
            dataGrid.Columns.Add(c3);

            DataGridTextColumn c5 = new DataGridTextColumn();
            c5.Header = "Status";
            c5.Binding = new Binding("Status");
            //c5.Width = 110;
            dataGrid.Columns.Add(c5);

            //dataGrid.ItemsSource = orders;
            for (int i = 0; i < orders.Count; i++)
            {
                dataGrid.Items.Add(new OrderToShow() { Address= orders[i].Address.City + orders[i].Address.Street + orders[i].Address.Number,
                                                       Date =orders[i].Date, Amount=orders[i].Amount, Status=orders[i].Status});
            }
            StackPanel dataGridWrapper = new StackPanel();
            dataGridWrapper.Children.Add(dataGrid);
            dataGridWrapper.HorizontalAlignment = HorizontalAlignment.Left;
            orderFeedbackPanel.Children.Add(dataGridWrapper);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(orderFeedbackPanel.Children.Count==1)
            {
                orderFeedbackPanel.Children.RemoveAt(0);
            }
            if (orderFeedbackPanel.Children.Count != 2)
            {
                InitializeSearchByDate();
            }
            if (orderFeedbackPanel.Children.Count == 2)
            {
                StackPanel st = orderFeedbackPanel.Children[1] as StackPanel;
                orderFeedbackPanel.Children.Remove(st);
            }
            InitializeDataGrid();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel = orderFeedbackPanel.Children[0] as StackPanel;

            TextBox year = panel.Children[5] as TextBox;
            TextBox month = panel.Children[3] as TextBox;
            TextBox day = panel.Children[1] as TextBox;
            int YEAR, MONTH, DAY;
            if(!int.TryParse(year.Text, out YEAR))
            {
                MessageBox.Show("Invalid input of year");
                return;
            }
            if (!int.TryParse(month.Text, out MONTH))
            {
                MessageBox.Show("Invalid input of month");
                return;
            }
            if (!int.TryParse(day.Text, out DAY))
            {
                MessageBox.Show("Invalid input of day");
                return;
            }
            if(YEAR<1753 || YEAR>9999)
            {
                MessageBox.Show("Invalid input of year");
                return;
            }
            if(MONTH<=0 || MONTH>12)
            {
                MessageBox.Show("Invalid month");
                return;
            }
            if(MONTH==1 || MONTH==3 || MONTH==5 || MONTH==7 || MONTH==8 || MONTH==10 || MONTH==12)
            {
                if (DAY <= 0 || DAY > 31)
                {
                    MessageBox.Show("Invalid day");
                    return;
                }
            }
            if(MONTH==2)
            {
                if(((YEAR % 4 == 0) && (YEAR % 100 != 0)) || (YEAR % 400 == 0))
                    {
                        if(DAY<=0 || DAY>29)
                        {
                            MessageBox.Show("Invalid day");
                            return;
                        }
                    }
                else
                {
                    if(DAY<=0 || DAY>28)
                    {
                        MessageBox.Show("Invalid day");
                        return;
                    }
                }
            }
            if(MONTH==4 || MONTH==6 || MONTH== 9 || MONTH==11)
            {
                if(DAY<=0 || DAY>30)
                {
                    MessageBox.Show("Invalid day");
                    return;
                }
            }
            DateTime date = new DateTime(YEAR, MONTH, DAY);
            List<Order> orders = new List<Order>();
            Orders order = new Orders();
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                orders = order.GetOrders(date);
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                   "Message: " + ex.Errors[i].Message + "\n" +
                   "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                   "Source: " + ex.Errors[i].Source + "\n" +
                   "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString());
            }
            panel = orderFeedbackPanel.Children[1] as StackPanel;
            DataGrid dataGrid = panel.Children[0] as DataGrid;
            dataGrid.Items.Clear();
            for (int i = 0; i < orders.Count; i++)
            {
                dataGrid.Items.Add(new OrderToShow()
                {
                    Address = orders[i].Address.City + orders[i].Address.Street + orders[i].Address.Number,
                    Date = orders[i].Date,
                    Amount = orders[i].Amount,
                    PhoneNumber = orders[i].PhoneNumber,
                    Status = orders[i].Status
                });
            }
            //dataGrid.ItemsSource = orders;
            dataGrid.Items.Refresh();
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            if (orderFeedbackPanel.Children.Count > 1)
                index = 1;
            else index = 0;
            StackPanel panel = orderFeedbackPanel.Children[index] as StackPanel;
            DataGrid dataGrid = panel.Children[0] as DataGrid;
            dataGrid.Items.Clear();
            dataGrid.Columns.Clear();

            List<FeedbackModel> feedback = new List<FeedbackModel>();
            Feedback feed = new Feedback();
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                feedback = feed.GetFeedback();
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                   "Message: " + ex.Errors[i].Message + "\n" +
                   "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                   "Source: " + ex.Errors[i].Source + "\n" +
                   "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString());
            }

            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Name";
            c1.Binding = new Binding("Name");
            //c1.Width = 110;
            dataGrid.Columns.Add(c1);

            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Feedback";
            c2.Binding = new Binding("Feedback");
            //c1.Width = 110;
            dataGrid.Columns.Add(c2);

            //dataGrid.ItemsSource = feedback;
            for (int i = 0; i < feedback.Count; i++)
            {
                dataGrid.Items.Add(new FeedbackModel()
                {
                    Name= feedback[i].Name,
                    Feedback=feedback[i].Feedback
                });
            }
            dataGrid.Items.Refresh();
            if(index==1)
                orderFeedbackPanel.Children.RemoveAt(0);
        }
        private void LoadPieChartData()
        {
            Statistics countOfOrders = new Statistics();
            var dictionary = new Dictionary<string, int>();
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                dictionary = countOfOrders.GetCountOfOrdersOfEveryCompany();
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                   "Message: " + ex.Errors[i].Message + "\n" +
                   "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                   "Source: " + ex.Errors[i].Source + "\n" +
                   "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString());
            }
            ((PieSeries)mcChart.Series[0]).ItemsSource = dictionary;
        }
    }
}
