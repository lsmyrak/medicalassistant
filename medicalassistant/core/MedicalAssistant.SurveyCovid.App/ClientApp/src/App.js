import React, { useMemo } from "react";
import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";
import { Provider } from "react-redux";

import { privatePages, publicPages, NotFound } from "./pages";
import { store } from "./shared/state";
import { AppUrls } from "./shared/constants";
import { Layout } from "./components";
import { PrivateRoute, AuthProvider } from "./components/common";

import "./custom.css";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");

export function App() {
  const { publicPaths, publicPageRoutes } = useMemo(() => {
    const publicPaths = [];
    const publicPageRoutes = [];
    for (const page of publicPages) {
      publicPaths.push(page.path);
      publicPageRoutes.push(<Route key={page.path} exact={page.exact} {...page} />);
    }
    return { publicPaths, publicPageRoutes };
  }, []);

  const { privatePaths, privatePageRoutes } = useMemo(() => {
    const privatePaths = [];
    const privatePageRoutes = [];
    for (const page of privatePages) {
      privatePaths.push(page.path);
      privatePageRoutes.push(<PrivateRoute key={page.path} {...page} />);
    }
    return { privatePaths, privatePageRoutes };
  }, []);
  return (
    <AuthProvider>
      <Provider store={store}>
        <BrowserRouter basename={baseUrl}>
          <Switch>
            <Route exact path={publicPaths}>
              <Switch>{publicPageRoutes}</Switch>
            </Route>
            <Route exact path={privatePaths}>
              <Layout.Wrapper>
                <Switch>{privatePageRoutes}</Switch>
              </Layout.Wrapper>
            </Route>
            <Route exact path={AppUrls.NotFound}>
              <NotFound />
            </Route>
            <Route>
              <Redirect to={AppUrls.NotFound} />
            </Route>
          </Switch>
        </BrowserRouter>
      </Provider>
    </AuthProvider>
  );
}
