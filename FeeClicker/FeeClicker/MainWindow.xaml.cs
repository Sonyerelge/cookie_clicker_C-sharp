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

            //ImageBrush ima = new ImageBrush();
            //ima.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/image.png", UriKind.Absolute));
            //this.Background = ima;
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
