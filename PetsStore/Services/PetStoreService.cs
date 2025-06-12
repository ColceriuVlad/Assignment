using PetStore.Models.Pet;
using RestSharp;

namespace PetStore.Services
{
    internal class PetStoreService : IPetStoreService
    {
        private readonly RestClient _client = new("https://petstore.swagger.io/v2");

        public RestResponse CreatePet(Pet pet)
        {
            var request = new RestRequest("pet", Method.Post);
            request.AddJsonBody(pet);
            return _client.Execute(request);
        }

        public RestResponse GetPet(long petId)
        {
            var request = new RestRequest($"pet/{petId}", Method.Get);
            return _client.Execute(request);
        }

        public RestResponse UpdatePet(Pet pet)
        {
            var request = new RestRequest("pet", Method.Put);
            request.AddJsonBody(pet);
            return _client.Execute(request);
        }

        public RestResponse DeletePet(long petId)
        {
            var request = new RestRequest($"pet/{petId}", Method.Delete);
            return _client.Execute(request);
        }

        public RestResponse CreateMalformedPet()
        {
            var request = new RestRequest("pet", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{ \"id\": 1234, \"name\": \"Negrut\", \"status\": }", ParameterType.RequestBody); // Invalid JSON
            return _client.Execute(request);
        }
    }
}
