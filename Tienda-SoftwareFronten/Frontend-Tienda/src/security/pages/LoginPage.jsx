
import { loginInitValues, loginValidationSchema } from "../../security/forms";
import { useFormik } from "formik";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuthStore } from "../../security/store";
import { Loading } from "../../shared/components/Loading";
import { Link } from "react-router-dom";
export const LoginPage = () => {

  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
  const login = useAuthStore((state) => state.login);
  const error = useAuthStore((state) => state.error);
  const message = useAuthStore((state) => state.message);

  useEffect(() => {
    if(isAuthenticated) {
      navigate('/home');
    }
  }, [isAuthenticated]);


  const formik = useFormik({

    initialValues: loginInitValues,
    validationSchema: loginValidationSchema,
    validateOnChange: true,

    onSubmit: async (formValues) => {
      setLoading(true);
      await login(formValues);
      setLoading(false);
    },
  });
  return (
    <div className="container flex min-h-screen items-center justify-center bg-gray-900">
      <div className="mx-auto w-full max-w-md space-y-8">
        <div className="text-center text-white">
          <h1 className="text-3xl font-bold tracking-tight">Inicia secion</h1>
          <p className="text-gray-400">
          <Link to="/security/register" className="font-medium text-gray-200 underline underline-offset-4 hover:text-white">
Registrate!
</Link>
          </p>
        </div>
        <form onSubmit={formik.handleSubmit} className="space-y-6">
          <div>
            <label htmlFor="name" className="block text-sm font-medium text-gray-300">
              Name
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
