import React, { ReactNode } from "react";
import { Router } from "react-router-dom";

export const CustomRouter = ({ basename, children, history }: CustomRouterProps) => {
  const [state, setState] = React.useState({
    action: history.action,
    location: history.location,
  });

  React.useLayoutEffect(() => history.listen(setState), [history]);

  return (
    <Router
      basename={basename}
      children={children}
      location={state.location}
      navigationType={state.action}
      navigator={history}
    />
  );
};

export type CustomRouterProps = {
  basename?: string | undefined;
  children?: ReactNode;
  history: any;
};
