import { BASE_URL } from '../configutations'
import axios, { AxiosRequestConfig, AxiosResponse } from 'axios'
import { IFetchResponse } from '../types/IFetchResponse'

export class BaseServices {
  protected static axios = axios.create({
    baseURL: `${BASE_URL}`,
    validateStatus: (status: number) => status < 500,
    headers: {
      'Content-Type': 'application/json',
    },
  })

  static async GetAllAsync<TEntity>(endPoint: string): Promise<IFetchResponse<TEntity[]>> {
    try {
      var res = await this.axios.get<TEntity[]>(endPoint)
      return this.FetchResponse(res)
    } catch (error) {
      return { statusCode: 0, messages: [(error as Error).message] }
    }
  }

  static async PostAsync<TInEntity, TOutEntity>(endpoint: string, entityToPost: TInEntity): Promise<IFetchResponse<TOutEntity>> {
    try {
      let res = await this.axios.post<TOutEntity>(endpoint, entityToPost)
      return this.FetchResponse(res)
    } catch (error) {
      return { statusCode: 0, messages: [(error as Error).message] }
    }
  }

  static async GetById<TEntity>(endpoint: string, id: string): Promise<IFetchResponse<TEntity>> {
    let url = `${endpoint}/${id}`
    console.log(url)
    try {
      let res = await this.axios.get<TEntity>(url)
      return this.FetchResponse(res)
    } catch (error) {
      return { statusCode: 0, messages: [(error as Error).message] }
    }
  }

  private static async FetchResponse<TEntity>(res: AxiosResponse<TEntity, any>): Promise<IFetchResponse<TEntity>> {
    return {
      statusCode: res.status,
      data: res.data,
      messages: [res.statusText],
    }
  }
}
