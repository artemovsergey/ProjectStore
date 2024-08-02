import './Login.css'
import axios from 'axios';
import { useState } from 'react';

export default function Login(){

    const [nameValid, setnameValid] = useState();

    // цвет границы для поля для ввода имени
    var nameColor = nameValid === true ? "green": "";

    async function validName(e){


        const data = {
            name: e.target.value,
            login: "login",
            password: "password",
            roleid: 1
        };

        try {
            const response = await axios.post('https://localhost:7240/api/User', data, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });
            console.log(response.data);
            setnameValid(true);
         
        } catch (error) {
            console.error(error.response.data.errors[0].defaultMessage); // Логирование ошибок
            setnameValid(false);
     
        }
    
        // if(e.target.value == 'user'){
        //     console.log(`${e.target.value}`)
        //     await setnameValid(true)
        // }
        // else{
        //     console.log(`${e.target.value}`)
        //     await setnameValid(false)
        // }

        
    }


    async function handleFormSubmit(e) {

        e.preventDefault();

        // Валидация 
        
        const data = {
            name: e.target.name.value,
            login: "login",
            password: "password",
            roleid: 1,
            role: {name:"admin"}
        };
    
        try {
            const response = await axios.post('https://localhost:7240/api/User', data, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });
            console.log(response.data);
         
        } catch (error) {
            console.error("Ошибка:" + (error.response.data).message); // Логирование ошибок
     
        }
    }


    return (

        <div className="container text-center">
        <h1> Регистрация</h1>
    
        <div className="row border border-0 border-warning">
            <div className="col-6 offset-3 border border-0 border-primary">
                <form className="form-signin" onSubmit = { (e) => {handleFormSubmit(e)} } role="form">
                    
                    <input type="text" name="name" className="form-control m-1" placeholder="Name" required autoFocus
                           onChange={ (e) => validName(e) }
                           style = {{borderColor: nameColor}}
                   
                    />

                    <input type="text" name="login" className="form-control m-1" placeholder="Login" required />
                    <input type="password" name="password" className="form-control m-1" placeholder="Password" required autoComplete="password"/>
                    <input type="text" name="roleid" className="form-control m-1" placeholder="roleid" required />


                    <button disabled = {!nameValid} className="btn btn-lg btn-primary btn-block m-1"
                            >
                        Создать
                    </button>
                </form>
            </div>
        </div>
    
    </div>
    )

}