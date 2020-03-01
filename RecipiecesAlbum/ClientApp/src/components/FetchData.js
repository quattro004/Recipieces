import React, { Component } from 'react';
import { MyDropZone } from './MyDropZone';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { albums: [], loading: true };
  }

  componentDidMount() {
    this.populateAlbumData();
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
      : FetchData.renderAlbumsTable(this.state.albums);

    return (
      <div>
        <h1 id="tabelLabel">Albums</h1>
        <a className="btn btn-primary" href="/albums/create">Create Album</a>
        <MyDropZone />
        {contents}
      </div>
    );
  }

  async populateAlbumData() {
    const response = await fetch('albums');
    const data = await response.json();
    this.setState({ albums: data, loading: false });
  }
}
