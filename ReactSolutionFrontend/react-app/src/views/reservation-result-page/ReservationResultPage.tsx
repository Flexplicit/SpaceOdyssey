import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router'
import { Route } from 'workbox-routing'
import { IReservation } from '../../domain/Reservation'
import { BaseServices } from '../../services/base-service'
import { convertStringToHoursMinutesString, getHumanReadableTimeFromHours } from '../../utils/date-conversion'
import { priceFormatter } from '../../utils/price-formatter'
import CloudArrow from '../../assets/cloud-arrow-down-solid.svg'
import './ReservationResultPage.scss'
import CustomRouteModal from '../../components/custom-route-modal/CustomRouteModal'
import { IRouteInfoData } from '../../domain/IRouteInfoData'

interface RouteParams {
  id: string
}
export interface ICustomRouteModalProps {
  routes: IRouteInfoData[]
  show: boolean
}

const ReservationResultPage = () => {
  const { id } = useParams<RouteParams>()

  const [reservationResult, setReservationResult] = useState({} as IReservation)
  const [modalState, setModalState] = useState({} as ICustomRouteModalProps)
  const fetchData = async () => {
    let res = await BaseServices.GetById<IReservation>('/Reservation', id)
    if (res.statusCode === 200) {
      setReservationResult(res.data!)
    }
  }
  useEffect(() => {
    fetchData()
  }, [])

  const handleRouteClick = () => {
    setModalState({ routes: reservationResult.routeInfoData, show: true })
  }
  return (
    <>
      <CustomRouteModal changeState={setModalState} modalState={modalState} />

      <div className="row text-center">
        <div className="text-center ">
          <h2>Your reserved travel plan</h2>
        </div>

        <div className="row d-flex justify-content-center mt-3">
          <div className="card col-6">
            <table className="table">
              <tbody>
                <tr>
                  <th>Reservation ID</th>
                  <td>{reservationResult.id}</td>
                </tr>
                <tr>
                  <th>Reserved Name</th>
                  <td>{`${reservationResult.firstName} ${reservationResult.lastName}`}</td>
                </tr>
                <tr>
                  <th>Total price</th>
                  <td>{priceFormatter.format(reservationResult.totalQuotedPrice)}</td>
                </tr>
                <tr>
                  <th>Total Travel time</th>
                  <td>{getHumanReadableTimeFromHours(reservationResult.totalQuotedTravelTimeInMinutes)}</td>
                </tr>
                <tr>
                  <th>Travel Routes</th>
                  <td>
                    <img src={CloudArrow} onClick={handleRouteClick} className="clickable-icon hvr-grow" width="20%" alt="" />
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </>
  )
}

export default ReservationResultPage
