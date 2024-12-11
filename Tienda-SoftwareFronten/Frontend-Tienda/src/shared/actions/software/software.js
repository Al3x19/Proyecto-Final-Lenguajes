import { tiendaApi } from "../../../config/api/tiendaApi";

export const getSoftwareList = async (searchTerm = "", page = 1, filter ="DateDes") => {
  try {
    const { data } = await tiendaApi.get(`/softwares?searchTerm=${searchTerm}&page=${page}&Filter=${filter}    
        `);
    return data;
  } catch (error) {
    console.error(error)
    return error.response;
  }
};

export const getSoftware = async (id) => {
  try {
    const { data } = await tiendaApi.get(`/softwares/${id}`); 
    return data;
  } catch (error) {
    console.error("womp womp",error);
    return error.response;
  }
};


export const downloadSoftware = async (id) => {
  try {
    const { data } = await tiendaApi.get(`/softwares/download/${id}`,{responseType: 'blob'}); 
    return data;
  } catch (error) {
    console.error("womp womp",error);
    return error.response;
  }
};

export const getSoftwarebyId = async (id, page = 1, filter ="DateDes") => {
  try {
    const { data } = await tiendaApi.get(`/softwares/publisher/${id}?searchTerm=${searchTerm}&page=${page}&Filter=${filter}    
        `);
    return data;
  } catch (error) {
    console.error(error)
    return error.response;
  }
};

  
export const createSoftware = async (Data, file) => {
  try {
    const formData = new FormData();
    Object.keys(Data).forEach((key) => {
      formData.append(key, Data[key]);
    });

    formData.append('file', file);
    const { data } = await tiendaApi.post(`/softwares`, formData);
    return data;
  } catch (error) {
    console.error(error);
    return error.response;
  }
};

export const getSoftwareByUserId = async (id) => {
  try {
    const { data } = await tiendaApi.get(`/softwares/user?searchTerm=${searchTerm}&page=${page}&Filter=${filter}    
        `);
    return data;
  } catch (error) {
    console.error(error)
    return error.response;
  }
};



export const editReview = async (body) => {
  try {
    const { data } = await tiendaApi.put(`/reviews`,body);
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