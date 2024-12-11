import { Navigate, Route, Routes } from 'react-router-dom';
import { LoginPage, RegisterPage } from '../pages';
// componentes 
import { Navbar, Footer } from "../../Tienda/components"
import React from 'react'

export const SecurityRouter = () => {
  return (
    <div className="overflow-x-hidden bg-white w-screen h-screen  bg-no-repeat bg-cover">
    <Navbar/>
    <div className="px-6 py-8">
      <div className="container flex justify-between mx-auto">
        <Routes>
          <Route path='/register' element={<RegisterPage />} />
          <Route path='/login' element={<LoginPage />} />
          <Route path='/*' element={<Navigate to={"/login"} />} />
        </Routes>
      </div>
    </div>
    <Footer />
  </div>
  )
}
