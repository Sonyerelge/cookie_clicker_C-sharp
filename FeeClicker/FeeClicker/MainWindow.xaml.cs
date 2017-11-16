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
            game.Owner = this;
            game.ShowDialog();
        }

        private void startNewGame(object sender, RoutedEventArgs e)
        {
            Game game = new Game(true);
            game.Owner = this;
            game.ShowDialog();
        }
    }
}
