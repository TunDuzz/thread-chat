import axios from 'axios';

const API_URL = 'http://localhost:5044/api/auth'; // Updated to match backend port

export interface LoginRequest {
  usernameOrEmail: string;
  password: string;
}

export interface RegisterRequest {
  username?: string;
  phoneNumber: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  gender?: string;
  dateOfBirth?: string;
}

export interface AuthResponse {
  id: string;
  username: string;
  email: string;
  accessToken: string;
  expiresAt: string;
  refreshToken: string;
}

export const authService = {
  login: async (data: LoginRequest) => {
    const response = await axios.post<AuthResponse>(`${API_URL}/login`, data);
    return response.data;
  },

  register: async (data: RegisterRequest) => {
    const response = await axios.post<AuthResponse>(`${API_URL}/register`, data);
    return response.data;
  },

  logout: () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('user');
  },

  setToken: (token: string) => {
    localStorage.setItem('accessToken', token);
  },

  setUser: (user: any) => {
    localStorage.setItem('user', JSON.stringify(user));
  },

  getUser: () => {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }
};
