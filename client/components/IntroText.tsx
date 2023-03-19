import React from 'react'
import styles from '@styles/introText.module.scss'

function IntroText() {
  return (
    <div className={styles.introText}>
      <p>{'Hi there!'}</p>
      <p>{'Chat with friends'}</p>
      <p>{'All around the world!'}</p>
    </div>
  )
}

export default IntroText
