import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import './header.css';
import axios from 'axios';
import { useLanguage } from '../../Context/LanguageContext';

interface Language{
  languageID: number,
  languageName: string,
  languageLocale: string
}

const Header = () => {
  const [languages, setLanguages] = useState<Language[]>([]);
  const [currentLanguage, setCurrentLanguage] = useState('')

    useEffect(() => {
        const fetchLanguages = async () => {
            try {
                const response = await axios.get('http://localhost:5130/api/Languages/GetAllFromDb');
                setLanguages(response.data);
                setCurrentLanguage(response.data[0].languageLocale);
            } catch (error) {
                console.error('Error fetching languages:', error);
            }
        };

        fetchLanguages();
    }, []);

    const handleLanguageChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
      setCurrentLanguage(event.target.value);
    };
    return (
      <header className="App-header">
          <nav className="nav-links">
              <Link to="/">Home</Link>
              <Link to="/Modules">Modules</Link>
              <Link to="/Languages">Languages</Link>
              <Link to="/Changes">Changes</Link>
          </nav>
          <div className="login-section">
          <Link to="/Login">Login</Link>
              <select className="language-select" value={currentLanguage} onChange={handleLanguageChange}>
                  {languages.map((lang) => (
                      <option key={lang.languageID} value={lang.languageLocale}>
                          {lang.languageName}
                      </option>
                  ))}
              </select>
          </div>
      </header>
  );

    // return (
    //     <header className="App-header">
    //         <nav className="nav-links">
    //             <Link to="/">Home</Link>
    //             <Link to="/Modules">Modules</Link>
    //             <Link to="/Languages">Languages</Link>
    //             <Link to="/Changes">Changes</Link>
    //         </nav>
    //         <div className="language-select">
    //             <select value={currentLanguage} onChange={handleLanguageChange}>
    //                 {languages.map((lang) => (
    //                     <option key={lang.languageID} value={lang.languageLocale}>
    //                         {lang.languageName}
    //                     </option>
    //                 ))}
    //             </select>
    //         </div>
    //         <div className="login-section">
    //             <Link to="/Login">Login</Link>
    //         </div>
    //     </header>
    // );
};

export default Header;