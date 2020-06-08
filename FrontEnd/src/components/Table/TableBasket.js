import React from 'react';
import { withStyles, makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import { TableContainer } from '@material-ui/core';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Button from "components/CustomButtons/Button.js";
import { useState, useEffect } from 'react';
import axios from "axios";
import OrderDetailsTable from './OrderDetailsTable.js'
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";
import ClearIcon from '@material-ui/icons/Clear';
import BubbleChartIcon from '@material-ui/icons/BubbleChart';
import CustomInput from "components/CustomInput/CustomInput.js";
import styles from "assets/jss/material-kit-react/views/componentsSections/typographyStyle.js";
const StyledTableCell = withStyles((theme) => ({
  head: {
    backgroundColor: '#555555',
    color: theme.palette.common.white,
  },
  body: {
    fontSize: 14,
  },
}))(TableCell);

const StyledTableRow = withStyles((theme) => ({
  root: {
    '&:nth-of-type(odd)': {
      backgroundColor: theme.palette.action.hover,
    },
  },
}))(TableRow);
const useImageStyles = makeStyles(styles);

const useStyles = makeStyles({
  table: {
    minWidth: 700,
  },
});
var k=0;
export default function CustomizedTables(props) {
  const classes = useStyles();
  const classesImage = useImageStyles();
  const [basket, setBasket] = useState([]);
  const [seeDetails, setSeeDetails]=useState(0);
  const [isSeeDetails, setIsSeeDetails]=useState([]);
  const [orderDetails, setOrderDetails] = useState([]);
  const [isOrdered, setIsOrdered] = useState(false);
  useEffect(() => {
    k=0;
    let id;
    
    axios.get('http://narotkars-001-site1.htempurl.com/api/Orders/' + props.id)
    .then(res => {
        setBasket(res.data.filter(order => order.status==="notOrdered"));
        var order=res.data.filter(order => order.status==="notOrdered");
        if(order.length!=0)
        {
        id=order[0].id;
        axios.get('http://narotkars-001-site1.htempurl.com/api/OrderDetails/' + id)
      .then(res => {
          setOrderDetails(res.data);
      })
      .catch(err => {
          console.log(err)
      })
    }
    })
    .catch(err => {
        console.log(err)
    })
    console.log(id);
    

  },[orderDetails]);
  
  const deleteProduct = (id) => {
    var url='http://narotkars-001-site1.htempurl.com/api/OrderDetails/ProductsInOrders/'+id;
      fetch(url ,{
        method: 'DELETE'
      }).catch((err)=>{console.log(err);})
      var deleted=orderDetails;
      const index=orderDetails.map(item => item.productInOrderId).indexOf(id);
      deleted.splice(index,1);
      console.log(deleted);
      setOrderDetails(deleted);
      console.log(id);
  }
  const [address, setAddressState]=useState("");
  const [isEnteredAddress, setIsEnteredAddress]=useState(true);
  const setAddress = e => {
    setAddressState(e.target.value);
    if(isEnteredAddress===false && e.target.value!=="" && e.target.value.trim()!=="") setIsEnteredAddress(true);
  }

  const [phoneNumber, setPhoneNumberState]=useState("");
  const [isEnteredPhoneNumber, setIsEnteredPhoneNumber]=useState(true);
  const setPhoneNumber = e => {
    setPhoneNumberState(e.target.value);
    if(isEnteredPhoneNumber===false && e.target.value!=="" && e.target.value.trim()!=="") setIsEnteredPhoneNumber(true);
  }

  const order = () => {
    if(address.trim()==="" || address.trim().split(" ").length<2)
    {
        if(address==="" || address.trim()==="") setIsEnteredAddress(false);
        else setIsEnteredAddress(true);
    }
    else
    {
        var newAddress=address.trim().split(" ");
        var city, street="", number;
        if(newAddress.length==2)
        {
            city=props.city;
            street=newAddress[0];
            number=newAddress[1];
        }
        else if(newAddress.length===3)
        {
            city=newAddress[0];
            street=newAddress[1];
            number=newAddress[2];
        }
        else
        {
            city=newAddress[0];
            for(var i=1;i<newAddress.length-1;i++)
            {
                street+=newAddress[i];
                if(i!==newAddress.length-2) street+=" ";
            }
            number = newAddress[newAddress.length-1];
        }
      const myOrder={
        Address: {"city":city, "street": street, "number": number},
        phoneNumber: phoneNumber,
        id: basket[0].id,
        customerId: parseInt(props.id,10),
        status: "new"
      }
      console.log(myOrder);
      fetch('http://narotkars-001-site1.htempurl.com/api/Orders', {
        method: 'PUT',
        body: JSON.stringify(myOrder),
        headers: {
            "Content-type" : "application/json"
        }
    })
      setIsOrdered(true);
    }
  }
  return (
      isOrdered===false ?
      basket.length!==0 ?
    <div>
    <TableContainer component={Paper}>
      <Table className={classes.table} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>#</StyledTableCell>
            <StyledTableCell align="center">Photo</StyledTableCell>
            <StyledTableCell align="center">Company Name</StyledTableCell>
            <StyledTableCell align="center">Amount</StyledTableCell>
            <StyledTableCell align="center">Delete</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {k=0,
            orderDetails.map((details) => (k++,
              <StyledTableRow key={details.productInOrderId}>
                <StyledTableCell component="th" scope="row">
                  {k}
                </StyledTableCell>
            <StyledTableCell align="center">{<GridContainer justify="center">
                                              <GridItem xs={4} sm={4} md={4}>
                                                  <img src={`data:image/jpeg;base64,${details.photo}`}
                                                  alt="..."
                                                  className={classesImage.imgRounded + " " + classesImage.imgFluid}/>
                                              </GridItem></GridContainer>} 
                                </StyledTableCell>
                <StyledTableCell align="center">{details.companyName}</StyledTableCell>
                <StyledTableCell align="center">{details.sellingPrice}</StyledTableCell>
            <StyledTableCell align="center">{<Button justIcon round color="primary" onClick={() => deleteProduct(details.productInOrderId)}>
                                                <ClearIcon className={classes.icons} />
                                             </Button>}</StyledTableCell>
              </StyledTableRow>
            ))}
        </TableBody>
      </Table>
    </TableContainer>
    <GridContainer>
    <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
    <GridItem xs={12} sm={12} md={12} lg={12}>
             { isEnteredAddress===true ?
              <CustomInput
                inputProps={{
                  onChange: (e) => setAddress(e)
                }}         
                labelText="YOUR ADDRESS*"
                id="float"
                formControlProps={{
                  fullWidth: true
                }}
              /> :
              <CustomInput
                inputProps={{
                  onChange: (e) => setAddress(e)
                }}      
                error   
                labelText="PLEASE ENTER YOUR ADDRESS"
                id="float"
                formControlProps={{
                  fullWidth: true
                }}
              />
              }
            </GridItem>

            <GridItem xs={12} sm={12} md={12} lg={12}>
             { isEnteredPhoneNumber===true ?
              <CustomInput
                inputProps={{
                  onChange: (e) => setPhoneNumber(e)
                }}         
                labelText="YOUR PHONE NUMBER*"
                id="float"
                formControlProps={{
                  fullWidth: true
                }}
              /> :
              <CustomInput
                inputProps={{
                  onChange: (e) => setPhoneNumber(e)
                }}      
                error   
                labelText="PLEASE ENTER YOUR PHONE NUMBER"
                id="float"
                formControlProps={{
                  fullWidth: true
                }}
              />
              }
            </GridItem>
    </GridContainer>
    <span>Amount: {basket[0].amount}
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
    <Button round color="primary" onClick={() => order()}>
        <BubbleChartIcon className={classes.icons} /> Order
    </Button>
    </div>:
    <h2>Your basket is empty</h2>:
    <h2>Thank you!! Your order has been recorded. We will call you soon.</h2>
  );
}