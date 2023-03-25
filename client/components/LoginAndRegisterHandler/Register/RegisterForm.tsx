'use client'
import {
  Register,
  SaveTokenToLocalStorage,
} from 'services/authentication/authentication.service'
import { useEffect, useState } from 'react'
import {
  RegisterDTO,
  UserDTO,
} from 'services/authentication/types/authentication.type'
import { useRouter } from 'next/navigation'
import useUser from 'hooks/useUser'
import styles from './RegisterForm.module.scss'
import Link from 'next/link'


const RegisterForm = () => {
  // const [showForm, setShowForm] = useState(true)
  const [userDTO, setUserDTO] = useState<RegisterDTO>({
    name: '',
    email: '',
    username: '',
    password: '',
    mobile: '',
  })

  const handleSubmit = async (e: any) => {
    e.preventDefault()
    const tokens = await Register(userDTO)
    SaveTokenToLocalStorage(tokens)

    e.target.reset()
    // setShowForm(false)
  }

  const handleChange = (e: any) => {
    setUserDTO({ ...userDTO, [e.target.name]: e.target.value })
  }

  // this automatically takes user to profile page upon registraion - commented out for testing
  // const loggedInUser: UserDTO = useUser()
  // const router = useRouter()
  // useEffect(() => {
  //   if (loggedInUser && loggedInUser.username) {
  //     router.push('/profile')
  //   }
  // })

  return (
    <div className={styles.componentWrapper}>
      {/* {showForm &&  */}
        <form className={styles.formWrapper} onSubmit={(e) => handleSubmit(e)}>
          <h2 className={styles.title}>Create an Account</h2>
          <input
            type="text"
            name="username"
            placeholder="Username"
            onChange={(e) => handleChange(e)}
          />
          <input
            type="text"
            name="password"
            placeholder="Password"
            onChange={(e) => handleChange(e)}
          />
          <input
            type="text"
            name="email"
            placeholder="Email"
            onChange={(e) => handleChange(e)}
          />
          <input
            type="text"
            name="name"
            placeholder="Name"
            onChange={(e) => handleChange(e)}
          />
          <input
            type="text"
            name="mobile"
            placeholder="mobile"
            onChange={(e) => handleChange(e)}
          />
          <div className={styles.buttonContainer}>
            <Link href="/" className={styles.cancelButton}>
              Cancel
            </Link>
            <button className={styles.submitButton} type="submit">
              Submit
            </button>
          </div>
        </form>
        {/* } */}
      {/* {!showForm && (
        <div>
          <h3>Account Created!</h3>
          <Link href="/login">Login</Link>
        </div> */}
      {/* )} */}
    </div>
  )
}

export default RegisterForm
