export interface RegisterDTO {
  name: string
  email: string
  username: string
  password: string
  mobile: string
}

export interface LoginDTO {
  email?: string
  username?: string
  password: string
}

export interface LoginResponseDTO {
  accessToken: string
  refreshToken: string
}

export interface UserDTO {
  id?: string
  name?: string
  email?: string
  username?: string
}
