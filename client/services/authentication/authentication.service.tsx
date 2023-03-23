import jwt_decode from 'jwt-decode'
import {
  RegisterDTO,
  LoginResponseDTO,
  LoginDTO,
  UserDTO,
} from './types/authentication.type'

export async function Register(registerDTO: RegisterDTO) {
  const url = `${process.env.NEXT_PUBLIC_API_URL}/users/register`

  const res = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(registerDTO),
  })
  const response = await res.json()

  return response
}

export async function Login(loginDTO: LoginDTO) {
  const url = `${process.env.NEXT_PUBLIC_API_URL}/users/login`

  const res = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(loginDTO),
  })

  const response = await res.json()
  return response
}

export function SaveTokenToLocalStorage(loginResponseDTO: LoginResponseDTO) {
  localStorage.setItem('accessToken', loginResponseDTO.accessToken)
  localStorage.setItem('refreshToken', loginResponseDTO.refreshToken)
}

export function ReadTokensFromLocalStorage() {
  const accessToken = localStorage.getItem('accessToken')
  const refreshToken = localStorage.getItem('refreshToken')

  return {
    accessToken,
    refreshToken,
  }
}

export function GetLoggedInUserFromToken() {
  const accessToken = localStorage.getItem('accessToken')
  if (accessToken) {
    const decodedToken: any = jwt_decode(accessToken)

    const user: UserDTO = {
      username: decodedToken.Username,
    }
    return user
  }
}
