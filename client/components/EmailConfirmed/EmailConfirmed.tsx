import React from 'react'
import ChameleonGraphic from '@components/ChameleonGraphic'
import styles from './EmailConfirmed.module.scss'

// ------------ DESCRIPTION AND CRITERIA ----------
// When users register for an account, they must confirm their email. Need a nice looking page that sends an httprequest to /api/Users/Confirmation and handles the success/failure

// Component submits httprequest with provided confirmation code in url query string & handles the success/response

//   Link for user to navigate back to login screen on successful response
// ------------------------------------------------

function EmailConfirmed() {
  return (
    <div className={styles.pageWrapper}>
      <ChameleonGraphic />
      <h1>Email Confirmation</h1>
      <div>
        <form className={styles.formWrapper}>
          <label htmlFor="confirmationCode">Confirmation code:</label>
          <input type="text" id="confirmationCode" />
          <div className={styles.buttons}>
            <button className={styles.cancelButton}>Cancel</button>
            <button className={styles.submitButton} type="submit">
              Submit
            </button>
          </div>
        </form>
      </div>
      <div>
        <p className={styles.instructionsText}>
          {'A confirmation code was sent to the email you provided.'}
        </p>
        <p className={styles.instructionsText}>
          {'Please enter that code below in order to confirm your email.'}
        </p>
      </div>
    </div>
  )
}

export default EmailConfirmed
