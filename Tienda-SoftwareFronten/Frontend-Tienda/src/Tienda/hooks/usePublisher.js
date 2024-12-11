import { useState } from "react";
import { createPublisher, getPublisher } from "../../shared/actions/Publisher/Publisher";


export const addPublisher = () => {
  const [Publisher, setPublisher] = useState();
  const [isloading, setIsLoading] = useState(false);

  const addingPublisher = async (PublisherData) => {
    setIsLoading(true);
    const result = await createPublisher(PublisherData);
    setPublisher(result);
    setIsLoading(false);
  };

  const loadSinglePublisher = async (id) => {
    setIsLoading(true);
    const result = await getPublisher(id);
    setPublisher(result);
    setIsLoading(false);
  };


  return {
    Publisher,
    isloading,
    loadSinglePublisher,
    addingPublisher
  };
};
