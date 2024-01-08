import React from 'react';
import './modal.css';

interface Languages{
  languageID: number,
  languageName: string,
  languageLocale: string
}

interface EditModalProps {
    isOpen: boolean;
    isAddingNewText: boolean;
    onClose: () => void;
    textId?: number | null;
    editText: string;
    onTextChange: (event: React.ChangeEvent<HTMLTextAreaElement>) => void;
    onTextSubmit: () => void;
    languageIDselector: (event: React.ChangeEvent<HTMLSelectElement>) => void;
    languageData: Languages[] | null
    languageSelector: string
  }

function EditModal({ isOpen, onClose, textId, editText, onTextChange, onTextSubmit, isAddingNewText, languageIDselector, languageData, languageSelector}: EditModalProps) {
  if (!isOpen) return null;

  // Function to handle form submit
  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onTextSubmit(); // Call the prop-function that handles the submit
  };

  return (
    <div className="modal-backdrop">
      <div className="modal-content">
        <form onSubmit={handleSubmit}>
          <label htmlFor="text">Text:</label>
          <textarea id="text" value={editText} onChange={onTextChange} />
          <input type="submit" value={isAddingNewText ? "Add Text" : "Update Text"} />

          <select defaultValue={languageData!.length >= 2 ? "intro" : `${languageData![0].languageID}`} onChange={languageIDselector}> 
          <option value={"intro"} disabled>Se alle sprog</option>
            {languageData?.map((data, index) => {
                return (
                    <option key={index} value={data.languageID}>{data.languageName}</option>
                );
            })}
        </select>
        </form>
        <button disabled={languageSelector === "" && languageData!.length >= 2 ? true: false} onClick={onClose}>Close</button>
      </div>
    </div>
  );
}


export default EditModal;