using System;
using System.IO;
using System.Windows;

namespace FeeClicker
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Initialization();
        }

        private void Initialization()
        {
            // On regarde si le fichier de sauvegarde existe
            if (File.Exists("savedVariables.txt"))
                button_startSavedGame.IsEnabled = true;
        }

        private void startSavedGame(object sender, RoutedEventArgs e)
        {
            Game game = new Game(false);
            this.Hide();
            game.Owner = this;
            game.ShowDialog();
            this.Show();
        }

        private void startNewGame(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hello !!");
            Game game = new Game(true);
            this.Hide();
            game.Owner = this;
            game.ShowDialog();
            this.Show();
            Console.WriteLine("Coucou !!");
            Initialization();
        }

        private void showOrHideCredit(object sender, RoutedEventArgs e)
        {
            if (button_credits.Content.ToString() == "Crédits")
            {
                button_credits.Content = "Retour au menu";
                label_credits.Visibility = Visibility.Visible;
                button_startSavedGame.Visibility = Visibility.Hidden;
                button_startNewGame.Visibility = Visibility.Hidden;
            }
            else
            {
                button_credits.Content = "Crédits";
                label_credits.Visibility = Visibility.Hidden;
                button_startSavedGame.Visibility = Visibility.Visible;
                button_startNewGame.Visibility = Visibility.Visible;
            }
        }
    }
}
