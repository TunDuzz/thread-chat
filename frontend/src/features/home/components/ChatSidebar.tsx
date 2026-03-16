import React from 'react';
import { Search, Edit, MoreHorizontal } from 'lucide-react';

const ChatSidebar: React.FC = () => {
  const chats = [
    { id: 1, name: 'Phòng Kỹ Thuật 🛠️', lastMsg: 'Bản cập nhật đã sẵn sàng', time: '10m', active: false },
    { id: 2, name: 'Alice Freeman', lastMsg: 'Hẹn gặp bạn vào ngày mai!', time: '1h', active: false },
    { id: 3, name: 'Trần Văn A', lastMsg: 'Đã bày tỏ cảm xúc 😍 về tin n...', time: '1d', active: true },
    { id: 4, name: 'Design Team', lastMsg: 'Gửi các bản mockup mới nhất', time: '1d', active: false },
    { id: 5, name: 'Robert Fox', lastMsg: 'Trao đổi về dự án...', time: '2d', active: false },
    { id: 6, name: 'Lê Thị B', lastMsg: 'Cảm ơn bạn nhiều nhé', time: '3d', active: false },
    { id: 7, name: 'Hỗ Trợ Khách Hàng', lastMsg: 'Yêu cầu của bạn đã được xử lý', time: '3d', active: false },
    { id: 8, name: 'Michael Chen', lastMsg: 'Tối nay cafe không?', time: '4d', active: false },
  ];

  return (
    <div className="chat-sidebar">
      <div className="sidebar-header">
        <h2 style={{ fontSize: '1.5rem', fontWeight: '800' }}>Đoạn chat</h2>
        <div style={{ display: 'flex', gap: '8px' }}>
          <div className="icon-btn-round">
            <MoreHorizontal size={20} />
          </div>
          <div className="icon-btn-round">
            <Edit size={20} />
          </div>
        </div>
      </div>

      <div className="search-container">
        <div className="input-premium" style={{ display: 'flex', alignItems: 'center', gap: '10px', padding: '10px 16px', borderRadius: '20px', background: 'rgba(255,255,255,0.05)' }}>
          <Search size={18} style={{ color: 'var(--text-muted)' }} />
          <input 
            type="text" 
            placeholder="Tìm kiếm trên Messenger" 
            style={{ background: 'transparent', border: 'none', outline: 'none', color: '#fff', fontSize: '0.9rem', width: '100%' }}
          />
        </div>
      </div>

      <div style={{ display: 'flex', gap: '8px', padding: '0 16px 12px' }}>
        {['Tất cả', 'Chưa đọc', 'Nhóm'].map((tab, i) => (
          <button 
            key={tab}
            style={{ 
              padding: '6px 12px', 
              borderRadius: '16px', 
              border: 'none', 
              background: i === 0 ? 'rgba(0, 242, 255, 0.1)' : 'transparent',
              color: i === 0 ? 'var(--primary)' : 'var(--text-muted)',
              fontSize: '0.85rem',
              fontWeight: '600',
              cursor: 'pointer'
            }}
          >
            {tab}
          </button>
        ))}
      </div>

      <div className="chat-list">
        {chats.map(chat => (
          <div key={chat.id} className={`chat-item ${chat.active ? 'active' : ''}`}>
            <div className="avatar-container">
              <img src={`https://ui-avatars.com/api/?name=${chat.name}&background=random&color=fff`} alt={chat.name} className="avatar-img" />
              <div className="status-indicator"></div>
            </div>
            <div style={{ flex: 1, minWidth: 0 }}>
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '4px', gap: '8px' }}>
                <div style={{ fontWeight: '500', fontSize: '0.95rem', overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap', flex: 1 }}>{chat.name}</div>
                {chat.time && <div style={{ fontSize: '0.75rem', color: 'var(--text-muted)', flexShrink: 0 }}>{chat.time}</div>}
              </div>
              <div style={{ fontSize: '0.85rem', color: 'var(--text-muted)', overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>
                {chat.lastMsg}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ChatSidebar;
