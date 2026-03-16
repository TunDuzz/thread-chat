import { User, Bell, Search, ChevronRight, ShieldAlert } from 'lucide-react';

const ChatInfo: React.FC = () => {
  const sections = [
    { title: 'Thông tin về đoạn chat', icon: <ChevronRight size={18} /> },
    { title: 'Xem tin nhắn đã ghim', icon: <ChevronRight size={18} /> },
    { title: 'Tùy chỉnh đoạn chat', icon: <ChevronRight size={18} /> },
    { title: 'File phương tiện & file', icon: <ChevronRight size={18} /> },
    { title: 'Quyền riêng tư và hỗ trợ', icon: <ChevronRight size={18} /> },
  ];

  return (
    <div className="chat-info-pane">
      <div style={{ padding: '40px 16px 24px', display: 'flex', flexDirection: 'column', alignItems: 'center', width: '100%' }}>
        <div style={{ width: '80px', height: '80px', marginBottom: '16px', borderRadius: '50%', overflow: 'hidden', border: '2px solid var(--primary)' }}>
          <img src="https://ui-avatars.com/api/?name=Tran+Van+A&background=random&color=fff" alt="Trần Văn A" style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
        </div>
        <h3 style={{ fontSize: '1.2rem', fontWeight: '700', marginBottom: '8px', color: '#fff' }}>Trần Văn A</h3>
        <div style={{ fontSize: '0.75rem', color: 'var(--text-muted)', display: 'flex', alignItems: 'center', gap: '6px' }}>
          <ShieldAlert size={12} />
          <span>Được mã hóa đầu cuối</span>
        </div>

        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '8px', marginTop: '24px', width: '100%', padding: '0 8px' }}>
          <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '10px', cursor: 'pointer' }}>
            <div className="icon-btn-round" style={{ width: '42px', height: '42px' }}>
              <User size={20} />
            </div>
            <span style={{ fontSize: '0.65rem', color: 'var(--text-muted)', textAlign: 'center', lineHeight: '1.2' }}>Trang cá nhân</span>
          </div>
          <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '10px', cursor: 'pointer' }}>
            <div className="icon-btn-round" style={{ width: '42px', height: '42px' }}>
              <Bell size={20} />
            </div>
            <span style={{ fontSize: '0.65rem', color: 'var(--text-muted)', textAlign: 'center', lineHeight: '1.2' }}>Tắt thông báo</span>
          </div>
          <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '10px', cursor: 'pointer' }}>
            <div className="icon-btn-round" style={{ width: '42px', height: '42px' }}>
              <Search size={20} />
            </div>
            <span style={{ fontSize: '0.65rem', color: 'var(--text-muted)', textAlign: 'center', lineHeight: '1.2' }}>Tìm kiếm</span>
          </div>
        </div>
      </div>

      <div style={{ flex: 1, overflowY: 'auto', padding: '0 8px', scrollbarWidth: 'thin' }}>
        {sections.map(section => (
          <div key={section.title} className="info-header" style={{ padding: '12px 12px', margin: '4px 4px', borderRadius: '8px', cursor: 'pointer' }}>
            <span style={{ fontSize: '0.85rem', fontWeight: '500' }}>{section.title}</span>
            <ChevronRight size={16} style={{ opacity: 0.5 }} />
          </div>
        ))}
      </div>
    </div>
  );
};

export default ChatInfo;
