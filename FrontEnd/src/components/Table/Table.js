import React from 'react';
import { withStyles, makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import { TableContainer } from '@material-ui/core';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import { useState, useEffect } from 'react';
import axios from "axios";
import OrderDetailsTable from './OrderDetailsTable.js'
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
  const [seeDetails, setSeeDetails]=useState(0);
  const [isSeeDetails, setIsSeeDetails]=useState(false);
  const [orderDetails, setOrderDetails] = useState([]);
  const seeOrderDetails = (id) =>  {
      axios.get('http://narotkars-001-site1.htempurl.com/api/OrderDetails/' + id)
      .then(res => {
          setOrderDetails(res.data);
      })
      .catch(err => {
          console.log(err)
      })

    setSeeDetails(id);
    setIsSeeDetails(true);
  }
  useEffect(() => {
    k=0;
    axios.get('http://narotkars-001-site1.htempurl.com/api/Orders/' + props.id)
    .then(res => {
        setOrders(res.data); 
    })
    .catch(err => {
        console.log(err)
    })
  });
  
  return (
    <div>
    <TableContainer component={Paper}>
      <Table className={classes.table} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>#</StyledTableCell>
            <StyledTableCell align="center">Address</StyledTableCell>
            <StyledTableCell align="center">Date</StyledTableCell>
            <StyledTableCell align="center">Amount</StyledTableCell>
            <StyledTableCell align="center">Status</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {k=0,orders.map((order) => (
            k++,
            <StyledTableRow key={order.id} onClick={() =>seeOrderDetails(order.id)}>
              <StyledTableCell component="th" scope="row">
                {k}
              </StyledTableCell>
              <StyledTableCell align="center">{order.address.city} {order.address.street} {order.address.number}</StyledTableCell>
              <StyledTableCell align="center">{order.date}</StyledTableCell>
              <StyledTableCell align="center">{order.amount}</StyledTableCell>
              <StyledTableCell align="center">{order.status}</StyledTableCell>
            </StyledTableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
    
    <div className={classesImage.space50} />
    <div className={classes.space50} />
    {isSeeDetails ?
    <div>
    <h4>Details of the selected order</h4>
    <OrderDetailsTable id={seeDetails} /> </div> : ''}
    </div>
  );
}