'use client'

import React, { useState } from 'react'
import styles from './EmailConfirmation.module.scss'
import Link from 'next/link'
// import { useSearchParams } from 'next/navigation'

function EmailConfirmation() {
  const [showClickToConfirm, setShowClickToConfirm] = useState(true)
  const [showSuccessOrFailureMessage, setShowSuccessOrFailureMessage] =
    useState(false)
  const [isResponseOk, setIsResponseOk] = useState(false)
  const [isOtherError, setIsOtherError] = useState(false)

  function SuccessResponse() {
    const message = 'Your email has been confirmed!'
    const buttonText = 'Login'
    const hrefText = '/login'

    return (
      <div className={styles.messageBoxWrapper}>
        <h3>{message}</h3>
        <Link href={hrefText} className={styles.messageBox__link}>
          {buttonText}
        </Link>
      </div>
    )
  }

  function ErrorResponse() {
    const message = 'Oops! Your email could not be confirmed.'
    const buttonText = 'Try Again'
    setIsResponseOk(false)

    const handleTryAgainClick = () => {
      setShowSuccessOrFailureMessage(false)
      setIsOtherError(false)
      setShowClickToConfirm(true)
    }

    return (
      <div className={styles.messageBoxWrapper}>
        <h3>{message}</h3>
        {isOtherError && (
          <p className={styles.errorMessage}>There was a connection issue.</p>
        )}
        <button className={styles.messageBox__link} onClick={handleTryAgainClick}>
          {buttonText}
        </button>
      </div>
    )
  }

  const ConfirmEmail = () => {
    // const [confirmationCodeInput, setConfirmationCodeInput] = useState('')

    const url = `${process.env.NEXT_PUBLIC_API_URL}/Users/EmailConfirmation/`
    // const searchParams = useSearchParams()
    // const confirmationCode = searchParams?.get('confirmationCode')
    // const userName = searchParams?.get('userName')
    const testConfirmationCode = process.env.TEST_CONFIRMATION_CODE
    const testUserName = 'TestUser'
    const httpRequestQueryString = `${url}?confirmationCode=${testConfirmationCode}&userName=${testUserName}`

    const handleClick = async () => {
      try {
        const response = await fetch(httpRequestQueryString)

        if (response.status === 200) {
          setIsResponseOk(true)
        } else if (response.status === 400) {
          setIsResponseOk(false)
        } else if (response.status === 404) {
          setIsResponseOk(false)
          setIsOtherError(true)
        }
      } catch (error) {
        console.log(error)
      }

      setShowClickToConfirm(false)
      setShowSuccessOrFailureMessage(true)
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
            value={confirmationCode}
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
      {showClickToConfirm && <ConfirmEmail />}
      {showSuccessOrFailureMessage &&
        (isResponseOk ? <SuccessResponse /> : <ErrorResponse />)}
      <Link className={styles.homeLink} href="/">
        Back to Home
      </Link>
    </div>
  )
}

export default EmailConfirmation
