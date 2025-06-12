@petStoreApiSanity
Feature: PetStore

  Scenario: Create a pet
    Given I have a pet payload
    When I send a POST request to create the pet
    Then The response status for "create" should be 200
    And the returned pet should match the sent data

  # Validate that the Get request returns the created pet, as a preqrequsite this test creates a pet
  # The API Sometimes has time delays so I implemented a Retry Mechanism for GetPet Method
  Scenario: Get the pet
    Given a pet has been created
    When I send a GET request for the pet by ID
    Then The response status for "get" should be 200
    And the returned pet should match the created pet

  Scenario: Update the pet
    Given a pet has been created
    When I send a PUT request with updated pet data
    Then The response status for "update" should be 200
    And the updated pet should be returned

  # Validate Status code 404 for the get request at the end of the test
  # Also has time delays so also uses a Retry Mechanism for the Delete Method
  Scenario: Delete the pet
    Given a pet has been created
    When I send a DELETE request for the pet
    Then The response status for "delete" should be 200
    And the pet should no longer exist

  # Validate bad request 400 Scenario
  # Previously tried to use a missing "Name" field or a "Status" that doesn't exist
  # In the end I couldn't use a model to make that work so I had to create a method that allows me to use a malformed jason request
  Scenario: Creating a malformed pet should return 400
    When I send a malformed POST request to create a pet
    Then The response status for "create" should be 400