import React from 'react'
import Rocket from '../../assets/rocket.svg'
import { IProvider } from '../../domain/IProvider'
import { IRouteInfoProvider } from '../../domain/IRouteInfoProvider'
import { convertStringToDateAndHours, getHumanReadableTimeFromMilliseconds } from '../../utils/date-conversion'
import { priceFormatter } from '../../utils/price-formatter'
import './PlanetRouteDescription.scss'

interface IPlanetRouteDescription {
  routeInfoId: string
  from: string
  to: string
  distance: number
  provider: IProvider
}

const PlanetRouteDescription = ({ routeInfoId, from, to, distance, provider }: IPlanetRouteDescription) => {
  return (
    <div>
      <div className="row p-1">
        <span className="modal-flight-info">
          <img className="img-responsive p-1 rotate-ship-up" width="3%" src={Rocket} alt="My Happy SVG" />
          {convertStringToDateAndHours(provider.flightStart)} - {from}
        </span>
        <span className="modal-info-smallinfo">{provider.company.name} provider</span>
        <span className="modal-info-smallinfo">{priceFormatter.format(provider.price)} </span>
        <span className="modal-info-smallinfo">
          {getHumanReadableTimeFromMilliseconds(new Date(provider.flightEnd).getTime() - new Date(provider.flightStart).getTime())} flight
        </span>
        <span className="modal-info-smallinfo">{distance} km</span>
      </div>
      <div className="row">
        <span className="modal-flight-info">
          <img className="img-responsive p-1 rotate-ship-down" width="3%" src={Rocket} alt="My Happy SVG" />
          {convertStringToDateAndHours(provider.flightEnd)} - {to}
        </span>
      </div>
      <div className="layover modal-info-smallinfo">Layover: 17h 30min in Earth</div>
    </div>
  )
}

export default PlanetRouteDescription
