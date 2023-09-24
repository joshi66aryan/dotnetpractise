//import React, {useState} from 'react'
import "./Chat.scss"
import {  MdOutlineAttachment,  } from "react-icons/md";
import { BsFillEmojiLaughingFill, BsSearch, BsFillMicFill, BsPersonCircle } from "react-icons/bs";
import { HiOutlineDotsVertical } from "react-icons/hi";

//import * as  signalR from '@microsoft/signalR'; 

const ChatBox = () => {

  return (
        <>
          <div className="container__chatnavbar">


             <div className="user">

                <div className="navbar_logo_div">

                  {/*<img className = "users_list_navbar_logo" src = {photo} alt = " " />*/}

                  <div className='logo__icon'>
                    <BsPersonCircle/>
                  </div>
                  
                </div>

                <div className="user__status">
                  <p className="user_name"> Name </p>
                  <p className="login_date"> Date </p>
                </div>

              </div>

              <div className="nav_buttons">
                <div className="nav_buttons1"> <BsSearch/> </div>
                <div className="nav_buttons2"> <HiOutlineDotsVertical/> </div>
              </div>


          </div>

          <div className="messageArea">

            <div className="msg__recieve">

              <div className="msg__recieve_box">
                <p className="paragraph" >
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor 
                  incididunt ut labore et dolore magna aliqua. Dolor sit amet consectetur adipiscing elit. Scelerisque 
                  varius morbi enim nunc faucibus a pellentesque. Pellentesque pulvinar pellentesque habitant morbi 
                  tristique senectus et netus. Ultricies lacus sed turpis tincidunt id aliquet risus. Risus nec feugiat 
                  in fermentum posuere urna nec. Arcu cursus vitae congue mauris rhoncus aenean vel. Massa id neque aliquam 
                  vestibulum morbi. Fringilla urna porttitor rhoncus dolor purus. Bibendum arcu vitae elementum curabitur. 
                  Turpis egestas integer eget aliquet nibh praesent. In fermentum et sollicitudin ac orci phasellus egestas
                  tellus. Faucibus scelerisque eleifend donec pretium vulputate. Risus commodo viverra maecenas accumsan lacus.
                  Urna nec tincidunt praesent semper feugiat nibh sed pulvinar proin.
                </p>
              </div>


              <div className="msg__recieve_box">
                <p className="paragraph" >
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor 
                  incididunt ut labore et dolore magna aliqua. Dolor sit amet consectetur adipiscing elit. Scelerisque 
                  varius morbi enim nunc faucibus a pellentesque. Pellentesque pulvinar pellentesque habitant morbi 
                  tristique senectus et netus. Ultricies lacus sed turpis tincidunt id aliquet risus. Risus nec feugiat 
                  in fermentum posuere urna nec. Arcu cursus vitae congue mauris rhoncus aenean vel. Massa id neque aliquam 
                  vestibulum morbi. Fringilla urna porttitor rhoncus dolor purus. Bibendum arcu vitae elementum curabitur. 
                  Turpis egestas integer eget aliquet nibh praesent. In fermentum et sollicitudin ac orci phasellus egestas
                  tellus. Faucibus scelerisque eleifend donec pretium vulputate. Risus commodo viverra maecenas accumsan lacus.
                  Urna nec tincidunt praesent semper feugiat nibh sed pulvinar proin. 
                </p>
              </div>

            </div>  

            <div className="msg__send">

              <div className="msg__send_box">
                <p className="paragraph">

                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor 
                  incididunt ut labore et dolore magna aliqua. Dolor sit amet consectetur adipiscing elit. Scelerisque 
                  varius morbi enim nunc faucibus a pellentesque. Pellentesque pulvinar pellentesque habitant morbi 
                  tristique senectus et netus. Ultricies lacus sed turpis tincidunt id aliquet risus. Risus nec feugiat 
                  in fermentum posuere urna nec. Arcu cursus vitae congue mauris rhoncus aenean vel. Massa id neque aliquam 
                  vestibulum morbi. Fringilla urna porttitor rhoncus dolor purus. Bibendum arcu vitae elementum curabitur. 
                  Turpis egestas integer eget aliquet nibh praesent. In fermentum et sollicitudin ac orci phasellus egestas
                  tellus. Faucibus scelerisque eleifend donec pretium vulputate. Risus commodo viverra maecenas accumsan lacus.
                  Urna nec tincidunt praesent semper feugiat nibh sed pulvinar proin.

                  Proin fermentum leo vel orci porta non pulvinar neque laoreet. Vitae turpis massa sed elementum tempus. 
                  Aliquam nulla facilisi cras fermentum odio eu. Non curabitur gravida arcu ac tortor dignissim convallis
                  aenean et. Aliquet risus feugiat in ante metus dictum at tempor commodo. Vitae tortor condimentum lacinia 
                  quis vel. Commodo sed egestas egestas fringilla. Magna eget est lorem ipsum dolor sit. Viverra nam libero
                  justo laoreet. Vel elit scelerisque mauris pellentesque pulvinar. Velit scelerisque in dictum non. 
                  Facilisi nullam vehicula ipsum a arcu cursus vitae congue. Eget gravida cum sociis natoque penatibus et.
                  Sed viverra ipsum nunc aliquet bibendum. Nam at lectus urna duis convallis.
                </p>
              </div>
              <div className="msg__send_box">
                <p className="paragraph">

                  Lorem 
                 
                </p>
              </div>

              
            </div>  

            <div className="msg__recieve">

              <div className="msg__recieve_box">
                <p className="paragraph" >
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor 
                  incididunt ut labore et dolore magna aliqua. Dolor sit amet consectetur adipiscing elit. Scelerisque 
                  varius morbi enim nunc faucibus a pellentesque. Pellentesque pulvinar pellentesque habitant morbi 
                  tristique senectus et netus. Ultricies lacus sed turpis tincidunt id aliquet risus. Risus nec feugiat 
                  in fermentum posuere urna nec. Arcu cursus vitae congue mauris rhoncus aenean vel. Massa id neque aliquam 
                  vestibulum morbi. Fringilla urna porttitor rhoncus dolor purus. Bibendum arcu vitae elementum curabitur. 
                  Turpis egestas integer eget aliquet nibh praesent. In fermentum et sollicitudin ac orci phasellus egestas
                  tellus. Faucibus scelerisque eleifend donec pretium vulputate. Risus commodo viverra maecenas accumsan lacus.
                  Urna nec tincidunt praesent semper feugiat nibh sed pulvinar proin.
                </p>
              </div>


              <div className="msg__recieve_box">
                <p className="paragraph" >
                  Lorem Urna nec tincidunt praesent semper feugiat nibh sed pulvinar proin.
                </p>
              </div>

            </div>  

   
          </div>


          <div className="footer">
            <div className="footer_items">

              <div className="footer_buttons">
                <div className="emoji"> <BsFillEmojiLaughingFill/> </div>
                <div className="attach"> <MdOutlineAttachment/> </div>
              </div>

              <div className="footer_input">
                <input  className="input" placeholder="Type a message..."/>
              </div>

              <div className="footer_mic"> <BsFillMicFill/> </div>

            </div>
          </div>
        </>  
  )
}

export default ChatBox