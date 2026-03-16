import React from 'react';
import AuthLayout from '../components/AuthLayout';
import LoginForm from '../components/LoginForm';

const LoginPage: React.FC = () => {
  return (
    <AuthLayout 
      title="Welcome Back" 
      subtitle="Đăng nhập để tiếp tục hành trình của bạn"
    >
      <LoginForm />
    </AuthLayout>
  );
};

export default LoginPage;
