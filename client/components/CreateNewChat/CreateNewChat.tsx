'use client'

import Link from 'next/link'
import { useState } from 'react'
import { useRouter } from 'next/navigation'
import styles from './CreateNewChat.module.scss'
import { HttpTransportType, HubConnectionBuilder } from '@microsoft/signalr'
import { ReadTokensFromLocalStorage } from 'services/authentication/authentication.service'

type CreateChatValues = {
  name: string,
  description: string,
}

export default function CreateNewChat() {
  const [formValues, setFormValues] = useState<CreateChatValues>({
    name: '',
    description: '',
  })

  const router = useRouter()

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormValues({
      ...formValues,
      [name]: value,
    })
  }

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()

    try {
      const connection = new HubConnectionBuilder()
        .withUrl(`https://nas.lightshowdepot.com/chatHub`, {
          skipNegotiation: true,
          transport: HttpTransportType.WebSockets,
          accessTokenFactory: () => {
            return ReadTokensFromLocalStorage().accessToken ?? ''
          }
        })
        .build() 

      await connection.start()
      connection.invoke('CreateChatroom', formValues.name, formValues.description)
      const connectionArr = Object.entries(connection)
      connection.invoke('GetActiveChatRooms')
      connection.on('activeRoomsMessage', (chatRoomList) =>
        chatRoomList.unshift(connectionArr)
      )
      if (connectionArr[16][1] === 'Connected') {
        router.push('/profile')
      }
    } catch (error) {
      alert('Failed to create new chat, please try again.')
      router.push('/profile')
      console.log(error)
    }
  }

  return (
    <div className={styles.wrapper}>
      <div className={styles.titleWrapper}>
        <Link href="/chat-room">&#x25c4; Back</Link>
        <h2 className={styles.title}>New Chat</h2>
      </div>
      <form onSubmit={handleSubmit}>
        <input 
          id="name"
          type="text"
          name="name"
          placeholder="Name"
          required
          value={formValues.name}
          onChange={handleInputChange}
        />

        <input 
          id="description"
          type="text"
          name="description"
          placeholder="Description"
          required
          value={formValues.description}
          onChange={handleInputChange}
        />

        <div>
          <button type="submit">Create</button>
        </div>
      </form>
    </div>
  )
}