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

## Prerequisites 
dotnet core sdk

## Run the app
In visual studio press F5 and it should run in debug mode.

##TODO:
- Unit tests.
- Extend the ClientFactory when there are more requirements.