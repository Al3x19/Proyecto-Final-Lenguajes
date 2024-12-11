import { useState } from "react";
import { Link } from "react-router-dom";
import { SiPaloaltosoftware } from "react-icons/si";
import { FaShoppingCart } from "react-icons/fa";
import { CartItem } from "../components/cartitem";
import { useAuthStore } from "../../security/store";
import { ProtectedComponent } from "../../shared/components/ProtectedComponent";
import { useSearchStore } from "../store/useSearchStore";
import { useNavigate } from "react-router-dom";

export const Navbar = () => {
  const navigate = useNavigate();
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [openCart, setOpenCart] = useState(false);

  const logout = useAuthStore((state) => state.logout);
  const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
  const handleLogout = () => {
    logout();
  };
  const setSearchTerm = useSearchStore((state) => state.setSearchTerm);

  const handleSubmit = (e) => {
    e.preventDefault();
    const query = e.target.elements.search.value;
    setSearchTerm(query);
    navigate("/searchResults");
  };

  const handleMenuToggle = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <nav className="px-6 py-4 bg-gray-100 shadow-md h-20">
      <div className="container flex flex-col mx-auto md:flex-row md:items-center md:justify-between">
        {/* Logo Section */}
        <div className="flex items-center justify-between w-full md:w-auto">
          <Link
            to="/home"
            className="flex items-center text-xl font-bold text-gray-800 md:text-2xl space-x-2"
          >
            <SiPaloaltosoftware />
            <span>Software Store</span>
          </Link>
          <button
            type="button"
            onClick={handleMenuToggle}
            className="block text-gray-800 hover:text-gray-600 md:hidden"
          >
            <svg viewBox="0 0 24 24" className="w-6 h-6 fill-current">
              <path d="M4 5h16a1 1 0 0 1 0 2H4a1 1 0 1 1 0-2zm0 6h16a1 1 0 0 1 0 2H4a1 1 0 1 1 0-2zm0 6h16a1 1 0 0 1 0 2H4a1 1 0 1 1 0-2z"></path>
            </svg>
          </button>
        </div>

        {/* Navigation Links */}
        <div
          className={`${
            isMenuOpen ? "flex" : "hidden"
          } flex-col md:flex md:flex-row md:mx-4`}
        >
          <form
            onSubmit={handleSubmit}
            className="flex items-center bg-white rounded-lg w-56"
          >
            <input
              type="search"
              name="search"
              className="w-48 px-4 py-2 text-gray-800 rounded-l-lg focus:outline-none border"
              placeholder="Buscar"
            />
            <button
              type="submit"
              className="w-16 h-10 text-white bg-gray-800 rounded-r-lg hover:bg-gray-600 flex items-center justify-center"
            >
              Buscar
            </button>
          </form>

          <Link
            to="/home"
            className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
          >
            Inicio
          </Link>

          <ProtectedComponent requiredRoles={"ADMIN"}>
            <Link
              to="/administration/dashboard"
              className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
            >
              Administración
            </Link>
          </ProtectedComponent>

          <Link
            to="/software"
            className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
          >
            Software
          </Link>

          {isAuthenticated ? (
            <button
              onClick={handleLogout}
              className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
            >
              Log Off
            </button>
          ) : (
            <>
              <Link
                to="/security/login"
                className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
              >
                Login
              </Link>
              <Link
                to="/security/register"
                className="my-1 text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
              >
                Register
              </Link>
            </>
          )}

          {/* Cart Section */}
          <div className="relative">
            <button
              onClick={() => setOpenCart(!openCart)}
              className="flex items-center text-gray-800 hover:text-gray-600 md:mx-4 md:my-0"
            >
              <FaShoppingCart className="w-5 h-5" />
            </button>
            {openCart && (
              <div className="absolute right-0 top-8 bg-white shadow-lg rounded-lg p-4 w-80">
                <CartItem />
              </div>
            )}
          </div>
        </div>
      </div>
    </nav>
  );
};
