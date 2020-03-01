import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <NavMenu />
        <main role="main" class="pb-3">
          <Container>
            {this.props.children}
          </Container>
        </main>
        <footer class="border-top footer text-muted">
          <div class="container">
            &copy; 2020 - Reese J. Hodge IV
          </div>
        </footer>
      </div>
    );
  }
}
