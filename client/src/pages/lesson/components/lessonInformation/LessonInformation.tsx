import * as sel from 'store/lesson/selectors'
import * as actions from 'store/lesson/reducer'
import { ReactElement } from 'react'
import { useAppDispatch, useAppSelector } from 'store/store'

export function LessonInformation(): ReactElement {
  const dispatch = useAppDispatch()
  const questions = useAppSelector(sel.selectRepeats)
  const results = useAppSelector(sel.selectResults)
  const status = useAppSelector(sel.selectLessonState)

  const markCard = () => {
    dispatch(actions.tickCard())
  }

  return (
    <div className="flex w-full flex-wrap items-center border-b border-b-accent-dark">
      <div className="flex grow flex-col items-center p-1">
        <div className="text-xs md:text-xl">Remained</div>
        <div className="md:text:3xl text-xl font-black">{questions.length}</div>
      </div>
      <div className="flex grow flex-col items-center p-1">
        <div className="text-xs md:text-xl">Counter</div>
        <div className="md:text:3xl text-xl font-black">{results.answers ?? 0}</div>
      </div>
      <div className="flex grow flex-col items-center p-1">
        <div className="text-xs md:text-xl">Correct</div>
        <div className="md:text:3xl text-xl font-black text-green-600">{results.correct ?? 0}</div>
      </div>
      <div className="flex grow flex-col items-center p-1">
        <div className="text-xs md:text-xl">Accepted</div>
        <div className="md:text:3xl text-xl font-black text-yellow-500">{results.accept ?? 0}</div>
      </div>
      <div className="flex grow flex-col items-center p-1">
        <div className="text-xs md:text-xl">Wrong</div>
        <div className="md:text:3xl text-xl font-black text-red-600">{results.wrong ?? 0}</div>
      </div>
      <div className={status.btnCorrect ? 'opacity-100' : 'opacity-0'}>
        <button
          onClick={markCard}
          className="cursor-pointer rounded-md border-none py-5 text-accent-super-light outline-none hover:bg-accent-light md:px-3"
        >
          Mark
        </button>
      </div>
    </div>
  )
}
