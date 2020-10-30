import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <NavMenu />
          <Container>
            <main role="main" className="pb-3">
              {this.props.children}
            </main>
          </Container>
        <footer className="border-top footer text-muted">
          <div className="container">
            &copy; 2020 - Reese J. Hodge IV
          </div>
        </footer>
      </div>
    );
  }
}
