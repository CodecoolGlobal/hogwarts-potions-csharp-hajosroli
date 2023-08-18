import {NavLink, Outlet} from 'react-router-dom';
import Navbar from 'react-bootstrap/Navbar';
import 'bootstrap/dist/css/bootstrap.css';
import image from './hogwarts-logo.png';
import './Menu.css'

const Menu = () => (
    <div>
    <Navbar 
    className="navbar navbar-expand-sm bg-light navbar-dark"
    style={{backgroundColor: '#2c3e50' }}>
    <Navbar.Brand to="/"> 
      <img 
      src={image} 
      className="hogwarts-logo" 
      alt="hogwarts-logo"
      height={110}
      width={140} />
    </Navbar.Brand>
        <ul className="navbar-nav">
          <li className="nav-item- m-1">
            <NavLink className="custom-btn" to="/">
              Home
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="custom-btn" to="/brewing-form">
              Brew A Potion
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="custom-btn" to="/students">
              Students
            </NavLink>
          </li>
        </ul>
      </Navbar>
      <Outlet/>
      </div>
)

export default Menu;