import ChameleonGraphic from '@components/ChameleonGraphic/ChameleonGraphic'
import IntroText from '@components/IntroText/IntroText'
import LoginAndRegisterHandler from '@components/LoginAndRegisterHandler/LoginAndRegisterHandler'
import styles from '@styles/App.module.scss'

export default function Index() {
  return (
    <div className={styles.homeLayout}>
      <ChameleonGraphic />
      <IntroText />
      <LoginAndRegisterHandler />
    </div>
  )
}
