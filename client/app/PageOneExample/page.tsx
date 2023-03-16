const PageOneExample = () => {
  const h1Text =
    "This is a server rendered page, because it doesn't have 'use client' this file"
  return (
    <div>
      <h1>This is the /PageOneExample route</h1>
      <h1>{h1Text}</h1>
    </div>
  )
}

export default PageOneExample
