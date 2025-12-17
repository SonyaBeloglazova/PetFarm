using System.Windows;
using System.Windows.Controls;
using PetFarm.Data;
using PetFarm.Models;

namespace PetFarm.Pages
{
    public partial class MedicalDataPage : Page
    {
        private Pet _currentPet;

        public MedicalDataPage(Pet pet)
        {
            InitializeComponent();
            _currentPet = pet;
            LoadExistingData();
        }

        private void LoadExistingData()
        {
            // Загружаем существующие данные в ItemsControl
            VaccinationsItemsControl.ItemsSource = _currentPet.Vaccinations;
            VetVisitsItemsControl.ItemsSource = _currentPet.VetVisits;
        }

        private void AddVaccination_Click(object sender, RoutedEventArgs e)
        {
            string vaccination = VaccinationTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(vaccination))
            {
                _currentPet.Vaccinations.Add(vaccination);
                VaccinationTextBox.Clear();
                // Обновляем ItemsSource
                VaccinationsItemsControl.ItemsSource = null;
                VaccinationsItemsControl.ItemsSource = _currentPet.Vaccinations;
            }
            else
            {
                MessageBox.Show("Введите название вакцины", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddVetVisit_Click(object sender, RoutedEventArgs e)
        {
            string visit = VetVisitTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(visit))
            {
                _currentPet.VetVisits.Add(visit);
                VetVisitTextBox.Clear();
                // Обновляем ItemsSource
                VetVisitsItemsControl.ItemsSource = null;
                VetVisitsItemsControl.ItemsSource = _currentPet.VetVisits;
            }
            else
            {
                MessageBox.Show("Введите информацию о визите", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteVaccination_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string vaccination)
            {
                var result = MessageBox.Show(
                    $"Удалить вакцинацию: {vaccination}?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _currentPet.Vaccinations.Remove(vaccination);
                    // Обновляем ItemsSource
                    VaccinationsItemsControl.ItemsSource = null;
                    VaccinationsItemsControl.ItemsSource = _currentPet.Vaccinations;
                }
            }
        }

        private void DeleteVetVisit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string visit)
            {
                var result = MessageBox.Show(
                    $"Удалить визит: {visit}?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _currentPet.VetVisits.Remove(visit);
                    // Обновляем ItemsSource
                    VetVisitsItemsControl.ItemsSource = null;
                    VetVisitsItemsControl.ItemsSource = _currentPet.VetVisits;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем изменения
            PetManager.SavePets();

            MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}