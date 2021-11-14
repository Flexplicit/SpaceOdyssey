import React from 'react'
import { Link } from 'react-router-dom'
import Logo from '../../assets/rocket.png'
import "./Header.scss";

const Header = () => {
  return (
    <div className="header">
      <Link to="/" className="logo-container" >
        <img src={Logo} height="100px" width="100px" />
      </Link>
      <div className="options">
        <Link to="/reservations" className="option hvr-grow">
          Reservations
        </Link>
        <Link to="/" className="option hvr-grow">
          Travels
        </Link>
      </div>
    </div>
  )
}

export default Header
