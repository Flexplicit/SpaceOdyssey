import React, { ChangeEvent, FormEvent, useEffect, useState } from 'react'
import { BaseServices } from '../../services/base-service'
import FormErrorMessage from '../errors/FormErrorMessage'
import { ISearchData } from './ISearchData'
import DatePicker from 'react-date-picker'
import { useHistory } from 'react-router'
import { ICompany } from '../../domain/ICompany'
import "./PlanetSearchForm.scss"
import { getTomorrowsDate } from '../../utils/date-conversion'

const filters = [{ filterName: "Price", chosen: true }, { filterName: "Distance", chosen: false }, { filterName: "Time", chosen: false }] as IFilterState[]

interface ICompanyState {
  company: ICompany;
  chosen: boolean;
}
interface IFilterState {
  filterName: string;
  chosen: boolean;
}

const PlanetSearchForm = () => {
  const [companiesState, setCompaniesState] = useState([] as ICompanyState[])
  const [planetState, setPlanetState] = useState([] as string[])
  const [searchData, setSearchData] = useState({ from: '', to: '', date: getTomorrowsDate() } as ISearchData)
  const [radioState, setradioState] = useState(filters as IFilterState[])
  const [errors, setErrors] = useState([] as string[])
  var history = useHistory()

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault()
    let currentErrors = [] as string[]
    if (searchData.from === '') {
      currentErrors.push('Origin planet must be chosen')
    }
    if (searchData.to === '') {
      currentErrors.push('Destination planet must be chosen')
    }
    if (searchData.date < new Date()) {
      currentErrors.push('Search Date must be in the future')
    }
    if (searchData.from === searchData.to) {
      currentErrors.push('Travel destination and origin should be different')
    }
    if (!companiesState.some(x => x.chosen)) {
      currentErrors.push('Must choose atleast 1 provider')
    }
    if (currentErrors.length === 0) {
      let chosenCompanies = companiesState.filter((companyState) => companyState.chosen);
      let companies = chosenCompanies.map((companyState) => companyState.company.name);
      let jsonCompanies = JSON.stringify(companies);
      history.push(`/search/planetroute/${searchData.from}/${searchData.to}/${searchData.date.toISOString()}/${radioState.find((radio)=>radio.chosen)?.filterName}/${jsonCompanies}`)
    }

    else {
      setErrors([...currentErrors])
    }
  }


  const handleCheckBoxChange = (e: ChangeEvent<HTMLInputElement>) => {
    let chosenCompany = companiesState.find((companyState) => companyState.company.name === e.target.value);
    chosenCompany!.chosen = !chosenCompany?.chosen;
    setCompaniesState([...companiesState]);
  }

  const handleRadioClick = (radio: IFilterState) => {
    let state = [] as IFilterState[];
    radio.chosen = true;
    radioState.forEach((currRadio) => {
      if (radio.filterName != currRadio.filterName) {
        currRadio.chosen = false
      }
      state.push(currRadio)
    })
    setradioState([...state]);
  }

  const fetchPlanets = async () => {
    let result = await BaseServices.GetAllAsync<string>('/planet')
    if (result.statusCode === 200) {
      setPlanetState([...result.data!] as string[])
    }
  }

  const fetchCompanies = async () => {
    let result = await BaseServices.GetAllAsync<ICompany>("/company");
    if (result.statusCode === 200) {
      let mapDataToCompanyState = result.data!.map((company) => {
        return {
          chosen: true,
          company: company
        } as ICompanyState
      })!
      setCompaniesState([...mapDataToCompanyState])
    }
  }


  useEffect(() => {
    fetchPlanets()
    fetchCompanies()
  }, [])

  return (
    <>
      <FormErrorMessage errors={errors} />

      <div className="row tool-bar-wrapper">
        <div className="col-4">
          Sort by filters<i className="fas fa-filter p-1 sort-icon"></i>
          {radioState.map((radio, index) => (

            <div key={index} className="form-check">
              <input className="form-check-input" onChange={() => handleRadioClick(radio)} type="radio" name="travelFilterRadio"  checked={radio.chosen}  id={radio.filterName} />
              <label className="form-check-label" htmlFor={radio.filterName}>
                {radio.filterName}
              </label>
            </div>
          ))}

          {/* <div className="form-check">
            <input className="form-check-input" type="radio" name="travelFilterRadio" id="destinationLength" />
            <label className="form-check-label" htmlFor="destinationLength">
              Destination length
            </label>
          </div><div className="form-check">
            <input className="form-check-input" type="radio" name="travelFilterRadio" id="travelTime" />
            <label className="form-check-label" htmlFor="travelTime">
              Travel time
            </label>
          </div> */}
        </div>

        <div className="col-8 form-check">
          <div className="text-secondary"> Choose providers</div>
          {companiesState.map((companyState, index) => (
            <div key={index} className="form-check form-check-inline">
              <input className="form-check-input" onChange={handleCheckBoxChange} type="checkbox" value={companyState.company.name} checked={companyState.chosen} id="flexCheckDefault" />
              <label className="form-check-label" htmlFor="flexCheckDefault">{companyState.company.name}</label>
            </div>
          ))}
        </div>
      </div>

      <div className="search-box">
        <form action="" method="post">

          <div className="search-controls">
            <div className="form-group m-0">
              <select onChange={(e) => setSearchData({ ...searchData, from: e.target.value })} className="form-control  form-control-lg rounded-pill selectpicker">
                <option selected disabled>
                  Leaving from
                </option>
                {planetState.map((planet) => (
                  <option key={planet} value={planet}>
                    {planet}
                  </option>
                ))}
              </select>
            </div>
            <div className="form-group m-0">
              <select onChange={(e) => setSearchData({ ...searchData, to: e.target.value })} className="form-control  form-control-lg rounded-pill selectpicker">
                <option defaultValue={'default'} selected disabled>
                  Going to
                </option>
                {planetState.map((planet) => (
                  <option key={planet} value={planet}>
                    {planet}
                  </option>
                ))}
              </select>
            </div>
            <div className="form-group m-0">
              <div className="form-control form-control-lg rounded-pill">
                <DatePicker
                  minDate={getTomorrowsDate()}
                  onChange={(e: Date) => setSearchData({ ...searchData, date: e })}
                  autoFocus={false}
                  value={searchData.date}
                  className={'datepicker-custom'}
                />
                Search at
              </div>
            </div>
            <input type="submit" onClick={(e: FormEvent) => handleSubmit(e)} className="btn btn-danger btn-lg rounded-pill header-btn" value="Search" />
          </div>
        </form>
      </div>
    </>
  )
}

export default PlanetSearchForm
