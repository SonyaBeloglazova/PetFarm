using System.Windows;
using System.Windows.Controls;
using PetFarm.Data;
using PetFarm.Models;

namespace PetFarm.Pages
{
    public partial class MyPetsPage : Page
    {
        public MyPetsPage()
        {
            InitializeComponent();
            LoadPets();
        }

        private void LoadPets()
        {
            PetsItemsControl.ItemsSource = PetManager.Pets;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.ShowMainMenu();
            }
        }

        private void PetButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Pet pet)
            {
                NavigationService.Navigate(new EditPetPage(pet));
            }
        }

        private void MedicalDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Pet pet)
            {
                NavigationService.Navigate(new MedicalDataPage(pet));
            }
        }

        private void DeletePetButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Pet pet)
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить питомца {pet.Name} ({pet.Type})?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PetManager.RemovePet(pet); // Автоматически вызовет SavePets()
                }
            }
        }
    }
}