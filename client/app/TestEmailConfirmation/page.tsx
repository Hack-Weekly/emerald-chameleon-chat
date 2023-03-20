import EmailConfirmed from '@components/EmailConfirmed/EmailConfirmed'

// ------------ DESCRIPTION AND CRITERIA ----------
// When users register for an account, they must confirm their email. Need a nice looking page that sends an httprequest to /api/Users/Confirmation and handles the success/failure

// Component submits httprequest with provided confirmation code in url query string & handles the success/response

//   Link for user to navigate back to login screen on successful response
// ------------------------------------------------

function TestEmailConfirmation() {


  return (
    <div>
      <EmailConfirmed />
    </div>
  )
}

export default TestEmailConfirmation
