import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useSoftware } from "../hooks/useSoftware";
import { Link } from "react-router-dom";
import pngwing from "../../Tienda/assets/pngwing.png";
import { addPublisher } from "../hooks/usePublisher.js";
import { Loading, Pagination } from "../../shared/components";
import {
  reviewInitValues,
  reviewValidationSchema,
} from "../forms/review.data.js";
import { useFormik } from "formik";
import { useAuthStore } from "../../security/store";


export const DesarrolladorPage = () => {
  const { id } = useParams();
  const { Software, loadSoftware } = useSoftware();
  const { Publisher, loadSinglePublisher, isLoading } = addPublisher();
  const [loaded, setLoaded] = useState(false);
  const [fetching, setFetching] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const user = useAuthStore((state) => state.user);
  const [filter, setFilter] = useState("Popularity");
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    if (fetching) {
      loadSinglePublisher(id);
      loadSoftware(searchTerm, currentPage, filter);
      setFetching(false);
    }
  }, [fetching, searchTerm, currentPage, filter, currentPage, user]);

  const handlePreviousPage = () => {
    if (Software?.data?.hasPreviousPage) {
      setCurrentPage((prevPage) => prevPage - 1);
      setFetching(true);
    }
  };

  const handleNextPage = () => {
    if (Software?.data?.hasNextPage) {
      setCurrentPage((prevPage) => prevPage + 1);
      setFetching(true);
    }
  };

  return (
    <div className="container mx-auto p-4 bg-opacity-90 min-h-screen bg-gray-50">
      <div className="w-full bg-gray-50 rounded-lg shadow-lg p-6 flex flex-col md:flex-row items-center md:items-start mb-10">
        {/* Imagen a la izquierda */}
        <div className="flex-shrink-0">
          <img
            src={Publisher?.data?.icon || pngwing}
            alt="Foto del usuario"
            className="w-72 h-72 rounded-full object-cover shadow-md"
          />
        </div>

        {/* Información a la derecha */}
        <div className="md:text-left border-b-2 border-black w-full md:w-3/4 h-80 ml-0 md:ml-6 text-justify">
          <h1 className="text-2xl font-bold text-slate-800 mt-20">
          {Publisher?.data?.name} 
            
          </h1>
          <p className="text-slate-600 text-lg mt-4">
          
          {Publisher?.data?.description} 
          </p>

        </div>
      </div>

      <div className="container mx-auto p-4">
        <div className="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 w-4/5 mx-auto h-2/3">
          {Software?.data?.items?.length ? (
            Software.data.items.map((software) => (
              <Link
                key={software.id}
                to={`/softwareDescription/${software.id}`}
              >
                <div className="flex-none w-64 h-96 bg-white rounded-lg shadow-md p-4">
                  <img
                    src={software.icon ?? pngwing}
                    alt="Card Image 1"
                    className="w-40 h-40 object-cover rounded-t-lg"
                  />
                  <h2 className="text-xl font-semibold text-gray-800 mt-4">
                    {software.name}
                  </h2>
                  <p className="text-gray-600">{software.description}</p>
                </div>
              </Link>
            ))
          ) : (
            <p>No hay publicaciones disponibles</p>
          )}
        </div>
        <div className="flex justify-between mt-4">
          <button
            onClick={handlePreviousPage}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-700"
          >
            Previous
          </button>
          <button
            onClick={handleNextPage}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-700"
          >
            Next
          </button>
        </div>
      </div>
    </div>
  );
};
