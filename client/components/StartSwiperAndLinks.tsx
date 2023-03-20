import React from 'react'
import Image from 'next/image'
import styles from '@styles/startSwiperAndLinks.module.scss'
import Link from 'next/link'

function StartSwiperAndLinks() {
  return (
    <div className={styles.startContainer}>
      <div>
        <Link className={styles.slider} href="/Register">
          <span>Register</span>
          <Image
            priority
            src="/images/right-arrow.svg"
            alt="arrow"
            width={70}
            height={70}
          />
        </Link>
      </div>
      <div>
        <Link className={styles.links} href="/Login">
          <span className={styles.linkSpan}>Log In</span>
        </Link>
      </div>
    </div>
    
  )
}

export default StartSwiperAndLinks