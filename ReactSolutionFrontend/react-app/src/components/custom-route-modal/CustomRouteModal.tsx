import React, { useState } from 'react'
import { IRouteInfoData } from '../../domain/IRouteInfoData'
import './CustomRouteModal.scss'
import Modal from 'react-modal'
import PlanetRouteDescription from '../planet-route-description/PlanetRouteDescription'
import { IRouteInfoProvider } from '../../domain/IRouteInfoProvider'
import { ICustomRouteModalProps } from '../../views/reservation-result-page/ReservationResultPage'

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
          {modalState?.routes?.map((routeObj) => (
            <PlanetRouteDescription
              key={routeObj.routeInfo.id}
              routeInfoId={routeObj.routeInfo.id}
              from={routeObj.routeInfo.from.name}
              to={routeObj.routeInfo.to.name}
              distance={routeObj.routeInfo.distance}
              provider={routeObj.provider}
            />
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
