import React, { useEffect, useState } from 'react'
import CustomRouteModal from '../../components/custom-route-modal/CustomRouteModal'
import { IRouteInfoData } from '../../domain/IRouteInfoData'
import { IReservation } from '../../domain/Reservation'
import { BaseServices } from '../../services/base-service'
import { getHumanReadableTimeFromHours } from '../../utils/date-conversion'
import { priceFormatter } from '../../utils/price-formatter'
import CloudArrow from '../../assets/cloud-arrow-down-solid.svg'

export interface ICustomRouteModalProps {
  routes: IRouteInfoData[]
  show: boolean
}
const ReservationsListPage = () => {
  const [reservationsState, setReservationsState] = useState([] as IReservation[])
  const [modalState, setModalState] = useState({} as ICustomRouteModalProps)

  const openSpecificReservationModalRoute = (reservationId: string) => {
    let reservation = reservationsState.find((reservation) => reservation.id === reservationId)
    if (reservation !== undefined) {
      setModalState({ routes: reservation.routeInfoData, show: true })
    }
  }

  const fetchReservations = async () => {
    let res = await BaseServices.GetAllAsync<IReservation>('reservation')
    if (res.statusCode === 200) {
      let data = res.data!
      setReservationsState([...data])
    }
  }

  useEffect(() => {
    fetchReservations()
  }, [])

  return (
    <>
      <CustomRouteModal modalState={modalState} changeState={setModalState} />
      <table className="table table-striped">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Reservation name</th>
            <th scope="col">Total price</th>
            <th scope="col">Total time</th>
            <th scope="col">Total distance</th>
            <th scope="col">View Routes</th>
          </tr>
        </thead>

        <tbody>
          {reservationsState.map((reservation, index) => (
            <tr key={reservation.id}>
              <td>{index + 1}</td>
              <td>{`${reservation.firstName} ${reservation.lastName}`}</td>
              <td>{priceFormatter.format(reservation.totalQuotedPrice)}</td>
              <td>{getHumanReadableTimeFromHours(reservation.totalQuotedTravelTimeInMinutes)}</td>
              <td>{reservation.routeInfoData.reduce((prev, curr) => prev + curr.routeInfo.distance, 0)} km</td>
              <td>
                <img
                  src={CloudArrow}
                  onClick={() => openSpecificReservationModalRoute(reservation.id)}
                  className="clickable-icon hvr-grow"
                  width="5%"
                  alt="cloud-arrow-image"
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}

export default ReservationsListPage
