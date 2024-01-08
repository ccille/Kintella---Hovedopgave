import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import Header from './Components/Header/header';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './Login';
import Modules from './Components/Modules/Modules';
import Changes from './Changes';
import Languages from './Languages';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
	<React.StrictMode>
		<BrowserRouter>
			<Header /> {/* Header for all pages */}
			<Routes>
				<Route path="/" element={<App />} />
				<Route path="/Login" element={<Login />} />
        		<Route path="/Modules" element={<Modules />}/>
				<Route path="/Languages" element={<Languages />} />
				<Route path="/Changes" element={<Changes />} />
			</Routes>
		</BrowserRouter>
	</React.StrictMode>
);

reportWebVitals();