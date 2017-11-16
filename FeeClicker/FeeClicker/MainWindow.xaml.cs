using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            //Money momo = new Money();
            //this.tatata.Content = momo.writeMoney();
            //momo.setMoney(new int[] { 0, 0, 0, 0, 132, 766});
            //this.tototo.Content = momo.writeMoney();
            //momo.setMoney(new int[] { 0, 234, 654, 32, 10, 654 });
            //this.tututu.Content = momo.writeMoney();
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
