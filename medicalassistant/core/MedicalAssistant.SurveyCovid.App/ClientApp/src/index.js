import React from "react";
import ReactDOM from "react-dom";
import { App } from "./App";
import registerServiceWorker from "./registerServiceWorker";
//import "../node_modules/material-dashboard/assets/css/material-dashboard.css";

//const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");

ReactDOM.render(<App />, document.getElementById("root"));

registerServiceWorker();
