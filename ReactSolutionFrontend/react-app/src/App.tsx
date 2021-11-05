import { Route, Switch } from 'react-router-dom'
import './App.css'
import Header from './components/header/Header'
import Homepage from './views/homepage/Homepage'
import TravelSearchResult from './views/travel-search-result-page/TravelSearchResult'

function App() {
  return (
    <>
      <Header />
      <main className="container">
        <Switch>
          <Route exact path="/" component={Homepage} />
          <Route path="/search/planetroute/:from/:to/:date" component={TravelSearchResult} />
          {/* <Route path="/search/planetroute?from=:from&to=:to&date=:date" component={TravelSearchResult} /> */}
          {/* <Route component={Page404} /> */}
        </Switch>
      </main>
    </>
  )
}

export default App
