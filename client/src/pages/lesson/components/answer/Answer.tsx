import "./Answer.scss";
import { ReactElement } from "react";
import { getAnswerLetters } from "pages/lesson/services/getAnswerLetters";
import * as leven from "pages/lesson/services/levenshteinDistance";

export default function Answer({
  isVisible,
  userAnswer,
  correctAnswer,
  exampleAnswer,
}: Model): ReactElement {
  const answerLetters = getAnswerLetters(correctAnswer, userAnswer);
  const answer = leven.levenshtein(correctAnswer, userAnswer);
  return (
    <div className={`correct-answer `}>
      <div className="correct-answer-value">
        {answer.map((item: any, i: number) => (
          <span
            key={i}
            className={`correct-answer-value ${item.type === leven.Equal ? "correct" : "wrong"} ${
              item.type === leven.Insert ? "additional" : ""
            }
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
