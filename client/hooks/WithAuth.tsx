'use client'
import { useRouter } from 'next/navigation'
import { FC, useEffect, useState } from 'react'
import { GetLoggedInUserFromToken } from 'services/authentication/authentication.service'
import { UserDTO } from 'services/authentication/types/authentication.type'

// eslint-disable-next-line react/display-name
const withAuth = (Component: FC<any>) => (props: any) => {
  const [user, setUser] = useState<UserDTO>({})
  const router = useRouter()

  useEffect(() => {
    const tokenUser = GetLoggedInUserFromToken()
    setUser(tokenUser as UserDTO)
    if (!tokenUser || !tokenUser.username) {
      router.push('/login')
    }
  }, [])

  return <Component {...props} user={user} />
}

withAuth.displayName = 'withAuth'
export default withAuth
