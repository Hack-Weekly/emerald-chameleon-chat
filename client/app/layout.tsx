import '@styles/globals.scss'

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
      </body>
    </html>
  )
}
