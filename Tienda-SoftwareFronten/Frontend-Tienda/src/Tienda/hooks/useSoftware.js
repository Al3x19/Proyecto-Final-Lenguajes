import { useState} from "react";
import { getSoftwareList, getSoftware, downloadSoftware, createSoftware } from "../../shared/actions/software/Software";

export const useSoftware = () => {
  const [Software, setSoftware] = useState(null);
  const [isLoading, setIsLoading] = useState(false);

  const loadSoftware = async (searchTerm, page, Filter) => {
    setIsLoading(true);
    const result = await getSoftwareList(searchTerm, page, Filter);
    setSoftware(result);
    setIsLoading(false);
  };

  const loadSingleSoftware = async (id) => {
    setIsLoading(true);
    const result = await getSoftware(id);
    setSoftware(result);
    setIsLoading(false);
  };


  const addSoftware = async (Data, file) => {
    setIsLoading(true);
    const result = await createSoftware(Data, file);
    setSoftware(result);
    setIsLoading(false);
  };


    const downloadingSoftware = async (id, name) => {
      setIsLoading(true);
      try {
        const result = await downloadSoftware(id);
        
        if (result) {
          const url = window.URL.createObjectURL(new Blob([result], { type: 'application/octet-stream' }));
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', name || 'downloaded_file'); // Use the provided name or a default name
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link); // Clean up the link element
        } else {
          console.error('Failed to download the file.');
        }
        
        setIsLoading(false);
      } catch (error) {
        console.error('Download error', error);
        setIsLoading(false);
      }
    };
    




  const loadSoftwarebyTag = async (id, page, Filter) => {
    setIsLoading(true);
    const result = await getSoftwareList(id, page, Filter);
    setSoftware(result);
    setIsLoading(false);
  };


  return {
    Software,
    isLoading,
    loadSoftware,
    addSoftware,
    loadSingleSoftware,
    downloadingSoftware,
    loadSoftwarebyTag
  };
};
