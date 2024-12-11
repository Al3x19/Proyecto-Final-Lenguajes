import React, { useEffect, useState } from "react";
import { useSoftware } from "../hooks/useSoftware";

import { SoftwareItem } from "../components/SoftwareItem";
import { Loading, Pagination } from "../../shared/components";

export const SoftwarePage = () => {
  const { Software, loadSoftware, isLoading} = useSoftware();

  const [filter, setFilter] = useState('DateDes');
  const [searchTerm, setSearchTerm] = useState("");
  const [fetching, setFetching] = useState(true);

  
  const [currentPage, setCurrentPage] = useState(1);
  useEffect(() => {
    if (fetching) {
      loadSoftware(searchTerm, currentPage, filter);
      setFetching(false);
    }
  }, [fetching, searchTerm, currentPage, filter, loadSoftware]);

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

  const handleSubmit = (e) => {
    e.preventDefault();
    setFetching(true);
  };

  const handleCurrentPage = (index = 1) => {
    setCurrentPage(index);
    setFetching(true);
  };

  const handleFilterChange = (event) => {
    setFilter(event.target.value);
    setFetching(true);

  };
  return (
    <div className="mt-[120px] sm:mt-10 max-w-[1100px] mx-auto py-4 min-h-screen bg-gray-100">
      <div className="text-center mb-10">
        <h3 className="text-3xl sm:text-4xl leading-normal font-extrabold tracking-tight text-gray-900">
          Todos Nue<span className="text-gray-400">stros Productos</span>
        </h3>
      </div>
  
      <div className="w-full h-2 border-t-2 border-black mb-6"></div> {/* Ajusté el borde superior */}
  
      <div className="relative inline-block m-2 w-full max-w-xs mx-auto sm:max-w-none">
        <select
          value={filter}
          onChange={handleFilterChange}
          className="block w-48 px-4 py-2 border bg-white text-black text-center text-lg rounded-xl "
        >
          <option value="NameAsc">Alfabético ↓</option>
          <option value="NameDesc">Alfabético ↑</option>
          <option value="Popularity">Popularidad</option>
          <option value="DateDes">Más Recientes</option>
          <option value="PriceAsc">Más Costosos</option>
          <option value="PriceDes">Mas economicos</option>
          <option value="Score">Mejor valorados</option>

        </select>
      </div>
  
      {isLoading ? (
        <Loading />
      ) : (
        <div className="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 max-w-full mx-auto h-2/3">
          {Software?.data?.items?.length ? (
            Software.data.items.map((software) => (
              <SoftwareItem key={software.id} software={software} />
            ))
          ) : (
            <p>No hay publicaciones disponibles</p>
          )}
        </div>
      )}
  
      <div className="m-4 border-b-2 border-black h-12">
        <Pagination
          totalPages={Software?.data?.totalPages}
          hasNextPage={Software?.data?.hasNextPage}
          hasPreviousPage={Software?.data?.hasPreviousPage}
          currentPage={currentPage}
          handleNextPage={handleNextPage}
          handlePreviousPage={handlePreviousPage}
          setCurrentPage={setCurrentPage}
          handleCurrentPage={handleCurrentPage}
        />
      </div>
    </div>
  );
};
