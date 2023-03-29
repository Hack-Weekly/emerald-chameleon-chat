'use client'
import AvailableChatRooms from '@components/AvailableChatRooms/AvailableChatRooms'
import withAuth from 'hooks/WithAuth'
import { UserDTO } from 'services/authentication/types/authentication.type'
import styles from '@styles/Register.module.scss'

const Profile = (props: { user: UserDTO }) => {
  const username = props?.user?.username
  return (
    <div className={styles.pageLayout}>
      <h2>Hi {username}</h2>
      <AvailableChatRooms name={username} />
    </div>
  )
}

export default withAuth(Profile)
