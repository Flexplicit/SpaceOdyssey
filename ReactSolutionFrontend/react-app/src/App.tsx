import { Route, Switch } from 'react-router-dom'
import './App.css'
import Header from './components/header/Header'
import Homepage from './views/homepage/Homepage'
import TravelSearchResult from './views/travel-search-result-page/TravelSearchResult'
import ReservationPage from './views/reservation-page/ReservationPage'

function App() {
  return (
    <>
      <Header />
      <main className="container">
        <Switch>
          <Route exact path="/" component={Homepage} />
          <Route path="/search/planetroute/:from/:to/:date" component={TravelSearchResult} />
          <Route path="/reservations/add" component={ReservationPage} />
          {/* <Route component={Page404} /> */}
        </Switch>
      </main>
    </>
  )
}

export default App
