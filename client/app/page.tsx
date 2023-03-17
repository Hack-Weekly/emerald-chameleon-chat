import styles from '@styles/App.module.scss'
import Image from 'next/image'

export default function Index() {
  return (
    <div className={styles.homeLayout}>
      <Image
        priority
        src="/images/chameleon.svg"
        alt="Chameleon Logo"
        width={150}
        height={150}
        className={styles.logo}
      />
      <div className={styles.introText}>
        <p>{'Hi there!'}</p>
        <p>{'Chat with friends'}</p>
        <p>{'All around the world!'}</p>
      </div>
      <div className={styles.sliderWrapper}>
        <div className={styles.slider}>Start</div>
        <p className={styles.loginLink}>{'Log In >'}</p>
      </div>
      <div className={styles.footer}>
        <div className={styles.footerTextandLogo}>
          <Image
            priority
            src="/images/chameleon.svg"
            alt="Chameleon Logo"
            width={20}
            height={20}
            className={styles.footerIcon}
          />
          <p className={styles.footerText}>Emerald Chat</p>
        </div>
        <p>2023</p>
      </div>
    </div>
  )
}
