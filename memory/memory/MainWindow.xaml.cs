using System;
using System.Collections.Generic;
using System.IO;
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
        int front = 0;
        int plaats1;
        int plaats2;
        int front1;
        int front2;
        int[] random1 = new int[16];
        List<ImageSource> plaatjes;
        List<int> kaartnmr;
        private int[,] statuskaarten = new int[Rows, Cols];
        Uri path = new Uri("images/achterkant.tif", UriKind.Relative);
        DispatcherTimer timer = new DispatcherTimer();
        bool magklicken = true;
        Image card1;
        Image card2; 
        List<ImageSource> imagelist = new List<ImageSource>();
        List<int> positie = new List<int>();
        public MainWindow()
        {


            InitializeComponent();
            
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            
            InitializeGameGrid(Rows, Cols);
            random1 = randomnr();
            plaatjes = plaatjeslist();
            addplaatjes(Rows, Cols);
            kaartnmr = checkpositie();
            if (clicks == 2)
            {
                score();
            }
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
            if (magklicken)
            {
                
                Image card = (Image)sender;
                front = (int)card.Tag;
                card.Source = plaatjes[front];
                clicks = clicks + 1;
                if (clicks == 1)
                {   plaats1 = front;
                    front1 = kaartnmr[front];
                    card1 = (Image)sender;
                }   
                if (clicks == 2)
                {
                    front2 = kaartnmr[front];
                    card2 = (Image)sender;
                    plaats2 = front;
                    timer.Start();
                    magklicken = false;
                }
            }

            
        }
        void timer_Tick(object sender, EventArgs e)
        {
            score();
            timer.Stop();
            magklicken = true;
        }
       
        private void score()
            {
            
            clicks = 0;
            if (front1 == front2)
            {
                ImageSource leeg = null;
                card1.Source = leeg;
                card2.Source = leeg;
                positie[plaats1] = 9;
                positie[plaats2] = 9;
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
                ImageSource terug = new BitmapImage(new Uri("images/achterkant.tif", UriKind.Relative));
                card1.Source = terug;
                card2.Source = terug;
                if (beurtPlayer1 == true)
                {
                    beurtPlayer1 = false;
                }
                else
                {
                    beurtPlayer1 = true;
                }

            }
            savebutton();

        }
        private void savebutton()
        {
           
            using (var writer = new StreamWriter(@"D:\memory.sav"))
            {   writer.WriteLine(beurtPlayer1);
                writer.WriteLine(PPlayer1);
                writer.WriteLine(PPlayer2);
                for(int i = 0; i < 16; i++)
                {
                    writer.WriteLine(positie[i]);

                }
            }
        }

        private void loadbutton()
        {
            int regel = 0;
            string line;
            using (StreamReader reader = new StreamReader(@"D:\memory.sav"))
            {
               while((line = reader.ReadLine()) != null)
                {
                    if (regel == 0)
                    {
                        beurtPlayer1 = Convert.ToBoolean(line);
                    }
                    if(regel == 1)
                    {
                        PPlayer1 = Convert.ToInt32(line) - 48;
                    }
                    if (regel == 2)
                    {
                        PPlayer2 = Convert.ToInt32(line) - 48;
                    }
                    else
                    {

                    }


                }regel++;
            }
           
        }




        private void savescore (object sender, RoutedEventArgs e) 
        {
            MessageBox.Show("Score opgeslagen");

            using (var writer = new StreamWriter(@"C:\highscore.sav")) 
            {
                writer.WriteLine(PPlayer1);
                writer.WriteLine(PPlayer2);



            }


        }
        
        private void scorebord() 
        {
            MessageBox.Show("laad test");

            using (var writer = new StreamWriter(@"C:\highscore.sav"))
            {
                
                Dictionary<string, int> scorebord = new Dictionary<string, int>();
                

                




               




            }

        
        
        }
        










    }

    

        
    
}
