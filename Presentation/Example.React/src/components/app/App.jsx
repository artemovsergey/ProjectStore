import { BrowserRouter as Router, Route, Routes  } from 'react-router-dom';
import {Navigate} from "react-router-dom"
import ReactDOM from 'react-dom/client'

import Users from '../users/Users.jsx'
import User from '../user/User.jsx'
import Login from '../login/Login.jsx'
import '../../index.css'
import './App.css'

ReactDOM.createRoot(document.getElementById('root')).render(
    <Router>
       
        <div className="container mt-5">
        
            <Routes>
               
                <Route path="/" element={<User />} >
                    <Route index element={<h3>Каталог продуктов новых</h3>} />
                    <Route path="phones" element={<User />} />
                    <Route path="tablets" element={<User />} />
                </Route>
                <Route path="/Login" element={<Login />} />
                <Route path="/Users" element={<Users />} />
                <Route path="/user/:id" element={<User />} />
                <Route path="/old" element={<Navigate to="/users" />} />
               
            </Routes>
    
        </div>  
    </Router>
)
