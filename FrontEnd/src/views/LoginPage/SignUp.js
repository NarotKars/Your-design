import React from "react";
import { useState, useEffect } from 'react';
import { Link } from "react-router-dom";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputAdornment from "@material-ui/core/InputAdornment";
import Icon from "@material-ui/core/Icon";
// @material-ui/icons
import Email from "@material-ui/icons/Email";
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
import Radio from "@material-ui/core/Radio";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import FiberManualRecord from "@material-ui/icons/FiberManualRecord";
import radioStyles from "assets/jss/material-kit-react/views/componentsSections/basicsStyle.js";
import styles from "assets/jss/material-kit-react/views/loginPage.js";

import image from "assets/img/bg7.jpg";
const useStyles = makeStyles(styles);
const useRadioStyles = makeStyles(radioStyles);
export default function LoginPage(props) {
  const [cardAnimaton, setCardAnimation] = React.useState("cardHidden");
  setTimeout(function() {
    setCardAnimation("");
  }, 700);
  const classes = useStyles();
  const radioClasses = useRadioStyles();
  const { ...rest } = props;
  const [name, setNameState] = useState("");
  const [email, setEmailState] = useState("");
  const [password, setPasswordState] = useState("");
  const [isEnteredName, setIsEnteredName] =useState(true);
  const [isEnteredEmail, setIsEnteredEmail] = useState(true);
  const [isEnteredPassword, setIsEnteredPassword] = useState(true);
  const [helperText, setHelperText]= useState("");
  const [isOk, setIsOk] = useState(false);
  const [data, setData] =useState();
function checkCapital (password){
    for(var i=0;i<password.length;i++)
    {
        if(password[i]>='A' && password[i]<='Z') return true;
    }
    return false;
  }
  function checkNumber(password){
    
    for(var i=0;i<password.length;i++)
    {
      if(password[i]>='0' && password[i]<='9') return true;
    }
    return false;
  }
  const setName = e => {
    setNameState(e.target.value);
    if(isEnteredName===false && e.target.value!=="" && e.target.value.trim()!=="") setIsEnteredName(true);
  }
  const setEmail = e => {
    setEmailState(e.target.value);
    if(isEnteredEmail===false && e.target.value!=="" && e.target.value.trim()!=="") setIsEnteredEmail(true);
  }
  const setPassword = e => {
    setPasswordState(e.target.value);
    if(isEnteredPassword===false && e.target.value!=="" && e.target.value.trim()!=="") setIsEnteredPassword(true);
    console.log(password);
  }
  function Login () {
    var s=0;
    for(var k=0;k<name.length;k++)
    {
      if(name[k]===' ') s=100;
    }
    if(s===100) 
    {
      console.log(s);
      setIsEnteredName(false);
      setHelperText("Without whitespaces!!!");
    }
    else if(name==="" || name.trim()==="" || email==="" || email.trim()==="" || password==="" || password.trim()==="" || selectedEnabled==="")
    {
      if(name==="" || name.trim()==="") setIsEnteredName(false);
      if(email==="" || email.trim()==="") setIsEnteredEmail(false);
      if(password==="" || password.trim()==="") setIsEnteredPassword(false);
      if(selectedEnabled==="") setHelperText("Please check one of the radio buttons");
      else setHelperText("");
    }   
    else if(password.length<6)
    {
      setIsEnteredPassword(false);
      setHelperText("Your password must be at least six digits");
    }
    else if(!checkCapital(password))
    {
      setIsEnteredPassword(false);
      setHelperText("Your password must contain at least one Capital letter");
    }
    else if(!checkNumber(password))
    {
      setIsEnteredPassword(false);
      setHelperText("Your password must contain at least one digit");
    }
    else
    {
      setHelperText("");
    const myUser = {
      name : name,
      email: email,
      passwordHash: password,
      rank : selectedEnabled
    }
    console.log(myUser);
    fetch('http://narotkars-001-site1.htempurl.com/api/Users/Register', {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(myUser),
            headers: { 'Content-Type' : 'application/json', 'Accept': 'application/json'} })
            .then(res => res.json())
            .then(data => {
              if((typeof data == 'string' || data instanceof String) && data.startsWith("This"))
              {
                setHelperText("Please change the user name. This user name is already registered");
                setIsOk(false);
              }
              else if((typeof data == 'string' || data instanceof String) && data.startsWith("Account"))
              {
                setHelperText("This account is deleted");
                setIsOk(false);
              }
              else 
              {
                localStorage.setItem('id',data.data.id);
                localStorage.setItem('name', data.data.name);
                setData(data.data);
                setIsOk(true);
              }
            })
            .catch(error => console.error('Error:', error))
  }
  }

  const [selectedEnabled, setSelectedEnabled] = React.useState("");
  function checkAnswerA()
  {
    setSelectedEnabled("customer");
  }
  function checkAnswerB()
  {
    setSelectedEnabled("company");
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
                    <h4>SIGN UP</h4>
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
                    { isEnteredEmail ?
                    <CustomInput
                      labelText="Email"
                      id="email"
                      formControlProps={{
                        fullWidth: true
                      }}
                      inputProps={{
                        onChange : (e) => setEmail(e),
                        type: "email",
                        endAdornment: (
                          <InputAdornment position="end">
                            <Email className={classes.inputIconsColor} />
                          </InputAdornment>
                        )
                      }}
                    />:
                    <CustomInput
                      inputProps={{
                        onChange: (e) => setEmail(e),
                        type: "email",
                        endAdornment: (
                          <InputAdornment position="end">
                            <Email className={classes.inputIconsColor} />
                          </InputAdornment>
                        )
                      }}
                      error
                      labelText="Please enter your email"
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
                    <div
                className={
                  radioClasses.checkboxAndRadio +
                  " " +
                  radioClasses.checkboxAndRadioHorizontal
                }
              >
                  <FormControlLabel
                  control={
                    <Radio
                      checked={selectedEnabled === "customer"}
                      onChange={checkAnswerA}
                      value="customer"
                      name="radio button enabled"
                      aria-label="A"
                      icon={<FiberManualRecord className={radioClasses.radioUnchecked} />}
                      checkedIcon={<FiberManualRecord className={radioClasses.radioChecked} />}
                      classes={{checked: radioClasses.radio,
                                root: radioClasses.radioRoot}}/>}
                      classes={{label: radioClasses.label,
                                root: radioClasses.labelRoot}}
                  label="customer"
                /></div>

<div
                className={
                  radioClasses.checkboxAndRadio +
                  " " +
                  radioClasses.checkboxAndRadioHorizontal
                }
              >
                  <FormControlLabel
                  control={
                    <Radio
                      checked={selectedEnabled === "company"}
                      onChange={checkAnswerB}
                      value="company"
                      name="radio button enabled"
                      aria-label="A"
                      icon={<FiberManualRecord className={radioClasses.radioUnchecked} />}
                      checkedIcon={<FiberManualRecord className={radioClasses.radioChecked} />}
                      classes={{checked: radioClasses.radio,
                                root: radioClasses.radioRoot}}/>}
                      classes={{label: radioClasses.label,
                                root: radioClasses.labelRoot}}
                  label="company"
                /></div>

                    <p>{helperText}</p>
                  </CardBody>
                  <CardFooter className={classes.cardFooter}>
                    {isOk===false ?
                    <Button simple color="primary" size="lg" onClick={() => Login()}>
                      Get started
                    </Button>: isOk===true && selectedEnabled==="customer" ?
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
