'use client'
import React, { useEffect, useState } from 'react'
import styles from './AvailableChatRooms.module.scss'
import chatRooms from './mockData.json'
import Link from 'next/link'
import type { ChatRoom } from 'types/data'
import withAuth from 'hooks/WithAuth'
import { UserDTO } from 'services/authentication/types/authentication.type'
import type { HubConnection } from '@microsoft/signalr'
import useSignalR from '@functions/useSignalR'

type chatRooms = ChatRoom[]

const AvailableChatRooms = (props: { user: UserDTO }) => {
  const username = JSON.stringify(props?.user?.username)
  // HubConnection.invoke('GetActiveChatRooms')
  // connection.on("activeRoomsMessage")

  const [chatRooms, setChatRooms] = useState<string[]>([])

  //first get the HubUrl based on what page we are on
  const HubUrl = `chatHub`

  //then use the useSignalR hook to connect to the Hub
  const SignalRConnection = useSignalR(HubUrl)
  
  

  // useEffect(() => {
  //   if (!SignalRConnection) {
  //     console.log('SignalRConnection is undefined')
  //     return
  //   }
  //   const rooms = SignalRConnection.invoke('GetActiveChatRooms')
  //   console.log('rooms: ', rooms)
  //   // SignalRConnection.on('activeRoomsMessage', (room: string) => {
  //   //   console.log('rooms: ', room)
  //   //   setChatRooms([...chatRooms, room])
  //   // })
  // }, [chatRooms])

  console.log('chat rooms: ', chatRooms)

  return (
    <div className={styles.wrapper}>
      <h2>Available Chat Rooms</h2>
      <p>{`Hi, ${username}. Please choose a room to join`}</p>
      <div className={styles.listWrapper}>
        {/* <ChatRoomList /> */}
        <Link href="/createChat" className={styles.createChatLink}>
          Create Chat Room
        </Link>
      </div>
      <Link href="/" className={styles.homeLink}>
        Home
      </Link>
    </div>
  )
}

// function ChatRoomList() {
//   // HubConnection.invoke('GetActiveChatRooms')
//   // connection.on("activeRoomsMessage")

//   const [chatRooms, setChatRooms] = useState<string[]>([])

//   //first get the HubUrl based on what page we are on
//   const HubUrl = `chatHub`

//   //then use the useSignalR hook to connect to the Hub
//   const SignalRConnection = useSignalR(HubUrl)

//   useEffect(() => {
//     if (!SignalRConnection) {
//       console.log('SignalRConnection is undefined')
//       return
//     }
//     SignalRConnection.invoke('GetActiveChatRooms')
//     SignalRConnection.on("activeRoomsMessage", (message: string) => {
//       setChatRooms([...chatRooms, message])
//     })
//   }, [chatRooms])
//   console.log(chatRooms)

//   return (
//     <ul>
//       {chatRooms.map((room, index) => (
//         <li key={index}>{room}</li>
//       ))}
//     </ul>
//     // room.isActive && (
//     //   <li key={room.creatorId}>
//     //     <Link href={`/chat-room/${room.creatorId}`}>
//     //       <div className={styles.roomInfo}>
//     //         <h3>{room.name}</h3>
//     //         <p>{room.description}</p>
//     //       </div>
//     //     </Link>
//     //   </li>
//     // )
//    )
// }


export default withAuth(AvailableChatRooms)
