'use client'
import AvailableChatRooms from '@components/AvailableChatRooms/AvailableChatRooms'
import withAuth from 'hooks/WithAuth'
import { UserDTO } from 'services/authentication/types/authentication.type'

const Profile = (props: { user: UserDTO }) => {
  return (
    <div>
      <h1>Your Profile?</h1>
      <h2>{JSON.stringify(props?.user?.username)}</h2>
      <AvailableChatRooms />
    </div>
  )
}

export default withAuth(Profile)
