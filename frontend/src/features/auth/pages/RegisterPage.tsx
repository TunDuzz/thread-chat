import React from 'react';
import AuthLayout from '../components/AuthLayout';
import RegisterForm from '../components/RegisterForm';

const RegisterPage: React.FC = () => {
  return (
    <AuthLayout 
      title="Join ThreadChat" 
      subtitle="Tạo tài khoản mới để bắt đầu kết nối"
    >
      <RegisterForm />
    </AuthLayout>
  );
};

export default RegisterPage;
