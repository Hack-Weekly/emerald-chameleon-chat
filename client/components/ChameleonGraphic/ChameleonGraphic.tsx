import React from 'react'
import Image from 'next/image'

function ChameleonGraphic() {
  return (
    <Image
      priority
      src="/images/chameleon.svg"
      alt="Chameleon Logo"
      width={100}
      height={100}
    />
  )
}

export default ChameleonGraphic
