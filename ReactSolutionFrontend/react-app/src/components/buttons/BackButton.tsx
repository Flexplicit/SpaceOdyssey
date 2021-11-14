import React, { FormEvent, MouseEventHandler } from 'react'
import { useHistory } from 'react-router'

const BackButton = () => {

    let history = useHistory()

    const onClickHandle = (e: FormEvent<HTMLButtonElement>) => {
        e.preventDefault();
        console.log("hello")
        console.log(history);
        history.goBack();
    }


    return (
        <button onClick={(e) => onClickHandle(e)} type="button" className="btn btn-primary fa-sm hvr-grow" >
            <i className="fas fa-arrow-left"></i>
            Back
        </button >

    )
}

export default BackButton
