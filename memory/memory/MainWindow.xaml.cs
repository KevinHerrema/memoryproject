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
using System.Windows.Media.TextFormatting;
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
        int totaalkaarten = 16;
        bool beurtPlayer1 = true;
        int[] random2 = new int[16];
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


        string Naam1;
        string Naam2;



        public MainWindow()
        {
            InitializeComponent();
            InitializeMenuGrid();
            InitializeGameGrid(Rows + 1, Cols);

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            random1 = randomnr();
            plaatjes = plaatjeslist();
            
            kaartnmr = checkpositie();
            
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) //start button
        {
            Pkleur();
            random1 = randomnr();
            plaatjes = plaatjeslist();
            kaartnmr = checkpositie();
            addplaatjes(Rows, Cols);

            StartButton.Visibility = Visibility.Hidden;
            StopButton.Visibility = Visibility.Visible;
            GameGrid.Visibility = Visibility.Visible;
            
            Back1.Visibility = Visibility.Hidden;
            Ui.Visibility = Visibility.Visible;
            Reset.Visibility = Visibility.Visible;
            Save.Visibility = Visibility.Visible;
            Load.Visibility = Visibility.Visible;

            TextBlock3.Visibility = Visibility.Hidden;
            TextBlock2.Visibility = Visibility.Hidden;
            TextBlock1.Visibility = Visibility.Hidden;
            TextBox1.Visibility = Visibility.Hidden;
            TextBox2.Visibility = Visibility.Hidden;
            SpNaam.Visibility = Visibility.Hidden;
            SpNaam1.Visibility = Visibility.Hidden;
            Ui.Visibility = Visibility.Visible;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e) //stop button
        {
            Array.Clear(random2, 0, random2.Length);
            positie.Clear();
            imagelist.Clear();
            cleargame();
            PPlayer1 = 0;
            PPlayer2 = 0;
            StopButton.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Visible;
            GameGrid.Visibility = Visibility.Hidden;
            Ui.Visibility = Visibility.Hidden;
            Back1.Visibility = Visibility.Visible;
            Ui.Visibility = Visibility.Hidden;
            Reset.Visibility = Visibility.Hidden;
            Load.Visibility = Visibility.Hidden;
            Save.Visibility = Visibility.Hidden;
            TextBlock3.Visibility = Visibility.Visible;
            TextBlock2.Visibility = Visibility.Visible;
            TextBlock1.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            StartButton.Visibility = Visibility.Hidden;
        }

        private async void Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset.Visibility = Visibility.Hidden;
            GameGrid.Visibility = Visibility.Hidden;

            await Task.Delay(100);

            Array.Clear(random2, 0, random2.Length);
            positie.Clear();
            imagelist.Clear();
            cleargame();
            PPlayer1 = 0;
            PPlayer2 = 0;

            random1 = randomnr();
            plaatjes = plaatjeslist();
            kaartnmr = checkpositie();
            addplaatjes(Rows, Cols);

            Reset.Visibility = Visibility.Visible;
            GameGrid.Visibility = Visibility.Visible;


        }


        private void InitializeMenuGrid()
        {
            StopButton.Visibility = Visibility.Hidden;
            GameGrid.Visibility = Visibility.Hidden;
            Ui.Visibility = Visibility.Hidden;
           
            Reset.Visibility = Visibility.Hidden;
            Load.Visibility = Visibility.Hidden;
            Save.Visibility = Visibility.Hidden;
            SpNaam1.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Hidden;



        }
              

        private void SpNaam_Click(object sender, RoutedEventArgs e) //uhm ja misschien effe save files fixen
        {
            TextBlock2.Visibility = Visibility.Hidden;
            TextBox1.Visibility = Visibility.Hidden;
            SpNaam.Visibility = Visibility.Hidden;
            SpNaam1.Visibility = Visibility.Visible;
            Naam1 = TextBox1.Text;
            



        }


        private void SpNaam1_Click(object sender, RoutedEventArgs e)
        {
            TextBlock3.Visibility = Visibility.Hidden;
            TextBlock1.Visibility = Visibility.Hidden;
            TextBox2.Visibility = Visibility.Hidden;
            SpNaam1.Visibility = Visibility.Hidden;
                        
            Naam2 = TextBox2.Text;
            StartButton.Visibility = Visibility.Visible;
        }

        private void Pkleur()
        {
            if (beurtPlayer1 == true)
            {
                P1.Visibility = Visibility.Visible;
                P2.Visibility = Visibility.Hidden;
                DisplayNm1.Text = Naam1 + ": " + PPlayer1;
                DisplayNm1.Visibility = Visibility.Visible;
                DisplayNm2.Visibility = Visibility.Hidden;
            }
            if (beurtPlayer1 == false)
            {
                P2.Visibility = Visibility.Visible;
                P1.Visibility = Visibility.Hidden;
                DisplayNm2.Text = Naam2 + ": " + PPlayer2;
                DisplayNm1.Visibility = Visibility.Hidden;
                DisplayNm2.Visibility = Visibility.Visible;
            }
           
        }
       






        private void cleargame()
        {
            for (int i = GameGrid.Children.Count - 1; i >= 0; i--)
            {
                GameGrid.Children.RemoveAt(i);
            }

        }



        /// <summary>
        /// maakt de grid aan waar het spel in gespeeld zal worden
        /// </summary>
        /// <param name="Rows">het aantal rijen</param>
        /// <param name="Cols">het aantal collomen</param>
        private void InitializeGameGrid(int Rows, int Cols)
        {

            for (int i = 0; i < Rows; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < Cols; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

        }
        /// <summary>
        /// methode om de grid te vullen met achtergrond plaatjes en hieraan een tag te verbinden voor de positie
        /// </summary>
        /// <param name="rows">het aantal rijen</param>
        /// <param name="cols">aantal collomen</param>
        private void addplaatjes(int rows, int cols)
        {

            int c = 0;//index om te zien op welke positie het plaatje staat
            for (int a = 0; a < rows; a++)
            {
                for (int b = 0; b < cols; b++)
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
        /// <summary>
        /// methode om de lijst met plaatjes aan te maken die worden gehusselt door een random index hieraan toe te voegen
        /// </summary>
        /// <returns>een gehusselde lijst met plaatjes</returns>
        private List<ImageSource> plaatjeslist()
        {
            List<ImageSource> images = new List<ImageSource>();



            for (int i = 0; i < (Rows * Cols); i++)
            {
                int nr = i % 8 + 1;
                Uri path = new Uri("images/kaart_" + nr + ".tif", UriKind.Relative);
                images.Add(new BitmapImage(path));

            }




            //verplaats de inhoud van de ene lijst naar de ander in een random volgorde door de random index
            imagelist.Add(images[random1[0] - 1]);
            imagelist.Add(images[random1[1] - 1]);
            imagelist.Add(images[random1[2] - 1]);
            imagelist.Add(images[random1[3] - 1]);
            imagelist.Add(images[random1[4] - 1]);
            imagelist.Add(images[random1[5] - 1]);
            imagelist.Add(images[random1[6] - 1]);
            imagelist.Add(images[random1[7] - 1]);
            imagelist.Add(images[random1[8] - 1]);
            imagelist.Add(images[random1[9] - 1]);
            imagelist.Add(images[random1[10] - 1]);
            imagelist.Add(images[random1[11] - 1]);
            imagelist.Add(images[random1[12] - 1]);
            imagelist.Add(images[random1[13] - 1]);
            imagelist.Add(images[random1[14] - 1]);
            imagelist.Add(images[random1[15] - 1]);

            return imagelist;


        }
        /// <summary>
        /// maakt een lijst met posities aan die identiek is aan de plaajeslijst zodat deze posities vergelijkt kunnen worden
        /// </summary>
        /// <returns>een identieke lijst aan die van plaatjes</returns>
        private List<int> checkpositie()
        {
            List<int> nummerlijst = new List<int>();


            for (int i = 0; i < (Rows * Cols); i++)
            {
                int nr = i % 8 + 1;
                nummerlijst.Add(nr);
            }
            //verplaats de inhoud van de ene lijst naar de ander in een random volgorde door de random index
            positie.Add(nummerlijst[random1[0] - 1]);
            positie.Add(nummerlijst[random1[1] - 1]);
            positie.Add(nummerlijst[random1[2] - 1]);
            positie.Add(nummerlijst[random1[3] - 1]);
            positie.Add(nummerlijst[random1[4] - 1]);
            positie.Add(nummerlijst[random1[5] - 1]);
            positie.Add(nummerlijst[random1[6] - 1]);
            positie.Add(nummerlijst[random1[7] - 1]);
            positie.Add(nummerlijst[random1[8] - 1]);
            positie.Add(nummerlijst[random1[9] - 1]);
            positie.Add(nummerlijst[random1[10] - 1]);
            positie.Add(nummerlijst[random1[11] - 1]);
            positie.Add(nummerlijst[random1[12] - 1]);
            positie.Add(nummerlijst[random1[13] - 1]);
            positie.Add(nummerlijst[random1[14] - 1]);
            positie.Add(nummerlijst[random1[15] - 1]);
            return positie;

        }
        /// <summary>
        /// maakt een array aan random nummers aan die als index worden gebruikt voor het husselen van de kaarten
        /// </summary>
        /// <returns>random array</returns>
        private int[] randomnr()
        {

            int nummer1;// het nummer dat getest word
            int[] random2 = new int[16];
            for (int i = 0; i < 16; i++)
            {
                nummer1 = r.Next(17);

                for (int b = 0; b < 16; b++)
                {
                    while (random2.Contains(nummer1) == true)// als het nummer in de lijst zit blijft hij door genereren totdat er een nummer is wat er niet in zit
                    {
                        nummer1 = r.Next(17);
                        b = 0;
                    }
                }
                random2[i] = nummer1;
            }
            return random2;
        }
        /// <summary>
        /// de methode voor het omdraaien van kaarten
        /// </summary>
        /// <param name="sender">het plaatje dat omgedraaid word</param>
        /// <param name="e"></param>
        private void cardflip(object sender, MouseButtonEventArgs e)
        {
            if (magklicken)//zorgt dat er niet oneindig geclickt mag worden
            {

                Image card = (Image)sender;
                front = (int)card.Tag;
                card.Source = plaatjes[front];
                clicks = clicks + 1;

                if (clicks == 1)//houd de informatie van de eerste click bij
                {
                    plaats1 = front;
                    front1 = kaartnmr[front];
                    card1 = (Image)sender;
                }

                if (clicks == 2)//houd de informatie van de 2e click bij, start de timer en zorgt dat je niet meer mag clicken
                {
                    if (plaats1 == front)
                    {
                        clicks = clicks - 1;
                    }
                    else
                    {

                        front2 = kaartnmr[front];
                        card2 = (Image)sender;
                        plaats2 = front;
                        timer.Start();
                        magklicken = false;

                    }

                }
            }


        }
        /// <summary>
        /// de code die moet lopen als de timer klaar is en start score methode
        /// </summary>
        /// <param name="sender">de tijd</param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            score();
            timer.Stop();
            magklicken = true;
        }

        /// <summary>
        /// methode om de score en status van de kaarten te updaten
        /// </summary>
        private void score()
        {

            clicks = 0;
            if (front1 == front2)//als het nummer van de kaarten hetzelfde is
            {
                ImageSource leeg = null; // leeg de image source
                card1.Source = leeg;
                card2.Source = leeg;
                positie[plaats1] = 9; // zet de positie op negen want deze valt buiten de 8 kaarten
                positie[plaats2] = 9;
                if (beurtPlayer1 == true)//verhoog de score van de speler die aan de beurt is
                {
                    PPlayer1 = PPlayer1 + 1;
                }

                else
                {
                    PPlayer2 = PPlayer2 + 1;
                }
                totaalkaarten = totaalkaarten - 2;// haal 2 kaarten van het totaal aantal kaarten af
                Pkleur();
            }
            else//zijn de kaarten niet gelijk zet de kaarten terug en wissel van beurt
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
                   
                } Pkleur();

            }
            if (totaalkaarten == 0)// als er geen kaarten meer over zijn stop het spel
            {
                MessageBox.Show("score van" + "speler 1 " + "=" + PPlayer1);// laat de scores van beide spelers zien
                MessageBox.Show("score van " + "speler 2 " + "=" + PPlayer2);
                //reset alle waardes en ga terug naar het hoofd menu
                Array.Clear(random2, 0, random2.Length);
                positie.Clear();
                imagelist.Clear();
                cleargame();
                StopButton.Visibility = Visibility.Hidden;
                StartButton.Visibility = Visibility.Visible;
                GameGrid.Visibility = Visibility.Hidden;
                
                Back1.Visibility = Visibility.Visible;
                Ui.Visibility = Visibility.Hidden;
                Reset.Visibility = Visibility.Hidden;
                Load.Visibility = Visibility.Hidden;
                Save.Visibility = Visibility.Hidden;

            }


        }
        /// <summary>
        /// maakt een file aan voor het opslaan van het spel en doet hier alle waardes in van de score, beurt en kaar posities
        /// </summary>
        /// <param name="sender">de knop</param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {

            using (var writer = new StreamWriter(@"D:\memory.sav"))
            {
                writer.WriteLine(beurtPlayer1);
                writer.WriteLine(PPlayer1);
                writer.WriteLine(PPlayer2);
                writer.WriteLine(totaalkaarten);
                writer.WriteLine(Naam1);
                writer.WriteLine(Naam2);
                for (int i = 0; i < 16; i++)
                {
                    writer.WriteLine(positie[i]);

                }

            }
        }
        /// <summary>
        /// laad het text bestand in met de naam memory.sav en update de status van het spel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Click(object sender, EventArgs e)
        {
            positie.Clear();// maak de kaarten en positie lijst leeg zodat de opgeslagen waardes kunnen worden ingevuld
            clicks = 0;// reset clicks voor als iemand halferwege een beurt op load drukt
            imagelist.Clear();
            cleargame();//maak het speelveld leeg

            int regel = 0;
            string line;
            using (StreamReader reader = new StreamReader(@"D:\memory.sav"))
            {
                while ((line = reader.ReadLine()) != null)// zolang er nog regels in het bestand zijn doe
                {
                    if (regel == 0)
                    {
                        beurtPlayer1 = Convert.ToBoolean(line);
                    }
                    else
                    if (regel == 1)
                    {
                        PPlayer1 = Convert.ToInt32(line);

                    }
                    else
                    if (regel == 2)
                    {
                        PPlayer2 = Convert.ToInt32(line);

                    }
                    else
                    if (regel == 3)
                    {
                        totaalkaarten = Convert.ToInt32(line);
                    }
                    else
                    if(regel == 4)
                    {
                        Naam1 = line;
                    }
                    else
                    if(regel == 5)
                    {
                        Naam2 = line;
                    }
                    else
                    {
                        positie.Add(Convert.ToInt32(line));// vul de lijst positie en plaatjes met de opgeslagen waardes
                        BitmapImage image = null;
                        if (line != "9")
                        {
                            Uri path = new Uri("images/kaart_" + line + ".tif", UriKind.Relative);
                            image = new BitmapImage(path);
                        }

                        imagelist.Add(image);
                    }
                    regel++;
                }

            }
            int c = 0;
            int check;
            Pkleur();
            //maak een nieuw speelveld aan met de geupdate plaatjes
            for (int a = 0; a < Rows; a++)
            {
                for (int b = 0; b < Cols; b++)
                {
                    Image backgroundimage = new Image();
                    backgroundimage.Source = new BitmapImage(path);
                    check = positie[c];
                    backgroundimage.Tag = c;
                    c++;
                    backgroundimage.MouseDown += new MouseButtonEventHandler(cardflip);
                    if (check == 9)//als de positie op 9 staat laat het vakje leeg
                    {

                    }
                    else// is de positie onder de 9 maak een achtergrond aan
                    {
                        Grid.SetColumn(backgroundimage, b);
                        Grid.SetRow(backgroundimage, a);
                        GameGrid.Children.Add(backgroundimage);

                    }



                }
            }


        }



    }





}