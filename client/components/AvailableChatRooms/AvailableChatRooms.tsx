'use client'
import React, {  useState } from 'react'
import styles from './AvailableChatRooms.module.scss'
import Link from 'next/link'
import type { ChatRoom } from 'types/data'
import withAuth from 'hooks/WithAuth'
import { UserDTO } from 'services/authentication/types/authentication.type'
import { ReadTokensFromLocalStorage } from 'services/authentication/authentication.service'
import { HubConnectionBuilder, HttpTransportType } from '@microsoft/signalr'

// type chatRooms = ChatRoom[]

const AvailableChatRooms = (props: { user: UserDTO }) => {
  const username = props?.user?.username

  const [chatRooms, setChatRooms] = useState<string[]>([])
  const [showChatRoomList, setShowChatRoomList] = useState(false)

  //first get the HubUrl based on what page we are on
  // const HubUrl = `chatHub`

  //then use the useSignalR hook to connect to the Hub
  // const SignalRConnection = useSignalR(HubUrl)

  const handleGetRooms = async () => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl(`${process.env.NEXT_PUBLIC_HUB_URL}`, {
          skipNegotiation: true,
          transport: HttpTransportType.WebSockets,
          accessTokenFactory: () => { 
            return ReadTokensFromLocalStorage().accessToken ?? ''
          }
        })
        .build()

      await connection.start()

      connection.invoke('GetActiveChatRooms')
      connection.on('activeRoomsMessage', (chatRoomList) => {
        console.log('chatRoomList: ', chatRoomList)
        setChatRooms([...chatRoomList])
        setShowChatRoomList(true)
      })
    } catch (err) {
      console.log(err)
    }
  }

  const CreateChatRoomList = () => {
    return (
      <div className={styles.listWrapper}>
        <ul>
          {chatRooms.map((room: string, index: number) => (
            <li key={index} className={styles.roomInfo}>
              <Link href={`/chat-room/${room}`}>{room}</Link>
            </li>
          ))}
        </ul>
        <div className={styles.buttonContainer}>
          <Link href="/createChat" className={styles.createChatLink}>
            Create Chat Room
          </Link>
          <button onClick={() => setShowChatRoomList(false)}>Cancel</button>
        </div>
      </div>
    )
}

  return (
    <div>
      <div className={styles.wrapper}>
        {!showChatRoomList &&
          <button onClick={handleGetRooms} className={styles.seeChatRoomsBtn}>
            See Available Rooms
          </button>}
        {showChatRoomList && <CreateChatRoomList />}
        <Link href="/" className={styles.homeLink}>
          Home
        </Link>
      </div>
    </div>
  )
}

export default withAuth(AvailableChatRooms)
