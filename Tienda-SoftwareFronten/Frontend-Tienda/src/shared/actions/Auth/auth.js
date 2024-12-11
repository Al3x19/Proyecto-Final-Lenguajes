import { tiendaApi } from '../../../config/api';


export const loginAsync = async (form) => {
    try {
        const { data } = await tiendaApi.post('/auth/login', form);
        
        return data;
    } catch (error) {
        console.error({...error});
        return error?.response?.data;        
    }
}


export const registerAsync = async (form) => {
    try {
        const { data } = await tiendaApi.post('/auth/register', form);
        
        return data;
    } catch (error) {
        console.error({...error});
        return error?.response?.data;        
    }
}


export const registerPublisherAsync = async (form) => {
    try {
        const { data } = await tiendaApi.post('/auth/register/Publisher', form);
        
        return data;
        
    } catch (error) {
        console.error({...error});
        return error?.response?.data;        
    }
}

