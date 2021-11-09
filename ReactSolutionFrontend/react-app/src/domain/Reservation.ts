import { IRouteInfoData, IRouteInfoDataAdd } from './IRouteInfoData'

export interface IReservation extends IReservationCommon {
  id: string
  totalQuotedPrice: number;
  totalQuotedTravelTimeInMinutes: number;
  routeInfoData: IRouteInfoData[]
}

export interface IReservationAdd extends IReservationCommon {
  routes: IRouteInfoDataAdd[]
}

interface IReservationCommon {
  firstName: string
  lastName: string
  travelPricesId: string
}
