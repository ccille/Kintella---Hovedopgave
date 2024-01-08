import React, {useState, useEffect} from "react";
import "./App.css";
import './Changes.css';
import axios from 'axios';

interface Checkbox {
    // label: string;
    checked: boolean;
}

interface textChanges {
    textChangeID: number,
    textID: number,
    dateModified: Date,
    textChangeFrom: string,
    textChangeTo: string,
    //For the check mark
    checked? : boolean
}

const url: string = "http://localhost:5130/api/ComponentContens/";
const urlTextChanges: string = "http://localhost:5130/api/TextChanges/";
function Changes(){
    const [selectedLanguage, setSelectedLanguage] = useState<number>(1)
    const [selectedLanguageData, setSelectedLanguageData] = useState<[]>()
    const [textChangeData, setTextChangeData] = useState<textChanges[]>()
    const [selectedTextChange, setSelectedTextChange] = useState<textChanges | null>(null)
    const [checkboxes, setCheckboxes] = useState<Checkbox[]>([])

    // text localization
    useEffect(() => {        
        const fechdata = async () => {
            try{
                const textChangeResponse = await axios.get<textChanges[]>(urlTextChanges + "GetAllTextChanges", {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${sessionStorage.getItem("token")}`
                }
            });
            // Map the response data to the Checkbox interface
            const checkboxesData: Checkbox[] = textChangeResponse.data.map((item: any) => ({
                label: item.label,
                checked: false, // You may set this to true based on your requirements
            }));
                setCheckboxes(checkboxesData);
                setTextChangeData(textChangeResponse.data)
                // setSelectedLanguageData(response.data)
                console.log(textChangeResponse.data)
                // console.log("setlanID" + response)
            }
            catch (error){
                //Handling of error
                console.error('There was an error', error)
            }
        }
        fechdata()
    }, [selectedLanguage]);
    
    const handleCheckboxChange = (index: number) => {
        const updatedCheckboxes = [...checkboxes];
        updatedCheckboxes[index].checked = !updatedCheckboxes[index].checked;

        if (updatedCheckboxes[index].checked) {
            // If checkbox is marked, the text will not swow in staging/production
            setSelectedTextChange(null);
        }
        
        setCheckboxes(updatedCheckboxes);
    };

    const [textAreaValue, setTextAreaValue] = useState<string>("");

    const handleMarkAll = () => {
        const updatedCheckboxes = textChangeData?.map((Checkbox) => ({
            ...Checkbox, 
            checked: true,
        })) || []

        setCheckboxes(updatedCheckboxes)
        setTextChangeData(updatedCheckboxes)
    };

    const handleUnmarkAll = () => {
        const updatedCheckboxes = textChangeData?.map((checkbox) => ({
          ...checkbox,
          checked: false,
        })) || []
        
        setCheckboxes(updatedCheckboxes);
        setTextChangeData(updatedCheckboxes)
    };


    const handleTextAreaChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
        if (selectedTextChange) {
            // Update Staging Text
            setSelectedTextChange({
                ...selectedTextChange,
                textChangeTo: event.target.value,
            });
        } else {
            // Update Production Text
            setTextAreaValue(event.target.value);
        }
    };

    const handlePublishText = async (textChangeId: number) => {
        try {
            const response = await axios.post<textChanges[] | textChanges>(urlTextChanges + "PublishText?textChangesID="+ textChangeId, {
                textChangesID: textChangeId
                }, {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${sessionStorage.getItem("token")}`
                }
            });
            if(response !== undefined || response !== null) {
                console.log(response);
            }
        }
        catch (error) {
            console.error(error);
        }
    }
    
    const handleCancelText = async (textChangeId: number) => {
        try {
            const response = await axios.post<textChanges[]>(urlTextChanges + "CancelTextPublishing?textChangesID="+ textChangeId, {
                textChangeID: textChangeId
                }, {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${sessionStorage.getItem("token")}`
                }
            });
            if(response !== undefined || response !== null) {
                console.log(response);
            }

        }
        catch (error) {
            console.error(error);
        }
    }

    return (
        <div>
            <div className="ContentWrapper">
                <h2>Texts:</h2>
                <p>Click on the text to show it in staging and the old text in production</p>
                <div className="TopBtns">
                    <button type="button" onClick={handleMarkAll}>
                        Mark all
                    </button>
                    <button type="button" onClick={handleUnmarkAll}>
                        Remove mark all
                    </button>
                </div>
                <div className="Textwrapper">
                    {textChangeData?.map((checkbox, index) => (
                        <div key={index} onClick={() => setSelectedTextChange(checkbox)} 
                            onChange={() => handleCheckboxChange(index) }>
                            <input
                                type="checkbox"
                                checked={checkbox.checked}
                                />
                            <label>
                                    {checkbox.textChangeTo}
                            </label>
                            <div>
                            <button onClick={() => handlePublishText(checkbox.textChangeID)} type="button">Publish</button>
                            <button onClick={() => handleCancelText(checkbox.textChangeID)} type="button">Delete</button>  
                            </div>
                        </div>
                    ))}
                </div>
            </div>
            <div className="Textareawrapper">
                <h2>Staging:</h2>
                <textarea  
                    value={selectedTextChange ? selectedTextChange.textChangeFrom : ""}
                    onChange={handleTextAreaChange}>
                </textarea>
                
                <h2>Production:</h2>
                <textarea  
                    value={selectedTextChange ? selectedTextChange.textChangeTo : ""}
                    onChange={handleTextAreaChange}>
                </textarea>
            </div>
            
        </div>
    );
}

export default Changes;