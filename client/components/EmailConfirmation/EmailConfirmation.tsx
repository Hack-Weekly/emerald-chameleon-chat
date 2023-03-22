'use client'

import React, { useState } from 'react'
import ChameleonGraphic from '@components/ChameleonGraphic'
import styles from './EmailConfirmation.module.scss'
import Link from 'next/link'

function EmailConfirmation() {
  console.log('Email Confirmed rendered')
  const [showForm, setShowForm] = useState(true)
 
  const [responseMessage, setResponseMessage] = useState('')
  const [showSuccessOrFailureMessage, setShowSuccessOrFailureMessage] = useState(false)


  function SuccessOrFailureHandler() {
    let message = ''
    let buttonText = ''
    let hrefText = ''
    let isSuccess = false
    if (responseMessage === 'success') {
      message = 'Yay! Email has been confirmed'
      buttonText = 'Login'
      hrefText = '/Login'
      isSuccess = true
    } else if (responseMessage === 'failure') {
      message = 'Email could not be verified'
      buttonText = 'Try Again'
      isSuccess = false
    }

    const handleTryAgainClick = () => {
      setShowSuccessOrFailureMessage(false)
      setShowForm(true)
    }

    return (
      <div className={styles.messageBoxWrapper}>
        <h3>{message}</h3>
        {isSuccess && <Link href={hrefText} className={styles.messageBox__link}>
          {buttonText}
          </Link>
        }
        {!isSuccess && (
          <button className={styles.messageBox__link} onClick={handleTryAgainClick}>
            {buttonText}
          </button>
        )}
      </div>
    )
  }

  const ConfirmationCodeForm = () => {
    const [confirmationCodeInput, setConfirmationCodeInput] = useState('')
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
      console.log('submit button clicked')
      if (confirmationCodeInput === 'good') {
        setResponseMessage('success')
      } else {
        setResponseMessage('failure')
      }
      setShowForm(false)
      setShowSuccessOrFailureMessage(true)
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
            value={confirmationCodeInput}
            onChange={(e) => setConfirmationCodeInput(e.currentTarget.value)}
          />
          <p>Please enter your code</p>
          <div className={styles.button}>
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
      {showSuccessOrFailureMessage && <SuccessOrFailureHandler />}
    </div>
  )
}

export default EmailConfirmation