# BTC markets - Transaction search solution

## Design
Language: dotnet core
Testing framework: xUnit
This solution focuses on backend so the front-end was left bare minimum react app.
Backend comprises of:
- TransactionController impolementing SearchAsync method to help searching for transactions of a specific address and block number.
- InfuraHttpClientInstanceFactory helps creating InfuraClientInstances on demand with utility methods in it to query the Infura api, at the moment there are: GetBlockByNumber, GetTransactionDetailsJArrayOfBlockNumber and GetTransactionDetailsJArrayOfBlockNumber for the time being with potential to extend to other api methods.
- Minimal Integration tests to query the api and make sure the client gets back what it needs. It would be more reliable to be able  to mock thesse api call and turn some into unit tests when there's time to do so.'
- Exceptions are not handled to allow raw errors to bubble up when debugging.


--------------------
1/04/2021:
- competed implementation of Method factory pattern infused with Generic types to help guid method implementers. To create a new method follow the following steps:

1. Create concreate method class implementing InfuraMethod. It will force you to implement inT, outT, Input property and Execute method.
e.g:
    /// <summary>
    /// Concrete method
    /// </summary>
    public class GetListOfTransactionDetailsFromAddressInBlockMethod : InfuraMethod<(string address, int blockNumber), IEnumerable<TransactionDetails>>
    ...

2. Create concrete factory for the method class you have just done with above, implementing InfuraMethodFactory. This will force you to give inT, outT of the method above and overriding GetMethod to give consumers the final meaningful method to work on. 
e.g:
    /// <summary>
    /// Concrete factory/creator
    /// </summary>
    public class GetListOfTransactionDetailsFromAddressInBlockMethodFactory : InfuraMethodFactory<(string, int), IEnumerable<TransactionDetails>>

3. As consumer all you need to do is new up the method class created above, give it the input it wants and execute it (either sync or async).
e.g:
            var methodFactory = new GetListOfTransactionDetailsFromAddressInBlockMethodFactory();
            var methodInstance = methodFactory.GetMethod((address, blockNumber));
            var result = await methodInstance.ExecuteAsync();

4. Testings: Specs tests are mandatory for each method class created. Tests are around the execute method in the class. Make sure you cover all scenarios of the method specs.
e.g: UnexisingAddressShouldReturnNoResult, UnexisingBlockShouldReturnNoResult, ResultsShouldMatchWithExpectedJson ...

Notes:
- validations on input and output data type is forced at compile time.
- validations of input, output business rules will be enforced by ValueObjects and EntityObjects.


## Prerequisites 
dotnet core sdk
nodejs

## Run the app
To build front end project run `npm i` in ClientApp folder to install all npm packages.
In visual studio press F5 and it should run in debug mode.

##TODO:
- Unit tests.
- Extend the ClientFactory when there are more requirements.