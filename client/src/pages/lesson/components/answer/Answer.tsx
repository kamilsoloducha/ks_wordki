import "./Answer.scss";
import { getAnswerLetters } from "pages/lesson/services/answerComparer";
import { ReactElement } from "react";
import { AnswerLetter } from "pages/lesson/models/answer";

export default function Answer({
  userAnswer,
  correctAnswer,
}: Model): ReactElement {
  const answerLetters = getAnswerLetters(correctAnswer, userAnswer);
  console.log(answerLetters);
  return (
    <div className="correct-answer">
      <div className="correct-answer-header">Poprawna odpowiedź</div>
      <div className="correct-answer-value">
        {answerLetters.map((item: AnswerLetter, i: number) => (
          <span
            key={i}
            className={`correct-answer-value ${
              item.isCorrect ? "correct" : "wrong"
            }`}
          >
            {item.letter}
          </span>
        ))}
      </div>
    </div>
  );
}

interface Model {
  userAnswer: string;
  correctAnswer: string;
}
