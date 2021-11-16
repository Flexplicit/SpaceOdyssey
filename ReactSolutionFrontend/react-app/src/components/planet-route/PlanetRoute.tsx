import React from 'react'
import './PlanetRoute.scss'
import Rocket from '../../assets/rocket.svg'
import { IRouteInfoProvider } from '../../domain/IRouteInfoProvider'
import { ITravelData } from '../../domain/ITravelData'
import { convertStringToDateAndHours, convertStringToHoursMinutesString, getHumanReadableTimeFromHours } from '../../utils/date-conversion'

interface IPlanetRouteProps {
  data: ITravelData
  onRouteClick: (data: ITravelData) => void
}

const PlanetRoute = ({ data, onRouteClick }: IPlanetRouteProps) => {
  const priceFormatter = new Intl.NumberFormat('et-EE', {
    style: 'currency',
    currency: 'EUR',
  })

  return (
    <>
      <div className="row w-50 mx-auto justify-content-center d-flex align-items-center summary-leg-border" onClick={() => onRouteClick(data)}>
        <div className="col">
          <div className="row align-items-center">
            <span className="text-center summaryleg-price">{priceFormatter.format(data.totalPrice)}</span>
          </div>
        </div>
        <div className="col-2 ">
          <div className="row justify-content-end">
            <span className="text-end text-summaryleg-time">{convertStringToDateAndHours(data.routes?.[0].provider?.flightStart)}</span>
          </div>

          <div className="row justify-content-end">
            <span className="text-end text-summaryleg-planet">{data.routes?.[0].from}</span>
          </div>
        </div>
        <div className="col-5">
          <div className="row">
            <div className="col-8">
              <div className="row justify-content-center p-1">
                <span className="text-summary-leg-maxtime text-center">{getHumanReadableTimeFromHours(data.totalLengthInHours)} </span>
              </div>
              <div className="line"></div>
              <div className="row justify-content-center p-1">
                <span className="text-summaryleg-stopcount text-summary-leg-maxtime text-center">
                  ({data.routes?.length} stop{data.routes?.length > 1 ? 's' : ''})
                </span>
              </div>
            </div>
            <div className="col-4 d-flex align-items-center">
              <img className="img-responsive rocket-rotate-straight rocket-margin" width="40%" src={Rocket} alt="My Happy SVG" />
            </div>
          </div>
        </div>
        <div className="col-2 summaryleg-after-rocket ">
          <div className="row">
            <span className="text-summaryleg-time">{convertStringToDateAndHours(data.routes?.[data.routes.length - 1].provider?.flightEnd)}</span>
          </div>
          <div className="row">
            <span className="text-summaryleg-planet">{data.routes?.[data.routes?.length - 1]?.to}</span>
          </div>
        </div>
        <div className="col">
          <div className="row justify-content-end">
            <div className="col-8">
              <span className="summary-leg-down-arrow">
                <i className="fas fa-chevron-right"></i>
              </span>
            </div>
          </div>
        </div>
      </div>
    </>
  )
}

export default PlanetRoute
