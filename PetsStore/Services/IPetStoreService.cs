using PetStore.Models.Pet;
using RestSharp;

namespace PetStore.Services
{
    internal interface IPetStoreService
    {
        RestResponse CreatePet(Pet pet);
        RestResponse GetPet(long petId);
        RestResponse UpdatePet(Pet pet);
        RestResponse DeletePet(long petId);
        RestResponse CreateMalformedPet();
    }
}
