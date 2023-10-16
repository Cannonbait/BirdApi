# BirdApi

Correct Kolli Id within package limits: 999000000000000000
Correct Kolli Id outside package limits: 999000000000000001

# Running

To start the api, launch the BirdApi project using either Visual Studio or similar. Or navigate to the folder and run `dotnet run` in the terminal.

# Tests

To run the tests, write `dotnet test` in the root folder.

# Structure

## Controllers

For recieving the API call and looping in the model.

## DTO

Contains classes for sending/receiving HTTP requests.

## Model

For handling business/domain logic. Also contains the classes that are "stored" in the database.

## Repository

For handling the connection to the database.

# Assumptions

Perhaps the biggest assumption made was that the API generates KolliIds. This means that currently the KolliId that can be sent as a parameter to the Post request is ignored. Changing it so that KolliId is a mandatory parameter would be relatively trivial.

It is assumed that the packages have a lower limit, which for now is set to 1 gram and 1 cm (in each dimension).

# Improvements

- The project is not adapted to docker
- More granular error messages (which dimension is incorrect)
- Package is not created if outside dimensions
