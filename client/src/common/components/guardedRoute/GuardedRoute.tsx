import { ReactElement } from "react";
import { Route } from "react-router-dom";

export default function GuardedRoute({ path, component, auth }: GuardedRouteParams) {
  return <Route path={path} element={component} />;
}

export type GuardedRouteParams = {
  path: string;
  component: ReactElement;
  auth: boolean;
};
