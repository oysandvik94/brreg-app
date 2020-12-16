import React from "react";
import { Container, Navbar, NavbarBrand} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import AddForm from "./AddForm";

export function ToolBar(props) {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">webapp</NavbarBrand>
              <ul className="navbar-nav flex-grow">
                <AddForm />
              </ul>
          </Container>
        </Navbar>
      </header>
    );
}
