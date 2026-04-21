const API_BASE = "/api"

type QueryValue = string | number | boolean | null | undefined

function buildUrl(path: string, query?: Record<string, QueryValue>): string {
  const url = new URL(path.startsWith("http") ? path : `${API_BASE}${path}`, window.location.origin)

  if (!query) {
    return url.toString()
  }

  for (const [key, value] of Object.entries(query)) {
    if (value === null || value === undefined || value === "") {
      continue
    }

    url.searchParams.set(key, String(value))
  }

  return url.toString()
}

export class ApiError extends Error {
  status: number

  constructor(message: string, status: number) {
    super(message)
    this.name = "ApiError"
    this.status = status
  }
}

export async function fetchJson<TResponse>(
  path: string,
  init?: RequestInit,
  query?: Record<string, QueryValue>,
): Promise<TResponse> {
  const response = await fetch(buildUrl(path, query), {
    ...init,
    headers: {
      "Content-Type": "application/json",
      ...(init?.headers ?? {}),
    },
  })

  if (!response.ok) {
    let message = `Помилка запиту (${response.status})`

    try {
      const maybeProblem = (await response.json()) as { title?: string; detail?: string }
      if (maybeProblem.detail || maybeProblem.title) {
        message = maybeProblem.detail ?? maybeProblem.title ?? message
      }
    } catch {
    }

    throw new ApiError(message, response.status)
  }

  if (response.status === 204) {
    return undefined as TResponse
  }

  return (await response.json()) as TResponse
}

export function postJson<TResponse>(path: string, body: unknown): Promise<TResponse> {
  return fetchJson<TResponse>(path, {
    method: "POST",
    body: JSON.stringify(body),
  })
}

export function putJson<TResponse>(path: string, body: unknown): Promise<TResponse> {
  return fetchJson<TResponse>(path, {
    method: "PUT",
    body: JSON.stringify(body),
  })
}

export function patchJson<TResponse>(path: string, body: unknown): Promise<TResponse> {
  return fetchJson<TResponse>(path, {
    method: "PATCH",
    body: JSON.stringify(body),
  })
}

export function deleteJson(path: string): Promise<void> {
  return fetchJson<void>(path, { method: "DELETE" })
}
