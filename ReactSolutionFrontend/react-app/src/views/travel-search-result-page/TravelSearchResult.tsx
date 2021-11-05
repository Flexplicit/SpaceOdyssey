import React, { useEffect } from 'react'
import { useParams } from 'react-router'
import { TravelRoutesService } from '../../services/travel-routes-service'

type SearchParams = {
  from: string
  to: string
  date: string
}
const TravelSearchResult = () => {
  var params = useParams<SearchParams>()

  let getSearchResultAsync = async () => {
    let res = await TravelRoutesService.GetTravelRoutes<>('/route', params.from, params.to, '')
    if(res.statusCode === 200){
      console.log(res.data);
    }
  }

  useEffect(() => {
    // effect
  }, [])

  return (
    <>
      AAAAAAAA
      <div>result</div>
    </>
  )
}

export default TravelSearchResult
