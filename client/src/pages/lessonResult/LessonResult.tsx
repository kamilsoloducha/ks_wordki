import { ReactElement, useEffect } from "react";
import { useDispatch } from "react-redux";
import { reset } from "store/lesson/actions";

export default function LessonResult(): ReactElement {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(reset());
  }, [dispatch]);

  return <>results</>;
}
