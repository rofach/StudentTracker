import type { AuthSessionDto, CurrentUserDto, LoginRequestDto } from "../types/api"
import { fetchJson, postJson } from "./http"

export function login(dto: LoginRequestDto): Promise<AuthSessionDto> {
  return postJson<AuthSessionDto>("/auth/login", dto)
}

export function getCurrentUser(): Promise<CurrentUserDto> {
  return fetchJson<CurrentUserDto>("/auth/me")
}
