'use client'
import { useEffect } from 'react'

const PageOneExample = () => {
  const ExampleAPIRoute = () => {
    fetch('/api/ExampleAPIRoute').then((res) => {
      console.log(res)
    })
  }

  useEffect(() => {
    ExampleAPIRoute()
  }, [])

  const h1Text =
    "This is a client rendered page, because it does have 'use client' at the the top of this file"

  return (
    <div>
      <h1>This is the /ClientPageExample route</h1>
      <h1>{h1Text}</h1>
    </div>
  )
}

export default PageOneExample
