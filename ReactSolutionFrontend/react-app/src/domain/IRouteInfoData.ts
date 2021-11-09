import { IProvider } from './IProvider'
import { IRouteInfo } from './IRouteInfo'

export interface IRouteInfoData {
  provider: IProvider
  routeInfo: IRouteInfo
}

export interface IRouteInfoDataAdd {
  routeInfoId: string
  providerId: string
}
