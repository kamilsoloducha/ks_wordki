import "./CardItem.scss";
import { ReactElement } from "react";
import { CardSummary, SideSummary } from "pages/cards/models";

export function CardItem({ card, onClick }: RowModel): ReactElement {
  return (
    <div className="row-container" onClick={() => onClick && onClick(card)}>
      <div className="row-sides">
        <Side side={card.front} />
        <Side side={card.back} />
      </div>
    </div>
  );
}

export interface RowModel {
  card: CardSummary;
  onClick?: (card: CardSummary) => void;
}

function Side({ side }: { side: SideSummary }): ReactElement {
  const extendedText = convertIntoExtendedText(side.value);
  return (
    <div className={`row-side-container ${drawerClassName(side.drawer)}`}>
      <div className="row-side-value">
        <strong>
          {extendedText.map((x, index) => (
            <span key={index} className={x.isLowlight ? "lowlight" : ""}>
              {x.text}
            </span>
          ))}
        </strong>
      </div>
      <div className="row-side-example">{side.example}</div>
    </div>
  );
}

function drawerClassName(drawer: number): string {
  return "drawer" + drawer;
}

export function convertIntoExtendedText(text: string): { text: string; isLowlight: boolean }[] {
  const result: { text: string; isLowlight: boolean }[] = [];
  if (!text.includes("{")) {
    result.push({ text, isLowlight: false });
    return result;
  }
  const begins = [];
  const ends = [];

  for (let i = 0; i < text.length; i++) {
    const element = text[i];
    if (element === "{") {
      begins.push(i);
      continue;
    }
    if (element === "}") {
      ends.push(i);
      continue;
    }
  }

  if (!text.startsWith("{")) {
    result.push({ text: text.slice(0, begins[0]), isLowlight: false });
  }

  for (let i = 0; i < begins.length; i++) {
    result.push({ text: text.slice(begins[i] + 1, ends[i]), isLowlight: true });
    if (i + 1 < begins.length) {
      const begin = ends[i] + 1;
      const end = begins[i + 1];
      if (begin !== end)
        result.push({ text: text.slice(ends[i] + 1, begins[i + 1]), isLowlight: false });
    }
  }

  if (!text.endsWith("}")) {
    result.push({ text: text.slice(ends[ends.length - 1] + 1), isLowlight: false });
  }

  return result;
}
