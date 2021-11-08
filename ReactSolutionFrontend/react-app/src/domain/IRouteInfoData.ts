import { IProvider } from './IProvider'
import { IRouteInfo } from './IRouteInfo'

// export interface IRouteInfoData extends IRouteInfoDataAdd {
//   provider: IProvider
//   routeInfo: IRouteInfo
//   reservation: IReservation
// }

export interface IRouteInfoDataAdd {
    routeInfoId: string;
    providerId: string;
}
