'use client'
import {
  Login,
  SaveTokenToLocalStorage,
} from 'services/authentication/authentication.service'
import { useEffect, useState } from 'react'
import { LoginDTO, UserDTO } from 'services/authentication/types/authentication.type'
import useUser from 'hooks/useUser'
import { useRouter } from 'next/navigation'

const PageOneExample = () => {
  const [userDTO, setUserDTO] = useState<LoginDTO>({
    email: '',
    username: '',
    password: '',
  })

  const handleSubmit = async (e: any) => {
    e.preventDefault()
    const tokens = await Login(userDTO)
    SaveTokenToLocalStorage(tokens)
  }

  const handleChange = (e: any) => {
    setUserDTO({ ...userDTO, [e.target.name]: e.target.value })
  }

  const loggedInUser: UserDTO = useUser()
  const router = useRouter()
  useEffect(() => {
    if (loggedInUser && loggedInUser.username) {
      router.push('/profile')
    }
  })

  return (
    <div>
      <form onSubmit={(e) => handleSubmit(e)}>
        <input
          type="text"
          name="username"
          placeholder="Username"
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
          name="password"
          placeholder="Password"
          onChange={(e) => handleChange(e)}
        />
        <button type="submit">Submit</button>
      </form>
    </div>
  )
}

export default PageOneExample
