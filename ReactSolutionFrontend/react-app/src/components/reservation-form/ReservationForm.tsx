import React, { FormEvent, useState } from 'react'

export interface IReservationFormProps {
  firstName: string
  lastName: string
}
interface IFormProps {
  submitHandler: (res: IReservationFormProps) => void
}

const ReservationForm = ({ submitHandler: parentSubmitHandler }: IFormProps) => {
  const [formState, setFormState] = useState({} as IReservationFormProps)

  const submitHandle = (e: FormEvent) => {
    e.preventDefault()
    parentSubmitHandler(formState)
  }

  return (
    <form>
      <div className="mb-3">
        <label htmlFor="firstName" className="form-label">
          First name
        </label>
        <input type="text" onChange={(e) => setFormState({ ...formState, firstName: e.target.value })} className="form-control" id="firstName" />
      </div>

      <div className="mb-3">
        <label htmlFor="lastName" className="form-label">
          Last name
        </label>
        <input type="text" onChange={(e) => setFormState({ ...formState, lastName: e.target.value })} className="form-control" id="lastName" />
      </div>

      <button type="submit" onClick={submitHandle} className="btn btn-primary">
        Reserve
      </button>
    </form>
  )
}

export default ReservationForm
