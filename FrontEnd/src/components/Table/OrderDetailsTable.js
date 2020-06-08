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
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";
import styles from "assets/jss/material-kit-react/views/componentsSections/typographyStyle.js";
import axios from "axios";
const StyledTableCell = withStyles((theme) => ({
  head: {
    backgroundColor: '#555555',
    color: theme.palette.common.white,
  },
  body: {
    fontSize: 14,
  },
}))(TableCell);
const useImageStyles = makeStyles(styles);
const StyledTableRow = withStyles((theme) => ({
  root: {
    '&:nth-of-type(odd)': {
      backgroundColor: theme.palette.action.hover,
    },
  },
}))(TableRow);

const useStyles = makeStyles({
  table: {
    minWidth: 700,
  },
});

export default function CustomizedTables(props) {
    const classesImage = useImageStyles();
  const classes = useStyles();
  const [orderDetails, setOrderDetails] = useState([]);
  useEffect(() => {
    axios.get('http://narotkars-001-site1.htempurl.com/api/OrderDetails/' + props.id)
    .then(res => {
        setOrderDetails(res.data);
    })
    .catch(err => {
        console.log(err)
    })
  },[props.id]);
  function seeDetails(){
    alert('yes');
  }
  return (
    <TableContainer component={Paper}>
      <Table className={classes.table} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>#</StyledTableCell>
            <StyledTableCell align="center">Image</StyledTableCell>
            <StyledTableCell align="center" rowSpan="4">Company Name</StyledTableCell>
            <StyledTableCell align="center">Price</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {orderDetails.map((details) => (
            <StyledTableRow key={details.id} onClick={seeDetails}>
              <StyledTableCell component="th" scope="row">
                #
              </StyledTableCell>
          <StyledTableCell align="center">{<GridContainer justify="center">
                                            <GridItem xs={12} sm={2}>
                                                <img src={`data:image/jpeg;base64,${details.photo}`}
                                                alt="..."
                                                className={classesImage.imgRounded + " " + classesImage.imgFluid}/>
                                            </GridItem></GridContainer>} 
                              </StyledTableCell>
              <StyledTableCell align="center">{details.companyName}</StyledTableCell>
              <StyledTableCell align="center">{details.sellingPrice}</StyledTableCell>
            </StyledTableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}