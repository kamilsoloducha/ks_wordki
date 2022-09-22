import { useTitle } from "common";
import { ReactElement } from "react";

export default function ErrorPage(): ReactElement {
    useTitle("Error");
    return <>
        <h2>Error occured</h2>
        <p>An unexpected exception occured.</p>
    </>;
}