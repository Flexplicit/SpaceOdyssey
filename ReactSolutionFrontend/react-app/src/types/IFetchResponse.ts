export interface IFetchResponse<TData> {
  data?: TData
  messages?: string[]
  statusCode: number
}
