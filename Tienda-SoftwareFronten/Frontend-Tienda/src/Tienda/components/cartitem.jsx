import React, { useState } from "react";
import { MdDeleteForever } from "react-icons/md";

export const CartItem = () => {
  // Estado para manejar los productos del carrito
  const [cartItems, setCartItems] = useState([]);

  // Función para eliminar un producto del carrito
  const handleDelete = (index) => {
    const updatedCart = [...cartItems];
    updatedCart.splice(index, 1);
    setCartItems(updatedCart);
  };

  return (
    <div className="bg-white w-96 border rounded-lg shadow-lg mx-5">
      {cartItems.length === 0 ? (
        // Mostrar mensaje si el carrito está vacío
        <div className="p-4 text-center text-gray-500">
          El carrito está vacío
        </div>
      ) : (
        <>
          {/* Renderizar los elementos del carrito */}
          {cartItems.map((item, index) => (
            <div
              key={index}
              className="flex justify-between items-center p-4 border-b"
            >
              <div className="flex items-center">
                <span className="mr-4 text-gray-700">{item.quantity}</span>
                <span className="text-gray-800 font-medium">{item.name}</span>
              </div>
              <div className="flex items-center">
                <span className="mr-4 text-gray-800 font-semibold">
                  ${item.price}
                </span>
                <button
                  className="text-red-500 hover:text-red-700"
                  onClick={() => handleDelete(index)}
                >
                  <MdDeleteForever className="w-6 h-6" />
                </button>
              </div>
            </div>
          ))}

          {/* Total del carrito */}
          <div className="p-4 border-t">
            <div className="flex justify-between">
              <span className="text-gray-700">Total:</span>
              <span className="font-bold text-gray-800">
                ${cartItems.reduce((acc, item) => acc + item.price * item.quantity, 0)}
              </span>
            </div>
          </div>
        </>
      )}
    </div>
  );
};


