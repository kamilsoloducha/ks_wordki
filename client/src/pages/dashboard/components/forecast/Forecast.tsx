import "./Forecast.scss";
import { ReactElement } from "react";
import { useSelector } from "react-redux";
import { selectForecast } from "store/dashboard/selectors";

export function Forecast(): ReactElement {
  const data = useSelector(selectForecast);

  return (
    <div className="forecast-container">
      <div className="forecast-title">Repetitions forecast</div>
      <div className="forecast-items-container">
        {data
          ?.map((item: any) => prepareForecasModel(item))
          .map((item: any, index: number) => {
            return (
              <ForecaseItem
                key={index}
                count={item.count}
                date={item.date}
                weekDay={item.weekDay}
              />
            );
          })}
      </div>
    </div>
  );
}

function ForecaseItem({ count, weekDay, date }: ForecaseModel): ReactElement {
  return (
    <div className="forecast-item-container">
      <div className="forecast-item-date">
        {date}
        <br />
        {weekDay}
      </div>
      <div className="forecast-item-value">{count}</div>
    </div>
  );
}

interface ForecaseModel {
  count: number;
  weekDay: string;
  date: string;
}

// const data = [
//   { date: "02-09-2022", count: 530 },
//   { date: "02-10-2022", count: 321 },
//   { date: "02-11-2022", count: 121 },
//   { date: "02-12-2022", count: 54 },
//   { date: "02-13-2022", count: 20 },
//   { date: "02-14-2022", count: 1 },
//   { date: "02-15-2022", count: 0 },
// ];

function prepareForecasModel(data: any): ForecaseModel {
  const date = new Date(data.date);
  return {
    count: data.count,
    weekDay: date.toLocaleDateString(undefined, { weekday: "long" }),
    date: date.toLocaleDateString(undefined, { day: "2-digit", month: "short" }),
  };
}
