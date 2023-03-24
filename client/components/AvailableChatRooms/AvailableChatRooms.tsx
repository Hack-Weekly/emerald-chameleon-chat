import React from 'react'
import styles from './AvailableChatRooms.module.scss'
import chatRooms from './mockData.json'

function AvailableChatRooms() {
 
  return (
    <div>
      <h2>Available Chat Rooms</h2>
      <div>This will display the availabe rooms</div>
      <ChatRoomList />
    </div>
  )
}

function ChatRoomList() {
  return (
    <div>
      <ul>
        {chatRooms.map((room, index) => (
          <li key={index}>{room.name}</li>
        ))}
      </ul>
    </div>
  )
}

export default AvailableChatRooms
