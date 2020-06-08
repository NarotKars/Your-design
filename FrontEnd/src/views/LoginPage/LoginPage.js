import React from "react";
import { useState, useEffect } from 'react';
import { Link } from "react-router-dom";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputAdornment from "@material-ui/core/InputAdornment";
import Icon from "@material-ui/core/Icon";
// @material-ui/icons
import People from "@material-ui/icons/People";
// core components
import Header from "components/Header/HeaderLogin.js";
import HeaderLinks from "components/Header/HeaderLinksLogin.js";
import Footer from "components/Footer/Footer.js";
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
import Button from "components/CustomButtons/Button.js";
import Card from "components/Card/Card.js";
import CardBody from "components/Card/CardBody.js";
import CardHeader from "components/Card/CardHeader.js";
import CardFooter from "components/Card/CardFooter.js";
import CustomInput from "components/CustomInput/CustomInput.js";
import styles from "assets/jss/material-kit-react/views/loginPage.js";

import image from "assets/img/bg7.jpg";
const useStyles = makeStyles(styles);
export default function LoginPage(props) {
  const [cardAnimaton, setCardAnimation] = React.useState("cardHidden");
  setTimeout(function() {
    setCardAnimation("");
  }, 700);
  const classes = useStyles();
  const { ...rest } = props;
  const [name, setNameState] = useState("");
  const [password, setPasswordState] = useState("");
  const [isEnteredName, setIsEnteredName] =useState(true);
  const [isEnteredPassword, setIsEnteredPassword] = useState(true);
  const [helperText, setHelperText]= useState("");
  const [isOk, setIsOk] = useState(false);
  const [data, setData] =useState();
  const setName = e => {
    setNameState(e.target.value);
    if(isEnteredName===false && e.target.value!=="" && e.target.value.trim()!=="") setIsEnteredName(true);
  }
  const setPassword = e => {
    setPasswordState(e.target.value);
    if(isEnteredPassword===false && e.target.value!=="" && e.target.value.trim()!=="") setIsEnteredPassword(true);
    console.log(password);
  }
  function Login () {
    if(name==="" || name.trim()==="" || password==="" || password.trim()==="")
    {
      if(name==="" || name.trim()==="") setIsEnteredName(false);
      if(password==="" || password.trim()==="") setIsEnteredPassword(false);
    }
    else
    {
      setHelperText("");
      const myUser = {
        name : name,
        passwordHash: password
    }
    console.log(myUser);
    fetch('http://narotkars-001-site1.htempurl.com/api/Users/Authenticate', {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(myUser),
            headers: { 'Content-Type' : 'application/json', 'Accept': 'application/json'} })
            .then(res => res.json())
            .then(data => {
              
              if((typeof data == 'string' || data instanceof String) && data.startsWith("Invalid"))
              {
                console.log(data);
                setHelperText("Invalid name or password");
                setIsOk(false);
              }
              else if((typeof data == 'string' || data instanceof String) && data.startsWith("This"))
              {
                setHelperText("This account is deleted");
                setIsOk(false);
              }
              else
              {
                localStorage.setItem('id',data.id);
                localStorage.setItem('name', data.name);
                setData(data);
                setIsOk(true);
              }
            })
            .catch(error => console.error('Error:', error))
  }
  }

  return (
    <div>
      <Header
        absolute
        color="transparent"
        brand="Your Design"
        rightLinks={<HeaderLinks />}
        {...rest}
      />
      <div
        className={classes.pageHeader}
        style={{
          backgroundImage: "url(" + image + ")",
          backgroundSize: "cover",
          backgroundPosition: "top center"
        }}
      >
        <div className={classes.container}>
          <GridContainer justify="center">
            <GridItem xs={12} sm={12} md={4}>
              <Card className={classes[cardAnimaton]}>
                <form className={classes.form}>
                  <CardHeader color="primary" className={classes.cardHeader}>
                    <h4>LOGIN</h4>
                  </CardHeader>
                  <CardBody>
                    {isEnteredName ?
                      <CustomInput
                      labelText="User Name"
                      id="first"
                      formControlProps={{
                        fullWidth: true
                      }}
                      inputProps={{
                        onChange: (e) => setName(e),
                        type: "text",
                        endAdornment: (
                          <InputAdornment position="end">
                            <People className={classes.inputIconsColor} />
                          </InputAdornment>
                        )
                      }}
                    /> :
                    <CustomInput
                      inputProps={{
                        onChange: (e) => setName(e),
                        type: "text",
                        endAdornment: (
                          <InputAdornment position="end">
                            <People className={classes.inputIconsColor} />
                          </InputAdornment>
                        )                       
                      }}
                      error
                      labelText="Please enter your name"
                      id="float"
                      formControlProps={{
                        fullWidth: true
                      }}
                    />}
                    { isEnteredPassword ?
                    <CustomInput
                      labelText="Password"
                      id="pass"
                      formControlProps={{
                        fullWidth: true
                      }}
                      inputProps={{
                        onChange : (e) => setPassword(e),
                        type: "password",
                        endAdornment: (
                          <InputAdornment position="end">
                            <Icon className={classes.inputIconsColor}>
                              lock_outline
                            </Icon>
                          </InputAdornment>
                        ),
                        autoComplete: "off"
                      }}
                    />:
                    <CustomInput
                      inputProps={{
                        onChange: (e) => setPassword(e),
                        type: "password",
                        endAdornment: (
                          <InputAdornment position="end">
                            <Icon className={classes.inputIconsColor}>
                              lock_outline
                            </Icon>
                          </InputAdornment>
                        ),
                        autoComplete: "off"
                      }}
                      error
                      labelText="Please enter your password"
                      id="float"
                      formControlProps={{
                        fullWidth: true
                      }}
                    />}

                    <p>{helperText}</p>
                  </CardBody>
                  <CardFooter className={classes.cardFooter}>
                    {isOk===false ?
                    <Button simple color="primary" size="lg" onClick={() => Login()}>
                      Get started
                    </Button>: isOk===true && data.rank==="customer" ?
                    <Link to={`/${data.id}`}>
                    <Button simple color="primary" size="lg" onClick={() => Login()}>
                    Get started
                  </Button> </Link>:
                  <Link to={`/company-page/${data.id}`}>
                  <Button simple color="primary" size="lg" onClick={() => Login()}>
                  Get started
                  </Button> </Link>}
                  </CardFooter>
                </form>
              </Card>
            </GridItem>
          </GridContainer>
        </div>
        <Footer whiteFont />
      </div>
    </div>
  );
}
