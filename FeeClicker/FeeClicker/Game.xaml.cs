using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace FeeClicker
{
    /// <summary>
    /// Logique d'interaction pour Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private Character magicWands = new Character("Baguettes magiques", 0, 1, 1, 20);
        private Character fairies = new Character("Fées", 0, 5, 1, 100);
        private Character farms = new Character("Fermes", 0, 20, 1, 500);
        private ulong stars = 0;
        private ulong starsPerSecond = 0;

        public Game(Boolean newGame)
        {
            InitializeComponent();
            if (newGame)
            {
                //Vider les fichiers textes
                ecritureFichier("./savedGame/stars.txt", "0");
                ecritureFichier("./savedGame/fairies.txt", "0");
                ecritureFichier("./savedGame/fairiesBonus.txt", "1");
            }
            else
            {
                //Récupérer les valeurs des fichiers textes
                String svgStars = lectureFichierSvg("./savedGame/stars.txt");
                if (svgStars != null)
                    stars = Convert.ToUInt64(svgStars);

                String svgFairies = lectureFichierSvg("./savedGame/fairies.txt");
                if (svgFairies != null)
                    fairies.setNumber(Convert.ToUInt32(svgFairies));

                starsPerSecond = magicWands.getStarsPerSecond();
                starsPerSecond += fairies.getStarsPerSecond();
                starsPerSecond += farms.getStarsPerSecond();
            }

            label_starsCounter.Content = stars;
            label_starsPerSecondCounter.Content = starsPerSecond;

            label_magicWands.Content = magicWands.getNumber();
            label_fairies.Content = fairies.getNumber();
            label_farms.Content = farms.getNumber();

            label_magicWandPrice.Content = magicWands.getPrice();
            label_fairyPrice.Content = fairies.getPrice();
            label_farmPrice.Content = farms.getPrice();

            DispatcherTimer timer1second = new DispatcherTimer();
            timer1second.Interval = TimeSpan.FromSeconds(1);
            timer1second.Tick += addStarsPerSecond;
            timer1second.Tick += checkWhatWeCanBuy;
            timer1second.Start();
        }

        void addStarsPerSecond(object sender, EventArgs e)
        {
            stars += starsPerSecond;
            label_starsCounter.Content = stars;
        }

        void checkWhatWeCanBuy(object sender, EventArgs e)
        {
            if (stars >= magicWands.getPrice())
            {
                button_addMagicWand.IsEnabled = true;
            }
            else
            {
                button_addMagicWand.IsEnabled = false;
            }
            if (stars >= fairies.getPrice())
            {
                button_addFairy.IsEnabled = true;
            }
            else
            {
                button_addFairy.IsEnabled = false;
            }
            if (stars >= farms.getPrice())
            {
                button_addFarm.IsEnabled = true;
            }
            else
            {
                button_addFarm.IsEnabled = false;
            }
        }

        private void addStar(object sender, RoutedEventArgs e)
        {
            stars++;
            label_starsCounter.Content = stars;
        }

        private void addMagicWand(object sender, RoutedEventArgs e)
        {
            stars -= magicWands.getPrice();
            magicWands.addOne();
            if (stars >= magicWands.getPrice())
            {
                button_addMagicWand.IsEnabled = true;
            }
            else
            {
                button_addMagicWand.IsEnabled = false;
            }
            label_magicWands.Content = magicWands.getNumber();
            label_magicWandPrice.Content = magicWands.getPrice();
            label_starsCounter.Content = stars;
            updateStarsPerSecond();
        }

        private void addFairy(object sender, RoutedEventArgs e)
        {
            stars -= fairies.getPrice();
            fairies.addOne();
            if (stars >= fairies.getPrice())
            {
                button_addFairy.IsEnabled = true;
            }
            else
            {
                button_addFairy.IsEnabled = false;
            }
            label_fairies.Content = fairies.getNumber();
            label_fairyPrice.Content = fairies.getPrice();
            label_starsCounter.Content = stars;
            updateStarsPerSecond();
        }

        private void addFarm(object sender, RoutedEventArgs e)
        {
            stars -= farms.getPrice();
            farms.addOne();
            if (stars >= farms.getPrice())
            {
                button_addFarm.IsEnabled = true;
            }
            else
            {
                button_addFarm.IsEnabled = false;
            }
            label_farms.Content = farms.getNumber();
            label_farmPrice.Content = farms.getPrice();
            label_starsCounter.Content = stars;
            updateStarsPerSecond();
        }

        private void updateStarsPerSecond()
        {
            starsPerSecond = magicWands.getStarsPerSecond();
            starsPerSecond += fairies.getStarsPerSecond();
            starsPerSecond += farms.getStarsPerSecond();
            label_starsPerSecondCounter.Content = starsPerSecond;
        }


        public string lectureFichierSvg(string fichier)
        {
            try
            {
                // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                //StreamReader monStreamReader = new StreamReader(Server.MapPath(fichier));
                StreamReader monStreamReader = new StreamReader(fichier);
                string ligne = monStreamReader.ReadLine();

                // Lecture de toutes les lignes et affichage de chacune sur la page 

                // Fermeture du StreamReader (attention très important) 
                monStreamReader.Close();

                return ligne;
            }
            catch (Exception ex)
            {
                // Code exécuté en cas d'exception 
                Console.WriteLine("Une erreur est survenue au cours de la lecture !");
                Console.WriteLine("</br>");
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public void lectureFichier(string fichier)
        {
            try
            {
                // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                //StreamReader monStreamReader = new StreamReader(Server.MapPath(fichier));
                StreamReader monStreamReader = new StreamReader(fichier);
                string ligne = monStreamReader.ReadLine();

                // Lecture de toutes les lignes et affichage de chacune sur la page 
                while (ligne != null)
                {
                    Console.WriteLine(ligne);
                    Console.WriteLine("</br>");
                    ligne = monStreamReader.ReadLine();
                }
                // Fermeture du StreamReader (attention très important) 
                monStreamReader.Close();
            }
            catch (Exception ex)
            {
                // Code exécuté en cas d'exception 
                Console.WriteLine("Une erreur est survenue au cours de la lecture !");
                Console.WriteLine("</br>");
                Console.WriteLine(ex.Message);
            }
        }

        public void ecritureFichier(string fichier, String chaine)
        {
            try
            {
                // Instanciation du StreamWriter avec passage du nom du fichier 
                //StreamWriter monStreamWriter = new StreamWriter(Server.MapPath("./") + @"admin\logs\" + fichier);
                StreamWriter monStreamWriter = new StreamWriter(fichier);
                // ou bien 
                // StreamWriter monStreamWriter = new StreamWriter(Server.MapPath("./") + "admin\\logs\\" + fichier); 

                //Ecriture du texte dans votre fichier 
                monStreamWriter.WriteLine(chaine);

                // Fermeture du StreamWriter (Très important) 
                monStreamWriter.Close();
            }
            catch (Exception ex)
            {
                // Code exécuté en cas d'exception 
                Console.WriteLine(ex.Message);
            }
        }

        public void ecritureFichierV2(string fichier)
        {
            try
            {
                // Instanciation du StreamWriter avec passage du nom du fichier 
                StreamWriter monStreamWriter = new StreamWriter(fichier);
                // ou bien 
                // StreamWriter monStreamWriter = new StreamWriter(Server.MapPath("./") + "admin\\logs\\" + fichier); 

                //Ecriture du texte dans votre fichier 
                monStreamWriter.WriteLine("Ma toute première ligne ...");
                monStreamWriter.WriteLine("Ma seconde ligne ...");
                monStreamWriter.WriteLine("Ma troisième ligne ...");

                // Fermeture du StreamWriter (Très important) 
                monStreamWriter.Close();
            }
            catch (Exception ex)
            {
                // Code exécuté en cas d'exception
                Console.WriteLine(ex.Message);
            }
        }

        private void saveAndQuit(object sender, RoutedEventArgs e)
        {
            ecritureFichier("./savedGame/stars.txt", Convert.ToString(stars));
            ecritureFichier("./savedGame/fairies.txt", Convert.ToString(fairies.getNumber()));
            ecritureFichier("./savedGame/fairiesBonus.txt", Convert.ToString(fairies.getMultiplier()));
            this.Close();
        }
    }
}
