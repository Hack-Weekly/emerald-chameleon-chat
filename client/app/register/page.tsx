import RegisterForm from '@components/LoginAndRegisterHandler/Register/RegisterForm'
import ChameleonGraphic from '@components/ChameleonGraphic/ChameleonGraphic'
import IntroText from '@components/IntroText/IntroText'
import styles from '@styles/Register.module.scss'


const Register = () => {
  return (
    <div className={styles.pageLayout}>
      <ChameleonGraphic />
      <IntroText />
      <RegisterForm />
    </div>
  )
}

export default Register
