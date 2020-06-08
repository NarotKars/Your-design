import React, { useState, useEffect } from "react";
import axios from 'axios';
import { makeStyles } from "@material-ui/core/styles";
import { Link } from "react-router-dom";
import './designer.css';
//import "./HomePage.css";
import "tui-image-editor/dist/tui-image-editor.css";
import ImageEditor from "@toast-ui/react-image-editor";
import Favorite from "@material-ui/icons/Favorite";
import Button from "components/CustomButtons/Button.js";
import ShoppingBasketIcon from '@material-ui/icons/ShoppingBasket';
import styles from "assets/jss/material-kit-react/views/componentsSections/basicsStyle.js";
const icona = require("tui-image-editor/dist/svg/icon-a.svg");
const iconb = require("tui-image-editor/dist/svg/icon-b.svg");
const iconc = require("tui-image-editor/dist/svg/icon-c.svg");
const icond = require("tui-image-editor/dist/svg/icon-d.svg");
const download = require("downloadjs");
const myTheme = {
  "menu.backgroundColor": "#ffe6b6",
  "common.backgroundColor": "AliceBlue",
  "downloadButton.backgroundColor": "AliceBlue",
  "downloadButton.borderColor": "AliceBlue",
  "downloadButton.color": "AliceBlue",
  "menu.normalIcon.path": icond,
  "menu.activeIcon.path": iconb,
  "menu.disabledIcon.path": icona,
  "menu.hoverIcon.path": iconc,
  "loadButton.backgroundColor": "AliceBlue",
  "loadButton.color": "AliceBlue",
  "loadButton.borderColor": "AliceBlue",
  //'common.border': '10px solid #c1c1c1',
};
const useStyles = makeStyles(styles);
var userId=1;
function LandingPage(props) {
  const classes = useStyles();
  const [imageSrc, setImageSrc] = useState([]);
  const [isImageLoaded, setImageLoaded] = useState(false);
  const [isNextStep,setIsNextStep]=useState(false);
  const [thePhoto, setThePhoto]=useState();
  const [price, setPrice]=useState(0);
  const [basket, setBasket]=useState([]);
  const [prod, setProd]= useState();
  useEffect(() => {
    axios.get('http://narotkars-001-site1.htempurl.com/api/Products/' + props.productId)
    .then(res => {
      setPrice(res.data.sellingPrice + 1000);
      setProd(res.data);
        setImageSrc(res.data.photo);
        setImageLoaded(true);
    })
    .catch(err => {
        console.log(err)
    })
  },[]);
  useEffect(() => {
    axios.get('http://narotkars-001-site1.htempurl.com/api/Orders/' + localStorage.getItem('id'))
    .then(res => {
        setBasket(res.data.filter(order => order.status==="notOrdered"));
        console.log(res.data);
    })
    .catch(err => {
        console.log(err)
    })
  },[]);
  const imageEditor = React.createRef();
  const saveImageToDisk = () => {
    const imageEditorInst = imageEditor.current.imageEditorInst;
    const data = imageEditorInst.toDataURL();
    if (data) {
      const mimeType = data.split(";")[0];
      const extension = data.split(";")[0].split("/")[1];
      download(data, `image.${extension}`, mimeType);
      console.log(data.split(",")[1]);
    }
    
  };
  const goNext = () => {
    const imageEditorInst = imageEditor.current.imageEditorInst;
    const data = imageEditorInst.toDataURL();
    setThePhoto(data);
    setIsNextStep(true);
    console.log(isNextStep);
  }
  var photo="data:image/jpeg;base64,"+imageSrc;

  function addToBasket()
  {
    const imageEditorInst = imageEditor.current.imageEditorInst;
    const data = imageEditorInst.toDataURL();
    if(basket.length===0)
    {
      let newDate = new Date()
      let date = newDate.getDate();
      let month = newDate.getMonth() + 1;
      let year = newDate.getFullYear();
      let separator='-';
      console.log(parseInt(localStorage.getItem('id'),10));
      console.log(date);
      const myOrder= {
        date: `${year}${separator}${month<10?`0${month}`:`${month}`}${separator}${date<10?`0${date}`:`${date}`}`,
        status: 'notOrdered',
        amount: 0,
        customerId: parseInt(localStorage.getItem('id'),10),
        photo: data.split(",")[1],
        buyingPrice: prod.buyingPrice,
        sellingPrice: price,
        productId: 0,
        categoryId: prod.categoryId,
        companyId: prod.companyId
      }
      console.log(myOrder);
      fetch('http://narotkars-001-site1.htempurl.com/api/Orders', {
             method: 'POST',
             body: JSON.stringify(myOrder),
             headers: { 'Content-Type' : 'application/json'} })
             .catch(error => console.error('Error:', error))
    }
    else
    {
      const myOrder = {
        photo: data.split(",")[1],
        buyingPrice: prod.buyingPrice,
        sellingPrice: price,
        productId: 0,
        categoryId: prod.categoryId,
        companyId: prod.companyId,
        customerId: parseInt(localStorage.getItem('id'),10)
      }
      console.log(myOrder);
      fetch('http://narotkars-001-site1.htempurl.com/api/Orders/SetDesignInOrder', {
             method: 'POST',
             body: JSON.stringify(myOrder),
             headers: { 'Content-Type' : 'application/json'} })
             .catch(error => console.error('Error:', error))
    }
  }

  return (
    isNextStep===false ?
    isImageLoaded ?
    <div>
    <div className="home-page">
      <div className="center">
          <h1 className="headerText">Create your design here</h1>
          <h3 className="headerText"><strong>Step 1:</strong> Add the photos you want by clicking on the camera icon and loading the image</h3>   
      </div>
      <ImageEditor
        includeUI={{
          loadImage: {
            path: photo,
            name: "yourDesign",
          },
          theme: myTheme,
          //menu: ["draw", "shape", "text", "mask"],
          menu: ["mask"],
          initMenu: 'icon',
          uiSize: {
            height: `calc(110vh)`,
          },
          menuBarPosition: "bottom",
        }}
        cssMaxHeight={window.innerHeight}
        cssMaxWidth={window.innerWidth}
        selectionStyle={{
          cornerSize: 20,
          rotatingPointOffset: 70,
        }}
        usageStatistics={true}
        ref={imageEditor}
      />
    </div>
    <div className="buttons">
    <Button color ="primary" round onClick={saveImageToDisk} >
      <Favorite className={classes.icons} /> Save Image to Disk
    </Button>
      <span>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      </span>
    <Button color="primary" round onClick={goNext}>
        <Favorite /> Next Step
    </Button>
    </div>
    </div>:
    <div>
        Loading
    </div>:
    <div>
      <div className="center">
          <h1 className="headerText">Create your design here</h1>
          <h3 className="headerText"><strong>Step 2:</strong> Add texts, shapes or draw :))</h3>   
      </div>
    <ImageEditor
    includeUI={{
      loadImage: {
        path: thePhoto,
        name: "yourDesign",
      },
      theme: myTheme,
      menu: ["draw", "shape", "text"],
      initMenu: 'icon',
      uiSize: {
        height: `calc(110vh)`,
      },
      menuBarPosition: "bottom",
    }}
    cssMaxHeight={window.innerHeight}
    cssMaxWidth={window.innerWidth}
    selectionStyle={{
      cornerSize: 20,
      rotatingPointOffset: 70,
    }}
    usageStatistics={true}
    ref={imageEditor}
  />
  <div className="buttons">
    <Button color ="primary" round onClick={saveImageToDisk} >
      <Favorite className={classes.icons} /> Save Image to Disk
    </Button>
      <span>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      </span>
      <Link to={`/profile-page/${localStorage.getItem('id')}`} >
    <Button color="primary" round onClick={() => addToBasket()}>
        <ShoppingBasketIcon /> Add To Basket
    </Button>
    </Link>
  <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Price: {price}</span>
    </div>
    </div>
  );
}
export default LandingPage;