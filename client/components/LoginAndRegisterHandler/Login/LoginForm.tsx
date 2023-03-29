'use client'
import {
  Login,
  SaveTokenToLocalStorage,
} from 'services/authentication/authentication.service'
import { useEffect, useState } from 'react'
import { LoginDTO, UserDTO } from 'services/authentication/types/authentication.type'
import useUser from 'hooks/useUser'
import { useRouter } from 'next/navigation'
import styles from './LoginForm.module.scss'
import Link from 'next/link'

const LoginForm = () => {
  const [userDTO, setUserDTO] = useState<LoginDTO>({
    email: '',
    username: '',
    password: '',
  })

  const loggedInUser: UserDTO = useUser()
  const router = useRouter()

  const handleSubmit = async (e: any) => {
    const tokens = await Login(userDTO)
    SaveTokenToLocalStorage(tokens)
  }

  const handleChange = (e: any) => {
    setUserDTO({ ...userDTO, [e.target.name]: e.target.value })
  }

  useEffect(() => {
    if (loggedInUser && loggedInUser.username) {
      router.push('/profile')
    }
  })

  return (
    <div  className={styles.componentWrapper}>
      <h2 className={styles.title}>Welcome Back!</h2>
      <form className={styles.formWrapper} onSubmit={(e) => handleSubmit(e)}>
        <input
          type="text"
          name="username"
          placeholder="Username"
          required
          onChange={(e) => handleChange(e)}
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          required
          onChange={(e) => handleChange(e)}
        />
        <input
          type="password"
          name="password"
          placeholder="Password"
          required
          onChange={(e) => handleChange(e)}
        />
        <div className={styles.buttonContainer}>
          <Link href="/" className={styles.cancelButton}>
            Cancel
          </Link>

          <button className={styles.loginButton} type="submit">
            Login
          </button>
        </div>
      </form>
    </div>
  )
}

export default LoginForm
