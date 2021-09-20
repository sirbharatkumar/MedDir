# MedDir
An API to determine number of patient groups for a given set of people.

Development Enviornment used:
VS2019
.Net Core 3.1
Postman 8.12.2

Requirement: To determine patient groups for a given people matrix

Approach:
1) To Iterate each index element and prepare the list of its surronding and consolidate them at the very end.
2) To Iterate each index, and if a patient is found than get all its surronding patients and iterate on them and add it to the main list before going to the next index. While Iterating through main matrix check if the index is already present in the master list before proceeding
Following approach will get all the patients from a single source, before going to next individual.

API Swagger details:
https://meddir.azurewebsites.net/index.html

API Postman file for testing
MedDir Postman.postman_collection.json