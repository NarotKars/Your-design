import React from "react";
import ReactDOM from "react-dom";
import { createBrowserHistory } from "history";
import { Router, Route, Switch } from "react-router-dom";

import "assets/scss/material-kit-react.scss?v=1.8.0";

// pages for this product
import Components from "views/Components/Components.js";
import LandingPage from "views/LandingPage/LandingPage.js";
import ProfilePage from "views/ProfilePage/ProfilePage.js";
import SignUpPage from "views/LoginPage/SignUp.js";
import LoginPage from "views/LoginPage/LoginPage.js";
import DesignerPage from "views/DesignerPage/DesignerPage.js";
import DesignerPageUnlogged from "views/DesignerPage/DesignerPageUnlogged.js";
import CompanyPage from "views/CompanyPage/CompanyPage.js";
import ComponentsLoggedIn from "views/Components/ComponentsLoggedIn.js"

var hist = createBrowserHistory();

ReactDOM.render(
  <Router history={hist}>
    <Switch>
      <Route path="/products-for-design/:categoryName" render={(props) => <DesignerPage {...props}  categoryName={props.match.params.categoryName}/>}/>
      <Route path="/products-categories/:categoryName" render={(props) => <DesignerPageUnlogged {...props}  categoryName={props.match.params.categoryName}/>}/>
      <Route path="/designer-page/:id" render={(props) => <LandingPage {...props}  productId={props.match.params.id}/>}/>
      <Route path="/profile-page/:id" render={(props) => <ProfilePage {...props} id={props.match.params.id}/>}/>
      <Route path="/login-page" component={LoginPage} />
      <Route path="/sign-up-page" component={SignUpPage} />
      <Route path="/company-page/:companyId" render={(props) => <CompanyPage {...props} companyId ={props.match.params.companyId}/>}/>
      <Route path="/:customerId" render={(props) => <ComponentsLoggedIn {...props} customerId={props.match.params.customerId}/>}/>
      <Route path="/" component={Components} />
    </Switch>
  </Router>,
  document.getElementById("root")
);
