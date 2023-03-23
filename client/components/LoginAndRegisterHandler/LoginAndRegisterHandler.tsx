'use client'

import React from 'react'
import Image from 'next/image'
import styles from '@styles/startSwiperAndLinks.module.scss'
import Link from 'next/link'
import { useEffect, useRef } from 'react'

function LoginAndRegisterHandler() {
  // Divs to listen for clicks on
  const registerRef = useRef<HTMLDivElement>(null)
  const registerCompRef = useRef<HTMLDivElement>(null)
  const loginRef = useRef<HTMLDivElement>(null)
  const loginCompRef = useRef<HTMLDivElement>(null)
  const componentRef = useRef<HTMLDivElement>(null)

  // Click Events

  // Click event handlers get the attribute visibility of the divs they are listening to
  // If the attribute is hidden, they set it to visible and vice versa
  // This is to toggle the visibility of the divs
  const handleClickSlider = (e: MouseEvent) => {
    if (e.target === loginRef.current) {
      const loginAttr = loginCompRef.current?.getAttribute('style')
      const regAttr = registerCompRef.current?.getAttribute('style')
      if (loginAttr === 'visibility: hidden') {
        loginCompRef.current?.setAttribute('style', 'visibility: visible')
      } else {
        loginCompRef.current?.setAttribute('style', 'visibility: hidden')
      }
      if (regAttr === 'visibility: visible') {
        registerCompRef.current?.setAttribute('style', 'visibility: hidden')
      }
    }
  }

  const handleClickRegister = (e: MouseEvent) => {
    if (e.target === registerRef.current) {
      const loginAttr = loginCompRef.current?.getAttribute('style')
      const regAttr = registerCompRef.current?.getAttribute('style')
      if (regAttr === 'visibility: hidden') {
        registerCompRef.current?.setAttribute('style', 'visibility: visible')
      } else {
        registerCompRef.current?.setAttribute('style', 'visibility: hidden')
      }
      if (loginAttr === 'visibility: visible') {
        loginCompRef.current?.setAttribute('style', 'visibility: hidden')
      }
    }
  }

  const handleClickOutside = (e: MouseEvent) => {
    if (
      e.target === componentRef.current &&
      e.target !== loginCompRef.current &&
      e.target !== registerCompRef.current &&
      e.target !== loginRef.current &&
      e.target !== registerRef.current
    ) {
      registerCompRef.current?.setAttribute('style', 'visibility: hidden')
      loginCompRef.current?.setAttribute('style', 'visibility: hidden')
    }
  }

  // UseEffect

  // mounting all of the event listeners
  useEffect(() => {
    // first set the visibility of the register and login components to hidden
    loginCompRef.current?.setAttribute('style', 'visibility: hidden')
    registerCompRef.current?.setAttribute('style', 'visibility: hidden')

    componentRef.current?.addEventListener('click', handleClickSlider)
    componentRef.current?.addEventListener('click', handleClickRegister)
    componentRef.current?.addEventListener('click', handleClickOutside)
  }, [])

  //TODO Create Register Component
  //TODO SCSS: Make the components be cenetered over the slider

  return (
    <div className={styles.startContainer} ref={componentRef}>
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
