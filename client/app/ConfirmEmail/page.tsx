'use client'

import React, { useState } from 'react'
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
      message = 'Your email has been confirmed!'
      buttonText = 'Login'
      hrefText = '/login'
      isSuccess = true
    } else if (responseMessage === 'failure') {
      message = 'Oops! Your email could not be confirmed.'
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

    const handleClick = (e: any) => {
      console.log(e.target.value)

      // <<<<<<<<<<<<<<<<
      // for testing UI until actual http request is set up
      if (e.target.value === '123') {
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
      <div className={styles.messageBoxWrapper}>
        <h3>Click to confirm your email.</h3>
        <button
          className={styles.messageBox__link}
          type="submit"
          onClick={handleClick}
        >
          Confirm Email
        </button>
        {/* These Buttons are just for testing */}
        <div>
          <button onClick={handleClick} value="123">
            Test success
          </button>
          <button onClick={handleClick} value="456">
            Test failure
          </button>
        </div>
        
        {/* This is if user is supposed to enter a code */}
        {/* <div className={styles.instructions}>
          <p>
            {'Please enter the confirmation code that was emailed to you below.'}
          </p>
        </div>
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
        </form> */}
      </div>
    )
  }

  return (
    <div className={styles.pageWrapper}>
      <h1>Email Confirmation</h1>
      {showForm && <ConfirmationCodeForm />}
      {showSuccessOrFailureMessage && <SuccessOrFailureHandler />}
      <Link className={styles.homeLink} href="/">Back to Home</Link>
    </div>
  )
}

export default EmailConfirmation
