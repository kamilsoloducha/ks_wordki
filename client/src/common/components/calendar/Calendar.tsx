import { ReactElement } from "react";

const today = new Date();
const daysCount = 7;
const indexes = [0, 1, 2, 3, 4, 5, 6];
const days = indexes.map((x) => {
  const date = new Date();
  date.setDate(date.getDate() + x);
  return date;
});

export default function Calendar(): ReactElement {
  return <></>;
}
