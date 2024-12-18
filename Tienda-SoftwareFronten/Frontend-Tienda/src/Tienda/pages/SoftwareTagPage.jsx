import React, { useEffect, useState } from "react";
import { useSoftware } from "../hooks/useSoftware";
import { useParams } from "react-router-dom";
import { SoftwareItem } from "../components/SoftwareItem";
import { Loading, Pagination } from "../../shared/components";

export const SoftwareTagPage = () => {
  const { name } = useParams();
  const { Software, loadSoftwarebyTag, isLoading } = useSoftware();
  const [currentPage, setCurrentPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState("");
  const [fetching, setFetching] = useState(true);

  useEffect(() => {
    if (fetching) {
      loadSoftwarebyTag(name, currentPage);
      setFetching(false);
    }
  }, [fetching, searchTerm, currentPage,loadSoftwarebyTag]);

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

  return (
    <section className="mt-[120px] sm:mt-10 max-w-[1100px] mx-auto py-16">
      <div className="container mx-auto ">
        <div className="text-left mb-10">
          <h3 className="text-3xl sm:text-4xl leading-normal font-extrabold tracking-tight text-gray-900">
           Software bajo el tag de: {name}
          </h3>

        </div>
        {isLoading ? (
          <Loading />
        ) : (
          <div className="grid grid-cols-1 gap-5 md:grid-cols-2 lg:grid-cols-4 max-w-sm mx-auto md:max-w-none md:mx-0">
            {Software?.data?.items?.length ? (
              Software.data.items.map((software) => (
                <SoftwareItem key={software.id} software={software} />
              ))
            ) : (
              <p>No hay publicaciones disponibles</p>
            )}
          </div>
        )}
        <div className="mt-8">
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
    </section>
  );
};
