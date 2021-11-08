import { IRouteInfoProvider } from "./IRouteInfoProvider";

export interface ITravelData{
    travelPricesId: string;
    validUntil: Date
    totalDistanceInKilometers: number;
    totalPrice: number;
    totalLengthInHours: number;
    routes: IRouteInfoProvider[];
}