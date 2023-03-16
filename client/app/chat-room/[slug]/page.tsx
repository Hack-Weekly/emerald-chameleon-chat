export default async function ChatRoomSlug({ params }: any) {
  const { page } = params

  return (
    <div>
      <h1>Welcome to page ${page}</h1>
    </div>
  )
}

export async function generateStaticParams() {
  const pages = ['1', '2', '3', '4', '5', '6']
  return pages.map((page) => ({ params: { page } }))
}
