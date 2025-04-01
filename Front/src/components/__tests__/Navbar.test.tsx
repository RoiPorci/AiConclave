import React from 'react';
import { render, screen } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import Navbar from '../Navbar';

const renderWithRouter = (component: React.ReactElement) => {
  return render(
    <BrowserRouter>
      {component}
    </BrowserRouter>
  );
};

describe('Navbar', () => {
  it('renders the logo', () => {
    renderWithRouter(<Navbar />);
    const logo = screen.getByText('AiConclave');
    expect(logo).toBeInTheDocument();
  });

  it('renders navigation links', () => {
    renderWithRouter(<Navbar />);
    
    const homeLink = screen.getByText('Accueil');
    const loginLink = screen.getByText('Connexion');
    
    expect(homeLink).toBeInTheDocument();
    expect(loginLink).toBeInTheDocument();
  });

  it('has correct links to pages', () => {
    renderWithRouter(<Navbar />);
    
    const homeLink = screen.getByText('Accueil');
    const loginLink = screen.getByText('Connexion');
    
    expect(homeLink).toHaveAttribute('href', '/');
    expect(loginLink).toHaveAttribute('href', '/login');
  });

  it('applies correct styling classes', () => {
    renderWithRouter(<Navbar />);
    
    const nav = screen.getByRole('navigation');
    const logo = screen.getByText('AiConclave');
    
    expect(nav).toHaveClass('flex', 'justify-between', 'p-4', 'border-b', 'bg-white');
    expect(logo).toHaveClass('text-xl', 'font-bold');
  });
}); 