import { ReactElement } from "react";
import "./LanguageSelector.scss";

export function LanguageSelector({ selected, onSelectedChanged }: Model): ReactElement {
  const onLanguageChanged = ($event: any) => {
    const value = parseInt($event.target.value);
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
        <input
          className="input-fiszki"
          type="checkbox"
          name="language"
          id="polish"
          value={1}
          onChange={onLanguageChanged}
          checked={selected.includes(1)}
        />
        <label htmlFor="polish" className="item-container">
          <img alt="polish" className="setting-icon" src="/flags/polish.svg" />
          <p>Polish</p>
        </label>
        <input
          className="input-fiszki"
          type="checkbox"
          name="language"
          id="english"
          value={2}
          onChange={onLanguageChanged}
          checked={selected.includes(2)}
        />
        <label htmlFor="english" className="item-container">
          <img alt="english" className="setting-icon" src="/flags/english.svg" />
          <p>English</p>
        </label>
      </div>
    </div>
  );
}

interface Model {
  selected: number[];
  onSelectedChanged: (value: number[]) => void;
}
