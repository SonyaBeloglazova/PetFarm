using System.Windows;
using System.Windows.Controls;
using PetFarm.Data;
using PetFarm.Models;

namespace PetFarm.Pages
{
    public partial class EditPetPage : Page
    {
        private Pet _currentPet;

        public EditPetPage(Pet pet)
        {
            InitializeComponent();
            _currentPet = pet;
            LoadPetData();
        }

        private void LoadPetData()
        {
            TypeTextBox.Text = _currentPet.Type;
            NameTextBox.Text = _currentPet.Name;
            AgeTextBox.Text = _currentPet.Age;

            // Загружаем вакцинации
            VaccinationsPanel.Children.Clear();
            foreach (var vaccination in _currentPet.Vaccinations)
            {
                var textBlock = new TextBlock
                {
                    Text = vaccination,
                    FontFamily = new System.Windows.Media.FontFamily("Cascadia Mono"),
                    Margin = new Thickness(0, 2, 0, 2)
                };
                VaccinationsPanel.Children.Add(textBlock);
            }

            // Загружаем визиты к ветеринару
            VetVisitsPanel.Children.Clear();
            foreach (var visit in _currentPet.VetVisits)
            {
                var textBlock = new TextBlock
                {
                    Text = visit,
                    FontFamily = new System.Windows.Media.FontFamily("Cascadia Mono"),
                    Margin = new Thickness(0, 2, 0, 2)
                };
                VetVisitsPanel.Children.Add(textBlock);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем заполненность полей
            if (string.IsNullOrWhiteSpace(TypeTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(AgeTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _currentPet.Type = TypeTextBox.Text;
            _currentPet.Name = NameTextBox.Text;
            _currentPet.Age = AgeTextBox.Text;

            // Сохраняем изменения
            PetManager.SavePets();

            MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }
    }
}