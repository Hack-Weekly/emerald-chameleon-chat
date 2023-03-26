import LoginForm from '@components/LoginAndRegisterHandler/Login/LoginForm'
import ChameleonGraphic from '@components/ChameleonGraphic/ChameleonGraphic'
import IntroText from '@components/IntroText/IntroText'
import styles from '@styles/Register.module.scss'

const Login = () => {
  return (
    <div className={styles.pageLayout}>
      <ChameleonGraphic />
      <IntroText />
      <LoginForm />
    </div>
  )
}

export default Login
