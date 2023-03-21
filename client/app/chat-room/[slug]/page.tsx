// This is a server only page. Must import client components to use hooks.
import ChatRoomExample from '@components/ChatRoomExample/ChatRoomExample'

export interface Props {
  params: {
    page: string
  }
}

export default async function ChatRoomSlug(props: Props) {
  const { params } = props

  return (
    <div>
      <h1>Welcome to page ${params.page}</h1>
      <ChatRoomExample page={params.page} />
    </div>
  )
}

export async function generateStaticParams() {
  // here's where we will have a function that gets us all the page names, example chameleon-chat-app.com/chat-room/1
  const pages = ['1', '2', '3', '4', '5', '6']
  const paths = pages.map((page) => {
    return {
      params: {
        page: page,
      },
    }
  })
  return paths
}
