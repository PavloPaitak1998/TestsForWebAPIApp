# Tests For Web API App
A simple ASP .NET Core Web API App which presents work of airline dispatchers using EF Core with Tests

## Based on the created project (ASPNETCoreWebAPIDispatcherUsingEFCore) it is necessary to cover the code with tests:

* All requests for creation and updating of entities must be covered by unit-tests that verify validation and mapping. Optional, you can cover some complex read requests with tests. Unit tests can only be applied to services, custom validators and mappers (repositories, uof and other classes from DAL, PL, must be mocked)

* Write 10 integration tests when working with the database. (Mock is not needed)

* Write 5-7 tests that check the operation of controllers: check input parameters, validate, return status codes.

* Write 5-7 functional tests that will call the API and check the result (that is, simulate real queries from users).

### To write all tests, use NUnit or XUnit.

#### Have fun)

 _"Motivation is good, but not the answer to keep you going in the long run. Become passionate about what you do."_

– Yad Faeq 
