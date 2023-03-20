import ChameleonGraphic from '@components/ChameleonGraphic'
import IntroText from '@components/IntroText/IntroText'
import StartSwiperAndLinks from '@components/StartSwiperAndLinks'
import styles from '@styles/App.module.scss'

export default function Index() {
  return (
    <div className={styles.homeLayout}>
      <ChameleonGraphic />
      <IntroText />
      <StartSwiperAndLinks />
    </div>
  )
}
