using System.Windows;
using System.Windows.Controls;
using PetFarm.Data;
using PetFarm.Models;

namespace PetFarm.Pages
{
    public partial class AddPetPage : Page
    {
        public AddPetPage()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.ShowMainMenu();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string petName = PetNameTextBox.Text.Trim();
            string petType = PetTypeTextBox.Text.Trim();
            string petAge = PetAgeTextBox.Text.Trim();

            if (string.IsNullOrEmpty(petName) || string.IsNullOrEmpty(petType) || string.IsNullOrEmpty(petAge))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newPet = new Pet
            {
                
                Name = petName,
                Type = petType,
                Age = petAge
            };

            PetManager.AddPet(newPet);

            MessageBox.Show("Питомец успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.ShowMainMenu();
            }
        }
    }
}