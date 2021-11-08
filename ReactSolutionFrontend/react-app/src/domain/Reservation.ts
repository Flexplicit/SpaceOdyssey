import { IRouteInfoDataAdd } from './IRouteInfoData'

export interface IReservation extends IReservationCommon {
    id: string;
//   routes: IRouteInfoData[]
}

export interface IReservationAdd extends IReservationCommon {
  routes: IRouteInfoDataAdd[]
}

interface IReservationCommon {
  firstName: string
  lastName: string
  travelPricesId: string
}
