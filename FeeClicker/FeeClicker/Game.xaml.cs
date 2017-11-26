using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
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
        private List<Character> listCharacters= new List<Character>();

        public Game(Boolean newGame)
        {
            InitializeComponent();

            listCharacters.Add(new Character("Baguette magique", 0, 1, 1, 20));
            listCharacters.Add(new Character("Fée", 0, 5, 1, 100));
            listCharacters.Add(new Character("Ferme", 0, 20, 1, 500));

            /*
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
            */

            label_starsCounter.Content = stars;
            label_starsPerSecondCounter.Content = starsPerSecond;

            int numero = 1;
            Button btn;
            Label lbl;
            foreach(Character character in listCharacters)
            {
                btn = FindName("button_addCharacter_" + numero) as Button;
                btn.Content = "Ajouter " + character.getName();

                lbl = FindName("label_character_" + numero) as Label;
                lbl.Content = character.getNumber();

                lbl = FindName("label_characterPrice_" + numero) as Label;
                lbl.Content = character.getPrice();
                numero++;
            }

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
            int numero = 1;
            Button btn;
            foreach (Character character in listCharacters)
            {
                btn = FindName("button_addCharacter_" + numero) as Button;
                if (stars >= character.getPrice())
                {
                    btn.IsEnabled = true;
                }
                else
                {
                    btn.IsEnabled = false;
                }
                numero++;
            }
        }

        private void addStar(object sender, RoutedEventArgs e)
        {
            stars++;
            label_starsCounter.Content = stars;
        }

        private void addCharacter(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            String name = button.GetValue(NameProperty) as String;
            int numero = int.Parse(name.Split('_')[2]);
            int index = numero - 1;

            stars -= listCharacters[index].getPrice();
            listCharacters[index].addOne();

            Button btn = sender as Button;
            btn = FindName("button_addCharacter_" + numero) as Button;
            if (stars >= listCharacters[index].getPrice())
            {
                btn.IsEnabled = true;
            }
            else
            {
                btn.IsEnabled = false;
            }
            Label lbl = FindName("label_character_" + numero) as Label;
            lbl.Content = listCharacters[index].getNumber();
            lbl = FindName("label_characterPrice_" + numero) as Label;
            lbl.Content = listCharacters[index].getPrice();
            
            label_starsCounter.Content = stars;
            updateStarsPerSecond();
        }

        private void updateStarsPerSecond()
        {
            starsPerSecond = 0;
            foreach (Character character in listCharacters)
            {
                starsPerSecond += character.getStarsPerSecond();
            }
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
            /*
            ecritureFichier("./savedGame/stars.txt", Convert.ToString(stars));
            ecritureFichier("./savedGame/fairies.txt", Convert.ToString(fairies.getNumber()));
            ecritureFichier("./savedGame/fairiesBonus.txt", Convert.ToString(fairies.getMultiplier()));
            */
            this.Close();
        }
    }
}
