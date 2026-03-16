import React, { useEffect, useRef } from 'react';
import { MessageSquare } from 'lucide-react';

interface AuthLayoutProps {
  children: React.ReactNode;
  title: string;
  subtitle: string;
}

const AuthLayout: React.FC<AuthLayoutProps> = ({ children, title, subtitle }) => {
  const canvasRef = useRef<HTMLCanvasElement>(null);

  useEffect(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const ctx = canvas.getContext('2d');
    if (!ctx) return;

    let animationFrameId: number;
    let particles: Particle[] = [];
    const particleCount = 50;

    class Particle {
      x: number;
      y: number;
      vx: number;
      vy: number;
      size: number;

      constructor() {
        this.x = Math.random() * canvas!.width;
        this.y = Math.random() * canvas!.height;
        this.vx = (Math.random() - 0.5) * 0.4;
        this.vy = (Math.random() - 0.5) * 0.4;
        this.size = Math.random() * 2;
      }

      update() {
        this.x += this.vx;
        this.y += this.vy;
        if (this.x < 0 || this.x > canvas!.width) this.vx *= -1;
        if (this.y < 0 || this.y > canvas!.height) this.vy *= -1;
      }

      draw() {
        ctx!.beginPath();
        ctx!.arc(this.x, this.y, this.size, 0, Math.PI * 2);
        ctx!.fillStyle = 'rgba(0, 242, 255, 0.5)';
        ctx!.fill();
      }
    }

    const init = () => {
      canvas.width = window.innerWidth;
      canvas.height = window.innerHeight;
      particles = [];
      for (let i = 0; i < particleCount; i++) {
        particles.push(new Particle());
      }
    };

    const animate = () => {
      ctx.clearRect(0, 0, canvas.width, canvas.height);
      particles.forEach(p => {
        p.update();
        p.draw();
      });

      for (let i = 0; i < particles.length; i++) {
        for (let j = i + 1; j < particles.length; j++) {
          const dx = particles[i].x - particles[j].x;
          const dy = particles[i].y - particles[j].y;
          const dist = Math.sqrt(dx * dx + dy * dy);
          if (dist < 150) {
            ctx.beginPath();
            ctx.strokeStyle = `rgba(0, 242, 255, ${0.15 * (1 - dist / 150)})`;
            ctx.lineWidth = 1;
            ctx.moveTo(particles[i].x, particles[i].y);
            ctx.lineTo(particles[j].x, particles[j].y);
            ctx.stroke();
          }
        }
      }
      animationFrameId = requestAnimationFrame(animate);
    };

    init();
    animate();
    window.addEventListener('resize', init);

    return () => {
      cancelAnimationFrame(animationFrameId);
      window.removeEventListener('resize', init);
    };
  }, []);

  return (
    <div style={{
      height: '100vh',
      display: 'grid',
      gridTemplateColumns: 'minmax(400px, 1.2fr) 1fr',
      backgroundColor: '#020205',
      position: 'relative',
      overflow: 'hidden'
    }} className="auth-container">
      
      {/* Background Canvas */}
      <canvas 
        ref={canvasRef} 
        style={{ 
          position: 'absolute', 
          top: 0, 
          left: 0, 
          width: '100%', 
          height: '100%', 
          zIndex: 0,
          pointerEvents: 'none'
        }}
      />

      {/* Left Column: Branding Section */}
      <div style={{
        position: 'relative',
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        padding: '60px 80px',
        zIndex: 1,
      }} className="visual-side">
        {/* Logo at Top Left */}
        <div style={{ 
          position: 'absolute',
          top: '40px',
          left: '40px',
          width: '56px', 
          height: '56px', 
          background: 'linear-gradient(135deg, var(--primary), var(--primary-deep))',
          borderRadius: '14px',
          boxShadow: '0 0 30px var(--primary-glow)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          zIndex: 10
        }} className="animate-fade-in">
           <MessageSquare size={28} color="white" fill="white" style={{ opacity: 0.9 }} />
        </div>

        <div style={{ position: 'relative' }} className="animate-fade-in-up">
          
          <h1 style={{ 
            fontSize: 'max(4rem, 5vw)', 
            fontWeight: 900, 
            lineHeight: 0.9,
            marginBottom: '24px',
            letterSpacing: '-3px'
          }}>
            Khám phá <br />
            <span style={{ 
              background: 'linear-gradient(to right, var(--primary), #fff)', 
              WebkitBackgroundClip: 'text', 
              WebkitTextFillColor: 'transparent',
              filter: 'drop-shadow(0 0 20px var(--primary-glow))'
            }}>thế giới</span> <br />
            của riêng bạn.
          </h1>
          <p style={{ 
            fontSize: '1.2rem', 
            color: 'var(--text-muted)', 
            maxWidth: '540px',
            lineHeight: 1.5,
            fontWeight: 300
          }}>
            ThreadChat - Không gian số được thiết kế lại để mang đến sự tự do và bảo mật tuyệt đối cho mọi cuộc hội thoại.
          </p>
        </div>
      </div>

      {/* Right Column: Form Panel */}
      <div style={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        padding: '20px 60px',
        position: 'relative',
        zIndex: 2,
        backgroundColor: 'rgba(255, 255, 255, 0.01)',
        backdropFilter: 'blur(30px)',
        borderLeft: '1px solid rgba(255, 255, 255, 0.04)',
        height: '100vh',
        overflowY: 'auto'
      }} className="form-side">
        <div style={{ width: '100%', maxWidth: '440px' }} className="animate-slide-in-right">
          <div style={{ marginBottom: '32px' }}>
            <h2 style={{ fontSize: '2rem', marginBottom: '8px', fontWeight: 800, background: 'linear-gradient(to bottom, #fff, #888)', WebkitBackgroundClip: 'text', WebkitTextFillColor: 'transparent' }}>
              {title}
            </h2>
            <p style={{ color: 'var(--text-muted)', fontSize: '0.95rem' }}>{subtitle}</p>
          </div>
          
          {children}
          
          <div style={{ 
            marginTop: '48px', 
            textAlign: 'center',
            fontSize: '0.85rem',
            color: 'var(--text-muted)',
            opacity: 0.5
          }}>
            &copy; 2026 ThreadChat. Một trải nghiệm không giới hạn.
          </div>
        </div>
      </div>

      <style>{`
        @media (max-width: 1100px) {
          .auth-container { grid-template-columns: 1fr !important; }
          .visual-side { display: none !important; }
          .form-side { 
            padding: 40px !important; 
            border-left: none !important;
            background: radial-gradient(circle at 50% 50%, #0a0a1a 0%, #020205 100%) !important;
          }
        }
      `}</style>
    </div>
  );
};

export default AuthLayout;
