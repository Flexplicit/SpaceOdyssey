import React, { FormEvent, useEffect, useState } from 'react'
import { BaseServices } from '../../services/base-service'
import FormErrorMessage from '../errors/FormErrorMessage'
import { ISearchData } from './ISearchData'
import DatePicker from 'react-date-picker'
import { useHistory } from 'react-router'

const PlanetSearchForm = () => {
  const [planetState, setPlanetState] = useState([] as string[])
  const [searchData, setSearchData] = useState({ from: '', to: '', date: new Date() } as ISearchData)
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
    if (currentErrors.length === 0) {
      history.push(`/search/planetroute/${searchData.from}/${searchData.to}/${searchData.date.toISOString()}`)
    } else {
      setErrors([...currentErrors])
    }
  }

  const fetchPlanets = async () => {
    var result = await BaseServices.GetAllAsync('/planet')
    if (result.statusCode === 200) {
      setPlanetState(result.data as string[])
    }
  }

  useEffect(() => {
    fetchPlanets()
  }, [])

  return (
    <>
      <FormErrorMessage errors={errors} />

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
                <DatePicker onChange={(e: Date) => setSearchData({ ...searchData, date: e })} autoFocus={false} value={searchData.date} className={'datepicker-custom'} />
                Departing date
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
