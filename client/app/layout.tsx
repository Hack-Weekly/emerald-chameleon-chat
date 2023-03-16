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
      <body style={{ margin: 0, backgroundColor: '#f1f1f1' }}>
        <main>
          <div style={{ backgroundColor: 'rgba(30,20,10,0.4)' }}>{children}</div>
        </main>
      </body>
    </html>
  )
}
