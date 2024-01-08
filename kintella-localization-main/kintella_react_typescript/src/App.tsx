import React, { useState, useEffect } from "react";
import logo from "./logo.svg";
import "./App.css";
import axios from "axios";
import { Router, Routes } from "react-router-dom";

const url = "http://localhost:5130/api/Languages/";

interface languageAppTest {
  languageID: number;
  languageName: string;
  languageLocale?: string;
}

function App() {
  const [selectedLanguage, setSelectedLanguage] = useState<number>(1);
  const [selectedLanguageData, setSelectedLanguageData] =
    useState<languageAppTest[]>();

  // text localization
  // useEffect(() => {
  //   const fechdata = async () => {
  //     try {
  //       const response = await axios.get<languageAppTest[]>(
  //         url + "GetAllFromDb/" + selectedLanguage,
  //         {
  //           headers: {
  //             "Content-Type": "application/json",
  //             Authorization: `Bearer ${sessionStorage.getItem("token")}`,
  //           },
  //         }
  //       );

  //       setSelectedLanguageData(response.data);

  //     } catch (error) {
  //       //Handling of error
  //       console.error("There was an error", error);
  //     }
  //   };
  //   fechdata();
  // }, [selectedLanguage]);

  return (
    <div className="App">
      
      <h3>Lorem Ipsum</h3>

      <img
        src="https://steamuserimages-a.akamaihd.net/ugc/782994886515443237/1003D1F885CA6F06DC400DC8C892F4E433601AF2/?imw=1024&imh=1024&ima=fit&impolicy=Letterbox&imcolor=%23000000&letterbox=true"
        alt=""
        style={{ width: "20vw" }}
      />
    </div>
  );
}

export default App;
