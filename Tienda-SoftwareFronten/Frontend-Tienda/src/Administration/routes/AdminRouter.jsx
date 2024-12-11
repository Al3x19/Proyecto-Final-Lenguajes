import { Navigate, Route, Routes } from 'react-router-dom';
import { DashboardPage } from '../pages';
// componentes 
import { Navbar, Footer } from "../../Tienda/components"
import React from 'react'

export const AdminRouter = () => {
  return (
    <div className="overflow-x-hidden bg-white w-screen h-screen  bg-no-repeat bg-cover">
    <Navbar/>
    <div className="px-6 py-8">
      <div className="container flex justify-between mx-auto">
        <Routes>
        <Route path='/dashboard' element={<DashboardPage />} />
        <Route path='/*' element={<Navigate to={"/dashboard"} />} />
        </Routes>
      </div>
    </div>
    <Footer />
  </div>
  )
}
