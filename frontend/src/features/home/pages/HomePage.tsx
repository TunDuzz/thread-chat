import React from 'react';
import ChatSidebar from '../components/ChatSidebar';
import ChatWindow from '../components/ChatWindow';
import ChatInfo from '../components/ChatInfo';

const HomePage: React.FC = () => {
  return (
    <div className="home-layout">
      <ChatSidebar />
      <ChatWindow />
      <ChatInfo />
    </div>
  );
};

export default HomePage;
