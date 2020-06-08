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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccess;
using DataAccess.Models;
namespace Admin
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            List<Template> templates = new List<Template>();
            Templates template = new Templates();
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                templates = template.GetImages("HPNew", "HPCustomer");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            int rowNew = 0;
            int colNew = 1;
            int rowPermanent = 0;
            int colPermanent = 1;
            for (int i = 0; i < templates.Count; i++)
            {
                Image img = new Image();
                MemoryStream mstream = new MemoryStream(templates[i].Photo);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = mstream;
                bitmapImage.EndInit();
                img.Source = bitmapImage;
                StackPanel stackPanel = new StackPanel();
                StackPanel stackPanelButtons = new StackPanel();
                StackPanel stackPanelImage = new StackPanel();
                Border border = new Border { Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 238, 239)) };
                Button delete = new Button();
                delete.Click += new RoutedEventHandler(Delete_Click);
                Button update = new Button();
                update.Click += new RoutedEventHandler(Update_Click);
                if (templates[i].Status == "HPNew")
                {
                    stackPanel.SetValue(Grid.RowProperty, rowNew);
                    stackPanel.SetValue(Grid.ColumnProperty, colNew);

                    img.Name = "a" + HPNewGrid.Children.Count.ToString();

                    Functions.ImageFrame(img, stackPanel, stackPanelButtons, stackPanelImage, border, templates[i].Id, templates[i].Status, HPNewGrid.Children.Count, delete, update);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);

                    Grid.SetRow(border, rowNew);
                    Grid.SetColumn(border, colNew);
                    HPNewGrid.Children.Add(border);
                    colNew++;
                    if (colNew == HPNewGrid.ColumnDefinitions.Count)
                    {
                        colNew = 0;
                        rowNew++;
                        if (rowNew == HPNewGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            HPNewGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    HPNewGrid.Children.Add(stackPanel);
                }
                else if (templates[i].Status == "HPCustomer")
                {
                    stackPanel.SetValue(Grid.RowProperty, rowPermanent);
                    stackPanel.SetValue(Grid.ColumnProperty, colPermanent);

                    Functions.ImageFrame(img, stackPanel, stackPanelButtons, stackPanelImage, border, templates[i].Id, templates[i].Status, HPPermanentGrid.Children.Count,delete, update);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);



                    Grid.SetRow(border, rowPermanent);
                    Grid.SetColumn(border, colPermanent);
                    HPPermanentGrid.Children.Add(border);
                    colPermanent++;
                    if (colPermanent == HPPermanentGrid.ColumnDefinitions.Count)
                    {
                        colPermanent = 0;
                        rowPermanent++;
                        if (rowPermanent == HPPermanentGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            HPPermanentGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    HPPermanentGrid.Children.Add(stackPanel);
                }

            }
        }
        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            Media media = new Media();
            media.ShowDialog();
            Image img = new Image();
            if (media.img != null)
            {
                img.Source = media.img.Source;
                img.Tag = media.img.Tag;
                string imgId = img.Tag.ToString();
                Template template = new Template();
                template.Id = int.Parse(imgId);

                Button addButton = e.Source as Button;
                if (addButton.Name == "addToHompageNew")
                    template.Status = "HPNew";
                else template.Status = "HPCustomer";

                Templates templates = new Templates();
                StringBuilder errorMessages = new StringBuilder();
                try
                {
                    templates.UpdateStatus(template);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                long newImageId = long.Parse(imgId);
                int count = -1;

                int countOfImages;
                int col = -1;
                int row = -1;

                StackPanel stackPanel = new StackPanel();
                StackPanel stackPanelButtons = new StackPanel();
                StackPanel stackPanelImage = new StackPanel();
                Border border = new Border { Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 238, 239)) };
                Button delete = new Button();
                delete.Click += new RoutedEventHandler(Delete_Click);
                Button update = new Button();
                update.Click += new RoutedEventHandler(Update_Click);
                if (addButton.Name == "addToHompageNew")
                {
                    countOfImages = HPNewGrid.Children.Count / 2 + 1;
                    col = countOfImages % HPNewGrid.ColumnDefinitions.Count;
                    row = countOfImages / HPNewGrid.ColumnDefinitions.Count;
                    stackPanel.SetValue(Grid.RowProperty, row);
                    stackPanel.SetValue(Grid.ColumnProperty, col);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);
                    count = HPNewGrid.Children.Count;

                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                    HPNewGrid.Children.Add(border);
                    col++;
                    if (col == HPNewGrid.ColumnDefinitions.Count)
                    {
                        col = 0;
                        row++;
                        if (row == HPNewGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            HPNewGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    HPNewGrid.Children.Add(stackPanel);
                }
                else
                {
                    countOfImages = HPPermanentGrid.Children.Count / 2 + 1;
                    col = countOfImages % HPPermanentGrid.ColumnDefinitions.Count;
                    row = countOfImages / HPPermanentGrid.ColumnDefinitions.Count;
                    stackPanel.SetValue(Grid.RowProperty, row);
                    stackPanel.SetValue(Grid.ColumnProperty, col);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);
                    count = HPPermanentGrid.Children.Count;
                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                    HPPermanentGrid.Children.Add(border);
                    col++;
                    if (col == HPPermanentGrid.ColumnDefinitions.Count)
                    {
                        col = 0;
                        row++;
                        if (row == HPPermanentGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            HPPermanentGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    HPPermanentGrid.Children.Add(stackPanel);
                }
                Functions.ImageFrame(img, stackPanel, stackPanelButtons, stackPanelImage, border, newImageId, template.Status, count, delete, update);

            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.Source as Button;
            Template template = new Template();
            if (button.Name == "HPNew")
            {
                template.Status = "new";
                Functions.IterationOverGrid(HPNewGrid, button.Tag.ToString());
            }
            else
            {
                template.Status = "customer";
                Functions.IterationOverGrid(HPPermanentGrid, button.Tag.ToString());
            }
            template.Id = int.Parse(button.Tag.ToString());
            Templates templates = new Templates();
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                templates.UpdateStatus(template);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Media media = new Media();
            media.ShowDialog();
            Image img = new Image();
            if (media.img != null)
            {
                Button btn = e.Source as Button;
                string position = btn.Name;
                string realPosition = "";
                string status = "";
                for (int i = 0; i < position.Length; i++)
                {
                    if (position[i] >= '0' && position[i] <= '9') realPosition += position[i];
                    else status += position[i];
                }
                int pos = int.Parse(realPosition);

                StackPanel stackPanel = new StackPanel();
                StackPanel buttons = new StackPanel();
                StackPanel image = new StackPanel();
                Button delBtn = new Button();
                if (status == "HPNew")
                {
                    stackPanel = HPNewGrid.Children[pos + 1] as StackPanel;
                }
                else
                {
                    stackPanel = HPPermanentGrid.Children[pos + 1] as StackPanel;
                }
                image = stackPanel.Children[1] as StackPanel;
                Image oldImage = image.Children[0] as Image;

                image.Children.Remove(oldImage);
                Template template = new Template();
                template.Id = int.Parse(oldImage.Tag.ToString());
                //MessageBox.Show(oldImage.Tag.ToString());
                string oldImageStatus = btn.Name;
                if (oldImageStatus[2] == 'N') template.Status = "new";
                else template.Status = "customer";
                Templates templates = new Templates();
                templates.UpdateStatus(template);



                img.Source = media.img.Source;
                img.Tag = media.img.Tag;
                string imgId = img.Tag.ToString();
                image.Children.Add(img);
                template.Id = int.Parse(imgId);
                template.Status = status;
                templates.UpdateStatus(template);
            }
        }
    }
}
