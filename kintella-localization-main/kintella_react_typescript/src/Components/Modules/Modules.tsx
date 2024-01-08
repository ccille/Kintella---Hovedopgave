import React, {useState, useEffect} from 'react';
import './Modules.css';
import axios from 'axios';
import EditModal from '../Modal/modal';
 // TODO: ændre live text update
interface Modules{
    moduleID: number,
    moduleName: string,
    subModuleName: string,
    subModules: [ {
        subModuleID: number,
        subModuleName: string
}]}

interface SubModules{
    subModuleID: number,
    subModuleName: string,
    listOfSubModuleTexts?: Text[],
    moduleID: number,
    languageID: number
}

interface Text{
    textID: number,
    subModuleID: number,
    languageID: number,
    textContent?: string,
    dateCreated: Date,
    dateModified: Date,
    isProduction: boolean
}

interface Languages{
    languageID: number,
    languageName: string,
    languageLocale: string
}

interface TextChanges{
    textChangesID: number,
    textObject: Text,
    dateModified: Date,
    textChangeFrom: string,
    textChangeTo: string
}

function Modules(){
    let editOrAdd: boolean | null = true;

    const urlLanguages = 'http://localhost:5130/api/Languages/';
    const urlText = 'http://localhost:5130/api/Texts/';
    const urlModules = 'http://localhost:5130/api/Modules/'
    const urlSubModules = 'http://localhost:5130/api/SubModules/'
    const urlComponentContents = 'http://localhost:5130/api/ComponentContents/'

    const [moduleData, setModuleData] = useState<Modules[]>([])
    const [subModuleData, setSubModuleData] = useState<SubModules[]>()
    const [textData, setTextData] = useState<Text[]>([])
    const [selectedModule, setSelectedModule] = useState<number>()
    const [selectedText, setSelectedText] = useState<number | null>()
    const [textID, setTextID] = useState<number | null>(0)
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);
    const [editText, setEditText] = useState('');
    const [addTextID, setAddTextID] = useState<number | 0>(0);
    const [isAddingNewText, setIsAddingNewText] = useState<boolean>(false);
    const [selectLanguageID, setLanguageID] = useState<string>("");
    const [selectLanguageData, setLanguageData] = useState<Languages[] | null>([]);
    const [deleting, setDeleting] = useState<number | null>(); // driller mig med at fjerne elementet fra listen, live.
    const [backupLanguageData, setBackupLanguageData] = useState<Languages[] | null>([])
    const [languageSelector, setLanguageSelector] = useState<string | null>()
    
    useEffect(() => {        
        const fechdata = async () => {
            try{
                const moduleResponse = await axios.get<Modules[]>(urlModules + 'GetAllModules', {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${sessionStorage.getItem("token")}`
                }
            });
                const subModuleResponse = await axios.get<SubModules[]>(urlSubModules + 'GetAllSubModules', {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${sessionStorage.getItem("token")}`
                }
            });
                const textResponse = await axios.get<Text[]>(urlText + 'GetAllTexts', {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${sessionStorage.getItem("token")}`
                }
            });
                const languageResponse = await axios.get<Languages[]>(urlLanguages + "GetAllFromDB", {
                    headers: {
                        'Content-Type': 'application/json',
                }
            });
                setModuleData(moduleResponse.data)
                setSubModuleData(subModuleResponse.data)
                setTextData(textResponse.data)
                setLanguageData(languageResponse.data)
                setBackupLanguageData(languageResponse.data)

                console.log("MODULE");
                console.log(moduleResponse)

                console.log("SUBMODULE");
                console.log(subModuleResponse.data)   

                console.log("TEXT");
                console.log(textResponse.data)
            }
            catch (error){
                //Handling of error
                console.error('There was an error', error)
            }
        }
        fechdata()

    }, [deleting]);

    const handleModuleClick = (moduleId: number) => {
        setSelectedModule(moduleId);
        setSelectedText(null);
    };

    const HandleTextClick = async (submoduleID: number) => {
        setSelectedText(submoduleID);
        setAddTextID(submoduleID);
    }

    // Function to open the modal
    const openEditModal = (textId: number | null, currText: string | null, textID: number | null) => {
        editOrAdd = true;
        if (textId === null) {
          // TODO: handle error
          console.log('No text has been chosen.');
          return;
        }

        const lanID = textData.find(tID => tID.textID === textID)?.languageID;

        setLanguageData(backupLanguageData);
        // Check if lanID is defined before filtering
        if (lanID !== undefined) {
            const filteredLanguageData = selectLanguageData?.filter(lanId => lanId.languageID === lanID) || [];

            // Assuming setLanguageData is a state update function

        // Check if selectLanguageData is defined before setting
        if (selectLanguageData !== undefined) {
            setLanguageData(filteredLanguageData);              
        } else {
            // Handle the case where selectLanguageData is undefined
            console.error("selectLanguageData is undefined");
        }
        } else {
        // Handle the case where lanID is undefined
        console.error("lanID is undefined");
        }

        setTextID(textID);
        setEditText(currText || '');
        setSelectedText(textId);
        setIsAddingNewText(false);
        setIsEditModalOpen(true);
    };
    // Function to close the modal
    const closeEditModal = () => {
        setIsEditModalOpen(false);
        setLanguageData(backupLanguageData);
    }

    const handleTextChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
        setEditText(event.target.value);
    }
    const handleLanguageIDChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setLanguageID(event.target.value);
        setLanguageSelector(event.target.value);
    }

    const openAddTextModal = () => {
        editOrAdd = false;
        setEditText(''); // Resets the text in the modal
        setIsAddingNewText(true);
        setIsEditModalOpen(true);
    };

    const handleTextSubmit = async () => {
        // Makes sure that a text is chosen
        let getText = textData.filter(e => e.textID === textID)
        if (selectedText !== null) {
          try {
            const response = await axios.put(`${urlText}UpdateText/${textID}`, {
                // The new text data you wish to update
                textID: textID,
                subModuleID: addTextID,
                languageID: getText[0].languageID,
                textContent: editText,
                dateCreated: getText[0].dateCreated,
                dateModified: getText[0].dateModified,
                isProduction: getText[0].isProduction
                
              }, {
                headers: {
                  'Content-Type': 'application/json',
                  'Authorization': `Bearer ${sessionStorage.getItem("token")}`,
                },
              });
            
            // Update succesful, log the response, and updates the local state if needed
            console.log('Update response:', response.data);
      
            // Update 'datatext' entity to reflect the edited/changed text
              
                let updatedText: Text | null = textData.find(id => id.textID === response.data.textID) || null
                updatedText!.textID = response.data.textID;
                updatedText!.subModuleID = response.data.subModuleID;
                updatedText!.languageID = response.data.languageID;
                updatedText!.textContent = response.data.textContent;
                updatedText!.dateCreated = response.data.dateCreated;
                updatedText!.dateModified = response.data.dateModified;
                updatedText!.isProduction = false
                setTextData(textData);
      
            // Closing modal
            setIsEditModalOpen(false);
      
          } catch (error) {
            // Handle the error exception
            console.error('Error updating text:', error);
          }
        } else {
          console.log('No text is selected for updating.');
        }
    }

    const handleTextDelete = async (textID: number | null) => {
        if (textID !== null || 0) {
          try {
            await axios.delete(`${urlText}DeleteText/${textID}`, {
                headers: {
                  'Content-Type': 'application/json',
                  'Authorization': `Bearer ${sessionStorage.getItem("token")}`, 
                },
              });
              
              setDeleting(textID)
          } catch (error) {
            // Handle the error exception
            console.error('Error occured while deleting text:', error);
          }
        } else {
          console.log('No text is selected for deletion.');
        }
    }

    const handleAddNewText = async () => {
        try {
            let currentDate: Date = new Date();
            currentDate.setHours(currentDate.getHours() + 1)
            const response = await axios.post<[TextChanges, number]>(`${urlText}CreateText`, {
                textChangeID: 0,
                textObject: {
                    textID: textID,
                    subModuleID: selectedText,
                    languageID: selectLanguageID,
                    textContent: editText,
                    dateCreated: currentDate.toISOString(),
                    dateModified: currentDate.toISOString(),
                    isProduction: false
                },
                dateModified: currentDate.toISOString(),
                textChangeFrom: editText,
                textChangeTo: editText,
                subModuleID: selectedText

            }, {
                headers: {
                  'Content-Type': 'application/json',
                  'Authorization': `Bearer ${sessionStorage.getItem("token")}`,
                },
            });

            console.log('Add new text response:', response.data);

            // let newText: Text = {
            //   textID: response.data.textID,
            //   subModuleID: response.data.subModuleID,
            //   languageID: response.data.languageID,
            //   textContent: response.data.textContent,
            //   dateCreated: response.data.dateCreated,
            //   dateModified: response.data.dateModified,
            //   isProduction: response.data.isProduction
            // };
            
            // textData.push(newText);
            // setTextData(textData);

            setIsEditModalOpen(false);

        } catch (error) {
            console.error('Error adding new text:', error);
        }
    }

    return (
        <div>
            <div className='modulewrapper'>
                <div>
                    <h2>Moduler:</h2> 
                    <div className='module'>
                        <ul>
                            {moduleData.map((module, index) => (
                            <li key={index} onClick={() => handleModuleClick(module.moduleID)}
                            className={selectedModule === module.moduleID ? 'selected' : ''}>
                            {module.moduleName}
                            </li>))}
                        </ul>
                    </div>
                    <button className='module_btn'>Add new module</button> 
               </div>
                <div>
                    <h2>Submoduler:</h2> 
                    <div className='submodule'>
                        <ul>
                            {selectedModule !== null ? subModuleData?.map((subModule, index) => (
                                subModule.moduleID === selectedModule ? 
                                <li key={index}
                                    className={selectedText === subModule.subModuleID ? 'selected' : ''} // for the box marking
                                    onClick={() => HandleTextClick(subModule.subModuleID)}>{subModule.subModuleName}
                                </li>
                                : ""
                            )) : ""}
                        </ul>
                    </div>
                    <button className='module_btn'>Add new submodule</button> 
               </div>
               <div>
                    <h2>Tekster:</h2> 
                    <div className='text'>
                        {selectedModule === null ? "Select a module to present text." : moduleData.find((n) => n.moduleID === selectedModule)?.moduleName}
                        <hr />
                        {selectedText !== null && textData.find((text) => text.subModuleID === selectedText) ?
                         textData.filter((sub) => sub.subModuleID === selectedText).map((text, index) => (
                            <div key={index} className='textElement'>
                                <p>{text.textContent}</p>
                                <div>
                                    <button onClick={() => openEditModal(text.subModuleID, text.textContent || '', text.textID)}>Edit</button>
                                    <button onClick={() => handleTextDelete(text.textID)}>Delete</button>
                                </div>
                            </div>
                         )) : ""}
                    </div>
                    <button disabled={selectedText === null || selectedText === undefined} title={selectedText === null || selectedText === undefined ? 'Must select a submodule first!' : undefined} className='addText_btn' onClick={openAddTextModal}>Add new text</button>
               </div>
               <EditModal 
                isOpen={isEditModalOpen}
                isAddingNewText={isAddingNewText} 
                onClose={closeEditModal} 
                textId={selectedText} 
                editText={editText} 
                onTextChange={handleTextChange} 
                onTextSubmit={isAddingNewText ? handleAddNewText : handleTextSubmit}
                languageIDselector={handleLanguageIDChange}
                languageData={selectLanguageData}
                languageSelector={languageSelector || ""}
                ></EditModal>
            </div>
            
        </div>
    );
}

export default Modules