import React from 'react'
import styles from './AvailableChatRooms.module.scss'
import chatRooms from './mockData.json'
import Link from 'next/link'
import type { ChatRoom } from 'types/data'

type chatRooms = ChatRoom[]

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
      {chatRooms.map(
        (room) =>
          room.isActive && (
            <li key={room.creatorId}>
              <Link href={`/chat-room/${room.creatorId}`}>
                <div className={styles.roomInfo}>
                  <h3>{room.name}</h3>
                  <p>{room.description}</p>
                </div>
              </Link>
            </li>
          )
      )}
    </ul>
  )
}

export default AvailableChatRooms
