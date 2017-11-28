using System;
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
            listCharacters.Add(new Character("Ferme de fée", 0, 20, 1, 500));

            if (newGame)
            {
                // On vide le fichier de sauvegarde
                fillSavedVariablesFile();
            }
            else
            {
                // On récupère les valeurs du fichier de sauvegarde
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

            label_starsCounter.Content = stars;
            label_starsPerSecondCounter.Content = starsPerSecond;

            int numero = 1;
            Label lbl;
            foreach(Character character in listCharacters)
            {
                lbl = FindName("label_character_" + numero) as Label;
                lbl.Content = character.getName() + " :";

                lbl = FindName("label_characterNumber_" + numero) as Label;
                lbl.Content = character.getNumber();

                lbl = FindName("label_characterPrice_" + numero) as Label;
                lbl.Content = character.getPrice() + " poussières";
                numero++;
            }

            DispatcherTimer timer1second = new DispatcherTimer();
            timer1second.Interval = TimeSpan.FromSeconds(1);
            timer1second.Tick += addStarsPerSecond;
            timer1second.Tick += checkWhatWeCanBuy;
            timer1second.Start();
        }

        private void addStar(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            stars++;
            label_starsCounter.Content = stars;
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
            lbl.Content = listCharacters[index].getPrice() + " poussières";
            
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

        private void fillSavedVariablesFile()
        {
            String[] tab = new String[4];
            tab[0] = stars.ToString();
            int i = 1;
            foreach (Character character in listCharacters)
            {
                tab[i] = character.getNumber().ToString() + "_" + character.getPrice().ToString();
                i++;
            }
            File.WriteAllLines("savedVariables.txt", tab);
        }

        private void saveAndQuit(object sender, RoutedEventArgs e)
        {
            // On remplit le fichier de sauvegarde
            fillSavedVariablesFile();
            this.Close();
        }


    }
}
