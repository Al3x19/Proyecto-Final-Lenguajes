import React, { useEffect, useState } from "react";
import { useSoftware } from "../hooks/useSoftware";
import { useSearchStore } from "../store/useSearchStore";
import { SoftwareItem } from "../components/SoftwareItem";
import { Loading, Pagination } from "../../shared/components";
import { Link } from "react-router-dom";
import pngwing from "../../Tienda/assets/pngwing.png"


export const SearchResults = () => {

    const SoftwareData = useSearchStore((state) => state.SoftwareData);
    const loadSoftwareData =  useSearchStore((state) => state.loadSoftwareData);
    const [isLoading, setIsLoading] = useState(true);
    const DevData = useSearchStore((state) => state.DevsData);
    const loadDevData =  useSearchStore((state) => state.loadDevData);

    useEffect(() => {
      if(isLoading) {
       loadSoftwareData();
       loadDevData();
        setIsLoading(false);console.log(DevData.items)
      }
    }, [isLoading])
  
    if (isLoading) return <Loading/> 
  
    return (
        <div className="container mx-auto p-4">
          <div>
            <div className="mb-8">
              <h2 className="text-2xl font-bold mb-4">Software</h2>
              <div className="flex overflow-x-auto space-x-4">
                {SoftwareData?.items?.length > 0 ? (
                  SoftwareData.items.map((software) => (
                    <Link key={software.id} to={`/softwareDescription/${software.id}`}>
                      <div className="flex-none w-64 bg-white rounded-lg shadow-md p-4">
                        <img
                          src={software.icon || pngwing}
                          alt="Card Image 1"
                          className="w-full h-40 object-cover rounded-t-lg"
                        />
                        <h2 className="text-xl font-semibold text-gray-800 mt-4">{software.name}</h2>
                        <p className="text-gray-600">{software.description}</p>
                      </div>
                    </Link>
                  ))
                ) : (
                  <p>No hay software.</p>
                )}
              </div>
            </div>
      
            <div className="mb-8">
              <h2 className="text-2xl font-bold mb-4">Desarrolladores</h2>
              <div className="flex overflow-x-auto space-x-4">
                {DevData?.items?.length > 0 ? (
                  DevData.items.map((dev) => (
                    <Link key={dev.id} to={`/developer/${dev.id}`}>
                      <div className="flex-none w-64 bg-white rounded-lg shadow-md p-4">
                        <img
                          src={dev.icon || pngwing}
                          alt="Card Image 1"
                          className="w-full h-40 object-cover rounded-t-lg"
                        />
                        <h2 className="text-xl font-semibold text-gray-800 mt-4">{dev.name}</h2>
                        <p className="text-gray-600">{dev.description}</p>
                      </div>
                    </Link>
                  ))
                ) : (
                  <p>No hay devs.</p>
                )}
              </div>
            </div>
      
            <div className="mb-8">
              <h2 className="text-2xl font-bold mb-4">Tags</h2>
              <div className="flex overflow-x-auto space-x-4">

                  <p>No hay tags.</p>
                
              </div>
            </div>
          </div>
        </div>
      );
      
};