import ChameleonGraphic from '@components/ChameleonGraphic'
import IntroText from '@components/IntroText'
import styles from '@styles/App.module.scss'
import Image from 'next/image'

export default function Index() {
  return (
    <div className={styles.homeLayout}>
      <ChameleonGraphic />
      <IntroText />
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
    </div>
  )
}
