import { useState } from 'react'
import './App.css'
import { Home } from "./Container"
import { Registration, Login, NotFound } from './Components'
import { Routes, Route } from 'react-router-dom'

function App() {

  const [storedEmail, setStoredEmail] = useState("")

  return (
    <Routes>
      <Route path="/login"  element={<Login setStoredEmail = {setStoredEmail}/>} />
      <Route path="/home/*"  element={<Home storedEmail = {storedEmail}/>} />
      <Route path="/" element={<Registration/>} />
      <Route path="*" element={<NotFound/>}/>
    </Routes>

  )
}

export default App
