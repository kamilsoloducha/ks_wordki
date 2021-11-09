export enum LessonStateEnum {
  BeforeLoading,
  Loading,
  StartLessonPending,
  CheckPending,
  AnswerPending,
  Pause,
  FinishPending,
}

export const BeforeLoading = {
  btnStart: false,
  btnCheck: false,
  btnCorrect: false,
  btnFinish: false,
  btnPause: false,
  btnWrong: false,
  card: false,
  answer: false,
} as LessonState;

export const Loading = {
  btnStart: false,
  btnCheck: false,
  btnCorrect: false,
  btnFinish: false,
  btnPause: false,
  btnWrong: false,
  card: false,
  answer: false,
} as LessonState;

export const StartLessonPending = {
  btnStart: true,
  btnCheck: false,
  btnCorrect: false,
  btnFinish: false,
  btnPause: false,
  btnWrong: false,
  card: false,
  answer: false,
} as LessonState;

export const CheckPending = {
  btnStart: false,
  btnCheck: true,
  btnCorrect: false,
  btnFinish: true,
  btnPause: true,
  btnWrong: false,
  card: true,
  answer: false,
} as LessonState;

export const AnswerPending = {
  btnStart: false,
  btnCheck: false,
  btnCorrect: true,
  btnFinish: true,
  btnPause: true,
  btnWrong: true,
  card: true,
  answer: true,
} as LessonState;

export const Pause = {
  btnStart: true,
  btnCheck: false,
  btnCorrect: false,
  btnFinish: true,
  btnPause: false,
  btnWrong: false,
  card: false,
  answer: false,
} as LessonState;

export const FinishPending = {
  btnStart: false,
  btnCheck: false,
  btnCorrect: false,
  btnFinish: true,
  btnPause: false,
  btnWrong: false,
  card: false,
  answer: false,
} as LessonState;

export class LessonState {
  private static directory = new Map<LessonStateEnum, LessonState>([
    [LessonStateEnum.BeforeLoading, BeforeLoading],
    [LessonStateEnum.Loading, Loading],
    [LessonStateEnum.StartLessonPending, StartLessonPending],
    [LessonStateEnum.CheckPending, CheckPending],
    [LessonStateEnum.AnswerPending, AnswerPending],
    [LessonStateEnum.Pause, Pause],
    [LessonStateEnum.FinishPending, FinishPending],
  ]);

  constructor(
    public readonly btnStart: boolean,
    public readonly btnCheck: boolean,
    public readonly btnCorrect: boolean,
    public readonly btnWrong: boolean,
    public readonly btnPause: boolean,
    public readonly btnFinish: boolean,
    public readonly card: boolean,
    public readonly answer: boolean
  ) {}

  public static getState(state: LessonStateEnum): LessonState {
    return this.directory.get(state) as LessonState;
  }
}
