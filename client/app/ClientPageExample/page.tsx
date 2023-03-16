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

  return (
    <div>
      <h1>This is the /ClientPageExample route</h1>
      <h1>
        This is a client rendered page, because it does have 'use client' at the top
        of this file
      </h1>
    </div>
  )
}

export default PageOneExample
