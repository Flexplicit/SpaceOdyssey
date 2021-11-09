import React, { useEffect, useState } from 'react'
import { useHistory, useParams } from 'react-router'
import PlanetRoute from '../../components/planet-route/PlanetRoute'
import { ITravelData } from '../../domain/ITravelData'
import { TravelRoutesService } from '../../services/travel-routes-service'
import Modal from 'react-modal'
import PlanetRouteDescription from '../../components/planet-route-description/PlanetRouteDescription'

type SearchParams = {
  from: string
  to: string
  date: string
}

interface IModalRouteData {
  show: boolean
  chosenRoute: ITravelData
}

const customModalStyles = {
  content: {
    top: '30%',
    left: '50%',
    right: 'auto',
    bottom: 'auto',
    marginRight: '-50%',
    transform: 'translate(-50%, -50%)',
  },
}

const TravelSearchResult = () => {
  var params = useParams<SearchParams>()
  const [travelSearchResult, setTravelSearchResult] = useState({} as ITravelData)
  const [selectedRouteModalState, setSelectedRouteState] = useState({} as IModalRouteData)
  var history = useHistory()

  const handleReservation = () => {
    history.push({
      pathname: '/reservations/add',
      state: selectedRouteModalState.chosenRoute,
    })
  }
  const closeModal = () => setSelectedRouteState({ ...selectedRouteModalState, show: false })
  const onPlanetRouteClick = (chosenRoute: ITravelData) => {
    console.log('clicked data on planetroute', chosenRoute)
    setSelectedRouteState({ chosenRoute: chosenRoute, show: true })
  }

  let getSearchResultAsync = async () => {
    let res = await TravelRoutesService.GetTravelRoutes<ITravelData>('/route', params.from, params.to, '')
    if (res.statusCode === 200) {
      let resData = (res.data as ITravelData[])[0]
      console.log(resData)
      setTravelSearchResult({ ...resData, routes: [...resData.routes] })
    }
  }
  useEffect(() => {
    getSearchResultAsync()
  }, [])

  return (
    <>
      <Modal ariaHideApp={false} isOpen={selectedRouteModalState.show} onRequestClose={closeModal} shouldCloseOnEsc={true}>
        <div className="modal-header">
          <h5 className="modal-title">Travel routes</h5>
          <button type="button" className="btn-close" onClick={closeModal}></button>
        </div>
        <div className="modal-body">
          {selectedRouteModalState.chosenRoute?.routes.map((routeObj) => (
            <PlanetRouteDescription 
            key={routeObj.provider.id}
            routeInfoId={routeObj.provider.id}
            from={routeObj.from}
            to={routeObj.to}
            distance={routeObj.distance}
            provider={routeObj.provider} />
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

      <PlanetRoute data={travelSearchResult} onRouteClick={onPlanetRouteClick} />
      <div>result</div>
    </>
  )
}

export default TravelSearchResult
