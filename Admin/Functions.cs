using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

namespace Admin
{
    public static class Functions
    {
        public static byte[] ImageToByteArray(BitmapImage imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageIn));
                encoder.Save(memStream);
                return memStream.ToArray();
            }
        }
        public static void ImageFrame(System.Windows.Controls.Image img, StackPanel stackPanel, StackPanel stackPanelButtons, StackPanel stackPanelImage, Border border, long imgId, string status, int count, Button delete, Button update)
        {
            img.Margin = new Thickness(15, 0, 15, 5);
            img.Tag = imgId.ToString();

            stackPanelButtons.Orientation = Orientation.Horizontal;
            stackPanelButtons.HorizontalAlignment = HorizontalAlignment.Right;

            
            delete.Height = 15;
            delete.Width = 15;
            delete.Margin = new Thickness(5, 5, 15, 5);
            delete.Background = Brushes.Transparent;
            delete.BorderThickness = new Thickness(0);
            string src = @"..\..\..\Images\deleteIcon.png";
            System.Windows.Controls.Image deleteIcon = new System.Windows.Controls.Image();
            deleteIcon.Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource;
            //MessageBox.Show(deleteIcon.Source.ToString());
            delete.Content = deleteIcon;
            delete.Tag = imgId.ToString();
            
            delete.Name = status;

            
            update.Height = 15;
            update.Width = 15;
            update.Background = Brushes.Transparent;
            update.BorderThickness = new Thickness(0);
            
            update.Tag = imgId.ToString();
            update.Name = status + count.ToString();
            delete.Name = status;
            src = @"..\..\..\Images\updateIcon.png";
            System.Windows.Controls.Image updateIcon = new Image();
            updateIcon.Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource;
            update.Content = updateIcon;
            stackPanelButtons.Children.Add(update);
            stackPanelButtons.Children.Add(delete);
            stackPanelImage.Children.Add(img);

            border.CornerRadius = new CornerRadius(15);
            border.Margin = new Thickness(3, 0, 3, 4);
        }
        public static void IterationOverGrid(Grid grid, string delBtnTag)
        {
            bool flag = false;
            StackPanel panel = new StackPanel();
            Image image = new Image();
            Object stackPanels;
            Object buttonsImageStackPanels;
            Object imageOrButton;
            //allStackPanels
            for (int i = grid.Children.Count - 1; i >= 0; i--)
            {
                stackPanels = grid.Children[i];
                if (stackPanels is StackPanel)
                {
                    StackPanel stackPanel = stackPanels as StackPanel;
                    //There wil be two stack panels. one for buttons and another for the image.
                    for (int j = stackPanel.Children.Count - 1; j >= 0; j--)
                    {
                        buttonsImageStackPanels = stackPanel.Children[j];
                        if (buttonsImageStackPanels is StackPanel)
                        {
                            StackPanel imageStackPanels = buttonsImageStackPanels as StackPanel;
                            for (int k = imageStackPanels.Children.Count - 1; k >= 0; k--)
                            {
                                imageOrButton = imageStackPanels.Children[k];
                                if (!flag)
                                {
                                    if (imageOrButton is Image)
                                    {
                                        Image img = imageOrButton as Image;
                                        if (img.Tag.ToString() == delBtnTag)
                                        {
                                            grid.Children.Remove(stackPanel);
                                            grid.Children.RemoveAt(grid.Children.Count - 1);
                                            return;
                                        }
                                        else
                                        {
                                            image = img;
                                            imageStackPanels.Children.Remove(img);
                                            grid.Children.Remove(stackPanel);
                                            flag = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (imageOrButton is Image)
                                    {
                                        StackPanel st = new StackPanel();
                                        Image img = imageOrButton as Image;
                                        Button button = new Button();
                                        if (img.Tag.ToString() == delBtnTag)
                                        {
                                            imageStackPanels.Children.Remove(img);
                                            imageStackPanels.Children.Add(image);
                                            st = stackPanel.Children[0] as StackPanel;
                                            button = st.Children[1] as Button;
                                            button.Tag = image.Tag;
                                            grid.Children.RemoveAt(grid.Children.Count - 1);
                                            return;
                                        }
                                        imageStackPanels.Children.Remove(img);
                                        imageStackPanels.Children.Add(image);

                                        st = stackPanel.Children[0] as StackPanel;
                                        button = st.Children[1] as Button;
                                        button.Tag = image.Tag;
                                        image = img;

                                    }


                                }
                            }
                        }
                    }

                }
            }

        }
    }
}
