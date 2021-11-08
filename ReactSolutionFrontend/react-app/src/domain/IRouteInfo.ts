import { IPlanet } from './IPlanet'

export interface IRouteInfo {
  id: string
  from: IPlanet
  to: IPlanet
  distance: number
}
