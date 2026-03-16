import React from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { Mail, Lock, ArrowRight } from 'lucide-react';
import { authService } from '../services/authService';

const LoginForm: React.FC = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = React.useState(false);
  const [error, setError] = React.useState<string | null>(null);
  const [formData, setFormData] = React.useState({
    usernameOrEmail: '',
    password: ''
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const response = await authService.login(formData);
      authService.setToken(response.accessToken);
      authService.setUser(response);
      navigate('/');
    } catch (err: any) {
      setError(err.response?.data?.error || 'Đăng nhập thất bại. Vui lòng kiểm tra lại.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      {error && (
        <div style={{ color: '#ff4d4d', fontSize: '0.9rem', marginBottom: '16px', textAlign: 'center' }}>
          {error}
        </div>
      )}

      <div className="input-group">
        <Mail size={20} style={{
          position: 'absolute',
          top: '50%',
          left: '16px',
          transform: 'translateY(-50%)',
          color: 'var(--text-muted)',
          zIndex: 1
        }} />
        <input
          type="text"
          className="input-premium"
          placeholder="Email hoặc số di động"
          style={{ paddingLeft: '48px' }}
          required
          value={formData.usernameOrEmail}
          onChange={(e) => setFormData({ ...formData, usernameOrEmail: e.target.value })}
        />
      </div>

      <div className="input-group">
        <Lock size={20} style={{
          position: 'absolute',
          top: '50%',
          left: '16px',
          transform: 'translateY(-50%)',
          color: 'var(--text-muted)'
        }} />
        <input
          type="password"
          className="input-premium"
          placeholder="Mật khẩu"
          style={{ paddingLeft: '48px' }}
          required
          value={formData.password}
          onChange={(e) => setFormData({ ...formData, password: e.target.value })}
        />
      </div>

      <div style={{ display: 'flex', justifyContent: 'flex-end', marginBottom: '32px' }}>
        <a href="#" style={{ color: 'var(--primary)', textDecoration: 'none', fontSize: '0.9rem', opacity: 0.8 }}>Bạn quên mật khẩu?</a>
      </div>

      <button
        type="submit"
        className="btn-premium"
        disabled={loading}
        style={{ width: '100%', height: '56px', fontSize: '1.1rem', display: 'flex', alignItems: 'center', justifyContent: 'center', gap: '12px', opacity: loading ? 0.7 : 1 }}
      >
        <span>{loading ? 'Đang xử lý...' : 'Đăng nhập hệ thống'}</span>
        <ArrowRight size={22} />
      </button>

      <div style={{ marginTop: '48px', textAlign: 'center' }}>
        <p style={{ color: 'var(--text-muted)', marginBottom: '12px' }}>Lần đầu bạn đến với ThreadChat?</p>
        <Link to="/register" style={{
          color: 'var(--primary)',
          textDecoration: 'none',
          fontWeight: 700,
          fontSize: '1.1rem',
          letterSpacing: '0.5px'
        }}>
          Bắt đầu tạo tài khoản mới
        </Link>
      </div>
    </form>
  );
};

export default LoginForm;
