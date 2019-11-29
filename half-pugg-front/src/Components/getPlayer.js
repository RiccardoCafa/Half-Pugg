import api from '../services/api'

export default async function() {
    const jwt = localStorage.getItem("jwt");
    let stop = false;
    let myData;
    if(jwt){
        await api.get('api/Login', { headers: { "token-jwt": jwt }}).then(res => 
            myData = res.data
        ).catch(error => stop = true)
    } else {
        stop = true;
    }

    if(stop) {
        return null;
    }

    return myData;
}