import { useParams } from 'react-router-dom';
import { useSearchParams } from 'react-router-dom';

import 'bootstrap/dist/css/bootstrap.min.css';

export default function User() {

    // получаем параметры маршрута
    const userId = useParams().id 
    // получаем параметры строки запроса
    const [searchParams, setSearchParams] = useSearchParams();

    return (
            <div className="card" >
                <h1> User с id = {userId} </h1>
                <h1> Параметр запроса name:  {searchParams.get("name")}</h1>
            </div>
    )
}