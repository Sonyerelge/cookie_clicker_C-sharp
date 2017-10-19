using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FeeClicker
{
    /// <summary>
    /// Logique d'interaction pour Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        Money money = new Money();
        Character magicWand = new Character("Baguette magique", 0, 0.2, 1);
        Character fairy = new Character("Fée", 0, 1, 1);
        private double stars = 0;
        private double starsPerSecond = 0;

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
                    stars = Convert.ToDouble(svgStars);

                String svgFairies = lectureFichierSvg("./savedGame/fairies.txt");
                if (svgFairies != null)
                    fairy.setNumber(Convert.ToInt32(svgFairies));

                //String svgFairiesBonus = lectureFichierSvg("./savedGame/fairiesBonus.txt");
                //if (svgFairiesBonus != null)
                //    fairiesBonus = Convert.ToInt32(svgFairiesBonus);

                starsPerSecond = fairy.getStarsPerSecond();
            }

            compteur_etoiles.Content = stars;
            etoiles_seconde.Content = starsPerSecond;
            compteur_fees.Content = fairy.getNumber();
            compteur_fees_bonus.Content = fairy.getMultiplier();

            DispatcherTimer timer1MiliSecond = new DispatcherTimer();
            timer1MiliSecond.Interval = TimeSpan.FromSeconds(1 / 1000);
            timer1MiliSecond.Tick += addStarSecond;
            timer1MiliSecond.Start();

            DispatcherTimer timer1second = new DispatcherTimer();
            timer1second.Interval = TimeSpan.FromSeconds(1);
            timer1second.Tick += checkWhatWeCanBuy;
            timer1second.Start();

            money.setMoney(new int[] { 0, 234, 654, 32, 10, 654 });
            etoiles_seconde.Content = money.writeMoney();

            magicWand.setMoney(new int[] { 0, 0, 0, 0, 0, 10 });
            fairy.setMoney(new int[] { 0, 0, 0, 0, 0, 100 });
        }

        void addStarSecond(object sender, EventArgs e)
        {
            stars += starsPerSecond / 1000;
            compteur_etoiles.Content = stars;
        }

        void checkWhatWeCanBuy(object sender, EventArgs e)
        {

        }

        private void addStar(object sender, RoutedEventArgs e)
        {
            stars++;
            compteur_etoiles.Content = stars;
        }

        private void addFairy(object sender, RoutedEventArgs e)
        {
            fairy.addOne();
            compteur_fees.Content = fairy.getNumber();
            updateStarPerSecond();

        }

        private void addFairiesBonus(object sender, RoutedEventArgs e)
        {
            //fairiesBonus *= 2;
            //compteur_fees_bonus.Content = fairiesBonus;
            //updateStarPerSecond();
        }

        private void updateStarPerSecond()
        {
            starsPerSecond = fairy.getStarsPerSecond();
            etoiles_seconde.Content = starsPerSecond;
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
                    compteur_fees_bonus.Content = ligne;
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
                compteur_fees_bonus.Content = "Ca marche pas !!";
                Console.WriteLine(ex.Message);
            }
        }

        private void saveAndQuit(object sender, RoutedEventArgs e)
        {
            ecritureFichier("./savedGame/stars.txt", Convert.ToString(stars));
            ecritureFichier("./savedGame/fairies.txt", Convert.ToString(fairy.getNumber()));
            ecritureFichier("./savedGame/fairiesBonus.txt", Convert.ToString(fairy.getMultiplier()));

            Application.Current.Shutdown();
        }
    }
}
