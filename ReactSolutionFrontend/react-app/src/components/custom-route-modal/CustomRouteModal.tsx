import React, { useState } from 'react'
import { IRouteInfoData } from '../../domain/IRouteInfoData'
import './CustomRouteModal.scss'
import Modal from 'react-modal'
import PlanetRouteDescription from '../planet-route-description/PlanetRouteDescription'
import { IRouteInfoProvider } from '../../domain/IRouteInfoProvider'
import { ICustomRouteModalProps } from '../../views/reservation-result-page/ReservationResultPage'
import { getHoursBetweenDates, getHumanReadableTimeFromDifferenceBetweenDates } from '../../utils/date-conversion'

interface ICustomProps {
  modalState: ICustomRouteModalProps
  changeState: (value: React.SetStateAction<ICustomRouteModalProps>) => void
}

const CustomRouteModal = ({ modalState, changeState }: ICustomProps) => {
  const closeModal = () => changeState({ ...modalState, show: false })
  return (
    <>
      <Modal ariaHideApp={false} isOpen={modalState.show} onRequestClose={closeModal} shouldCloseOnEsc={true}>
        <div className="modal-header">
          <h5 className="modal-title">Travel routes</h5>
          <button type="button" className="btn-close" onClick={closeModal}></button>
        </div>
        <div className="modal-body">
          {modalState?.routes?.map((routeObj, index, array) => (
            <div key={routeObj.routeInfo.id}>
              {
                index > 0 ?
                  <div className="layover modal-info-smallinfo">Layover:
                    {getHumanReadableTimeFromDifferenceBetweenDates(array[index - 1].provider.flightEnd, array[index].provider.flightStart)}</div>
                  : null
              }              <PlanetRouteDescription
                routeInfoId={routeObj.routeInfo.id}
                from={routeObj.routeInfo.from.name}
                to={routeObj.routeInfo.to.name}
                distance={routeObj.routeInfo.distance}
                provider={routeObj.provider}
              />
            </div>
          ))}
        </div>
        <div className="modal-footer">
          <button type="button" className="btn btn-secondary" onClick={closeModal}>
            Close
          </button>
        </div>
      </Modal>
    </>
  )
}

export default CustomRouteModal
