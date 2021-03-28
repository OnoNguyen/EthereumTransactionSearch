import React, { Component } from "react";

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = {
      transactions: [],
      loading: false,
      address: "0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa",
      blockNumber: "9148873",
    };
  }

  static renderTransactionSearchResultTable(transactions) {
    console.log(transactions);
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>block hash</th>
            <th>block number</th>
            <th>gas</th>
            <th>from</th>
            <th>to</th>
            <th>value</th>
          </tr>
        </thead>
        <tbody>
          {transactions.map((tran) => (
            <tr key={tran.hash}>
              <td>{tran.blockHash}</td>
              <td>{tran.blockNumberInHex}</td>
              <td>{tran.gas}</td>
              <td>{tran.from}</td>
              <td>{tran.to}</td>
              <td>{tran.value}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      FetchData.renderTransactionSearchResultTable(this.state.transactions)
    );

    return (
      <div>
        <h1>Transaction search</h1>
        <input
          type="text"
          value={this.state.blockNumber}
          onChange={(e) => this.updateBlockNumber(e.target.value)}
          class="form-control"
          placeholder="block number"
          type="number"
        />
        <input
          type="text"
          value={this.state.address}
          onChange={(e) => this.updateAdddress(e.target.value)}
          class="form-control"
          placeholder="address"
        />
        <button
          onClick={this.searchAddressOnBlock}
          ck
          type="button"
          className="btn btn-primary"
        >
          Search
        </button>
        {contents}
      </div>
    );
  }

  searchAddressOnBlock = async () => {
    if (!this.state.address || !this.state.blockNumber)
    {
      alert('Address and BlockNumber are mandatory');
      return;
    }

    this.setState({ loading: true });
    const response = await fetch(
      `transaction/search?address=${this.state.address}&blockNumber=${this.state.blockNumber}`
    );
    const data = await response.json();
    this.setState({ transactions: data, loading: false });
  };

  updateBlockNumber = (val) => {
    this.setState({ blockNumber: val });
  };

  updateAdddress = (val) => {
    this.setState({ address: val });
  };
}
