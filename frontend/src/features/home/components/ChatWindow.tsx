import React from 'react';
import { Phone, Video, Info, Mic, Image, StickyNote, Smile, PlusCircle, ThumbsUp } from 'lucide-react';

const ChatWindow: React.FC = () => {
  const messages = [
    { id: 1, text: 'Chào bạn, bạn có đó không?', sender: 'received', avatar: 'https://ui-avatars.com/api/?name=Tran+Van+A&background=random&color=fff' },
    { id: 2, text: 'Mình đang xem qua bản thiết kế mới', sender: 'received', avatar: 'https://ui-avatars.com/api/?name=Tran+Van+A&background=random&color=fff' },
    { id: 3, text: 'Chào nhé! Mình đây.', sender: 'sent', avatar: 'https://ui-avatars.com/api/?name=ME&background=0088cc&color=fff' },
    { id: 4, text: 'Bạn thấy bản mockup thế nào?', sender: 'sent', avatar: 'https://ui-avatars.com/api/?name=ME&background=0088cc&color=fff' },
    { id: 5, text: 'Trông rất tuyệt vời luôn', sender: 'received', avatar: 'https://ui-avatars.com/api/?name=Tran+Van+A&background=random&color=fff' },
    { id: 6, text: 'Màu sắc rất hiện đại và sang trọng', sender: 'received', avatar: 'https://ui-avatars.com/api/?name=Tran+Van+A&background=random&color=fff' },
    { id: 7, text: 'Cảm ơn bạn, mình cũng nghĩ vậy :))', sender: 'sent', avatar: 'https://ui-avatars.com/api/?name=ME&background=0088cc&color=fff' },
    { id: 8, text: 'Để mình kiểm tra thêm phần layout', sender: 'received', avatar: 'https://ui-avatars.com/api/?name=Tran+Van+A&background=random&color=fff' },
    { id: 9, text: 'Ok, có gì cứ nhắn mình nhé', sender: 'sent', avatar: 'https://ui-avatars.com/api/?name=ME&background=0088cc&color=fff' },
    { id: 10, text: 'Đã cập nhật xong link demo rồi đấy', sender: 'sent', avatar: 'https://ui-avatars.com/api/?name=ME&background=0088cc&color=fff' },
    { id: 11, text: 'Để mình xem luôn', sender: 'received', avatar: 'https://ui-avatars.com/api/?name=Tran+Van+A&background=random&color=fff' },
    { id: 12, text: 'Cảm ơn bạn nhiều!', sender: 'sent', avatar: 'https://ui-avatars.com/api/?name=ME&background=0088cc&color=fff' },
  ];

  return (
    <div className="chat-main">
      {/* Header */}
      <div style={{ 
        height: 'var(--header-height)',
        padding: '0 20px', 
        borderBottom: '1px solid var(--glass-border)',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between',
        background: 'rgba(0,0,0,0.1)',
        backdropFilter: 'blur(10px)',
        zIndex: 10
      }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: '12px' }}>
          <div className="avatar-container" style={{ width: '40px', height: '40px', margin: 0 }}>
            <img src="https://ui-avatars.com/api/?name=Tran+Van+A&background=random&color=fff" alt="User" className="avatar-img" />
            <div className="status-indicator"></div>
          </div>
          <div>
            <div style={{ fontSize: '1rem', fontWeight: '600', color: '#fff' }}>Trần Văn A</div>
            <div style={{ fontSize: '0.75rem', color: '#4ade80' }}>Đang hoạt động</div>
          </div>
        </div>
        <div style={{ display: 'flex', gap: '16px', color: 'var(--primary)' }}>
          <Phone size={20} style={{ cursor: 'pointer' }} />
          <Video size={20} style={{ cursor: 'pointer' }} />
          <Info size={20} style={{ cursor: 'pointer' }} />
        </div>
      </div>

      {/* Messages */}
      <div className="chat-messages">
        {messages.map(msg => (
          <div key={msg.id} className={`message-group ${msg.sender}`}>
            <img src={msg.avatar} alt="avatar" className="message-avatar-small" />
            <div className={`message-bubble ${msg.sender}`}>
              {msg.text}
            </div>
          </div>
        ))}
      </div>

      {/* Input */}
      <div className="chat-input-area">
        <div style={{ display: 'flex', gap: '12px', color: 'var(--primary)' }}>
          <PlusCircle size={22} style={{ cursor: 'pointer' }} />
          <Image size={22} style={{ cursor: 'pointer' }} />
          <StickyNote size={22} style={{ cursor: 'pointer' }} />
          <Mic size={22} style={{ cursor: 'pointer' }} />
        </div>
        <div style={{ 
          flex: 1, 
          background: 'rgba(255,255,255,0.05)', 
          borderRadius: '20px', 
          padding: '8px 16px',
          display: 'flex',
          alignItems: 'center'
        }}>
          <input 
            type="text" 
            placeholder="Aa" 
            style={{ background: 'transparent', border: 'none', outline: 'none', color: '#fff', fontSize: '0.95rem', width: '100%' }}
          />
          <Smile size={20} style={{ color: 'var(--primary)', cursor: 'pointer' }} />
        </div>
        <ThumbsUp size={22} style={{ color: 'var(--primary)', cursor: 'pointer' }} />
      </div>
    </div>
  );
};

export default ChatWindow;
