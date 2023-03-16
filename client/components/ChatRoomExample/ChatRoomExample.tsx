'use client'

import styles from './ChatRoomExample.module.scss'
import { useEffect, useState } from 'react'
import type { HubConnection } from '@microsoft/signalr'
import useSignalR from '@functions/useSignalR'

type Props = {
  page: string
}

const ChatRoomExample = (page: Props) => {
  const [message, setMessage] = useState<string>('')
  const [messages, setMessages] = useState<string[]>([])

  //first get the HubUrl based on what page we are on
  const HubUrl = ''

  //then use the useSignalR hook to connect to the Hub
  const SignalRConnection = useSignalR(HubUrl)

  useEffect(() => {
    if (!SignalRConnection) {
      console.log('SignalRConnection is undefined')
      return
    }
    SignalRConnection.on('ReceiveMessage', (message: string) => {
      setMessages([...messages, message])
    })
  }, [messages])

  const sendMessage = () => {
    if (!SignalRConnection) {
      console.log('SignalRConnection is undefined')
      return
    }
    SignalRConnection.send('SendMessage', message)
    setMessage('')
  }

  return (
    <div className={styles.chatRoomExample}>
      <h1>Chat Room Example</h1>
      <div>
        {SignalRConnection ? (
          <h2>Connection Found</h2>
        ) : (
          <h2>Counnection Not Found</h2>
        )}
      </div>
      <div className={styles.chatRoomExample__messages}>
        {messages.map((message, index) => (
          <div key={index}>{message}</div>
        ))}
      </div>
      <div className={styles.chatRoomExample__input}>
        <input
          type="text"
          value={message}
          onChange={(e) => setMessage(e.target.value)}
        />
        <button onClick={sendMessage}>Send</button>
      </div>
    </div>
  )
}

export default ChatRoomExample
