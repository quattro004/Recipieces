import React, { Component } from 'react';
const axios = require('axios').default;
const API_QUERY = 'albums';

export class AlbumList extends Component {
  constructor(props) {
    super(props);
    this.state = { albums: [], loading: true, error: null, };
  }

  componentDidMount() {
    this.getAlbums();
  }

  async getAlbums() {
    console.log('Getting albums');
    try {
      const response = await axios.get(API_QUERY);
      this.setState({ albums: response.data, loading: false });
    } catch (err) {
      console.error(err);
      this.setState({ err, loading: false });
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
    const { albums, loading, error } = this.state;
    if (error) {
      return <p>{error.message}</p>;
    }
    if (loading) {
      return <p><em>Loading...</em></p>;
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