import React, { Component } from 'react';
import {OrganizationTable} from "./OrganizationsTable";

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <OrganizationTable />
    );
  }
}
