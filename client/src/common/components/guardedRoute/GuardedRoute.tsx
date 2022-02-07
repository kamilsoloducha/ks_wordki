import { ReactElement } from "react";
import { Redirect, Route, RouteComponentProps } from "react-router";

export default function GuardedRoute({ path, component, auth }: Model): ReactElement {
  return auth ? <Route path={path} component={component} /> : <Redirect to="/login" />;
}

interface Model {
  path: string;
  component: React.ComponentType<RouteComponentProps<any>> | React.ComponentType<any> | undefined;
  auth: boolean;
}
