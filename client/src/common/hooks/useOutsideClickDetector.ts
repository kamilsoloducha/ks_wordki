import React, { Ref, useEffect } from 'react'

export default function useOutsideClickDetector(
  ref: React.MutableRefObject<HTMLDivElement | null>,
  fn: () => void
) {
  useEffect(() => {
    function handleClickOutside(event: any) {
      if (ref.current && !ref.current.contains(event.target)) {
        fn()
      }
    }
    document.addEventListener('mousedown', handleClickOutside)
    return () => {
      document.removeEventListener('mousedown', handleClickOutside)
    }
  }, [ref])
}
