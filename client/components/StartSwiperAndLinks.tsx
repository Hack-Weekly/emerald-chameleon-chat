import React from 'react'
import Image from 'next/image'
import styles from '@styles/startSwiperAndLinks.module.scss'

function StartSwiperAndLinks() {
  return (
    <div className={styles.startContainer}>
      <div className={styles.slider}>
        <span>Start</span>
        <Image
          priority
          src="/images/right-arrow.svg"
          alt="arrow"
          width={70}
          height={70}
        />
      </div>
      <div className={styles.links}>
        <p>Log In</p>
        <p>Create Account</p>
      </div>
    </div>
    
  )
}

export default StartSwiperAndLinks