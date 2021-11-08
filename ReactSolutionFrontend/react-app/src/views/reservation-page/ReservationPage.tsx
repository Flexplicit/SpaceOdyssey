import React, { useState } from 'react'
import { useLocation } from 'react-router'
import PlanetRoute from '../../components/planet-route/PlanetRoute'
import ReservationForm, { IReservationFormProps } from '../../components/reservation-form/ReservationForm'
import { ITravelData } from '../../domain/ITravelData'

const ReservationPage = () => {
  let location = useLocation<ITravelData>()
  const [chosenTravelRoute, setChosenTravelRoute] = useState(location.state)
  const handleSubmit = (inputs: IReservationFormProps) => {
    console.log(inputs)
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
