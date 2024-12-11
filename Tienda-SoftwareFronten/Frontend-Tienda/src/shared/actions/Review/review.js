import { tiendaApi } from "../../../config/api/tiendaApi";


export const getBySoftware = async (id,  page = 1) => {
    try {
      const { data } = await tiendaApi.get(`/reviews/software/${id}?page=${page}`); 
      return data;
    } catch (error) {

      return error.response;
    }
  };

  
export const createReview = async (Data) => {
    try {
      const { data } = await tiendaApi.post(`/reviews`, Data);
      return data;
    } catch (error) {
      console.error(error);
      return error.response;
    }
  };



  export const getReviewByUserId = async (id, softwareId) => {
    try {
      const { data } = await tiendaApi.get(`/reviews/${id}?SoftwareId=${softwareId}   
          `);
      return data;
    } catch (error) {
      console.error(error)
      return error.response;
    }
  };

  

  export const editReview = async (id, body) => {
    try {
      const { data } = await tiendaApi.put(`/reviews/${id}`,body);
      return data;
    } catch (error) {
      console.error(error)
      return error.response;
    }
  };

  export const deleteReview = async (id) => {
    try {
      const { data } = await tiendaApi.delete(`/reviews/${id}`);
      return data;
    } catch (error) {
      console.error(error)
      return error.response;
    }
  };