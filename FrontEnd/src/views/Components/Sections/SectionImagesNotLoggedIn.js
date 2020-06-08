import React from "react";
import { useState, useEffect } from 'react';
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
// @material-ui/icons

// core components
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";

import styles from "assets/jss/material-kit-react/views/componentsSections/typographyStyle.js";
import axios from "axios";

const useStyles = makeStyles(styles);
export default function SectionImages(props) {
  //console.log("section images");
  //console.log(parseInt(props.id,10));
  const classes = useStyles();
  const [newProducts, setNewProducts] = useState([])
  const [permanentProducts, setPermanentProducts]=useState([])
  const [basket, setBasket]=useState([]);
  useEffect(() => {
    axios.get('http://narotkars-001-site1.htempurl.com/api/Products')
    .then(res => {
        setNewProducts(res.data.filter(product => product.status==="HPNew"))
        setPermanentProducts(res.data.filter(product => product.status==="HPCustomer"))
        console.log(res.data);
    })
    .catch(err => {
        console.log(err)
    })
  },[]);
  useEffect(() => {
    axios.get('http://narotkars-001-site1.htempurl.com/api/Orders/' + props.id)
    .then(res => {
        setBasket(res.data.filter(order => order.status==="notOrdered"));
        //console.log(res.data);
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
      let i=-1,j=-1,k=0,a=0;
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
        date: `${year}${separator}${month<10?`0${month}`:`${month}`}${separator}${date}`,
        status: 'notOrdered',
        amount: a,
        customerId: parseInt(props.id,10),
        productId: id
       }
       setBasket(myOrder);
       //console.log(myOrder);
       fetch('http://narotkars-001-site1.htempurl.com/api/Orders', {
             method: 'POST',
             body: JSON.stringify(myOrder),
             headers: { 'Content-Type' : 'application/json'} })
            .catch(error => console.error('Error:', error))
    }
    else
    {
      const myOrder={
        customerId: parseInt(props.id,10),
        productId: id
       }
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
                          <h4>price: {product.sellingPrice}</h4>
                        </GridItem>
                      ))}
                      {console.log(basket)}
          </GridContainer>
          </div>
          <div id="images">
          <div className={classes.title}>
            <h2>Our Customer's designs</h2>
          </div>
          <GridContainer>
                      {permanentProducts.map(product => (
                        <GridItem xs={12} sm={2}>
                          <img src={`data:image/jpeg;base64,${product.photo}`} 
                              alt="..."
                              className={classes.imgRounded + " " + classes.imgFluid}/>
                        <h4>price: {product.sellingPrice}</h4>
                        </GridItem>
                      ))}
          </GridContainer>
          </div>
      </div>
    </div>
  );
}
