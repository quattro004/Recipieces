import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { AlbumForm } from './components/AlbumForm';

import './site.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/albums/create' component={AlbumForm} />
        <Route path='/fetch-data' component={FetchData} />
      </Layout>
    );
  }
}
