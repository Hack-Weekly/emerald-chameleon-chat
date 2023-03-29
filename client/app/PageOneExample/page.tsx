const PageOneExample = () => {
  const h1Text =
    "This is a server rendered page, because it doesn't have 'use client' this file"
  return (
    <div>
      <h1 className="h1">This is the /PageOneExample route</h1>
      <h2>{h1Text}</h2>
    </div>
  )
}

export default PageOneExample
