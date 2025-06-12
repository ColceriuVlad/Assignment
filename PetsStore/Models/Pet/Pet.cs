using System.Collections.Generic;

namespace PetStore.Models.Pet
{
    internal class Pet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public Category Category { get; set; }
        public List<string> PhotoUrls { get; set; }

    }
}
