import { ICompany } from './ICompany'

export interface IProvider {
  id: string
  price: number
  flightStart: string
  flightEnd: string
  company: ICompany
}
