import jwt_decode from 'jwt-decode'
import { useEffect, useState } from 'react'
import { UserDTO } from 'services/authentication/types/authentication.type'

export default function useUser() {
  const [user, setUser] = useState<UserDTO>({})

  useEffect(() => {
    const accessToken = localStorage.getItem('accessToken')
    if (accessToken) {
      const decodedToken: any = jwt_decode(accessToken)

      setUser({ ...user, username: decodedToken.Username })
    }
  }, [])

  return user
}
