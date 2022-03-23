import React from "react";

export function Footer() {
  return (
    <footer>
      <div className="container-fluid">
        <div>
          <strong>
            Copyright © 2020
            <a href="/"> Medical Assistant. </a>
          </strong>
          All rights reserved.
        </div>
        <div className="float-right d-none d-sm-inline-block">
          <b>Version </b>
          0.0.1-dev
        </div>
      </div>
    </footer>
  );
}
