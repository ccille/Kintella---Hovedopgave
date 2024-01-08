import React, { createContext, useState, useContext, useEffect } from 'react';
import axios from 'axios';

interface LanguageContextType {
  language: string;
  setLanguage: (language: string) => void;
  languages: Array<{ languageID: number; languageName: string }>;
}

const defaultState: LanguageContextType = {
  language: 'da',
  setLanguage: language => console.warn('no language provider'),
  languages: [],
};

const LanguageContext = createContext<LanguageContextType>(defaultState);

export const useLanguage = () => useContext(LanguageContext);

type LanguageProviderProps = {
  children: React.ReactNode;
};

export const LanguageProvider: React.FC<LanguageProviderProps> = ({ children }) => {
  const [language, setLanguage] = useState('da');
  const [languages, setLanguages] = useState([]);

  useEffect(() => {
    const fetchLanguages = async () => {
      try {
        const response = await axios.get('http://localhost:5130/api/Languages/GetAllFromDb');
        setLanguages(response.data);
      } catch (error) {
        console.error('Error fetching languages:', error);
      }
    };

    fetchLanguages();
  }, []);

  return (
    <LanguageContext.Provider value={{ language, setLanguage, languages }}>
      {children}
    </LanguageContext.Provider>
  );
};

export default LanguageContext;