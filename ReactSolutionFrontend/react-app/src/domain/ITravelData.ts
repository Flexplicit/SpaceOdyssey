import { IRouteInfoProvider } from "./IRouteInfoProvider";

export interface ITravelData{
    validUntil: Date
    totalDistanceInKilometers: number;
    totalPrice: number;
    totalLengthInHours: number;
    routes: IRouteInfoProvider[];
}