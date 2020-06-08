/*eslint-disable*/
import React from "react";
import { useState, useEffect } from 'react';
import axios from 'axios';
// react components for routing our app without refresh
import { Link } from "react-router-dom";

// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import Tooltip from "@material-ui/core/Tooltip";

// @material-ui/icons
import { Apps, CloudDownload } from "@material-ui/icons";
import ShoppingBasketIcon from '@material-ui/icons/ShoppingBasket';
// core components
import CustomDropdown from "components/CustomDropdown/CustomDropdown.js";
import Button from "components/CustomButtons/Button.js";

import styles from "assets/jss/material-kit-react/components/headerLinksStyle.js";

const useStyles = makeStyles(styles);

export default function HeaderLinks(props) {
  const classes = useStyles();
  const [categories, setCategories] = useState([])
  useEffect(() => {
    axios.get('http://narotkars-001-site1.htempurl.com/api/Categories')
    .then(res => {
        setCategories(res.data)
    })
    .catch(err => {
        console.log(err)
    })
  },[]);
  /*function Refresh () {
    alert('refersh');
    window.location.reload(false);
  }*/
  return (
    <List className={classes.list}>
      <ListItem className={classes.listItem}>
      <Link to={"/sign-up-page"} className={classes.listItem}>
        <Button
          color="transparent"
          target="_blank"
          className={classes.navLink}
        >
          SIGN UP
        </Button>
        </Link>
      </ListItem>
      <ListItem className={classes.listItem}>
      <Link to={"/login-page"} className={classes.listItem}>
        <Button
          color="transparent"
          target="_blank"
          className={classes.navLink}
        >
          LOGIN
        </Button>
        </Link>
      </ListItem>
      <ListItem className={classes.listItem}>
        <CustomDropdown
          noLiPadding
          buttonText="Design Categories"
          buttonProps={{
            className: classes.navLink,
            color: "transparent"
          }}
          buttonIcon={Apps}
          dropdownList={[
           
          categories.map(category => (
                        <Link to={`/products-categories/${category.name}`} className={classes.dropdownLink} key={category.id}>
                          {category.name}
                        </Link>
                      ))
                      
         
          ]}
        />
      </ListItem>
      <ListItem className={classes.listItem}>
        <Tooltip
          id="instagram-facebook"
          title="Follow us on facebook"
          placement={window.innerWidth > 959 ? "top" : "left"}
          classes={{ tooltip: classes.tooltip }}
        >
          <Button
            color="transparent"
            href="https://www.facebook.com/narodgars.garabedian"
            target="_blank"
            className={classes.navLink}
          >
            <i className={classes.socialIcons + " fab fa-facebook"} />
          </Button>
        </Tooltip>
      </ListItem>
      <ListItem className={classes.listItem}>
        <Tooltip
          id="instagram-tooltip"
          title="Follow us on instagram"
          placement={window.innerWidth > 959 ? "top" : "left"}
          classes={{ tooltip: classes.tooltip }}
        >
          <Button
            color="transparent"
            href="https://www.instagram.com/narod_gars_garabedian/"
            target="_blank"
            className={classes.navLink}
          >
            <i className={classes.socialIcons + " fab fa-instagram"} />
          </Button>
        </Tooltip>
      </ListItem>
    </List>
  );
}
