import { IFetchResponse } from '../types/IFetchResponse'
import { BaseServices } from './base-service'

export class TravelRoutesService extends BaseServices {
  public static async GetTravelRoutes<TEntity>(endPoint: string, from: string, to: string, date: string): Promise<IFetchResponse<TEntity[]>> {
    let searchEndpoint = `${endPoint}/${from}/${to}`
    console.log(searchEndpoint)
    try {
      var res = await this.axios.get<TEntity[]>(searchEndpoint)
      return {
        statusCode: res.status,
        data: res.data,
        messages: [res.statusText],
      }
    } catch (error) {
      return { statusCode: 0, messages: [(error as Error).message] }
    }
  }
}
