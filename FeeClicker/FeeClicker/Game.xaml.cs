using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private int NB_PERSONNAGES = 6;
        private bool savedFlag = false;
        private DispatcherTimer timer;

        public Game(Boolean newGame)
        {
            InitializeComponent();
            Initialization(newGame);
        }

        private void Initialization(Boolean newGame)
        {
            listCharacters.Add(new Character("Baguettes magiques", 0, 1, 20));
            listCharacters.Add(new Character("Fées", 0, 5, 100));
            listCharacters.Add(new Character("Fermes de fées", 0, 20, 500));
            listCharacters.Add(new Character("Féeseuses", 0, 100, 5000));
            listCharacters.Add(new Character("Usines à fées", 0, 500, 20000));
            listCharacters.Add(new Character("Reines fées", 0, 1000, 100000));

            if (newGame)
            {
                // On vide le fichier de sauvegarde
                fillSavedVariablesFile();
            }
            else
            {
                // On regarde si le fichier de sauvegarde existe
                if (!File.Exists("savedVariables.txt"))
                    fillSavedVariablesFile(); // On vide le fichier de sauvegarde

                String[] tabSavedVariables = File.ReadAllLines("savedVariables.txt");

                if (tabSavedVariables[0] != null)
                    stars = Convert.ToUInt64(tabSavedVariables[0]);

                int i = 1;
                foreach (Character character in listCharacters)
                {
                    if (tabSavedVariables[i] != null)
                    {
                        character.setNumber(Convert.ToUInt32(tabSavedVariables[i].Split('_')[0]));
                        character.setPrice(Convert.ToUInt32(tabSavedVariables[i].Split('_')[1]));
                    }
                    starsPerSecond += character.getStarsPerSecond();
                    i++;
                }
            }

            label_starsCounter.Content = getPriceLabel(stars);
            label_starsPerSecondCounter.Content = getPriceLabel(starsPerSecond);

            int numero = 1;
            Label lbl;
            foreach (Character character in listCharacters)
            {
                lbl = FindName("label_character_" + numero) as Label;
                lbl.Content = character.getName() + " :";

                lbl = FindName("label_characterNumber_" + numero) as Label;
                lbl.Content = getPriceLabel(character.getNumber());

                lbl = FindName("label_characterPrice_" + numero) as Label;
                lbl.Content = getPriceLabel(character.getPrice()) + " pe";
                numero++;
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += addStarsPerSecond;
            timer.Tick += checkWhatWeCanBuy;
            timer.Start();
        }

        private void addStar(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            stars++;
            label_starsCounter.Content = getPriceLabel(stars);
        }

        private void addStarsPerSecond(object sender, EventArgs e)
        {
            stars += starsPerSecond;
            label_starsCounter.Content = getPriceLabel(stars);
        }

        private void checkWhatWeCanBuy(object sender, EventArgs e)
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

        private void addCharacter(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            // On récupère le numéro du personnage (button_addCharacter_1  on prend le 1)
            String name = btn.GetValue(NameProperty) as String; 
            int numero = int.Parse(name.Split('_')[2]);
            int index = numero - 1;

            stars -= listCharacters[index].getPrice();
            listCharacters[index].addOne();
            
            if (stars >= listCharacters[index].getPrice())
            {
                btn.IsEnabled = true;
            }
            else
            {
                btn.IsEnabled = false;
            }
            Label lbl = FindName("label_characterNumber_" + numero) as Label;
            lbl.Content = listCharacters[index].getNumber();
            lbl = FindName("label_characterPrice_" + numero) as Label;
            lbl.Content = getPriceLabel(listCharacters[index].getPrice()) + " pe";
            
            label_starsCounter.Content = getPriceLabel(stars);
            updateStarsPerSecond();
        }

        private String getPriceLabel(UInt64 price)
        {
            if (price > 999)
            {
                UInt64 unites = price % 1000;
                if (price < 1000000)
                {
                    return (price - unites)/1000 + " " + unites.ToString("D3");
                }
                else
                {
                    UInt64 miliers = (price % 1000000 - unites)/1000;
                    return (price - miliers)/1000000 + "," + miliers.ToString("D3") + " millions";
                }
            }  
            else
                return "" + price;
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

        private void fillSavedVariablesFile()
        {
            String[] tab = new String[NB_PERSONNAGES+ 1];
            tab[0] = stars.ToString();
            int i = 1;
            foreach (Character character in listCharacters)
            {
                tab[i] = character.getNumber().ToString() + "_" + character.getPrice().ToString();
                i++;
            }
            File.WriteAllLines("savedVariables.txt", tab);
        }

        private void pause(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            MessageBox.Show("Appuyer sur le bouton pour reprendre le jeu !", "Jeu en pause");
            timer.Start();
        }

        private void saveAndQuit(object sender, RoutedEventArgs e)
        {
            // On remplit le fichier de sauvegarde
            fillSavedVariablesFile();
            this.savedFlag = true;
            this.Close();
        }

        private void windowClosing(object sender, CancelEventArgs e)
        {
            // On arrete le timer qui comtabilise les points
            timer.Stop();
            if (!this.savedFlag)
            {
                // Si l'utilisateur n'a pas cliquer sur le boutton "Sauvegarder & quitter", on lui demande s'il souhaite sauvegarder sa partie
                string msg = "Voulez-vous sauvegarder avant de quitter la partie ?";
                MessageBoxResult result = MessageBox.Show(msg, "Etes-vous sur de vouloir quitter ?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    fillSavedVariablesFile();
                }
            }
        }
    }
}
