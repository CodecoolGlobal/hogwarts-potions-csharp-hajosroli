import {NavLink, Outlet} from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.css';

const NavBar = () => (
    <div>
    <nav className="navbar navbar-expand-sm bg-light navbar-dark">
        <ul className="navbar-nav">
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/">
              Home
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/brewing-form">
              Brew A Potion
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/students">
              Students
            </NavLink>
          </li>
        </ul>
      </nav>
      <Outlet/>
      </div>
)

export default NavBar;