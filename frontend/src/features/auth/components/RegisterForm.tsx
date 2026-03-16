import React, { useState, useRef, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { Mail, Lock, User, Phone, UserPlus, ArrowLeft, ChevronDown } from 'lucide-react';
import { authService } from '../services/authService';

interface CustomSelectProps {
  options: { value: string | number; label: string | number }[];
  placeholder: string;
  onChange?: (value: string | number) => void;
  style?: React.CSSProperties;
}

const CustomSelect: React.FC<CustomSelectProps> = ({ options, placeholder, onChange, style }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [selected, setSelected] = useState<string | number | null>(null);
  const [displayValue, setDisplayValue] = useState('');
  const containerRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (containerRef.current && !containerRef.current.contains(event.target as Node)) {
        setIsOpen(false);
        if (selected) {
          const opt = options.find(o => o.value === selected);
          if (opt) setDisplayValue(opt.label.toString());
        } else {
          const match = options.find(o => o.label.toString().toLowerCase() === displayValue.toLowerCase());
          if (!match) {
            setDisplayValue('');
          }
        }
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, [selected, options, displayValue]);

  const handleSelect = (opt: { value: string | number; label: string | number }) => {
    setSelected(opt.value);
    setDisplayValue(opt.label.toString());
    setIsOpen(false);
    if (onChange) onChange(opt.value);
  };

  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      if (filteredOptions.length > 0) {
        handleSelect(filteredOptions[0]);
      }
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const val = e.target.value;
    setDisplayValue(val);
    setIsOpen(true);
    
    const match = options.find(o => 
      o.label.toString().toLowerCase() === val.toLowerCase() || 
      o.value.toString() === val
    );
    
    if (match) {
      setSelected(match.value);
      if (onChange) onChange(match.value);
    } else {
      setSelected(null);
      if (onChange) onChange('');
    }
  };

  const filteredOptions = options.filter(o => 
    o.label.toString().toLowerCase().includes(displayValue.toLowerCase()) ||
    o.value.toString().includes(displayValue)
  );

  return (
    <div className="custom-select-container" ref={containerRef} style={style}>
      <div 
        className="input-premium" 
        style={{ 
          cursor: 'text', 
          display: 'flex', 
          alignItems: 'center', 
          justifyContent: 'space-between',
          paddingLeft: '16px',
          paddingRight: '12px',
          borderColor: isOpen ? 'var(--primary)' : 'rgba(255, 255, 255, 0.08)'
        }}
        onClick={() => setIsOpen(true)}
      >
        <input 
          type="text"
          value={displayValue}
          onChange={handleInputChange}
          onKeyDown={handleKeyDown}
          placeholder={placeholder}
          style={{
            background: 'transparent',
            border: 'none',
            outline: 'none',
            color: '#fff',
            width: '100%',
            fontSize: '0.95rem'
          }}
          onFocus={() => setIsOpen(true)}
          onBlur={() => {
            if (selected) {
              const opt = options.find(o => o.value === selected);
              if (opt && opt.label.toString() !== displayValue) {
                setDisplayValue(opt.label.toString());
              }
            } else if (displayValue && !options.some(o => 
              o.label.toString().toLowerCase() === displayValue.toLowerCase() || 
              o.value.toString() === displayValue
            )) {
              setDisplayValue('');
            }
          }}
        />
        <ChevronDown size={14} style={{ 
          transform: isOpen ? 'rotate(180deg)' : 'rotate(0)', 
          transition: 'transform 0.3s ease',
          opacity: 0.4,
          cursor: 'pointer'
        }} onClick={(e) => { e.stopPropagation(); setIsOpen(!isOpen); }} />
      </div>

      {isOpen && (
        <div className="custom-select-dropdown">
          {filteredOptions.length > 0 ? (
            filteredOptions.map((opt) => (
              <div 
                key={opt.value} 
                className={`custom-select-option ${selected === opt.value ? 'selected' : ''}`}
                onClick={() => handleSelect(opt)}
              >
                {opt.label}
              </div>
            ))
          ) : (
            <div style={{ padding: '10px 16px', color: 'var(--text-muted)', fontSize: '0.85rem' }}>Không tìm thấy</div>
          )}
        </div>
      )}
    </div>
  );
};

const RegisterForm: React.FC = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    gender: '',
    day: '',
    month: '',
    year: '',
    password: ''
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const dob = formData.year && formData.month && formData.day 
        ? `${formData.year}-${String(formData.month).padStart(2, '0')}-${String(formData.day).padStart(2, '0')}T00:00:00Z`
        : undefined;

      const response = await authService.register({
        firstName: formData.firstName,
        lastName: formData.lastName,
        email: formData.email,
        phoneNumber: formData.phoneNumber,
        gender: formData.gender,
        dateOfBirth: dob,
        password: formData.password
      });

      authService.setToken(response.accessToken);
      authService.setUser(response);
      navigate('/');
    } catch (err: any) {
      setError(err.response?.data?.error || 'Đăng ký thất bại. Vui lòng thử lại.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      {error && (
        <div style={{ color: '#ff4d4d', fontSize: '0.85rem', marginBottom: '12px', textAlign: 'center' }}>
          {error}
        </div>
      )}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px' }}>
        <div className="input-group">
          <User size={18} style={{ position: 'absolute', top: '50%', left: '16px', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
          <input 
            type="text" 
            className="input-premium" 
            placeholder="Họ" 
            style={{ paddingLeft: '44px' }} 
            required 
            value={formData.firstName}
            onChange={(e) => setFormData({...formData, firstName: e.target.value})}
          />
        </div>
        <div className="input-group">
          <User size={18} style={{ position: 'absolute', top: '50%', left: '16px', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
          <input 
            type="text" 
            className="input-premium" 
            placeholder="Tên" 
            style={{ paddingLeft: '44px' }} 
            required 
            value={formData.lastName}
            onChange={(e) => setFormData({...formData, lastName: e.target.value})}
          />
        </div>
      </div>

      <div className="input-group" style={{ marginBottom: '14px' }}>
        <Mail size={18} style={{ position: 'absolute', top: '50%', left: '16px', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
        <input 
          type="email" 
          className="input-premium" 
          placeholder="Địa chỉ Email" 
          required 
          style={{ paddingLeft: '44px' }} 
          value={formData.email}
          onChange={(e) => setFormData({...formData, email: e.target.value})}
        />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1.2fr 1fr', gap: '12px', marginBottom: '14px' }}>
        <div className="input-group" style={{ marginBottom: 0 }}>
          <Phone size={18} style={{ position: 'absolute', top: '50%', left: '16px', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
          <input 
            type="tel" 
            className="input-premium" 
            placeholder="Số điện thoại" 
            required 
            style={{ paddingLeft: '44px' }} 
            value={formData.phoneNumber}
            onChange={(e) => setFormData({...formData, phoneNumber: e.target.value})}
          />
        </div>
        <CustomSelect 
          placeholder="Giới tính"
          options={[
            { value: 'male', label: 'Nam' },
            { value: 'female', label: 'Nữ' },
            { value: 'other', label: 'Khác' }
          ]}
          onChange={(val) => setFormData({...formData, gender: val as string})}
        />
      </div>

      <div className="input-group" style={{ marginBottom: '14px' }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: '6px', marginBottom: '6px', color: 'var(--text-muted)', fontSize: '0.85rem' }}>
          <span>Ngày sinh</span>
        </div>
        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr 1fr', gap: '8px' }}>
          <CustomSelect 
            placeholder="Ngày"
            options={Array.from({ length: 31 }, (_, i) => ({ value: i + 1, label: i + 1 }))}
            onChange={(val) => setFormData({...formData, day: val as string})}
          />
          <CustomSelect 
            placeholder="Tháng"
            options={Array.from({ length: 12 }, (_, i) => ({ value: i + 1, label: `Tháng ${i + 1}` }))}
            onChange={(val) => setFormData({...formData, month: val as string})}
          />
          <CustomSelect 
            placeholder="Năm"
            options={Array.from({ length: 80 }, (_, i) => {
              const year = new Date().getFullYear() - i;
              return { value: year, label: year };
            })}
            onChange={(val) => setFormData({...formData, year: val as string})}
          />
        </div>
      </div>

      <div className="input-group" style={{ marginBottom: '20px' }}>
        <Lock size={18} style={{ position: 'absolute', top: '50%', left: '16px', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
        <input 
          type="password" 
          className="input-premium" 
          placeholder="Mật khẩu" 
          required 
          style={{ paddingLeft: '44px' }} 
          value={formData.password}
          onChange={(e) => setFormData({...formData, password: e.target.value})}
        />
      </div>

      <button 
        type="submit" 
        className="btn-premium" 
        disabled={loading}
        style={{ width: '100%', marginTop: '8px', opacity: loading ? 0.7 : 1 }}
      >
        <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', gap: '10px' }}>
          {loading ? <span>Đang đăng ký...</span> : (
            <>
              <UserPlus size={20} />
              <span>Bắt đầu kết nối</span>
            </>
          )}
        </div>
      </button>

      <div style={{ marginTop: '32px', textAlign: 'center' }}>
        <Link to="/login" style={{ 
          color: 'var(--text-muted)', 
          textDecoration: 'none', 
          display: 'flex', 
          alignItems: 'center', 
          justifyContent: 'center', 
          gap: '8px' 
        }}>
          <ArrowLeft size={16} /> 
          <span>Quay lại đăng nhập</span>
        </Link>
      </div>
    </form>
  );
};

export default RegisterForm;
