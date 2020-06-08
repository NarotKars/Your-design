import React from "react";
import { useState, useEffect } from 'react';
import axios from "axios";
// nodejs library that concatenates classes
import classNames from "classnames";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
// @material-ui/icons
import ShoppingBasketIcon from '@material-ui/icons/ShoppingBasket';
import HistoryIcon from '@material-ui/icons/History';
import Favorite from "@material-ui/icons/Favorite";
import FeedbackIcon from '@material-ui/icons/Feedback';
// core components
import Header from "components/Header/Header.js";
import Footer from "components/Footer/Footer.js";
import Button from "components/CustomButtons/Button.js";
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
import HeaderLinks from "components/Header/HeaderLinks.js";
import NavPills from "components/NavPills/NavPills.js";
import Parallax from "components/Parallax/Parallax.js";
import Table from "components/Table/Table.js";
import TableBasket from "components/Table/TableBasket.js";
import CustomInput from "components/CustomInput/CustomInput.js";
import profile from "assets/img/faces/profile.png";

import styles from "assets/jss/material-kit-react/views/profilePage.js";

const useStyles = makeStyles(styles);

export default function ProfilePage(props) {
  console.log(props);
  const classes = useStyles();
  const { ...rest } = props;
  const imageClasses = classNames(
    classes.imgRaised,
    classes.imgRoundedCircle,
    classes.imgFluid
  );
  const navImageClasses = classNames(classes.imgRounded, classes.imgGallery);
  const userId=1;
  const [personalInfo,setPersonalInfo]=useState([]);
  const [address,setAddress]=useState([]);
  const [isEnteredFeedback, setIsEnteredFeedback]=useState(true);
  const [feedback, setFeedbackState]=useState("");
  const [isFeedbacked, setIsFeedbacked]=useState(false);

  const setFeedback = e => {
    setFeedbackState(e.target.value);
    if(isEnteredFeedback===false && e.target.value!=="" && e.target.value.trim()!=="") setIsEnteredFeedback(true);
  }

  function PostWish ()
  {
    if(feedback.trim()==="")
    {
      if(feedback==="" || feedback.trim()==="") setIsEnteredFeedback(false);
      else setIsEnteredFeedback(true);
    }
    else
    {
      const myFeedback={
       customerId: parseInt(props.id,10),
        feedback: feedback
      }
      console.log(myFeedback);
      fetch('http://narotkars-001-site1.htempurl.com/api/Feedback', {
            method: 'POST',
            body: JSON.stringify(myFeedback),
            headers: { 'Content-Type' : 'application/json'} })
            .catch(error => console.error('Error:', error))
      setIsFeedbacked(true);
    }
  }
  return (
    <div>
      <Header
        color="transparent"
        brand="Your Design"
        rightLinks={<HeaderLinks />}
        fixed
        changeColorOnScroll={{
          height: 200,
          color: "white"
        }}
        {...rest}
      />
      <Parallax small filter image={require("assets/img/profile-bg.jpg")} />
      <div className={classNames(classes.main, classes.mainRaised)}>
        <div>
          <div className={classes.container}>
            <GridContainer justify="center">
              <GridItem xs={12} sm={12} md={6}>
                <div className={classes.profile}>
                  <div>
                    <img src={profile} alt="..." className={imageClasses} />
                  </div>
                  <div className={classes.name}>
                    <h3 className={classes.title}>{localStorage.getItem('name')}</h3>
                  </div>
                </div>
              </GridItem>
            </GridContainer>
            <GridContainer justify="center">
              <GridItem xs={12} sm={12} md={10} className={classes.navWrapper}>
                <NavPills
                  alignCenter
                  color="primary"
                  tabs={[
                    {
                      tabButton: "Basket",
                      tabIcon:  ShoppingBasketIcon,
                      tabContent: (
                        <GridContainer justify="center">
                          <GridItem xs={12} sm={12} md={12}>
                            <TableBasket id={props.id} city={address.city}/>
                          </GridItem>
                        </GridContainer>
                      )
                    },
                    {
                      tabButton: "Order History",
                      tabIcon: HistoryIcon,
                      tabContent: (
                        <Table id={props.id}/>
                      )
                    },
                    {
                      tabButton: "Feedback",
                      tabIcon: FeedbackIcon,
                      tabContent: (
                        isFeedbacked===false ?
                        <GridContainer justify="center">
                          <GridItem x={12} sm={12} md={12}>
                          “We all need people who will give us feedback. That’s how we improve.”
                          <br />– Bill Gates
                          <br /> Write feedback and we can be great together
                          </GridItem>
                          <GridItem xs={12} sm={12} md={12}>
                            {isEnteredFeedback ?
                          <CustomInput
                              inputProps={{
                                onChange: (e) => setFeedback(e)
                              }}         
                              labelText="YOUR FEEDBACK"
                              id="float"
                              formControlProps={{
                                fullWidth: true
                              }}
                            />:
                            <CustomInput
                                inputProps={{
                                  onChange: (e) => setFeedback(e)
                                }}      
                                error   
                                labelText="please enter your feedback"
                                id="float"
                                formControlProps={{
                                  fullWidth: true
                                }}
                              />}
                          </GridItem>
                          <GridItem xs={12} sm={12} md={12} align="right" className={classes.marginLeft}>
                            <Button color="primary" round onClick={PostWish}>
                              <Favorite className={classes.icons} /> POST
                            </Button>
                          </GridItem>
                        </GridContainer>:
                        <GridContainer>
                        <GridItem align="center">
                          <h1>THANK Y❤️U</h1>
                        </GridItem>
                      </GridContainer>
                      )
                    }
                  ]}
                />
              </GridItem>
            </GridContainer>
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
}
