//import React from 'react'
import { Chat, Navbar, Product } from "../Components"
import { Routes, Route } from 'react-router-dom'

const Home = ({storedEmail}) => {

  return (
    <>
      <Navbar storedEmail = {storedEmail}/>
      <div style ={{  paddingTop:'70px' }}>
        <Routes>
          <Route path="/chat" element={<Chat storedEmail ={storedEmail}/>} />
          <Route path="/product" element={<Product/>} />
        </Routes>
      </div>    
    </>
   
  )
}

export default Home