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
            Game game = new Game(true);
            this.Hide();
            game.Owner = this;
            game.ShowDialog();
            this.Show();
        }

        private void showOrHideCredit(object sender, RoutedEventArgs e)
        {
            if (button_credits.Content.ToString() == "Crédits")
            {
                button_credits.Content = "Retour au menu";
                label_credits.Visibility = System.Windows.Visibility.Visible;
                button_startSavedGame.Visibility = System.Windows.Visibility.Hidden;
                button_startNewGame.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                button_credits.Content = "Crédits";
                label_credits.Visibility = System.Windows.Visibility.Hidden;
                button_startSavedGame.Visibility = System.Windows.Visibility.Visible;
                button_startNewGame.Visibility = System.Windows.Visibility.Visible;
            }

        }
    }
}
