'use client'

import React, { useRef, useState, useContext } from 'react'
import ChameleonGraphic from '@components/ChameleonGraphic'
import styles from './EmailConfirmed.module.scss'
import LoginForm from '@components/LoginAndRegisterHandler/Login/LoginForm'

// ------------ DESCRIPTION AND CRITERIA ----------
// When users register for an account, they must confirm their email. Need a nice looking page that sends an httprequest to /api/Users/Confirmation and handles the success/failure

// Component submits httprequest with provided confirmation code in url query string & handles the success/response

//   Link for user to navigate back to login screen on successful response
// ------------------------------------------------

function EmailConfirmed() {
  const [showForm, setShowForm] = useState(true)
  const [showSuccessMessage, setShowSuccessMessage] = useState(false)
  const [showErrorMessage, setShowErrorMessage] = useState(false)
  const [confirmationCode, setConfirmationCode] = useState('')
  const [showLoginForm, setShowLoginForm] = useState(false)
  
  function FailureMessage() {
    const handleClick = () => {
      setShowForm(true)
      setShowErrorMessage(false)
    }

    return (
      <div className={styles.failureWrapper} >
        <p>{'Your email could not be verified'}</p>
        <button onClick={handleClick}>Try Again</button>
      </div>
    )
  }

  function SuccessMessage() {
    const handleClick = () => {
      setShowSuccessMessage(false)
      setShowLoginForm(true)
    }
    return (
      <div className={styles.successWrapper}>
        <p> {'Your email has been confirmed!'}</p>
        <button onClick={handleClick}>Login</button>
      </div>
    )
  }

  const ConfirmationCodeForm = () => {
    // const baseUrl =
    //   'https://house-plants2.p.rapidapi.com/id/53417c12-4824-5995-bce0-b81984ebbd1d'

    // onSubmit, GET request to server
    // compare confirmation code entered to value on server
    // need to know parameters for GET request (userID, email, etc?)
    // if success (codes match), display success message to user
    // if failure (codes don't match), display failure message
    // if other error, display different error message
    // send http GET request to
    const handleSubmit = (e: { preventDefault: () => void }) => {
      e.preventDefault()
      setShowForm(false)
      // form.current?.setAttribute('style', 'display: none')
      console.log('submit button clicked')
      // correct confirmation code
      if (confirmationCode === '1') {
        setShowSuccessMessage(true)
        // incorrect confirmation code
      } else if (confirmationCode === '2'){
        setShowErrorMessage(true)
      }
      // const options = {
      //   method: 'GET',
      //   headers: {
      //     'X-RapidAPI-Key': '3278048d53mshaf4402d97a086bep1bbe45jsnc2254c9864fd',
      //     'X-RapidAPI-Host': 'house-plants2.p.rapidapi.com'
      //   }
      // }
      // try {
      //   const response = await fetch(baseUrl, options)
      //   const data = await response.json()
      //   console.log(data)
      // } catch (error) {
      //   console.log(error)
      // }
    }
    return (
      <div>
        <form onSubmit={handleSubmit} className={styles.formWrapper}>
          <label htmlFor="confirmationCode">Confirmation code:</label>
          <input
            type="text"
            id="confirmationCode"
            name="confirmationCode"
            value={confirmationCode}
            onChange={(e) => setConfirmationCode(e.target.value)}
          />
          <div className={styles.buttons}>
            <button className={styles.submitButton} type="submit">
              Submit
            </button>
          </div>
        </form>
        <div className={styles.instructions}>
          <p>{'A confirmation code was sent to the email you provided.'}</p>
          <p>{'Please enter that code below in order to confirm your email.'}</p>
        </div>
      </div>
    )
  }

  return (
    <div className={styles.pageWrapper}>
      <ChameleonGraphic />
      <h1>Email Confirmation</h1>
      {showForm && <ConfirmationCodeForm />}
      {showSuccessMessage && <SuccessMessage />}
      {showErrorMessage && <FailureMessage />}
      {showLoginForm && <LoginForm />}
    </div>
  )
}




export default EmailConfirmed
