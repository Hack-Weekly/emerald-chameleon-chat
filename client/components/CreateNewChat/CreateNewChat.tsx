import Link from 'next/link'
import { useState } from 'react'
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
        // headers: {
        //   "Content-Type": "application/json"
        // }
      })
      const data = await res.json()

      if (res.ok) {
        // redirect to chatrooms screen after login
        // router.push('/chatrooms')
      } else {
        alert(data.message)
      }
    } catch (error) {
      console.log(error)
    }
  }

  return (
    <div>
      {/** /chatroom doesn't exist yet */}
      <Link href="/chatrooms">&rarr; Back</Link>
      <form onSubmit={handleSubmit}>
        <h2>New Chat</h2>

        <input 
          id="name"
          type="text"
          name="name"
          value={formValues.name}
          onChange={handleInputChange}
        />

        <input 
          id="description"
          type="text"
          name="description"
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