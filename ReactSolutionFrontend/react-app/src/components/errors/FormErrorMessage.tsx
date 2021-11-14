import React from 'react'
import './FormErrorMessage.scss'

interface IPropsState {
  errors: string[]
}

const FormErrorMessage = ({ errors }: IPropsState) => {
  return (
    <>
      {errors.length > 0 ? (
        <div className="error-message">
          <div className="alert alert-danger" role="alert">
            <ul>
              {errors.map((error, index) => (
                <li key={index}>{error}</li>
              ))}
            </ul>
          </div>
        </div>
      ) : null}
    </>
  )
}

export default FormErrorMessage
