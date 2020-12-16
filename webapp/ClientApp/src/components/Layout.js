import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { ToolBar } from './ToolBar';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <ToolBar />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
