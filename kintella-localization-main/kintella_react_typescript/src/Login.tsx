import React, { useState, useEffect } from "react";
import "./App.css";
import axios from "axios";

const endpoint = 'http://localhost:5130/api/Login/';

function GetLoginToken(username: string, password: string): any {
    return axios.get(endpoint + `GetToken?username=${username}&password=${password}`, {
            headers: {
                'Content-Type': 'application/json'
        }
    })
}
function Login() {
    let myContainer = document.querySelector('.FeedbackMsg') as HTMLInputElement;

    const [inputUsername, setInputUsername] = useState("");
    const [inputPassword, setInputPassword] = useState("");

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const status = await GetLoginToken(inputUsername, inputPassword)
        .then((response: any) => {
            console.log(response);
            myContainer.innerHTML = "Found the user, logging in..."; // TODO: feedback to user

            sessionStorage.setItem("token", response.data);
            return response.status;
        })
        .catch((error: any) => {
            // Handle any errors here
            console.error("There was an error!", error);
            myContainer.innerHTML = "Could not find the user. Check if your spelling is correct."; // TODO: feedback to user

            return error.response.status;
        });
        console.log(await status); // TODO: feedback to user
        
    };

    return (
        <div>
            <h1 style={{textAlign: "center"}}>Login</h1>
            <form onSubmit={handleSubmit} className="LoginForm">
                <label>
                    Username:
                    <input
                        type="text"
                        value={inputUsername}
                        onChange={(e) => setInputUsername(e.target.value)}
                    />
                </label>

                <label>
                    Password:
                    <input
                        type="password"
                        value={inputPassword}
                        onChange={(e) => setInputPassword(e.target.value)}
                    />
                </label>

                <button type="submit">Login</button>
            </form>

            <p className="FeedbackMsg"></p>
        </div>
    );
}

export default Login;
