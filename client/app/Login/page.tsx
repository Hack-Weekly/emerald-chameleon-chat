import ChameleonGraphic from '@components/ChameleonGraphic'
import IntroText from '@components/IntroText'
import LoginForm from '@components/Login/LoginForm'
import styles from '@styles/App.module.scss'
import Link from 'next/link'

export default function Index() {
  return (
    <div className={styles.homeLayout}>
      <ChameleonGraphic />
      <IntroText />
      <LoginForm />
    </div>
  )
}
