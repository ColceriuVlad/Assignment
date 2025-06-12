using System;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using PetStore.Models.Pet;
using PetStore.Services;
using Reqnroll;
using RestSharp;

namespace PetStore.StepDefinitions
{
    [Binding]
    public sealed class PetStoreStepDefinitions
    {
        private readonly IPetStoreService _petStoreService = new PetStoreService();
        private Pet _testPet;
        private RestResponse _createPetResponse;
        private RestResponse _getPetResponse;
        private RestResponse _updatePetResponse;
        private RestResponse _deletePetResponse;

        [Given("I have a pet payload")]
        public void GivenIHaveAPetPayload()
        {
            _testPet = new Pet
            {
                Id = new Random().Next(1000000),
                Name = "Negrut",
                Status = "available",
                PhotoUrls = ["http://example.com/photo.png"],
                Category = new Category { Id = 1, Name = "dog" }
            };
        }

        [When("I send a POST request to create the pet")]
        public void WhenISendPostRequestToCreateThePet()
        {
            _createPetResponse = _petStoreService.CreatePet(_testPet);
        }

        [Then("The response status for {string} should be {int}")]
        public void ThenTheResponseStatusForOperationShouldBe(string operation, int expectedStatusCode)
        {
            RestResponse responseToCheck = operation switch
            {
                "create" => _createPetResponse,
                "get" => _getPetResponse,
                "update" => _updatePetResponse,
                "delete" => _deletePetResponse,
                _ => throw new ArgumentException($"Unknown operation: {operation}")
            };

            Assert.That((int)responseToCheck.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Then("the returned pet should match the sent data")]
        public void ThenReturnedPetShouldMatchSentData()
        {
            if (_createPetResponse.Content == null)
            {
                Assert.Fail("The create pet request didn't return anything");
            }

            var returnedPet = JsonConvert.DeserializeObject<Pet>(_createPetResponse.Content);
            Assert.That(_testPet.Id.Equals(returnedPet.Id));
            Assert.That(_testPet.Name.Equals(returnedPet.Name));
        }

        [Given("a pet has been created")]
        public void GivenAPetHasBeenCreated()
        {
            GivenIHaveAPetPayload();
            WhenISendPostRequestToCreateThePet();
            Assert.That((int)_createPetResponse.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        // Needed retry mechanism due to API Delay
        [When("I send a GET request for the pet by ID")]
        public void WhenISendAGetRequestForThePetById()
        {
            const int maxRetries = 5;
            int attempt = 0;

            do
            {
                _getPetResponse = _petStoreService.GetPet(_testPet.Id);
                if ((int)_getPetResponse.StatusCode == 200)
                    break;

                attempt++;
                System.Threading.Thread.Sleep(3000); // wait 3 seconds before retrying
            }
            while (attempt < maxRetries);
        }

        [Then("the returned pet should match the created pet")]
        public void ThenReturnedPetShouldMatchCreatedPet()
        {
            if (_getPetResponse.Content == null)
            {
                Assert.Fail("The get pet response didn't return anything");

            }

            var returnedPet = JsonConvert.DeserializeObject<Pet>(_getPetResponse.Content);
            Assert.That(_testPet.Id.Equals(returnedPet.Id));
            Assert.That(_testPet.Name.Equals(returnedPet.Name));
        }

        [When("I send a PUT request with updated pet data")]
        public void WhenISendAPutRequestWithUpdatedPetData()
        {
            _testPet.Name = "NegrutGotSold";
            _testPet.Status = "sold";
            _updatePetResponse = _petStoreService.UpdatePet(_testPet);
        }

        [Then("the updated pet should be returned")]
        public void ThenTheUpdatedPetShouldBeReturned()
        {
            if (_updatePetResponse.Content == null)
            {
                Assert.Fail("The update pet response didn't return anything");
            }

            var updatedPet = JsonConvert.DeserializeObject<Pet>(_updatePetResponse.Content);
            Assert.That(_testPet.Id.Equals(updatedPet.Id));
            Assert.That(_testPet.Name.Equals(updatedPet.Name));
            Assert.That(_testPet.Status.Equals(updatedPet.Status));
        }

        [When("I send a DELETE request for the pet")]
        public void WhenISendDeleteRequestForThePet()
        {
            const int maxRetries = 5;
            int attempt = 0;

            do
            {
                _deletePetResponse = _petStoreService.DeletePet(_testPet.Id);
                if ((int)_deletePetResponse.StatusCode == 200)
                    break;

                attempt++;
                System.Threading.Thread.Sleep(3000);
            }
            while (attempt < maxRetries);
        }

        // Validate Status code 200 for the delete request and 404 for the get request
        [Then("the pet should no longer exist")]
        public void ThenThePetShouldNoLongerExist()
        {
            var checkResponse = _petStoreService.GetPet(_testPet.Id);
            Assert.That((int)checkResponse.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }

        [When("I send a malformed POST request to create a pet")]
        public void WhenISendMalformedPostRequestToCreateAPet()
        {
            _createPetResponse = _petStoreService.CreateMalformedPet();
        }
    }

}
