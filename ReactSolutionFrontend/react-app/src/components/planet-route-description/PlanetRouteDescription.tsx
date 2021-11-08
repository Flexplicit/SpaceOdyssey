import React from 'react'
import Rocket from '../../assets/rocket.svg'
import { IRouteInfoProvider } from '../../domain/IRouteInfoProvider'
import { convertStringToDateAndHours, getHumanReadableTimeFromMilliseconds } from '../../utils/date-conversion'
import { priceFormatter } from '../../utils/price-formatter'
import "./PlanetRouteDescription.scss"


interface IPlanetRouteDescription {
    planetRoute: IRouteInfoProvider
  }
  
  const PlanetRouteDescription = ({ planetRoute }: IPlanetRouteDescription) => {
    return (
      <div>
        <div className="row p-1">
          <span className="modal-flight-info">
            <img className="img-responsive p-1 rotate-ship-up" width="3%" src={Rocket} alt="My Happy SVG" />
            {convertStringToDateAndHours(planetRoute.provider.flightStart)} - {planetRoute.from}
          </span>
          <span className="modal-info-smallinfo">{planetRoute.provider.company.name} provider</span>
          <span className="modal-info-smallinfo">{priceFormatter.format(planetRoute.provider.price)} </span>
          <span className="modal-info-smallinfo">
            {getHumanReadableTimeFromMilliseconds(new Date(planetRoute.provider.flightEnd).getTime() - new Date(planetRoute.provider.flightStart).getTime())} flight
          </span>
          <span className="modal-info-smallinfo">{planetRoute.distance} km</span>
        </div>
        <div className="row">
          <span className="modal-flight-info">
            <img className="img-responsive p-1 rotate-ship-down" width="3%" src={Rocket} alt="My Happy SVG" />
            {convertStringToDateAndHours(planetRoute.provider.flightEnd)} - {planetRoute.to}
          </span>
        </div>
        <div className="layover modal-info-smallinfo">Layover: 17h 30min in Earth</div>
      </div>
    )
  }

export default PlanetRouteDescription
