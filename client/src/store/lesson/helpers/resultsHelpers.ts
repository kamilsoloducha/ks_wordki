import Results from 'pages/lesson/models/results'

export function calculateResultsForCorrect(
  baseState: Results,
  isAnswerCorrect: boolean,
  lessonLength: number
): Results {
  const shouldResultIncrease = lessonLength > baseState.answers

  let correct = baseState.correct
  if (isAnswerCorrect && shouldResultIncrease) correct++

  let accept = baseState.accept
  if (!isAnswerCorrect && shouldResultIncrease) accept++

  const answers = baseState.answers + 1

  return { ...baseState, correct, accept, answers }
}

export function calculateResultsForWrong(baseState: Results, lessonLength: number): Results {
  const shouldResultIncrease = lessonLength > baseState.answers

  let wrong = baseState.wrong
  if (shouldResultIncrease) wrong++

  const answers = baseState.answers + 1

  return { ...baseState, wrong, answers }
}
