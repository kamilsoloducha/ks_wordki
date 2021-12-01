import { ReactElement, useEffect } from "react";
import { useDispatch } from "react-redux";
import { resetAll } from "store/lesson/actions";

export default function LessonResult(): ReactElement {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(resetAll());
  }, [dispatch]);

  return <>results</>;
}
