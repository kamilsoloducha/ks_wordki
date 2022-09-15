import { ReactElement, ReactFragment } from "react";
import {  Route, Navigate } from "react-router-dom";

export default function GuardedRoute({ path, component, auth }: Model) {
  return <Route path={path} element={component} />;
}

interface Model {
  path: string;
  component: ReactElement
  auth: boolean;
}
