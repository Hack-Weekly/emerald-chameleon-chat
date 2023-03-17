import '@styles/globals.scss'
import Image from 'next/image'

interface Props {
  children: React.ReactNode
}

export default async function RootLayout({ children }: Props) {
  return (
    <html>
      <head>
        <meta charSet="utf-8" />
        <meta
          name="viewport"
          content="width=device-width, initial-scale=1, maximum-scale=1"
        />
        <meta name="description" content="Go-Events" />
        <link rel="icon" type="image/x-icon" href="/icons/favicon.ico" />
      </head>
      <body>
        <main>
          <div>{children}</div>
        </main>
        <footer>
          <div>
            <Image
              priority
              src="/images/logo.png"
              alt="Chameleon Logo"
              width={20}
              height={20}
            />
            <p>Emerald Chat</p>
          </div>
          <p>2023</p>
        </footer>
      </body>
    </html>
  )
}
