import { IFetchResponse } from '../types/IFetchResponse'
import { BaseServices } from './base-service'

export class TravelRoutesService extends BaseServices {
  public static async GetTravelRoutes<TEntity>(endPoint: string, from: string, to: string, date: string, sortBy: string, companies: string[])
  : Promise<IFetchResponse<TEntity[]>> {
    let jsonCompanies = JSON.stringify(companies)

    let searchEndpoint = `${endPoint}/${from}/${to}/${date}/${sortBy}/${jsonCompanies}`
    try {
      var res = await this.axios.get<TEntity[]>(searchEndpoint)
      return super.FetchResponse(res)
    } catch (error) {
      return { statusCode: 0, messages: [(error as Error).message] }
    }
  }
}
