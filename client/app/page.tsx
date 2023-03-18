import LoginForm from 'app/LoginPage/LoginForm'
import styles from '@styles/App.module.scss'

export default function Index() {
  return (
    <div>
      <div className={styles.header}>
        <h1 className={styles.h1}> This is the / route</h1>
      </div>
      <div>
        <LoginForm />
      </div>
    </div>
  )
}
