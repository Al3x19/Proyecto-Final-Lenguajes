import { Navigate, Route, Routes } from 'react-router-dom';
//paginas
import {HomePage, DesarrolladorPage,SearchResults, LoginPublisherPage, SoftwarePage,SoftwareTagPage, SoftwareDescription, SoftwareList} from "../pages"
// componentes 
import { Navbar, Footer } from "../components"
import React from 'react'

export const TiendaRoutes = () => {
  return (
    <div className="overflow-x-hidden backdrop-blur-sm bg-gradient-to-tr from-blue-200 via-teal-100 to-red-300 w-screen h-screen  bg-no-repeat bg-cover">
    <Navbar/>
    <div className="px-6 py-8">
      <div className="container flex justify-between mx-auto">
        <Routes>
          <Route path='/home' element={<HomePage />} />
          <Route path='/loginpublisher' element={<LoginPublisherPage/>} />
          <Route path='/software/' element={<SoftwarePage />} />
          <Route path='/softwareByTag/:name' element={<SoftwareTagPage />} />
          <Route path='/developer/:id' element={<DesarrolladorPage />} />
          <Route path='/softwareDescription/:id' element={<SoftwareDescription />} />
          <Route path='/softwareList' element={<SoftwareList />} />
          <Route path='/searchResults' element={<SearchResults />} />
          
          <Route path='/*' element={<Navigate to={"/home"} />} />
        </Routes>
      </div>
    </div>
    <Footer />
  </div>
  )
}