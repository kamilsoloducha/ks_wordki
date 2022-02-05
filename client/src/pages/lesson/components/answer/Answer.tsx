import "./Answer.scss";
import { ReactElement } from "react";
import { AnswerLetter } from "pages/lesson/models/answer";
import { getAnswerLetters } from "pages/lesson/services/getAnswerLetters";

export default function Answer({
  isVisible,
  userAnswer,
  correctAnswer,
  exampleAnswer,
}: Model): ReactElement {
  const answerLetters = getAnswerLetters(correctAnswer, userAnswer);
  return (
    <div className={`correct-answer `}>
      <div className="correct-answer-header">Poprawna odpowiedź</div>
      <div className="correct-answer-value">
        {answerLetters.map((item: AnswerLetter, i: number) => (
          <span
            key={i}
            className={`correct-answer-value ${
              item.isCorrect ? "correct" : "wrong"
            } ${item.isAdditional ? "additional" : ""}
            ${isVisible ? "" : "invisible"}`}
          >
            {item.letter}
          </span>
        ))}
      </div>
      <div className={`example-answer ${isVisible ? "" : "invisible"}`}>
        {exampleAnswer}
      </div>
    </div>
  );
}

interface Model {
  isVisible: boolean;
  userAnswer: string;
  correctAnswer: string;
  exampleAnswer: string;
}
