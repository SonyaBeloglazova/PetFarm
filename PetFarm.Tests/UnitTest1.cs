using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetFarm.Data;
using PetFarm.Models;

namespace PetFarm.Tests
{
    [TestClass]
    public class PetManagerTests
    {
        [TestMethod]
        public void PetManager_AddPet_IncreasesListCount()
        {
            var initialCount = PetManager.Pets.Count;
            var newPet = new Pet { Name = "Барсик", Type = "Кот" };
            PetManager.AddPet(newPet);
            Assert.AreEqual(initialCount + 1, PetManager.Pets.Count);
        }

        [TestMethod]
        public void PetManager_RemovePet_DecreasesListCount()
        {
            var pet = new Pet { Name = "Шарик", Type = "Собака" };
            PetManager.AddPet(pet);
            var countBefore = PetManager.Pets.Count;
            PetManager.RemovePet(pet);
            Assert.AreEqual(countBefore - 1, PetManager.Pets.Count);
        }
    }
}
