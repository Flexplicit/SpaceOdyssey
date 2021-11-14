import React, { useEffect, useState } from 'react'
import { useHistory, useParams } from 'react-router'
import PlanetRoute from '../../components/planet-route/PlanetRoute'
import { ITravelData } from '../../domain/ITravelData'
import { TravelRoutesService } from '../../services/travel-routes-service'
import Modal from 'react-modal'
import PlanetRouteDescription from '../../components/planet-route-description/PlanetRouteDescription'
import { getHumanReadableTimeFromDifferenceBetweenDates } from '../../utils/date-conversion'
import BackButton from '../../components/buttons/BackButton'

type SearchParams = {
  from: string
  to: string
  date: string
  sortBy: string;
  companies: string
}

interface FetchState {
  loaded: boolean
}

interface IModalRouteData {
  show: boolean
  chosenRoute: ITravelData
}

const TravelSearchResult = () => {
  var params = useParams<SearchParams>()
  const [travelSearchResult, setTravelSearchResult] = useState([] as ITravelData[])
  const [selectedRouteModalState, setSelectedRouteState] = useState({} as IModalRouteData)
  const [fetchState, setfetchState] = useState({ loaded: false } as FetchState)
  var history = useHistory()

  const handleReservation = () => {
    history.push({
      pathname: '/reservations/add',
      state: selectedRouteModalState.chosenRoute,
    })
  }

  const closeModal = () => setSelectedRouteState({ ...selectedRouteModalState, show: false })
  const onPlanetRouteClick = (chosenRoute: ITravelData) => {
    setSelectedRouteState({ chosenRoute: chosenRoute, show: true })
  }

  let getSearchResultAsync = async () => {
    console.log((params.companies))
    let res = await TravelRoutesService.GetTravelRoutes<ITravelData>('/route', params.from, params.to, params.date, params.sortBy, JSON.parse(params.companies))
    if (res.statusCode === 200) {
      let resData = res.data as ITravelData[]
      setTravelSearchResult([...resData])
    }
    setfetchState({ loaded: true });
  }
  useEffect(() => {
    getSearchResultAsync()
  }, [])

  return (
    <>
      <BackButton />

      {fetchState.loaded && travelSearchResult.length == 0 ?
        <h1>No Routes Found</h1>
        : null
      }

      <Modal ariaHideApp={false} isOpen={selectedRouteModalState.show} onRequestClose={closeModal} shouldCloseOnEsc={true}>
        <div className="modal-header">
          <h5 className="modal-title">Travel routes</h5>
          <button type="button" className="btn-close" onClick={closeModal}></button>
        </div>
        <div className="modal-body">
          {selectedRouteModalState.chosenRoute?.routes.map((routeObj, index, array) => (
            <div key={index}>
              {
                index > 0 ?
                  <div className="layover modal-info-smallinfo">Layover:
                    {getHumanReadableTimeFromDifferenceBetweenDates(array[index - 1].provider.flightEnd, array[index].provider.flightStart)} in {routeObj.from}</div>
                  : null
              }
              <PlanetRouteDescription
                routeInfoId={routeObj.provider.id}
                from={routeObj.from}
                to={routeObj.to}
                distance={routeObj.distance}
                provider={routeObj.provider}
              />
            </div>
          ))}
        </div>
        <div className="modal-footer">
          <button type="button" className="btn btn-secondary" onClick={closeModal}>
            Close
          </button>
          <button type="button" className="btn btn-success" onClick={handleReservation}>
            Make a reservation
          </button>
        </div>
      </Modal>

      {travelSearchResult.map((travelRoute, index) => (
        <div className="mt-4">
          <PlanetRoute key={index} data={travelRoute} onRouteClick={onPlanetRouteClick} />
        </div>
      ))}
    </>
  )
}

export default TravelSearchResult
