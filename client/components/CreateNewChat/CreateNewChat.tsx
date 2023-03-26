'use client'
import Link from 'next/link'
import { useState } from 'react'
import { useRouter } from 'next/navigation'
import styles from './CreateNewChat.module.scss'

type CreateChatValues = {
  name: string
  description: string
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
      const res = await fetch('api/create', {
        method: 'POST',
        body: JSON.stringify(formValues),
        headers: {
          'Content-Type': 'application/json'
        }
      })
      const data = await res.json()

      if (res.ok) {
        router.push('/chat-room')
      } else {
        alert(data.message)
      }
    } catch (error) {
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