import React, { useState, useEffect } from "react";
import "./App.css";
import axios from "axios";

const url = 'http://localhost:5130/api/Languages/';

interface LanguagesRest {
    languageID: number,
    languageName: string,
    languageLocale: string
}

const isValidLanguageCode = (code: string): boolean => {
    const regex = /^[a-z]{2,3}(?:-[a-zA-Z]{1,4}(?:-[a-zA-Z]{2})?)?$/;
    return regex.test(code);
  };
  let myContainer = document.querySelector('.FeedbackMsg') as HTMLInputElement;

function Languages() {

    const [errorMsg, setErrorMsg] = useState("");
    const [post, setpost] = useState<LanguagesRest[]>([]);
    const [language, setLanguage] = useState<LanguagesRest>({
        languageID: 0,
        languageName: "",
        languageLocale: ""
    });
    const [selectIndex, setIndex] = useState("");

    useEffect(() => {
        const Data = async () => {
            return axios.get(url + "GetAllFromDB", {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${sessionStorage.getItem("token")}`
            }
        })
        }

        Data()
        .then((response: any) => {
            setpost(response.data)
            console.log(response.data);
            
            return response;
        })
        .catch((error: any) => {
            // Handle any errors here
            console.error("There was an error!", error);
            
            setErrorMsg("There was an error! \n" + error)
        });
    
    }, []) 

    const handleSubmit = async (e: React.FormEvent) => { 
        e.preventDefault(); // siden genindlæser og får det nye sprog. er der andre måder at gøre det på?

        if(!isValidLanguageCode(language.languageLocale)) {
            console.log(isValidLanguageCode(language.languageLocale));
            
            myContainer.innerHTML = "The language code is formatted incorrect. The allowing format is\n 2-3 small letters (The rest is optional) followed by dash (-) then 1-4 letters (big and/or small) followed by a dash (-) then 2 letters (big and/or small) \n Some examepls: - aa\n - ff-EFsg \n - oaf-WWEL-hg"
            return ;
        }

        const response = 
            await axios.post<LanguagesRest>(url + `AddLanguage`, language,
                    {
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${sessionStorage.getItem("token")}`
                        },
                    },
                )
            .then((response: any) => {
                return response;
            })
            .catch((error: any) => {
                // Handle any errors here
                console.error("There was an error!", error);
                
                myContainer.innerHTML = "There was an error! \n" + error;
            });
            
            console.log(await response);
    }

    const deleteLanSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        console.log("id");
        
        console.log(selectIndex);
        
        setpost(post.filter(lan => lan.languageID.toString() !== selectIndex))
        
        const response = 
            await axios.delete(url + `DeleteLanguage?lan=${selectIndex}`, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${sessionStorage.getItem("token")}`
                }
            })
            .then((response: any) => {
                return response;
            })
            .catch((error: any) => {
                // Handle any errors here
                console.error("There was an error!", error);
                
                myContainer.innerHTML = "There was an error! \n" + error;
            });
            console.log(await response);
    }

return (
    <div>
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="AddLanguage">Add language</label>
                <input type="text" placeholder="Name of the language" onChange={(e) => setLanguage({...language, languageName: e.target.value})}/>
                <input type="text" placeholder="Language code (eg. da-DK)" onChange={(e) => setLanguage({...language, languageLocale: e.target.value})}/>
            </div>
            <button type="submit">Add</button>
        </form>

        <select onChange={(e) => setIndex(e.currentTarget.selectedOptions[0].value)}> 
        <option defaultValue={"se alle sprog"} disabled>Se alle sprog</option>
            {post.map((data: LanguagesRest, index) => {                
                return (
                    <option key={index} value={data.languageID}>{data.languageName}</option>
                );
            })}
        </select>
        <form onSubmit={deleteLanSubmit}>
            <button type="submit">Delete language</button>
        </form>

        <p className="FeedbackMsg">{errorMsg}</p>
    </div>
);
}

export default Languages;