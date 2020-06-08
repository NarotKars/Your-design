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
import CheckIcon from '@material-ui/icons/Check';
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
  const [orders, setOrders] = useState([]);
  useEffect(() => {
    k=0;
    
    axios.get('http://narotkars-001-site1.htempurl.com/api/Orders/Company/' + localStorage.getItem('id'))
    .then(res => {
        setOrders(res.data.filter(order => order.status!='done'));
    })
    .catch(err => {
        console.log(err)
    })
  },[orders]);
  
  const updateStatus = (id) => {
    const myOrderDetail = {
      id: id,
      status : 'accepted'
    }
    fetch('http://narotkars-001-site1.htempurl.com/api/OrderDetails/ProductsInOrders/' + id, {
        method: 'PUT',
        body: JSON.stringify(myOrderDetail),
        headers: {
            "Content-type" : "application/json"
        }
    })
  }

  const updateStatusDone = (id) => {
    const myOrderDetail = {
      id: id,
      status : 'done'
    }
    fetch('http://narotkars-001-site1.htempurl.com/api/OrderDetails/ProductsInOrders/' + id, {
        method: 'PUT',
        body: JSON.stringify(myOrderDetail),
        headers: {
            "Content-type" : "application/json"
        }
    })
  }
  const [address, setAddressState]=useState("");
  const [isEnteredAddress, setIsEnteredAddress]=useState(true);
  const setAddress = e => {
    setAddressState(e.target.value);
    if(isEnteredAddress===false && e.target.value!=="" && e.target.value.trim()!=="") setIsEnteredAddress(true);
  }
  return (
    <div>
    <TableContainer component={Paper}>
      <Table className={classes.table} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>#</StyledTableCell>
            <StyledTableCell align="center">Photo</StyledTableCell>
            <StyledTableCell align="center">Customer's Name</StyledTableCell>
            <StyledTableCell align="center">Customer's Phone Number</StyledTableCell>
            <StyledTableCell align="center">Accept</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {k=0,
            orders.map((order) => (k++,
              <StyledTableRow key={order.id}>
                <StyledTableCell component="th" scope="row">
                  {k}
                </StyledTableCell>
            <StyledTableCell align="center">{<GridContainer justify="center">
                                              <GridItem xs={4} sm={4} md={4}>
                                                  <img src={`data:image/jpeg;base64,${order.photo}`}
                                                  alt="..."
                                                  className={classesImage.imgRounded + " " + classesImage.imgFluid}/>
                                              </GridItem></GridContainer>} 
                                </StyledTableCell>
                <StyledTableCell align="center">{order.name}</StyledTableCell>
                <StyledTableCell align="center">{order.phoneNumber}</StyledTableCell>
            <StyledTableCell align="center">
              {order.status==='new' ?
              <Button  round color="primary" onClick={() => updateStatus(order.id)}>
                                                <CheckIcon className={classes.icons} /> Accept
                                             </Button> : 
              order.status==='accepted' ? <Button  round color="primary" onClick={() => updateStatusDone(order.id)}>
                                                <CheckIcon className={classes.icons} /> Done
                                             </Button> : ''}</StyledTableCell>
              </StyledTableRow>
            ))}
        </TableBody>
      </Table>
    </TableContainer>
    </div>
  );
}