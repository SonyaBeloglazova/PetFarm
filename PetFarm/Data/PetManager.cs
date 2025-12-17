using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using PetFarm.Models;

namespace PetFarm.Data
{
    public static class PetManager
    {
        private static ObservableCollection<Pet> _pets;
        private static readonly string SaveFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PetFarm",
            "pets.txt");

        public static ObservableCollection<Pet> Pets
        {
            get
            {
                if (_pets == null)
                {
                    _pets = new ObservableCollection<Pet>();
                    LoadPets();
                }
                return _pets;
            }
        }

        public static void AddPet(Pet pet)
        {
            Pets.Add(pet);
            SavePets();
        }

        public static void RemovePet(Pet pet)
        {
            Pets.Remove(pet);
            SavePets();
        }

        public static void UpdatePet(Pet oldPet, Pet newPet)
        {
            int index = Pets.IndexOf(oldPet);
            if (index != -1)
            {
                Pets[index] = newPet;
                SavePets();
            }
        }

        public static void SavePets()
        {
            try
            {
                // Создаем папку если её нет
                var directory = Path.GetDirectoryName(SaveFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Записываем в текстовый файл
                using (StreamWriter writer = new StreamWriter(SaveFilePath))
                {
                    foreach (var pet in _pets)
                    {
                        // Формат: PET|ID|Тип|Имя|Возраст|ДатаСоздания
                        writer.WriteLine($"PET|{pet.Id}|{pet.Type}|{pet.Name}|{pet.Age}|{pet.CreatedDate:yyyy-MM-dd HH:mm:ss}");

                        // Записываем все вакцинации
                        foreach (var vaccination in pet.Vaccinations)
                        {
                            writer.WriteLine($"VACCINATION|{vaccination}");
                        }

                        // Записываем все визиты к ветеринару
                        foreach (var visit in pet.VetVisits)
                        {
                            writer.WriteLine($"VISIT|{visit}");
                        }

                        // Разделитель между питомцами
                        writer.WriteLine("END");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private static void LoadPets()
        {
            try
            {
                if (!File.Exists(SaveFilePath))
                    return;

                // Читаем файл построчно
                string[] lines = File.ReadAllLines(SaveFilePath);
                Pet currentPet = null;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split('|');

                    if (parts[0] == "PET")
                    {
                        // Создаем нового питомца
                        currentPet = new Pet
                        {
                            Id = Guid.Parse(parts[1]),
                            Type = parts[2],
                            Name = parts[3],
                            Age = parts[4],
                            CreatedDate = DateTime.Parse(parts[5])
                        };
                        _pets.Add(currentPet);
                    }
                    else if (parts[0] == "VACCINATION" && currentPet != null)
                    {
                        // Добавляем вакцинацию
                        currentPet.Vaccinations.Add(parts[1]);
                    }
                    else if (parts[0] == "VISIT" && currentPet != null)
                    {
                        // Добавляем визит к ветеринару
                        currentPet.VetVisits.Add(parts[1]);
                    }
                    else if (parts[0] == "END")
                    {
                        currentPet = null;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }
}