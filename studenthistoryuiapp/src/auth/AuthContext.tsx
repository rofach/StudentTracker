import { createContext, useContext, useEffect, useMemo, useState, type ReactNode } from "react"
import { getCurrentUser, login as loginRequest } from "../api/authApi"
import type { ApiError } from "../api/http"
import type { CurrentUserDto, LoginRequestDto } from "../types/api"
import { clearAuthToken, getAuthToken, setAuthToken } from "./authStorage"

type AuthContextValue = {
  user: CurrentUserDto | null
  isLoading: boolean
  login: (dto: LoginRequestDto) => Promise<CurrentUserDto>
  logout: () => void
}

const AuthContext = createContext<AuthContextValue | null>(null)

type AuthProviderProps = {
  children: ReactNode
}

export function AuthProvider({ children }: AuthProviderProps) {
  const [user, setUser] = useState<CurrentUserDto | null>(null)
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    const token = getAuthToken()
    if (!token) {
      setIsLoading(false)
      return
    }

    getCurrentUser()
      .then((currentUser) => {
        setUser(currentUser)
      })
      .catch((error: ApiError | unknown) => {
        clearAuthToken()
        setUser(null)
        if (error instanceof Error) {
          console.warn(error.message)
        }
      })
      .finally(() => {
        setIsLoading(false)
      })
  }, [])

  const value = useMemo<AuthContextValue>(
    () => ({
      user,
      isLoading,
      async login(dto) {
        const session = await loginRequest(dto)
        setAuthToken(session.accessToken)
        setUser(session.user)
        return session.user
      },
      logout() {
        clearAuthToken()
        setUser(null)
      },
    }),
    [isLoading, user],
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

export function useAuth() {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error("useAuth must be used inside AuthProvider")
  }

  return context
}
