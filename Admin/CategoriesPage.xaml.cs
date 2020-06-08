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
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using DataAccess.Models;
using DataAccess;
using System.Data.SqlClient;

namespace Admin
{
    /// <summary>
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        private int id;
        public CategoriesPage(string categoryId)
        {
            InitializeComponent();
            id = int.Parse(categoryId);
            List<Template> templates = new List<Template>();
            Templates template = new Templates();
            
            try
            {
                templates = template.GetImages(null, null, id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            int rowNew = 0;
            int colNew = 1;
            int rowPermanent = 0;
            int colPermanent = 1;
            int rowTemplate = 0;
            int colTemplate = 1;
            for(int i=0;i<templates.Count;i++)
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
                update.Click += new RoutedEventHandler(update_Click);
                if (templates[i].Status == "new" || templates[i].Status=="HPNew")
                {
                    stackPanel.SetValue(Grid.RowProperty, rowNew);
                    stackPanel.SetValue(Grid.ColumnProperty, colNew);

                    img.Name = "a" + newCollectionGrid.Children.Count.ToString();

                    Functions.ImageFrame(img, stackPanel, stackPanelButtons, stackPanelImage, border, templates[i].Id, templates[i].Status, newCollectionGrid.Children.Count,delete,update);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);
                    
                    Grid.SetRow(border, rowNew);
                    Grid.SetColumn(border, colNew);
                    newCollectionGrid.Children.Add(border);
                    colNew++;
                    if (colNew == newCollectionGrid.ColumnDefinitions.Count)
                    {
                        colNew = 0;
                        rowNew++;
                        if (rowNew == newCollectionGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            newCollectionGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    newCollectionGrid.Children.Add(stackPanel);
                }
                else if (templates[i].Status == "permanent" || templates[i].Status=="HPPermanent")
                {
                    stackPanel.SetValue(Grid.RowProperty, rowPermanent);
                    stackPanel.SetValue(Grid.ColumnProperty, colPermanent);

                    Functions.ImageFrame(img, stackPanel, stackPanelButtons, stackPanelImage, border, templates[i].Id, templates[i].Status, permanentGrid.Children.Count,delete,update);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);

                    Grid.SetRow(border, rowPermanent);
                    Grid.SetColumn(border, colPermanent);
                    permanentGrid.Children.Add(border);
                    colPermanent++;
                    if (colPermanent == permanentGrid.ColumnDefinitions.Count)
                    {
                        colPermanent = 0;
                        rowPermanent++;
                        if (rowPermanent == permanentGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            permanentGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    permanentGrid.Children.Add(stackPanel);
                }
                /*else if (templates[i].Status == "template")
                {
                    stackPanel.SetValue(Grid.RowProperty, rowTemplate);
                    stackPanel.SetValue(Grid.ColumnProperty, colTemplate);

                    Functions.ImageFrame(img, stackPanel, stackPanelButtons, stackPanelImage, border, templates[i].Id, templates[i].Status, templatesGrid.Children.Count,delete,update);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);

                    Grid.SetRow(border, rowTemplate);
                    Grid.SetColumn(border, colTemplate);
                    templatesGrid.Children.Add(border);
                    colTemplate++;
                    if (colTemplate == templatesGrid.ColumnDefinitions.Count)
                    {
                        colTemplate = 0;
                        rowTemplate++;
                        if (rowTemplate == templatesGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            templatesGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    templatesGrid.Children.Add(stackPanel);
                }*/
            }
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            Button btn = e.Source as Button;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
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
                if (status == "new")
                {
                    stackPanel = newCollectionGrid.Children[pos + 1] as StackPanel;
                }
                else if (status == "permanent")
                {
                    stackPanel = permanentGrid.Children[pos + 1] as StackPanel;
                }
                /*else
                {
                    stackPanel = templatesGrid.Children[pos + 1] as StackPanel;
                }*/
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(op.FileName));
                byte[] b = Functions.ImageToByteArray(img.Source as BitmapImage);
                Template template = new Template();
                template.Photo = b;
                template.Id = long.Parse(btn.Tag.ToString());
                template.Status = status;
                Templates t = new Templates();
                int returnValue = -1;
                StringBuilder errorMessages = new StringBuilder();
                try
                {
                    returnValue = t.UpdateImage(template);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (returnValue == 0)
                {
                    MessageBox.Show("The product is already inserted!!!");
                }
                else
                {
                    buttons = stackPanel.Children[0] as StackPanel;
                    delBtn = buttons.Children[1] as Button;

                    image = stackPanel.Children[1] as StackPanel;
                    Image oldImage = image.Children[0] as Image;

                    image.Children.Remove(oldImage);

                    MemoryStream mstream = new MemoryStream(b);
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = mstream;
                    bitmapImage.EndInit();
                    img.Source = bitmapImage;
                    img.Tag = returnValue.ToString();
                    delBtn.Tag = returnValue.ToString();
                    btn.Tag = returnValue;
                    image.Children.Add(img);
                    flag = true;
                }
                if (flag)
                {
                    //ProductDetails productDetails = new ProductDetails(int.Parse(btn.Tag.ToString()), id);
                    ProductDetails productDetails = new ProductDetails(returnValue, id);
                    productDetails.ShowDialog();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button delBtn = e.Source as Button;
            Template template = new Template();
            template.Id = int.Parse(delBtn.Tag.ToString());
            template.Status = "deleted";
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
            //template.DeleteImage(long.Parse(delBtn.Tag.ToString()));
            if (delBtn.Name == "new" || delBtn.Name=="HPNew")
                Functions.IterationOverGrid(newCollectionGrid, delBtn.Tag.ToString());
            else if (delBtn.Name == "permanent" || delBtn.Name=="HPPermanent")
                Functions.IterationOverGrid(permanentGrid, delBtn.Tag.ToString());
            /*else if (delBtn.Name == "template")
                Functions.IterationOverGrid(templatesGrid, delBtn.Tag.ToString());*/
            
            
        }
        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            long newImageId = -1;
            int count = -1;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            Template t = new Template();
            if (op.ShowDialog() == true)
            {
                Image img = new Image();
                Button btn = e.Source as Button;
                
                int countOfImages;
                int col=-1;
                int row=-1;

                StackPanel stackPanel = new StackPanel();
                StackPanel stackPanelButtons = new StackPanel();
                StackPanel stackPanelImage = new StackPanel();
                Border border = new Border { Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 238, 239)) };
                Button delete = new Button();
                delete.Click += new RoutedEventHandler(Delete_Click);
                Button update = new Button();
                update.Click += new RoutedEventHandler(update_Click);
                if (btn.Name == "addToNewCollection")
                {
                    t.Status = "new";
                    countOfImages = newCollectionGrid.Children.Count / 2 + 1;
                    col = countOfImages % newCollectionGrid.ColumnDefinitions.Count;
                    row = countOfImages / newCollectionGrid.ColumnDefinitions.Count;
                    stackPanel.SetValue(Grid.RowProperty, row);
                    stackPanel.SetValue(Grid.ColumnProperty, col);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);
                    count = newCollectionGrid.Children.Count;

                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                    newCollectionGrid.Children.Add(border);
                    col++;
                    if (col == newCollectionGrid.ColumnDefinitions.Count)
                    {
                        col = 0;
                        row++;
                        if (row == newCollectionGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            newCollectionGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    newCollectionGrid.Children.Add(stackPanel);
                    
                }
                else if (btn.Name == "addToAlways")
                {
                    t.Status = "permanent";
                    countOfImages = permanentGrid.Children.Count / 2 + 1;
                    col = countOfImages % permanentGrid.ColumnDefinitions.Count;
                    row = countOfImages / permanentGrid.ColumnDefinitions.Count;
                    stackPanel.SetValue(Grid.RowProperty, row);
                    stackPanel.SetValue(Grid.ColumnProperty, col);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);
                    count = permanentGrid.Children.Count;
                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                    permanentGrid.Children.Add(border);
                    col++;
                    if (col == permanentGrid.ColumnDefinitions.Count)
                    {
                        col = 0;
                        row++;
                        if (row == permanentGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            permanentGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    permanentGrid.Children.Add(stackPanel);
                }
                /*else if (btn.Name == "AddToTemplates")
                {
                    t.Status = "template";
                    countOfImages = templatesGrid.Children.Count / 2 + 1;
                    col = countOfImages % templatesGrid.ColumnDefinitions.Count;
                    row = countOfImages / templatesGrid.ColumnDefinitions.Count;
                    stackPanel.SetValue(Grid.RowProperty, row);
                    stackPanel.SetValue(Grid.ColumnProperty, col);

                    stackPanel.Children.Add(stackPanelButtons);
                    stackPanel.Children.Add(stackPanelImage);

                    count = templatesGrid.Children.Count;

                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                    templatesGrid.Children.Add(border);
                    col++;
                    if (col == templatesGrid.ColumnDefinitions.Count)
                    {
                        col = 0;
                        row++;
                        if (row == templatesGrid.RowDefinitions.Count)
                        {
                            var rowDefinition = new RowDefinition();
                            rowDefinition.Height = new GridLength(150);
                            templatesGrid.RowDefinitions.Add(rowDefinition);
                        }
                    }
                    templatesGrid.Children.Add(stackPanel);
                    
                }*/

                img.Source = new BitmapImage(new Uri(op.FileName));
                byte[] b = Functions.ImageToByteArray(img.Source as BitmapImage);
                t.Photo = b;

                
                Templates template = new Templates();
                StringBuilder errorMessages = new StringBuilder();
                try
                {
                    newImageId = template.InsertTemplate(t);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (newImageId == 0)
                {
                    MessageBox.Show("This image is already inserted");
                    if (t.Status == "new")
                    {
                        newCollectionGrid.Children.RemoveAt(newCollectionGrid.Children.Count - 1);
                        newCollectionGrid.Children.RemoveAt(newCollectionGrid.Children.Count - 1);
                    }
                    else if (t.Status == "permanent")
                    {
                        permanentGrid.Children.RemoveAt(permanentGrid.Children.Count - 1);
                        permanentGrid.Children.RemoveAt(permanentGrid.Children.Count - 1);
                    }
                    /*else
                    {
                        templatesGrid.Children.RemoveAt(templatesGrid.Children.Count - 1);
                        templatesGrid.Children.RemoveAt(templatesGrid.Children.Count - 1);
                    }*/
                }
                else
                    Functions.ImageFrame(img, stackPanel, stackPanelButtons, stackPanelImage, border, newImageId, t.Status, count, delete, update);

                if (newImageId != 0) flag = true;
                else flag = false;

            }
            if (flag)
            {
                ProductDetails productDetails = new ProductDetails(newImageId, id);
                productDetails.ShowDialog();
                if(productDetails.canceled)
                {
                    if(t.Status=="new")
                    {
                        newCollectionGrid.Children.RemoveAt(newCollectionGrid.Children.Count - 1);
                        newCollectionGrid.Children.RemoveAt(newCollectionGrid.Children.Count - 1);
                    }
                    else if(t.Status=="permanent")
                    {
                        permanentGrid.Children.RemoveAt(permanentGrid.Children.Count - 1);
                        permanentGrid.Children.RemoveAt(permanentGrid.Children.Count - 1);
                    }
                    /*else
                    {
                        templatesGrid.Children.RemoveAt(templatesGrid.Children.Count - 1);
                        templatesGrid.Children.RemoveAt(templatesGrid.Children.Count - 1);
                    }*/
                    Template template = new Template();
                    template.Id = newImageId;
                    template.Status = "deleted";
                    Templates templates = new Templates();
                    templates.UpdateStatus(template);

                }
            }
        }



        
    }
}
