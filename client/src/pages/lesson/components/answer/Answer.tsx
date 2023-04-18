import "./Answer.scss";
import { ReactElement } from "react";
import * as leven from "pages/lesson/services/levenshteinDistance";
import { compare } from "pages/lesson/services/compare";

export default function Answer({
  isVisible,
  userAnswer,
  correctAnswer,
  exampleAnswer,
}: Model): ReactElement {
  const isCorrect = compare(correctAnswer, userAnswer);
  const answer = leven.levenshtein(correctAnswer, userAnswer);
  return (
    <div className={`correct-answer `}>
      <div className="correct-answer-value">
        {answer.map((item: leven.LevenpathResult, i: number) => (
          <span
            key={i}
            className={`correct-answer-value ${
              item.type === leven.Equal || isCorrect ? "correct" : "wrong"
            } ${item.type === leven.Insert ? "additional" : ""}
            ${isVisible ? "" : "invisible"}`}
          >
            {item.char}
          </span>
        ))}
      </div>
      <div className={`example-answer ${isVisible ? "" : "invisible"}`}>{exampleAnswer}</div>
    </div>
  );
}

interface Model {
  isVisible: boolean;
  userAnswer: string;
  correctAnswer: string;
  exampleAnswer: string;
}
