# BTC markets - Transaction search solution

## Design
![Class Diagram](infuraCD.svg)
--------------------
- Implementation of InfuraMethodCollection as an injectable service into webapp.
- Infura Methods are to be implemented with Generic types to help guide new implementers.
- To create a new method follow the following steps:

1. Create concreate method class implementing InfuraMethod. It will force you to implement inT, outT and Execute method.
e.g:
    /// <summary>
    /// Concrete method
    /// </summary>
    public class GetListOfTransactionDetailsFromAddressInBlockMethod : InfuraMethod<(Address address, BlockNumber blockNumber), IEnumerable<TransactionDetails>>
    ...

2. Add the above method class into the IInfuraMethodCollection and implement it in InfuraMethodCollection.
e.g:
- in IInfuraMethodCollection:
        GetListOfTransactionDetailsFromAddressInBlockMethod GetListOfTransactionDetailsFromAddressInBlockMethodInstance();

- in InfuraMethodCollection:
        public GetListOfTransactionDetailsFromAddressInBlockMethod GetListOfTransactionDetailsFromAddressInBlockMethodInstance() => new GetListOfTransactionDetailsFromAddressInBlockMethod();

3. As consumer all you need to do is injecting IInfuraMethodCollection into your controller and make use of the newly created method.
e.g:
            GetListOfTransactionDetailsFromAddressInBlockMethod methodInstance = _infuraMethodCollection.GetListOfTransactionDetailsFromAddressInBlockMethodInstance();

4. Testings: Specs tests are mandatory for each method class created. Tests are around the Execute method in the class. Make sure you cover all scenarios of the method specs.
e.g: UnexisingAddressShouldReturnNoResult, UnexisingBlockShouldReturnNoResult, ResultsShouldMatchWithExpectedJson, ...

Notes:
- validations on input and output data type is forced at compile time.
- validations of input, output business rules will be enforced by ValueObjects and EntityObjects.



Language: dotnet core
Testing framework: xUnit
This solution focuses on backend so the front-end was left bare minimum react app.
Backend comprises of:
- TransactionController impolementing SearchAsync method to help searching for transactions of a specific address and block number.
- InfuraHttpClientInstanceFactory helps creating InfuraClientInstances on demand with utility methods in it to query the Infura api, at the moment there are: GetBlockByNumber, GetTransactionDetailsJArrayOfBlockNumber and GetTransactionDetailsJArrayOfBlockNumber for the time being with potential to extend to other api methods.
- Minimal Integration tests to query the api and make sure the client gets back what it needs. It would be more reliable to be able  to mock thesse api call and turn some into unit tests when there's time to do so.'
- Exceptions are not handled to allow raw errors to bubble up when debugging.

## Prerequisites 
dotnet core sdk
nodejs

## Run the app
To build front end project run `npm i` in ClientApp folder to install all npm packages.
In visual studio press F5 and it should run in debug mode.

##TODO:
- Logging.