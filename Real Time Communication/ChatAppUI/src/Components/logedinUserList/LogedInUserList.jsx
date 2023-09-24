//import React from 'react'
import "./logedinuserlist.scss"
import { NavLink } from "react-router-dom";

const logedinUserList = ({loggedInUsers, createRoom, setUserClicked, userClicked, setDynamicId}) => {

  const formatDate = (dateString) => {

    if (!dateString) return ''; 

    const dateObj = new Date(dateString);
    const year = dateObj.getFullYear();
    const month = String(dateObj.getMonth() + 1).padStart(2, '0');
    const day = String(dateObj.getDate()).padStart(2, '0');
    return `${year}/${month}/${day}`;

  };

  return (
    <>
 
      { loggedInUsers.map((item) => (

        <NavLink 
          key={item.email} 
          className="userlist__items"

          style={({ isActive }) => ({ 
            backgroundColor: isActive ? 'rgba(191, 191, 191, 0.3)' : '',
            textDecoration: 'none',
            color: 'black',
          })}

          onClick = {
            (e) => {
                  setUserClicked(!userClicked); 
                  setDynamicId(item.email);
                }}
        >

          <div className="name">
            {item.name}
          </div>

          <div className="joined">{formatDate(item.createdOn)}</div>

        </NavLink> 

      ))}

      { createRoom.map((item) => (

        <NavLink 
          key={item.id} 
          className="userlist__items"

          style={({ isActive }) => ({ 
            backgroundColor: isActive ? 'rgba(191, 191, 191, 0.3)' : '',
            textDecoration: 'none',
            color: 'black',
          })}

          onClick = {
            (e) => { 
              setUserClicked(!userClicked);
              setDynamicId(item.id); 
            }}
        >

          <div className="name">
            {item.name}
          </div>

          <div className="joined">{item.description}</div>

        </NavLink> 

      ))}
    </>
  )
}

export default logedinUserList