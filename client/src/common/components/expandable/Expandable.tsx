import "./Expandable.scss";
import { ReactElement, useState } from "react";

export default function Expandable({ children }: Model): ReactElement {
  const [isExpanded, setIsExpanded] = useState(false);

  const showMore = () => {
    setIsExpanded(!isExpanded);
  };

  return (
    <>
      <div className="expandable-container">
        <div className={isExpanded ? "" : "invisible"}>{children}</div>
        <div className="expandable-container-button">
          <img
            onClick={showMore}
            src={isExpanded ? "/svgs/arrow-up.svg" : "/svgs/arrow-down.svg"}
            alt="arrow"
          />
        </div>
      </div>
    </>
  );
}

interface Model {
  children?: React.ReactNode;
}
