export type ChatRoom = {
  name: string
  description: string
  createdDate: string
  updatedDate: string
  creatorId: number
  isActive: boolean
}

export type Message = {
  name: string
  userId: string
  messageBody: string
  createdDate: string
}
