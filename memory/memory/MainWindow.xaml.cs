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

namespace memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Rows = 4;
        private const int Cols = 4;
        Random r = new Random();
       
        public MainWindow()
        {
            InitializeComponent();
            InitializeGameGrid(Rows, Cols);
           
            addplaatjes(Rows, Cols);
        }
        private void InitializeGameGrid(int Rows, int Cols)
        {
            for(int i = 0; i < Rows; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < Cols; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }


        private void addplaatjes(int rows, int cols)
        {
            List<ImageSource> plaatjes = plaatjeslist();
            for(int a = 0; a < rows; a++)
            {
                for(int b =0; b < cols; b++)
                {
                    Image backgroundimage = new Image();
                    Uri path = new Uri("plaatjes/kaart.png", UriKind.Relative);
                    backgroundimage.Source = new BitmapImage(path);
                    backgroundimage.Tag = plaatjes.First();
                    
                    plaatjes.RemoveAt(0);
                    backgroundimage.MouseDown += new MouseButtonEventHandler(cardflip);
                    Grid.SetColumn(backgroundimage, b);
                    Grid.SetRow(backgroundimage, a);
                    GameGrid.Children.Add(backgroundimage);
                    

                }
            } 
        } 
        private List<ImageSource> plaatjeslist()
        { List<ImageSource> images = new List<ImageSource>();
            List<ImageSource> imagelist = new List<ImageSource>();
            for(int i = 0; i < (Rows * Cols) ; i++)
            {
                int nr = i % 8 + 1;
                Uri path = new Uri("plaatjes/" + nr + ".png", UriKind.Relative);
                images.Add(new BitmapImage (path));
                
            }
            int nummer;
            int[] random = new int[8];
            for (int i = 0; i < 8; i++)
            {
                nummer = r.Next(9);

                for (int b = 0; b < 8; b++)
                {
                    while (random.Contains(nummer) == true)
                    {
                        nummer = r.Next(9);
                        b = 0;
                    }
                }
                random[i] = nummer;
            }
            int nummer1;
            int[] random1 = new int[8];
            for (int i = 0; i < 8; i++)
            {
                nummer1 = r.Next(9);

                for (int b = 0; b < 8; b++)
                {
                    while (random1.Contains(nummer1) == true)
                    {
                        nummer1 = r.Next(9);
                        b = 0;
                    }
                }
                random1[i] = nummer1;
            }
            imagelist.Add(images[random[0] - 1]);
            imagelist.Add(images[random[1] - 1]);
            imagelist.Add(images[random[2] - 1]);
            imagelist.Add(images[random[3] - 1]);
            imagelist.Add(images[random[4] - 1]);
            imagelist.Add(images[random[5] - 1]);
            imagelist.Add(images[random[6] - 1]);
            imagelist.Add(images[random[7] - 1]);
            imagelist.Add(images[random1[0] - 1]);
            imagelist.Add(images[random1[1] - 1]);
            imagelist.Add(images[random1[2] - 1]);
            imagelist.Add(images[random1[3] - 1]);
            imagelist.Add(images[random1[4] - 1]);
            imagelist.Add(images[random1[5] - 1]);
            imagelist.Add(images[random1[6] - 1]);
            imagelist.Add(images[random1[7] - 1]);
            return imagelist;
        }
       
        private void cardflip(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
        }

        





    }

    

        
    
}
