using System.Windows;
using System.Windows.Controls;
using PetFarm.Pages;

namespace PetFarm
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyPetButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MyPetsPage());
            ShowFrame();
        }

        private void AddPetButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddPetPage());
            ShowFrame();
        }

        private void ShowFrame()
        {
            MainMenu.Visibility = Visibility.Collapsed;
            MainFrame.Visibility = Visibility.Visible;
        }

        public void ShowMainMenu()
        {
            MainMenu.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Collapsed;
        }
    }
}