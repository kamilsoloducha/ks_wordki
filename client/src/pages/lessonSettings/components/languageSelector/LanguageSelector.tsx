import { Language } from "pages/lessonSettings/models/languages";
import React from "react";
import { ReactElement } from "react";
import "./LanguageSelector.scss";

export function LanguageSelector({ languages, selected, onSelectedChanged }: Model): ReactElement {
  const onLanguageChanged = ($event: any) => {
    const value = $event.target.value;
    if (selected.includes(value)) {
      const index = selected.indexOf(value);
      const newSelected = [...selected];
      newSelected.splice(index, 1);
      onSelectedChanged(newSelected);
    } else {
      const newSelected = [...selected, value];
      onSelectedChanged(newSelected);
    }
  };

  return (
    <div className="language-container">
      <p>Question language:</p>
      <div className="language-items-container">
        {languages.map((item, index) => (
          <React.Fragment key={index}>
            <input
              className="input-fiszki"
              type="checkbox"
              name="language"
              id={item.language}
              value={item.language}
              onChange={onLanguageChanged}
              checked={selected.includes(item.language)}
            />
            <label htmlFor={item.language} className="item-container">
              <p>{item.language}</p>
            </label>
          </React.Fragment>
        ))}
      </div>
    </div>
  );
}

interface Model {
  languages: Language[];
  selected: string[];
  onSelectedChanged: (value: string[]) => void;
}
