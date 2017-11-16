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
        private ulong stars = 0;
        private ulong starsPerSecond = 0;
        private Character magicWand = new Character("Baguette magique", 0, 1, 1, 10);
        private Character fairy = new Character("Fée", 0, 10000, 1, 100);

        public Game(Boolean newGame)
        {
            InitializeComponent();
            if (newGame)
            {
                //Vider les fichiers textes
                writeFile("./savedGame/stars.txt", "0");
                writeFile("./savedGame/fairies.txt", "0");
            }
            else
            {
                //Récupérer les valeurs des fichiers textes
                String svgStars = readFile("./savedGame/stars.txt");
                String svgFairies = readFile("./savedGame/fairies.txt");
                if (svgStars != null)
                    stars = Convert.ToUInt64(svgStars);
                if (svgFairies != null)
                    fairy.setNumber(Convert.ToUInt32(svgFairies));

                starsPerSecond = fairy.getStarsPerSecond();
            }

            compteur_etoiles.Content = stars;
            etoiles_seconde.Content = starsPerSecond;
            compteur_fees.Content = fairy.getNumber();

            DispatcherTimer timer1second = new DispatcherTimer();
            timer1second.Interval = TimeSpan.FromSeconds(1);
            timer1second.Tick += checkWhatWeCanBuy;
            timer1second.Tick += addStarsSecond;
            timer1second.Start();
        }

        private void addStar(object sender, RoutedEventArgs e)
        {
            stars++;
            //compteur_etoiles.Content = stars;
            compteur_etoiles.Content = this.convertMoneyToString(stars);
        }

        void addStarsSecond(object sender, EventArgs e)
        {
            stars += starsPerSecond;
            //compteur_etoiles.Content = stars;
            compteur_etoiles.Content = stars + " - " + this.convertMoneyToString(stars);
        }

        private void buyFairy(object sender, RoutedEventArgs e)
        {
            fairy.addOne();
            compteur_fees.Content = fairy.getNumber();
            updateStarPerSecond();
        }

        void checkWhatWeCanBuy(object sender, EventArgs e)
        {

        }

        public string convertMoneyToString(ulong num)
        {
            if (num < 1000)
                return num.ToString();
            if (num >= 1000 && num < 1000000)
                return (Math.Round(Convert.ToDecimal(num), 1)).ToString();
                //return (Math.Round(Convert.ToDecimal(num) / 1000, 1)).ToString();
            if (num >= 1000000 && num < 1000000000)
                return (Math.Round(Convert.ToDecimal(num) / 1000, 1)).ToString() + " million";
            if (num >= 1000000000 && num < 1000000000000)
                return (Math.Round(Convert.ToDecimal(num) / 1000000, 1)).ToString() + " milliard";
            if (num >= 1000000000000 && num < 1000000000000000)
                return (Math.Round(Convert.ToDecimal(num) / 1000000000, 1)).ToString() + " billion";
            if (num >= 1000000000000000 && num < 1000000000000000000)
                return (Math.Round(Convert.ToDecimal(num) / 1000000000000, 1)).ToString() + " billiard";
            if (num >= 1000000000000000000)
                return (Math.Round(Convert.ToDecimal(num) / 1000000000000000, 1)).ToString() + " trillion";
            else
                return "NaN";
        }

        public string readFile(string fichier)
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

        private void saveAndQuit(object sender, RoutedEventArgs e)
        {
            writeFile("./savedGame/stars.txt", Convert.ToString(stars));
            writeFile("./savedGame/fairies.txt", Convert.ToString(fairy.getNumber()));

            Application.Current.Shutdown();
        }

        private void updateStarPerSecond()
        {
            starsPerSecond = fairy.getStarsPerSecond();
            etoiles_seconde.Content = starsPerSecond;
        }

        public void writeFile(string fichier, String chaine)
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
                Console.WriteLine("Une erreur est survenue au cours de l'écriture !");
                Console.WriteLine("</br>");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
