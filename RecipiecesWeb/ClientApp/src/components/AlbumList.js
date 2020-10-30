import React, { Component } from 'react';
import { AlbumRow } from './AlbumRow';

const axios = require('axios').default;
const API_QUERY = '.';

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

  componentWillUnmount() {
    clearInterval(this.incrementer);
  }

  componentDidMount() {
    this.incrementer = setInterval(() =>
      this.setState({
        msElapsed: this.state.msElapsed + this.timeIncrementMs
      }), this.timeIncrementMs);
      
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
      <table className='table table-striped table-responsive table-hover' aria-labelledby="tableLabel">
        
        <tbody>
          {albums.map(album =>
            <AlbumRow item={album} />
          )}
        </tbody>
      </table>
    );
  }

  render() {
    const { albums, isLoading, error, msElapsed } = this.state;

    if (isLoading && msElapsed > this.showSpinnerIfReturnGreaterThanMs) {
      return (
        <div className="text-primary ">
          Loading Albums....  
          <span className="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
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
        <h1 id="tableLabel">Albums</h1>
        {contents}
        <div>
          <a className="btn btn-primary" href="/albums/create">Create</a>
        </div>
      </div>
    );
  }
}