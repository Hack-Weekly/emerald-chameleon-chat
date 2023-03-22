'use client'

import React, { useState } from 'react'
import ChameleonGraphic from '@components/ChameleonGraphic'
import styles from './EmailConfirmation.module.scss'
import Link from 'next/link'

function EmailConfirmation() {
  console.log('Email Confirmed rendered')
  const [showForm, setShowForm] = useState(true)
  const [responseMessage, setResponseMessage] = useState('')
  const [showSuccessOrFailureMessage, setShowSuccessOrFailureMessage] =
    useState(false)

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
        {isSuccess && (
          <Link href={hrefText} className={styles.messageBox__link}>
            {buttonText}
          </Link>
        )}
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

    const handleSubmit = (e: { preventDefault: () => void }) => {
      e.preventDefault()

      // <<<<<<<<<<<<<<<<
      // for testing UI until actual http request is set up
      if (confirmationCodeInput === '123') {
        setResponseMessage('success')
      } else {
        setResponseMessage('failure')
      }
      setShowForm(false)
      setShowSuccessOrFailureMessage(true)
      // >>>>>>>>>>>>>>>>>

      // send http request to the backend confirmation url
      // const baseUrl = '/api/Users/Confirmation'
      // const options = {
      //   method: 'GET',
      //   headers: {}
      // }
      // try {
      //   const response = await fetch(baseUrl, options)
      //   const data = await response.json()
      //
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
