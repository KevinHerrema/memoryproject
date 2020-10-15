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
using System.Windows.Media.TextFormatting;
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


        public MainWindow()
        {
            InitializeComponent();
            InitializeMenuGrid();
            InitializeGameGrid(Rows, Cols);


            GameGrid.Visibility = Visibility.Hidden;

        }

        

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            
            addplaatjes(Rows, Cols);
            Button1.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Visible;
            GameGrid.Visibility = Visibility.Visible;
            
            //voer hier verdere extra's in zoals de timer zal ik later in goede button toevoegen
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            addplaatjes(Rows, Cols);           
            Button2.Visibility = Visibility.Hidden;
            Button1.Visibility = Visibility.Visible;
            GameGrid.Visibility = Visibility.Hidden;


            //Proef versie Button + plaatje tijdelijk als stop button gebruikt
        }
        private void InitializeMenuGrid()
        {
            //heeft nog geen toevoeging , moet nog gesorteerd worden
            
        }

       





        private List<ImageSource> Assetlist()
        {
            List<ImageSource> images = new List<ImageSource>();
            Uri path = new Uri("Assets/Back.png", UriKind.Relative);
            images.Add(new BitmapImage(path));
            return images;
           //zonder borders
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
                    Grid.SetColumn(backgroundimage, a);
                    Grid.SetRow(backgroundimage, b);
                    GameGrid.Children.Add(backgroundimage);


                }
            }
        }
        private List<ImageSource> plaatjeslist()
        {
            List<ImageSource> images = new List<ImageSource>();
            for(int i = 0; i < (Rows * Cols) ; i++)
            {
                int nr = i % 8 + 1;
                Uri path = new Uri("plaatjes/" + nr + ".png", UriKind.Relative);
                images.Add(new BitmapImage(path));
            }
            return images;
        }
       
        private void cardflip(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
        }


    }

   

        
    
}
