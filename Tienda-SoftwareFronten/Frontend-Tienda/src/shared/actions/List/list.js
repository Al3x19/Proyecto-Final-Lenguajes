import { tiendaApi } from "../../../config/api";


export const getListsByUser = async (id,  page = 1) => {
    try {
      const { data } = await tiendaApi.get(`/lists/user/${id}?page=${page}`); 
      return data;
    } catch (error) {
      return error.response;
    }
  };

  export const getLists = async (searchTerm = "", page = 1, filter ="DateDes") => {
    try {
      const { data } = await tiendaApi.get(`/lists?searchTerm=${searchTerm}&page=${page}&Filter=${filter}    
          `);
      return data;
    } catch (error) {
      console.error(error)
      return error.response;
    }
  };

  export const getListBId = async (id) => {
    try {
      const { data } = await tiendaApi.get(`/lists/${searchTerm}   
          `);
      return data;
    } catch (error) {
      console.error(error)
      return error.response;
    }
  };

  
  export const createList = async (body) => {
    try {
      const { data } = await tiendaApi.post(`/lists`,body);
      return data;
    } catch (error) {
      console.error(error)
      return error.response;
    }
  };

  export const editList = async (body) => {
    try {
      const { data } = await tiendaApi.put(`/lists`,body);
      return data;
    } catch (error) {
      console.error(error)
      return error.response;
    }
  };

  export const deleteList = async (id) => {
    try {
      const { data } = await tiendaApi.delete(`/lists/${id}`);
      return data;
    } catch (error) {
      console.error(error)
      return error.response;
    }
  };