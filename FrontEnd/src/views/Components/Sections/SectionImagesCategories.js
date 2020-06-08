import React from "react";
import { useState, useEffect } from 'react';
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import { Link } from "react-router-dom";
// @material-ui/icons

// core components
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";
import Button from "components/CustomButtons/Button.js";
import ShoppingBasketIcon from '@material-ui/icons/ShoppingBasket';
import BrushIcon from '@material-ui/icons/Brush';

import styles from "assets/jss/material-kit-react/views/componentsSections/typographyStyle.js";
import axios from "axios";

const useStyles = makeStyles(styles);

export default function SectionImages(props) {
    console.log(props.categoryName);
  const classes = useStyles();
  const [newProducts, setNewProducts] = useState([])
  const [permanentProducts, setPermanentProducts]=useState([])
  const [basket, setBasket]=useState([])
  useEffect(() => {
    axios.get('http://narotkars-001-site1.htempurl.com/api/Products')
    .then(res => {
        setNewProducts(res.data.filter(product => (product.status==="new" || product.status==="HPNew") && product.categoryName===props.categoryName))
        setPermanentProducts(res.data.filter(product => (product.status==="permanent" || product.status==="template" || product.status==="HPPermanent") && product.categoryName===props.categoryName))
    })
    .catch(err => {
        console.log(err)
    })
  },[props.categoryName]);
  useEffect(() => {
    axios.get('http://narotkars-001-site1.htempurl.com/api/Orders/' + localStorage.getItem('id'))
    .then(res => {
        setBasket(res.data.filter(order => order.status==="notOrdered"));
    })
    .catch(err => {
        console.log(err)
    })
  },[]);


  function addToBasket(id)
  {
    if(basket.length===0)
    {
      let newDate = new Date()
      let date = newDate.getDate();
      let month = newDate.getMonth() + 1;
      let year = newDate.getFullYear();
      let separator='-';
      let i=-1,j=-1,k=0,a=0,prodId=-1;
      i=newProducts.map(product => product.id).indexOf(id);
      j=permanentProducts.map(product => product.id).indexOf(id);
      if(i===-1)
      {
        var prod=permanentProducts.filter(item => item.id=id);
        a=prod[0].sellingPrice;
      }
      else 
      {
        var prod=newProducts.filter(item => item.id=id);
        a=prod[0].sellingPrice;
      }
      const myOrder={
        date: `${year}${separator}${month<10?`0${month}`:`${month}`}${separator}${date<10?`0${date}`:`${date}`}`,
        status: 'notOrdered',
        amount: 0,
        customerId: parseInt(localStorage.getItem('id'),10),
        productId: id
       }
       setBasket(myOrder);
       console.log(myOrder);
       fetch('http://narotkars-001-site1.htempurl.com/api/Orders', {
             method: 'POST',
             body: JSON.stringify(myOrder),
             headers: { 'Content-Type' : 'application/json'} })
            .catch(error => console.error('Error:', error))
    }
    else
    {
      const myOrder={
        customerId: parseInt(localStorage.getItem('id'),10),
        productId: id
       }
       console.log(myOrder);
       fetch('http://narotkars-001-site1.htempurl.com/api/OrderDetails/SetProductInCustomersCurrentOrder', {
        method: 'POST',
        body: JSON.stringify(myOrder),
        headers: { 'Content-Type' : 'application/json'} })
        .catch(error => console.error('Error:', error))
    }
  }


  return (
    <div className={classes.section}>
      <div className={classes.container}>
        <div className={classes.space50} />
        <div id="images">
          <div className={classes.title}>
            <h2>Newly added</h2>
          </div>
          <GridContainer>
                      {newProducts.map(product => (
                        <GridItem xs={12} sm={2} key={product.id}>
                          <h4>{product.companyName}</h4>
                          <img src={`data:image/jpeg;base64,${product.photo}`} 
                              alt="..."
                              className={classes.imgRounded + " " + classes.imgFluid}/>
                          <Button justIcon round color="primary"  onClick={()=>addToBasket(product.id)}>
                            <ShoppingBasketIcon className={classes.icons} />
                          </Button>
                          
                          <Link to={`/designer-page/${product.id}`} className={classes.link}>
                            <Button justIcon round color="primary">
                              <BrushIcon className={classes.icons} />
                            </Button>
                          </Link>
                          <h4>price: {product.sellingPrice}</h4>
                        </GridItem>
                      ))}
          </GridContainer>
          </div>
          <div id="images">
          <div className={classes.title}>
            <h2>Our products</h2>
          </div>
          <GridContainer>
                      {permanentProducts.map(product => (
                        <GridItem xs={12} sm={2} key={product.id}>
                           <h4>{product.companyName}</h4>
                          <img src={`data:image/jpeg;base64,${product.photo}`} 
                              alt="..."
                              className={classes.imgRounded + " " + classes.imgFluid}/>
                          <Button justIcon round color="primary" onClick={()=>addToBasket(product.id)}>
                            <ShoppingBasketIcon className={classes.icons} />
                          </Button>
                          <Link to={`/designer-page/${product.id}`} className={classes.link}>
                            <Button justIcon round color="primary">
                              <BrushIcon className={classes.icons} />
                            </Button>
                          </Link>
                          <h4>price: {product.sellingPrice}</h4>
                        </GridItem>
                      ))}
          </GridContainer>
          </div>
      </div>
    </div>
  );
}
