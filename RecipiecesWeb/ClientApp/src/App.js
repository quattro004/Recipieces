import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { AlbumForm } from './components/AlbumForm';
import { AlbumList } from './components/AlbumList';

import './site.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route path='/list' component={AlbumList} />
        <Route path='/create' component={AlbumForm} />
      </Layout>
    );
  }
}
