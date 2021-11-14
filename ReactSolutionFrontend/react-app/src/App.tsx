import { Route, Switch } from 'react-router-dom'
import './App.css'
import Header from './components/header/Header'
import Homepage from './views/homepage/Homepage'
import TravelSearchResult from './views/travel-search-result-page/TravelSearchResult'
import ReservationPage from './views/reservation-page/ReservationPage'
import ReservationResultPage from './views/reservation-result-page/ReservationResultPage'
import ReservationsListPage from './views/reservations-list-page/ReservationsListPage'
import ErrorPage from './views/error-page/ErrorPage'

function App() {
  return (
    <>
      <Header />
      <main className="container">
        <Switch>
          <Route exact path="/" component={Homepage} />
          <Route exact path="/search/planetroute/:from/:to/:date/:sortBy/:companies" component={TravelSearchResult} />
          <Route exact path="/reservations/add" component={ReservationPage} />
          <Route exact path="/reservations/success/:id" component={ReservationResultPage} />
          <Route exact path="/reservations/" component={ReservationsListPage} />
          <Route component={ErrorPage} />
        </Switch>
      </main>
    </>
  )
}

export default App
