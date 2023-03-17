import styles from '@styles/App.module.scss'
import Image from 'next/image'

export default function Index() {
  return (
    <div className={styles.homeLayout}>
      <Image
        priority
        src="/images/chameleon.svg"
        alt="Chameleon Logo"
        width={120}
        height={120}
        className={styles.logo}
      />
      <div className={styles.introText}>
        <p>{'Hi there!'}</p>
        <p>{'Chat with friends'}</p>
        <p>{'All around the world!'}</p>
      </div>
      <div className={styles.loginContainer}>
        <div className={styles.slider}>
          <span>Start</span>
          <Image
            priority
            src="/images/right-arrow.svg"
            alt="arrow"
            width={50}
            height={50}
          />
        </div>
        <div className={styles.links}>
          <p>Log In</p>
          <p>Create Account</p>
        </div>
      </div>
      <div className={styles.footer}>
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
      </div>
      </div>
  )
}
