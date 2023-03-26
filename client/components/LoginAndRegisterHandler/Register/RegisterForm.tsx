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

const RegisterForm = () => {
  const [userDTO, setUserDTO] = useState<RegisterDTO>({
    name: '',
    email: '',
    username: '',
    password: '',
  })

  const handleSubmit = async (e: any) => {
    e.preventDefault()
    const tokens = await Register(userDTO)
    SaveTokenToLocalStorage(tokens)
  }

  const handleChange = (e: any) => {
    setUserDTO({ ...userDTO, [e.target.name]: e.target.value })
  }

  const loggedInUser: UserDTO = useUser()
  const router = useRouter()
  useEffect(() => {
    if (loggedInUser && loggedInUser.username) {
      router.push('/chat-room')
    }
  })

  return (
    <div>
      <h1>Register</h1>
      <form onSubmit={(e) => handleSubmit(e)}>
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
          name="mobile"
          placeholder="Mobile"
          onChange={(e) => handleChange(e)}
        />
        <input
          type="text"
          name="name"
          placeholder="Name"
          onChange={(e) => handleChange(e)}
        />
        <button type="submit">Submit</button>
      </form>
    </div>
  )
}

export default RegisterForm
