import  {useState, useEffect} from 'react'
import "./Chat.scss"

//import photo from "../../assets/photo.jpg"
//import chatphoto from "../../assets/chatphoto.png"

import ChatLogo from "../../assets/ChatLogo.png"
import LogedInUserList from "../logedinUserList/LogedInUserList"
import ChatBox from './ChatBox'

import { IoIosPeople } from "react-icons/io";
import { MdMessage, MdAddCircleOutline } from "react-icons/md";
import {  BsPersonCircle } from "react-icons/bs";
//import { HiOutlineDotsVertical } from "react-icons/hi";

import * as  signalR from '@microsoft/signalR'; 




const Chat = ({storedEmail}) => {

  const [createRoom, setCreateRoom] = useState([])

  const [loggedInUsers, setLoggedInUsers] = useState([]);
  const [userClicked, setUserClicked] = useState(false);

  const [ dynamicId , setDynamicId ] = useState();



  // Load users from localStorage when the component mounts
  useEffect(() => {
    loadLocalStorageUsers()
  }, []);

  // Load rooms from localStorage when the component mounts
  useEffect(() => {
    loadLocalStorageRoom()
  }, [])

  //users
  const loadLocalStorageUsers = () => {

    const users = [];
    for (let i = 0; i < localStorage.length; i++) {
      const key = localStorage.key(i);

      if (key.startsWith('user:')  && key !== `user:${storedEmail}`) {
        const userData = JSON.parse(localStorage.getItem(key));
        users.push({ ...userData });
      }
    }
    setLoggedInUsers(users);

  }

  // rooms
  const loadLocalStorageRoom = () => {

    const rooms = [];
    for (let i = 0; i < localStorage.length; i++) {
      const key = localStorage.key(i);

      if (key.startsWith('room:')) {
        const roomData = JSON.parse(localStorage.getItem(key));
        rooms.push({ ...roomData });
      }
    }
    setCreateRoom(rooms);
    
  }
  
  // create new group for chatting.  
  const newGroupFrom = ()  => {
    const nameValue = window.prompt("Enter your new group");
    const descriptionValue = window.prompt("Enter description ");


    if (nameValue !== null && descriptionValue !== null) {

      const idValue = generateId(nameValue, descriptionValue);

      // Create an object with name and description properties
      const newRoom = {
        id: idValue,
        name: nameValue,
        description: descriptionValue,
      };

      // Append the newRoom object to the existing createRoom array
      setCreateRoom(prevRooms => [...prevRooms, newRoom]);
      localStorage.setItem(`room:${nameValue}`, JSON.stringify({ id:idValue, name: nameValue, description: descriptionValue}))
    }
  }

  const generateId = (name, description) => {
    
    const combinedString = name + description;
    const id = combinedString.substring(0, 5);
    return id;
  }
  

  // connecting with signalR
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7024/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build()

 /* const startCommunication = async () =>  {
    try {

        await connection.start();
        console.log("SignalR Connected.");

    } catch (err) {
        console.log(err)
    }
  }


  const joinUser = () => {

    const getUserName = localStorage.getItem('LogInUserName');
    joinChat(getUserName)

  } 

  const joinChat = async(user) => {

    if(!user)
      return;

    try { 
        const  msg  = `${user} joined`;
        await  connection.invoke("JoinChat", user, msg)
        console.log("from client side",`${user} joined`)
    }
    catch(err)
    {
      console.log(err)
    }
  }

  // method for getting notified by server
  const recievceMessage = async () => {

    try {
        await connection.on("RecieveMessage", (user, message) => {   // "RecieveMessage" need to be same as server.
            console.log("msg from server",message)
        })  
    }
    catch (err) {
      console.log(err);
    }
  }

  const startApp = async ( ) => {
    await startCommunication();
    await joinUser();
    await recievceMessage();
  }

  startApp()   */
  
  console.log("id",dynamicId);

  return (
    <div className="chat__container">
      <div className="container__body">

        <div className="users_list">
          <div className="users__navbar">

            <div className="users_list_navbar_logo_div">

                {/*<img className = "users_list_navbar_logo" src = {photo} alt = " " />*/}

                <div className='logo__icon'>
                  <BsPersonCircle/>
                </div>
            </div>

           <div className="users_list_navbar_items">

              <div className="users_list_navbar_items_img">
                <div className="each_img"> <IoIosPeople/> </div>
                <div className="each_img"> <MdMessage/> </div> 
                <div className="each_img" onClick = {newGroupFrom}>  <MdAddCircleOutline/> </div> 
              </div>

            </div>

          </div>
          
          <div className="userlist__useritems"> <LogedInUserList loggedInUsers = {loggedInUsers} setDynamicId = {setDynamicId} createRoom= {createRoom} userClicked = {userClicked} setUserClicked={setUserClicked}/> </div>

        </div>


        <div className="container__chatbox">
        {
          userClicked && dynamicId == ? <ChatBox /> : <div className='img'><img src = {ChatLogo} alt = "connect" className='tempImg'/></div>
        }
        </div>  

      </div>
    </div>
  )
}

export default Chat