
import './Navbar.scss';
import { NavLink, useNavigate } from 'react-router-dom'

const Navbar = ({storedEmail}) => {

    const navigate = useNavigate();

    // Logout functionality to remove the user's data from localStorage
    const handleLogout = () => {
        localStorage.removeItem(`user:${storedEmail}`);
        navigate('/');
    };


  return (
    <div className='Container__Navbar' >

        <div className='Container__Navbar_container'>

            <div className= 'Container__Navbar_logo'>
                ChatApp  
            </div>

            <div className='Container__Navbar_items'>

                <NavLink 
                    to='/home/chat'
                    style={({ isActive }) => ({ 
                      borderBottom: isActive? '2px solid':'',
                      textDecoration: 'none',
                      color:'black'
                    })}
                >
                    <div>
                        Chat
                    </div>
                </NavLink>

                <NavLink 
                    to='/home/product'
                    style={({ isActive }) => ({ 
                      borderBottom: isActive? '2px solid':'',
                      textDecoration: 'none',
                     color:'black'
                    })}
                >
                    <div>
                        Product
                    </div>
                </NavLink>
                <div>
                    Notification
                </div>
            </div>

        </div>

        <div
            className='Container__Navbar_logout'
            onClick={handleLogout}
            /*style={({ isActive }) => ({ 
                borderBottom: isActive? '2px solid':'',
                textDecoration: 'none',
                color:'black'
            })}*/
        >
            Logout
        </div>

    </div>
  )
}

export default Navbar