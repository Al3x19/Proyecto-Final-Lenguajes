import { tiendaApi } from "../../../config/api/tiendaApi";

export const createPublisher = async (PublisherData) => {
  try {
    const { data } = await tiendaApi.post(`/Publishers`, PublisherData);
    return data;
  } catch (error) {
    console.error(error);
    return error.response;
  }
};

export const getPublisher = async (id) => {
  try {
    const { data } = await tiendaApi.get(`/publishers/${id}`); 
    return data;
  } catch (error) {
    console.error("womp womp",error);
    return error.response;
  }
};

export const getDevList = async (searchTerm = "", page = 1) => {
  try {
    const { data } = await tiendaApi.get(`/publishers?searchTerm=${searchTerm}&page=${page}
        `);
    return data;
  } catch (error) {
    console.error(error)
    return error.response;
  }
};