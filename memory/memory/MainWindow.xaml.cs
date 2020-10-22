using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool beurtPlayer1 = true;
         
        int PPlayer1 = 0;
        int PPlayer2 = 0;
        private const int Rows = 4;
        private const int Cols = 4;
        Random r = new Random();
        private static Random rng = new Random();
        int clicks = 0;
        int front1;
        int front2;
        int[] random1 = new int[16];
        List<ImageSource> plaatjes;
        List<int> kaartnmr;
        Uri path = new Uri("images/achterkant.tif", UriKind.Relative);
        public MainWindow()
        {

            InitializeComponent();
            InitializeGameGrid(Rows, Cols);
            random1 = randomnr();
            plaatjes = plaatjeslist();
            addplaatjes(Rows, Cols);
            kaartnmr = checkpositie();
            
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
            
            int c = 0; 
            for(int a = 0; a < rows; a++)
            {
                for(int b =0; b < cols; b++)
                {
                    Image backgroundimage = new Image();
                    backgroundimage.Source = new BitmapImage(path);
                    backgroundimage.Tag = c;
                    c++;
                    
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


            for (int i = 0; i < (Rows * Cols); i++)
            {
                int nr = i % 8 + 1;
                Uri path = new Uri("images/kaart_" + nr + ".tif", UriKind.Relative);
                images.Add(new BitmapImage(path));

            }



            imagelist.Add(images[random1[0]-1]);
            imagelist.Add(images[random1[1]-1]);
            imagelist.Add(images[random1[2]-1]);
            imagelist.Add(images[random1[3]-1]);
            imagelist.Add(images[random1[4]-1]);
            imagelist.Add(images[random1[5]-1]);
            imagelist.Add(images[random1[6]-1]);
            imagelist.Add(images[random1[7]-1]);
            imagelist.Add(images[random1[8]-1]);
            imagelist.Add(images[random1[9]-1]);
            imagelist.Add(images[random1[10]-1]);
            imagelist.Add(images[random1[11]-1]);
            imagelist.Add(images[random1[12]-1]);
            imagelist.Add(images[random1[13]-1]);
            imagelist.Add(images[random1[14]-1]);
            imagelist.Add(images[random1[15]-1]);

            return imagelist;
            
            
        }
        private List<int> checkpositie()
        {   List<int> nummerlijst = new List<int>();
            List<int> positie = new List<int>();
            
            for (int i = 0; i < (Rows * Cols); i++)
            {
                int nr = i % 8 + 1;
                nummerlijst.Add(nr);
            }

            positie.Add(nummerlijst[random1[0]-1]);
            positie.Add(nummerlijst[random1[1]-1]);
            positie.Add(nummerlijst[random1[2]-1]);
            positie.Add(nummerlijst[random1[3]-1]);
            positie.Add(nummerlijst[random1[4]-1]);
            positie.Add(nummerlijst[random1[5]-1]);
            positie.Add(nummerlijst[random1[6]-1]);
            positie.Add(nummerlijst[random1[7]-1]);
            positie.Add(nummerlijst[random1[8]-1]);
            positie.Add(nummerlijst[random1[9]-1]);
            positie.Add(nummerlijst[random1[10]-1]);
            positie.Add(nummerlijst[random1[11]-1]);
            positie.Add(nummerlijst[random1[12]-1]);
            positie.Add(nummerlijst[random1[13]-1]);
            positie.Add(nummerlijst[random1[14]-1]);
            positie.Add(nummerlijst[random1[15]-1]);
            return positie;

        } 
       
     
       private int[] randomnr()
        {
            
            int nummer1;
            int[] random2 = new int[16];
            for (int i = 0; i < 16; i++)
            {
                nummer1 = r.Next(17);

                for (int b = 0; b < 16; b++)
                {
                    while (random2.Contains(nummer1) == true)
                    {
                        nummer1 = r.Next(17);
                        b = 0;
                    }
                }
                random2[i] = nummer1;
            }return random2;
        } 
        
        private void cardflip(object sender, MouseButtonEventArgs e)
        {
            int front = 0;
            Image card = (Image)sender;
            front = (int)card.Tag;
            card.Source = plaatjes[front];
            clicks = clicks + 1;
            if(clicks == 1)
            {
                front1 = kaartnmr[front];
            }
            if(clicks == 2)
            {
                front2 = kaartnmr[front];
            }
            
        }private void score()
            {
            Thread.Sleep(1000);
            clicks = 0;
            if (front1 == front2)
            {
                if (beurtPlayer1 == true)
                {
                    PPlayer1 = PPlayer1 + 1;
                }



                else
                {
                    PPlayer2 = PPlayer2 + 1;
                }

            }
            else
            {
                addplaatjes(Rows, Cols);
                if (beurtPlayer1 == true)
                {
                    beurtPlayer1 = false;
                }
                else
                {
                    beurtPlayer1 = true;
                }
            }

        }

        










    }

    

        
    
}
