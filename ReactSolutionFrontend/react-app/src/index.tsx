import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
import App from './App'
import * as serviceWorkerRegistration from './serviceWorkerRegistration'

import 'bootstrap'
import 'jquery'
import 'popper.js'
import 'font-awesome/css/font-awesome.min.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import { BrowserRouter as Router } from 'react-router-dom'

import '@fortawesome/fontawesome-free/css/all.min.css'
import 'react-date-picker/dist/DatePicker.css'

ReactDOM.render(
  <Router>
    {/* <Router basename={process.env.PUBLIC_URL}> */}
    <React.StrictMode>
      <App />
    </React.StrictMode>
  </Router>,
  document.getElementById('root'),
)

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister()
