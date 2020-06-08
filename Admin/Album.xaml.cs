using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccess;
using DataAccess.Models;

namespace Admin
{
    /// <summary>
    /// Interaction logic for Media.xaml
    /// </summary>
    public partial class Media : Window
    {
        private Image image;
        readonly private long companyId;
        public Media(long id=-1)
        {
            InitializeComponent();
            companyId = id;
            List<Template> templates = new List<Template>();
            Templates template = new Templates();
            if (companyId == -1)
            {
                try
                {
                    templates = template.GetImages("new", "customer");
                    //MessageBox.Show(templates.Count.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    templates = template.GetImagesByCompany(companyId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            int row = 0;
            int col = 0;
            for (int i = 0; i < templates.Count; i++)
            {
                Image img = new Image();
                MemoryStream mstream = new MemoryStream(templates[i].Photo);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = mstream;
                bitmapImage.EndInit();
                img.Source = bitmapImage;
                Border border = new Border { Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 238, 239)) };
                border.CornerRadius = new CornerRadius(15);
                border.Margin = new Thickness(3, 0, 3, 4);

                Grid.SetRow(border, row);
                Grid.SetColumn(border, col);
                images.Children.Add(border);
                Button imageButton = new Button();
                imageButton.Content = img;
                imageButton.Background = Brushes.Transparent;
                imageButton.Margin = new Thickness(10);
                imageButton.BorderThickness = new Thickness(0);
                img.Tag = templates[i].Id.ToString();
                imageButton.Click += new RoutedEventHandler(AddToHomepage_Click);
                Grid.SetRow(imageButton, row);
                Grid.SetColumn(imageButton, col);
                images.Children.Add(imageButton);

                col++;
                if (col == images.ColumnDefinitions.Count)
                {
                    col = 0;
                    row++;
                    if (row == images.RowDefinitions.Count)
                    {
                        var rowDefinition = new RowDefinition();
                        rowDefinition.Height = new GridLength(150);
                        images.RowDefinitions.Add(rowDefinition);
                    }
                }
                
            }
        }

        private void AddToHomepage_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            image = (Image)btn.Content;
            if(companyId!=-1)
            {
                SeeProductDetails seeProductDetails = new SeeProductDetails(long.Parse(image.Tag.ToString()));
                seeProductDetails.ShowDialog();
            }
            else this.Close();
        }

        public Image img
        {
            get { return image; }
        }
    }
}
