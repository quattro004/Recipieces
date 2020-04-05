import React, { Component } from 'react';
const axios = require('axios').default;
const API_QUERY = 'albums';

export class AlbumList extends Component {
  constructor(props) {
    super(props);
    this.timeIncrementMs = 33;
    this.showSpinnerIfReturnGreaterThanMs = 100;
    this.state = {
      albums: [],
      isLoading: true,
      error: null,
      msElapsed: 0,
    };
  }

  componentWillMount() {
    this.incrementer = setInterval(() =>
      this.setState({
        msElapsed: this.state.msElapsed + this.timeIncrementMs
      }), this.timeIncrementMs);
  }
  componentWillUnmount() {
    clearInterval(this.incrementer);
  }

  componentDidMount() {
    this.getAlbums();
  }

  async getAlbums() {
    console.log('Getting albums');
    try {
      const response = await axios.get(API_QUERY);
      this.setState({ albums: response.data, isLoading: false });
    } catch (err) {
      console.error(err);
      this.setState({ err, isLoading: false });
    }
  }

  renderAlbumsTable(albums) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Created</th>
          </tr>
        </thead>
        <tbody>
          {albums.map(album =>
            <tr key={album.id}>
              <td>{album.name}</td>
              <td>{album.description}</td>
              <td>{new Date(album.createdOn).toDateString()}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    const { albums, isLoading, error, msElapsed } = this.state;

    if (isLoading && msElapsed > this.showSpinnerIfReturnGreaterThanMs) {
      return (
        <div class="text-primary">
          Loading Albums...  
          <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
        </div>
      );
    } else if (isLoading && msElapsed <= this.showSpinnerIfReturnGreaterThanMs) {
      return (null);
    } 
    if (error) {
      return <p>{error.message}</p>;
    }
    let contents = this.renderAlbumsTable(albums);
    return (
      <div>
        <h1 id="tabelLabel">Recipieces Albums</h1>
        {contents}
        <div>
          <a className="btn btn-primary" href="/albums/create">Create</a>
        </div>
      </div>
    );
  }
}