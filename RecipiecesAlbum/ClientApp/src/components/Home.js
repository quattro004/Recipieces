import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { albums: [], loading: true };
  }

  componentDidMount() {
    this.populateAlbumData();
  }

  async populateAlbumData() {
    const response = await fetch('albums');
    const data = await response.json();
    this.setState({ albums: data, loading: false });
  }

  static renderAlbumsTable(albums) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Created On</th>
          </tr>
        </thead>
        <tbody>
          {albums.map(album =>
            <tr key={album.id}>
              <td>{album.name}</td>
              <td>{album.description}</td>
              <td>{album.createdOn}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Home.renderAlbumsTable(this.state.albums);

    return (
      <div>
        <h1 id="tabelLabel">Recipieces Albums</h1>
        {contents}
        <div>
          <a className="btn btn-primary" href="/albums/create">Create Album</a>
        </div>
      </div>
    );
  }
}
