export interface AnswerLetter2 {
  letter: string;
  status: number; // 0-correct,  1-additional, 2-missing, 3-changed, 4-wrong
}

export function getAnswerLettersAdvanced(
  correctAnswer: string,
  userAnswer: string
): AnswerLetter2[] {
  const correctWords = correctAnswer.split(" ");
  const userWords = userAnswer.split(" ");
  let result: AnswerLetter2[] = [];
  if (correctWords.length == userWords.length) {
    for (let i = 0; i < correctWords.length; i++) {
      if (i !== 0) {
        result.push({ letter: " ", status: 0 });
      }
      result = result.concat(compareWord(correctWords[i], userWords[i]));
    }
  } else {
    return compareWord(correctAnswer, userAnswer);
  }
  return result;
}

export function getAnswerLetters(correctWord: string, userAnswer: string): AnswerLetter2[] {
  const result: AnswerLetter2[] = [];
  let i = 0;
  for (; i < correctWord.length; i++) {
    const correctLetter = correctWord[i];
    const answerLetter = i < userAnswer.length ? userAnswer[i] : "";

    result.push({
      letter: correctLetter,
      status: correctLetter === answerLetter ? 0 : 4,
    } as AnswerLetter2);
  }

  if (correctWord.length < userAnswer.length) {
    for (; i < userAnswer.length; i++) {
      const additionalLetter = userAnswer[i];

      result.push({
        letter: additionalLetter,
        status: 1,
      } as AnswerLetter2);
    }
  }

  return result;
}

export function compareWord(correctWord: string, userAnswer: string): AnswerLetter2[] {
  if (correctWord === userAnswer) {
    return createCorrectWord(correctWord);
  }
  const result: AnswerLetter2[] = [];

  if (correctWord.length === userAnswer.length) {
    for (let i = 0; i < correctWord.length; i++) {
      result.push({ letter: correctWord[i], status: correctWord[i] === userAnswer[i] ? 0 : 4 });
    }
    return result;
  }

  if (correctWord.length < userAnswer.length) {
    const userAnswerPrep = createWrongWord(userAnswer);
    for (let i = 0; i < correctWord.length; i++) {
      const followingLetter = correctWord[i];
      const letter = userAnswerPrep.find((x) => x.letter == followingLetter && x.status === 4);
      if (letter) letter.status = 0;
    }
    const correctLetters = userAnswerPrep.filter((x) => x.status === 0);
    for (let i = 0; i < correctWord.length; i++) {
      if (correctWord[i] !== correctLetters[i].letter) {
        correctLetters[i].status = 4;
        correctLetters[i].letter = correctWord[i];
      }
    }
    return userAnswerPrep;
  }

  if (correctWord.length > userAnswer.length) {
    const correctAnswerPrep = createWrongWord(correctWord);
    for (let i = 0; i < userAnswer.length; i++) {
      const followingLetter = userAnswer[i];
      const letter = correctAnswerPrep.find((x) => x.letter == followingLetter && x.status === 4);
      if (letter) letter.status = 0;
    }
    const correctLetters = correctAnswerPrep.filter((x) => x.status === 0);
    for (let i = 0; i < userAnswer.length; i++) {
      if (userAnswer[i] !== correctLetters[i].letter) {
        correctLetters[i].status = 4;
      }
    }
    return correctAnswerPrep;
  }

  return result;
}

export function createCorrectWord(word: string): AnswerLetter2[] {
  return word.split("").map((letter) => {
    const mapped: AnswerLetter2 = {
      letter,
      status: 0,
    };
    return mapped;
  });
}

export function createWrongWord(word: string): AnswerLetter2[] {
  return word.split("").map((letter) => {
    const mapped: AnswerLetter2 = {
      letter,
      status: 4,
    };
    return mapped;
  });
}
