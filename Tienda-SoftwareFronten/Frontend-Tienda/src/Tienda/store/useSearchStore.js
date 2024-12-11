import { create } from "zustand";
import { getDevList } from "../../shared/actions/Publisher/Publisher";
import { getSoftwareList } from "../../shared/actions/software/Software";
export const useSearchStore = create((set, get) => ({
    setSearchTerm: (name) => set({  searchedTerm: name }),
   
    searchedTerm: "",

    TagsData: {
        hasNextPage: false,
        hasPreviousPage: false,
        currentPage: 1,
        pageSize: 10,
        totalItems: 0,
        totalPages: 0,
        items: []
    },
    DevsData: {
      hasNextPage: false,
      hasPreviousPage: false,
      currentPage: 1,
      pageSize: 10,
      totalItems: 0,
      totalPages: 0,
      items: []
  },
  SoftwareData: {
    hasNextPage: false,
    hasPreviousPage: false,
    currentPage: 1,
    pageSize: 10,
    totalItems: 0,
    totalPages: 0,
    items: []
},  
  
  loadSoftwareData: async (searchTerm =  get().searchedTerm, page = 1, ) => {
        const result =  await getSoftwareList(searchTerm, page);

        if(result.status) {
            set({SoftwareData: result.data});
            return;
        }

        set({softwareData: null});
        return;
    },


    loadDevData: async (searchTerm = get().searchedTerm, page = 1) => {
      const result =  await getDevList(searchTerm, page);
 
      if(result.status) { 
          set({DevsData: result.data});
          console.log(get().DevsData)
          return;    
      }

      set({DevsData: null});
      return;
  },

    
}));