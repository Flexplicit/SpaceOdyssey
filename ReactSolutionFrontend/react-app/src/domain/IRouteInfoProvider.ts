import { Provider } from "react";
import { IProvider } from "./IProvider";
import { IRouteInfo } from "./IRouteInfo";

// export interface IRouteInfoProvider extends IAddRouteInfoProvider {
//   id: string
//   routeInfo: IRouteInfo
//   provider: IProvider
// }

// export interface IAddRouteInfoProvider {
//     routeInfoId: string;
//     providerId: string;
// }


export interface IRouteInfoProvider{
  id: string;
  from: string;
  to:string;
  distance: number;
  provider: IProvider
}
