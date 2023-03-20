'use client'
import { useState } from 'react'
import Link from 'next/link'
import styles from './LoginForm.module.scss'

type LoginValues = {
  user: string
  password: string
}

export default function LoginForm() {
  const [formValues, setFormValues] = useState<LoginValues>({
    user: '',
    password: '',
  })

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormValues({
      ...formValues,
      [name]: value
    })
  }

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()

    // Send login request
    try {
      const res = await fetch('api/login', {
        method: 'POST',
        body: JSON.stringify(formValues)
        // headers: {
        //   "Content-Type": "application/json"
        // }
      })
      const data = await res.json()

      if (res.ok) {
        // redirect to messages screen after login
        // router.push('/messages')
      } else {
        alert(data.message)
      }
    } catch (error) {
      console.log(error)
    }
  }

  return (
    <form onSubmit={handleSubmit} className={styles.formWrapper}>
      <h2 className={styles.formTitle}>Login</h2>
      <label htmlFor="user">User/ID</label>
      <input 
        id="user" 
        type="text"
        name="user"
        value={formValues.user}
        onChange={handleInputChange}
      />
      <label htmlFor="password">Password</label>
      <input 
        id="password"
        type="password"
        name="password"
        value={formValues.password}
        onChange={handleInputChange}
      />
      <div className={styles.buttonContainer}>
        {/* buttons not functional yet */}
        <button className={styles.cancelButton}>
          <Link href="/">Cancel</Link>
        </button>
        <button className={styles.loginButton} type="submit">
          <Link href="/messages">Login</Link>
        </button>
      </div>
    </form>
  )
}