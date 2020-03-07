import React, { Component } from 'react';
import { AlbumList } from './AlbumList';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <AlbumList />
    );
  }
}
