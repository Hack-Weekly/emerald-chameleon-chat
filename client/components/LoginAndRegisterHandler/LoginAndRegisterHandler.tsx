'use client'

import React from 'react'
import Image from 'next/image'
import styles from './LoginAndRegisterHandler.module.scss'
<<<<<<< HEAD
import LoginForm from './Login/LoginForm'
import Register from './Register/Register'
import { useEffect, useRef } from 'react'
=======
import Link from 'next/link'
>>>>>>> main

function LoginAndRegisterHandler() {
  return (
    <div className={styles.startContainer}>
      <Link href="/login">
        <div className={styles.loginLink}>
          <span className={styles.loginSpan}>Chat</span>
          <Image
            priority
            src="/images/right-arrow.svg"
            alt="arrow"
            width={70}
            height={70}
          />
        </div>
      </Link>
      <Link href="/register" className={styles.registerLink}>
        Register
      </Link>
    </div>
  )
}

export default LoginAndRegisterHandler
