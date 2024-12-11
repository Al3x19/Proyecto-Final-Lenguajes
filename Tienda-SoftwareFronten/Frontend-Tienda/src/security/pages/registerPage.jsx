
import { addUser } from "../../Tienda/hooks/useUser";
import { userFormInitValues, userValidationSchema } from "../forms";
import { useFormik } from "formik";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuthStore } from "../store";
import { Loading } from "../../shared/components/Loading";

export const RegisterPage = () => {

  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
  const register = useAuthStore((state) => state.register);
  const error = useAuthStore((state) => state.error);
  const validateAuthentication = useAuthStore((state) => state.validateAuthentication);
  const message = useAuthStore((state) => state.message);

  useEffect(() => {
    if(isAuthenticated) {
      navigate('/home');
    }
  }, [isAuthenticated]);


  const formik = useFormik({

    initialValues: userFormInitValues,
    validationSchema: userValidationSchema,
    validateOnChange: true,

    onSubmit: async (formValues) => {
      console.log("hola")
      setLoading(true);
      await register(formValues);
      validateAuthentication();
      setLoading(false);
    },
  });

  if(loading) {
    return <Loading />;
  }


  return (
    <div className="container flex min-h-screen items-center justify-center bg-gray-900">
      <div className="mx-auto w-full max-w-md space-y-8">
        <div className="text-center text-white">
          <h1 className="text-3xl font-bold tracking-tight">Crear nuevo usuario</h1>

        </div>
        <div className="bg-white shadow w-full rounded-lg divide-y divide-gray-200">
        {error ? (
          <span className="p-4 block bg-red-500 text-white text-center rounded-t-lg">
            {message}
          </span>
        ) : (
          ""
        )}
      </div>
        <form onSubmit={formik.handleSubmit} className="space-y-6">
          <div>
            <label htmlFor="name" className="block text-sm font-medium text-gray-300">
              email
            </label>
            <input
              id="email"
              type="text"
              value={formik.values.email}
              onChange={formik.handleChange}
              
              required
              className="mt-1 block w-full rounded-md border-gray-700 bg-gray-800 text-white shadow-lg focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
            />

          </div>
          {formik.touched.email && formik.errors.email && (
              <div className="text-red-500 text-xs mb-2">
                {formik.errors.email}
              </div>
            )}
          
          <div>
            <label htmlFor="name" className="block text-sm font-medium text-gray-300">
              userName
            </label>
            <input
              id="userName"
              type="text"
              value={formik.values.userName}
              onChange={formik.handleChange}
              
              required
              className="mt-1 block w-full rounded-md border-gray-700 bg-gray-800 text-white shadow-lg focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
            />

          </div>
          {formik.touched.userName && formik.errors.userName && (
              <div className="text-red-500 text-xs mb-2">
                {formik.errors.userName}
              </div>
            )}
          <div>
            <label htmlFor="securityCode" className="block text-sm font-medium text-gray-300">
              Contraseña
            </label>
            <input
              id="password"
              type="password"
              value={formik.values.password}
              onChange={formik.handleChange}
              required
              className="mt-1 block w-full rounded-md border-gray-700 bg-gray-800 text-white shadow-lg focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
            />

          </div>              
          {formik.touched.password && formik.errors.password && (
              <div className="text-red-500 text-xs mb-2">
                {formik.errors.password}
              </div>
            )}

          
          <div>
            <label htmlFor="securityCode" className="block text-sm font-medium text-gray-300">
              Repetir Contraseña
            </label>
            <input
              id="confirmPassword"
              type="password"
              value={formik.values.confirmPassword}
              onChange={formik.handleChange}
              required
              className="mt-1 block w-full rounded-md border-gray-700 bg-gray-800 text-white shadow-lg focus:border-gray-500 focus:ring-gray-500 sm:text-sm"
            />

          </div>              
          {formik.touched.confirmPassword && formik.errors.confirmPassword && (
              <div className="text-red-500 text-xs mb-2">
                {formik.errors.confirmPassword}
              </div>
            )}
          <button
            type="submit"
            className="w-full py-2 px-4 bg-gray-700 text-white rounded-md font-semibold hover:bg-gray-600 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-offset-2 shadow-md"
          >
            Iniciar secion
          </button>
          
        </form>
      </div>
    </div>
  );
};
