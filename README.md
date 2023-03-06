# CS295N-Example-BookReviews-DotNet6

# CS295N-Example-BookReviews-DotNet6

Book Review example from LCC-CIT/CS295N-Example-BookReviews migrated to ASP.NET 6.0 MVC
The migration process is described in https://lcc-cit.github.io/CS295N-CourseMaterials/Notes/UpgradeMvcAppToDotNeT6.html

This example uses MySQL for the database provider. This guide shows how to set up a MySQL database server on
Azure: https://lcc-cit.github.io/CS295N-CourseMaterials/Notes/AzureMySqlSetupGuide.html

The first branch in this repository is 7-RepositoryAndUnitTests

## Branchs

- 7-RepositoryAndUnitTests  
  The repository pattern is implemented to facilitate unit testing of controller methods.  
  A ReviewRepository was added and the ReviewController was refactored to use it. A FakeReviewRepository was added and
  unit tests were written that use it.
- 8A-SeedData  
  Code to seed the database with some initial book reviews was added.
- 8B-LinqFiltering  
  Added code to filter reviews by book title or reviewer.
