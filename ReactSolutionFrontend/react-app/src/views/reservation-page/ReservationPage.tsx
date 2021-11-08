import React, { useState } from 'react'
import { useHistory, useLocation } from 'react-router'
import PlanetRoute from '../../components/planet-route/PlanetRoute'
import ReservationForm, { IReservationFormProps } from '../../components/reservation-form/ReservationForm'
import { IRouteInfoDataAdd } from '../../domain/IRouteInfoData'
import { ITravelData } from '../../domain/ITravelData'
import { IReservation, IReservationAdd } from '../../domain/Reservation'
import { BaseServices } from '../../services/base-service'

const ReservationPage = () => {
  let location = useLocation<ITravelData>()
  let history = useHistory()
  const [chosenTravelRoute, setChosenTravelRoute] = useState(location.state)

  const handleSubmit = async (inputs: IReservationFormProps) => {
    let reservation = {
      firstName: inputs.firstName,
      lastName: inputs.lastName,
      travelPricesId: chosenTravelRoute.travelPricesId,
      routes: chosenTravelRoute.routes.map((route) => {
        return { routeInfoId: route.routeInfoId, providerId: route.provider.id } as IRouteInfoDataAdd
      }),
    } as IReservationAdd
    let res = await BaseServices.PostAsync('/reservation', reservation)
    if (res.statusCode === 200) {
      let resData = res.data as IReservation
      history.push(`/reservation/success/${resData.id}`)
    }
  }

  return (
    <>
      <div className="row">
        <div>
          <h3 className="text-center">Chosen Travel plan</h3>
        </div>
        <div className="col-12">
          <PlanetRoute data={chosenTravelRoute} onRouteClick={() => {}} />
        </div>
        <div className=" mt-5 d-flex justify-content-center">
          <ReservationForm submitHandler={handleSubmit} />
        </div>
      </div>
    </>
  )
}

export default ReservationPage
