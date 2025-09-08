export interface User {
  userId: number;
  fullName: string;
  email: string;
  role: 'User' | 'Admin';
  isActive: boolean;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  expiresAt: string;
  role: string;
  fullName: string;
}
