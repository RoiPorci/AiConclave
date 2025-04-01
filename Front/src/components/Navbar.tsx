import React from "react";
import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <nav className="flex justify-between p-4 border-b bg-white">
      <Link to="/" className="text-xl font-bold">
        AiConclave
      </Link>
      <div className="space-x-4">
        <Link to="/">Accueil</Link>
        <Link to="/login">Connexion</Link>
      </div>
    </nav>
  );
}
