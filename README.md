# BTC markets - Transaction search solution

## Design
##Context Map:
![Class Diagram](contextMap.svg)
##Class Diagram:
![Class Diagram](infuraCD.svg)
--------------------
- Implementation of TransactionMethods as injectable services into mvc controllers.
- TransactionMethods use input as string from InfuraMethods which directly query Infura RPC endpoint.
- TransactionMethods and InfuraMethods are to be implemented with Generic types to help guide new implementers.
- To create a new method follow the following steps:

1. Create InfuraMethod: This method will help calling the Infura rpc and return data as a json string.
Make sure this InfuraMethod implements the abstract class InfuraMethod<inT>. All it needs to do is overriding the GetInfuraRequestContentV2
e.g:
```
   public class GetBlockByNumber : InfuraMethod<(BlockNumber blockNumber, bool getTransactionDetails)>
    {
        public override InfuraRequestContentV2 GetInfuraRequestContentV2((BlockNumber blockNumber, bool getTransactionDetails) input)
            => new InfuraRequestContentV2(InfuraJsonRpcMethodNames.GetBlockByNumber,
                new object[] { $"0x{input.blockNumber.ToHex()}", input.getTransactionDetails });
    }
```
2. Add the newly created method in to startup.cs so it'd become injectable, as per following:
services.AddInfuraMethods(new List<Type> { typeof(GetBlockNumber) });
The above list in the future can be auto-populated by reflection.

3. Next, create concreate method class implementing TransactionMethod. It will force you to implement inT, outT and Execute methods.
e.g:
```
    public class GetListOfTransactionDetailsFromAddressInBlockMethod : TransactionMethod<(Address address, BlockNumber blockNumber), IEnumerable<TransactionDetails>>
    ...
```
4. Add the newly created method in to startup.cs so it'd be injectable, as per following:
```
services.AddTransactionMethods(new List<Type> { typeof(GetListOfTransactionDetailsFromAddressInBlockMethod) });
```
The above list in the future can be auto-populated by reflection.

5. As consumer all you need to do is injecting the transaction method needed into your controller and make use of the newly created method.
e.g:
```
       private readonly GetListOfTransactionDetailsFromAddressInBlockMethod _getListOfTransactionDetailsFromAddressInBlockMethod;

        public TransactionController(GetListOfTransactionDetailsFromAddressInBlockMethod getListOfTransactionDetailsFromAddressInBlockMethod)
        {
            _getListOfTransactionDetailsFromAddressInBlockMethod = getListOfTransactionDetailsFromAddressInBlockMethod;
        }
```
6. Testings: Specs tests are mandatory for each method class created. Tests are around the Execute method in the class. Make sure you cover all scenarios of the method specs.
![Specs Test](SpecsTest.png)
e.g: UnexisingAddressShouldReturnNoResult, UnexisingBlockShouldReturnNoResult, ResultsShouldMatchWithExpectedJson, ...
If there are unit tests worth testing then add them in UnitTests.cs class.
There are also tests for ValueObjects and Tests for controllers to cover tests on all layers of the design.

Notes:
- validations on input and output data type is forced at compile time.
- validations of input, output business rules will be enforced by ValueObjects and ReferenceObjects.
- ValueObjects are values in nature, and are immutable.



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
- Integration tests: (Haven't seen one).
- E2E tests: Tests on react app.

##QUESTIONS:
- Are there going to be write operations?