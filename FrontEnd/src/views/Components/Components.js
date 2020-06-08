import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
// @material-ui/icons
// core components
import Header from "components/Header/HeaderUnLogged.js";
import Footer from "components/Footer/Footer.js";
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
import Parallax from "components/Parallax/Parallax.js";
import Danger from "components/Typography/Danger.js";
// sections for this page
import HeaderLinks from "components/Header/HeaderLinksUnlogged.js";
import SectionImages from "./Sections/SectionImagesNotLoggedIn.js";

import styles from "assets/jss/material-kit-react/views/components.js";

const useStyles = makeStyles(styles);

export default function Components(props) {
  const classes = useStyles();
  const { ...rest } = props;
  return (
    <div>
      <Header
        brand="Your Design"
        rightLinks={<HeaderLinks />}
        fixed
        color="transparent"
        changeColorOnScroll={{
          height: 400,
          color: "white"
        }}
        {...rest}
      />
      <Parallax image={require("assets/img/bg4.jpg")}>
        <div className={classes.container}>
          <GridContainer>
            <GridItem>
              <div className={classes.brand}>
                <h1 className={classes.title}>Let Your Creativity Fly</h1>
                <h3 className={classes.subtitle}>
                  Create Your Own, Because You're Special
                </h3>
              </div>
            </GridItem>
          </GridContainer>
        </div>
      </Parallax>

      <div className={classNames(classes.main, classes.mainRaised)}>
        <SectionImages />
        <div className={classes.container}>
        <Danger>
          Sign up or login and create Your design :)
        </Danger>
        </div>
        <div className={classes.container}>
        <Danger>
          &nbsp;
        </Danger>
        </div>
      </div>
      <Footer />
    </div>
  );
}
