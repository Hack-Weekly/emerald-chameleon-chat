import React from 'react'
import styles from './AvailableChatRooms.module.scss'
import chatRooms from './mockData.json'
import Link from 'next/link'

function AvailableChatRooms() {
 
  return (
    <div className={styles.wrapper}>
      <h2>Available Chat Rooms</h2>
      <div className={styles.listWrapper}>
        <ChatRoomList />
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

function ChatRoomList() {
  // const url = `${process.env.NEXT_PUBLIC_API_URL}/chat-room-endpoint`
  // fetch list of chat rooms
  // set chatRooms = data.json()

  return (
    <ul>
      {chatRooms.map((room, index) => (
        <li key={index}>
          <Link href={`/chat-room/${room.id}`}>
            <div className={styles.roomInfo}>
              <h3>{room.name}</h3>
              <p>{room.description}</p>
            </div>
          </Link>
        </li>
      ))}
    </ul>
  )
}

export default AvailableChatRooms
