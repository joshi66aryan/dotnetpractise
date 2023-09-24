import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom';
//import { authContext } from '../../context/authContext';
import { FaFacebookF, FaGoogle } from 'react-icons/fa';
import {
  MDBBtn,
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBInput,
  MDBCheckbox,
  MDBIcon
}
from 'mdb-react-ui-kit';


const Login = () => {
  const [toggleLogin, setToggleLogin] = useState(true);
  const [error, setError] = useState(false)
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const navigate = useNavigate()
 // const {usedispatch} = useContext(authContext)

  /*const handleLogin = (e) => {
    e.preventDefault()
                                                  //paste the format from firebase docs
    signInWithEmailAndPassword(auth, email, password)
    .then((userCredential) => {                 // return back users data
                                              //  after Signed in 
      const user = userCredential.user;
      usedispatch({ type:"LOGIN", payload:user }) // send the payload to global context
      navigate("/")
    })
    .catch((error) => {
      setError(true)
      // ..
    });*/

  return (
    <div>
    <MDBContainer fluid className='p-4'>
      <MDBRow>
        <MDBCol md='6' className='text-center text-md-start d-flex flex-column justify-content-center'>

          <h1 className="my-5 display-3 fw-bold ls-tight px-3">
            Unleash Music Bliss<br />
            <span className="text-primary"> with Our Powerful Headphones</span>
          </h1>

          <p className='px-3' style={{color: 'hsl(217, 10%, 50.8%)'}}>
            Discover a whole new level of musical immersion with our cutting-edge headphones. 
            Designed to deliver unrivaled audio quality, our headphones will transport you to a world of pure sonic bliss.
          </p>

        </MDBCol>

        <MDBCol md='6'>

          <MDBCard className='my-5'>

            <MDBBtn tag='a' color='none' className='mx-3' style={{ color: '#1266f1' }}>
              <MDBIcon fab icon='google' size="sm"/>
            </MDBBtn>

            <MDBCardBody className='p-5'>

              { toggleLogin? 
                (
                  <>
                    <MDBInput wrapperClass='mb-4' label='Email' id='form1' type='email'/>
                    <MDBInput wrapperClass='mb-4' label='Password' id='form1' type='password'/>

                    <div className="d-flex justify-content-between mx-4 mb-4">
                      <MDBCheckbox name='flexCheck' value='' id='flexCheckDefault' label='Remember me' />
                      <a>Forgot password?</a>
                    </div>

                    <MDBBtn className='w-100 mb-4' size='md'>Login</MDBBtn>

                    <p className="text-center">Not a member? <a onClick={() => setToggleLogin(false) } style={{ cursor: 'pointer', color: 'hsl(217, 10%, 50.8%)' }} >Register</a></p>
                  </>

                ): (             
                  <>
                    <MDBInput wrapperClass='mb-4' label='Name' id='form1' type='text'/>
                    <MDBInput wrapperClass='mb-4' label='Username' id='form1' type='text'/>
                    <MDBInput wrapperClass='mb-4' label='Email' id='form1' type='email'/>
                    <MDBInput wrapperClass='mb-4' label='Password' id='form1' type='password'/>

                    <div className='d-flex justify-content-center mb-4'>
                      <MDBCheckbox name='flexCheck' id='flexCheckDefault' label='I have read and agree to the terms' />
                    </div>

                    <MDBBtn className="mb-4 w-100">Sign up</MDBBtn>
                    <p className="text-center"><a onClick={() => setToggleLogin(true)} style={{ cursor: 'pointer', color: 'hsl(217, 10%, 50.8%)' }} >Back to Login </a></p>
                  </>
                )
              }


              <div className="text-center">

                <p>or</p>

                <MDBBtn tag='a' color='none' className='mx-3' style={{ color: '#1266f1' }}>
                  <FaFacebookF size={20} />
                </MDBBtn>

                <MDBBtn tag='a' color='none' className='mx-3' style={{ color: '#1266f1' }}>
                  <FaGoogle size={20} />
                </MDBBtn>
              </div>

            </MDBCardBody>
          </MDBCard>

        </MDBCol>
      </MDBRow>
    </MDBContainer>
    </div>
  )
}

export default Login










    
    