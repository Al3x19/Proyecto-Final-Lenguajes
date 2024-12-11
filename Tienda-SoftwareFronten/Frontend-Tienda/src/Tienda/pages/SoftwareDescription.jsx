import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useSoftware } from "../hooks/useSoftware";
import { Link } from "react-router-dom";
import pngwing from "../../Tienda/assets/pngwing.png"
import { useReview } from "../hooks/useReviews.js";
import { Loading, Pagination } from "../../shared/components";
import { reviewInitValues, reviewValidationSchema } from "../forms/review.data.js";
import { useFormik } from "formik";
import { useAuthStore } from "../../security/store";
import { ProtectedComponent } from "../../shared/components/ProtectedComponent.jsx";
import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";

  

export const SoftwareDescription = () => {
  const { id } = useParams();
  const { addReview, loadReviews, Review, Reviews, isLoading, loadSingleReview, putReview } = useReview();
  const { Software, loadSingleSoftware, downloadingSoftware} = useSoftware();
  const [loaded, setLoaded] = useState(false); 
  const [fetching, setFetching] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [softwareid, setsoftwareid] = useState(id);
  const user = useAuthStore((state) => state.user);
  

  const formik = useFormik({

    initialValues: reviewInitValues,
    validationSchema: reviewValidationSchema,
    validateOnChange: true,

    onSubmit: async (formValues) => {
 
      formValues.SoftwareId = id;

      if (!Review || Review.data.score==null){

      await addReview(formValues);

      }else{

      await putReview(Review.data.id, formValues);

      }

      setFetching(true);
    },
  });


  useEffect(() => {

    if (fetching) {
      loadReviews(id, currentPage);
      loadSingleSoftware(id);
      loadSingleReview(user?.id , id);
      setFetching(false);
    }
  }, [fetching, currentPage, loadReviews, user]);

  const handlePreviousPage = () => {
    if (Reviews?.data?.hasPreviousPage) {
      setCurrentPage((prevPage) => prevPage - 1);
      setFetching(true);
    }
  };

  const handleNextPage = () => {
    if (Reviews?.data?.hasNextPage) {
      setCurrentPage((prevPage) => prevPage + 1);
      setFetching(true);
    }
  };

  const handleDownload = () => {
    downloadingSoftware(Software?.data?.id, Software?.data?.fileName);

  }

  const handleCurrentPage = (index = 1) => {
    setCurrentPage(index);
    setFetching(true);
  };

  useEffect(() => {
    if (!loaded) {
      loadSingleSoftware(softwareid);
      loadSingleReview(user?.id || null, softwareid)
       setLoaded(true); 
    }
  }, [softwareid, loadSingleSoftware, loadSingleReview]);

 console.log(user)
console.log(Software)
const PaypalButtonComponent = () =>{
  const initialOptions = {
      "client-id": "AU1Kh6GZk9f4khF_b-cBxDHg1RDPHnjP4gkzW6jsntIxXUXlpciyKDfNC34361NtPcrLeDPAoS_cF6LP",
      currency: "USD",
      intent : "capture"
  }

const createOrder = (data, actions) => {
  return actions.order.create({
      purchase_units: [
          {
              amount:{
                  currency_code: "USD",
                  value : Software.data.price
              }
          }
      ]

  })
}

const onAprove = (data, actions) => {
  return actions.order.capture().then(function (details){
    downloadingSoftware(Software?.data?.id, Software?.data?.fileName);

  })
}
return (
  <PayPalScriptProvider options={initialOptions}>
    <PayPalButtons createOrder={(data,actions)=>createOrder(data, actions,Software.data.price)}
      onApprove={(data,actions) => onAprove(data,actions,Software.data.id,Software.data.fileName)}
      >
pagar
    </PayPalButtons>
  </PayPalScriptProvider>
)
}


  if (isLoading && !loaded) return <p>cargando...</p>;

  if (!Software) return <p>Software no encontrado</p>;

  return (
    <main className="flex flex-col items-center justify-center p-8 space-y-6 md:p-16 bg-gray-100 w-full">
      {/* Sección: Imagen, descripción y botón de descarga */}
      <div className="w-full max-w-4xl bg-white rounded-lg shadow-lg p-6 flex flex-col md:flex-row space-y-6 md:space-y-0 md:space-x-6">
        {/* Imagen del software */}
        <div className="flex flex-col items-center md:items-start">
          <img
            src={Software.data.icon || pngwing}
            alt={Software.name || "Product Image"}
            className="w-64 h-64 rounded-lg shadow-md object-cover mb-4"
          />
          {/* Botón de descarga */}

          {Software?.data?.price === 0 ? (
  <button onClick={handleDownload}>Download</button>
) : (
  <PaypalButtonComponent />
)}
        </div>
        {/* Descripción del software */}
        <div className="flex-1 text-center md:text-left space-y-4">
          <h1 className="text-2xl font-bold text-gray-800">{Software.data.name}</h1>
          <Link
            to={`/developer/${Software?.data?.publisher?.id}`}
            className="text-lg font-medium text-blue-600 hover:underline"
          >
            {Software?.data?.publisher?.name}
          </Link>
          <p className="text-xl text-red-600 font-semibold">{Software?.data?.price}</p>
          <p className="text-lg text-gray-700">Score: {Software?.data?.score}</p>
          <p className="text-gray-600">{Software?.data?.description}</p>
          {/* Tags */}
          <div className="flex flex-wrap justify-center md:justify-start gap-2 mt-4">
            {Software.data.tags.map((tag, index) => (
              <Link
                key={index}
                to={`/softwareByTag/${tag}`}
                className="bg-green-100 text-green-800 px-2 py-1 rounded-md text-sm hover:bg-green-200"
              >
                {tag}
              </Link>
            ))}
          </div>
        </div>
      </div>
  
      {/* Crear o editar reseña */}
      <div className="w-full max-w-4xl bg-white rounded-lg shadow-lg p-6">
        <ProtectedComponent requiredRoles={["USER", "ADMIN", "PUBLISHER"]}>
          {!Review?.data?.score ? (
            <>
              <h2 className="text-lg font-semibold text-gray-800">Crear Nueva Reseña</h2>
              <form onSubmit={formik.handleSubmit} className="space-y-4">
                <div>
                  <label
                    htmlFor="content"
                    className="block text-sm font-medium text-gray-700"
                  >
                    Contenido
                  </label>
                  <input
                    type="text"
                    id="content"
                    value={formik.values.content}
                    onChange={formik.handleChange}
                    placeholder="Ingrese contenido"
                    className="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500"
                    required
                  />
                </div>
                <div>
                  <label
                    htmlFor="score"
                    className="block text-sm font-medium text-gray-700"
                  >
                    Score (0 - 100)
                  </label>
                  <input
                    type="number"
                    id="score"
                    value={formik.values.score}
                    min="0"
                    max="100"
                    onChange={formik.handleChange}
                    className="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500"
                    required
                  />
                </div>
                <div className="relative w-full h-4 bg-gray-300 rounded-full overflow-hidden">
                  <div
                    className="absolute top-0 left-0 h-full bg-blue-500"
                    style={{ width: `${formik.values.score}%` }}
                  ></div>
                </div>
                <button
                  type="submit"
                  className="w-full py-2 px-4 bg-blue-600 text-white rounded-lg font-semibold hover:bg-blue-500"
                >
                  Enviar
                </button>
              </form>
            </>
          ) : (
            <>
              <h2 className="text-lg font-semibold text-gray-800">Editar Reseña</h2>
              {/* Similar formulario para editar */}
            </>
          )}
        </ProtectedComponent>
      </div>
  
      {/* Tabla de entradas */}
      <div className="w-full max-w-4xl bg-white rounded-lg shadow-lg p-6">
        <h2 className="text-lg font-semibold mb-4">Tabla de Entradas</h2>
        <table className="w-full text-left border-collapse">
          <thead>
            <tr className="bg-gray-100">
              <th className="px-4 py-2 border">Usuario</th>
              <th className="px-4 py-2 border">Contenido</th>
              <th className="px-4 py-2 border">Score</th>
            </tr>
          </thead>
          <tbody>
            {isLoading ? (
              <tr>
                <td colSpan="3" className="text-center py-4">
                  <Loading />
                </td>
              </tr>
            ) : Reviews?.data?.items?.length ? (
              Reviews.data.items.map((review) => (
                <tr key={review.id} className="border-b">
                  <td className="px-4 py-2">
                    {review.createdByUser?.userName || "Usuario"}
                  </td>
                  <td className="px-4 py-2">{review.content}</td>
                  <td className="px-4 py-2">{review.score}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="3" className="text-center py-4">
                  No hay entradas aún
                </td>
              </tr>
            )}
          </tbody>
        </table>
        <div className="mt-4">
          <Pagination
            totalPages={Reviews?.data?.totalPages}
            hasNextPage={Reviews?.data?.hasNextPage}
            hasPreviousPage={Reviews?.data?.hasPreviousPage}
            currentPage={currentPage}
            handleNextPage={handleNextPage}
            handlePreviousPage={handlePreviousPage}
            setCurrentPage={setCurrentPage}
            handleCurrentPage={handleCurrentPage}
          />
        </div>
      </div>
    </main>
  );  

};
