using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PetFarm.Models
{
    public class Pet
    {
        private string _type;
        private string _name;
        private string _age;

        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public List<string> Vaccinations { get; set; }
        public List<string> VetVisits { get; set; }

        public Pet()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            Vaccinations = new List<string>();
            VetVisits = new List<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}