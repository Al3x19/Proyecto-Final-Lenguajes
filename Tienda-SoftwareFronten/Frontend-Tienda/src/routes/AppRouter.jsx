import { Route, Routes } from "react-router-dom"
import { TiendaRoutes } from "../Tienda/routes"
import { SecurityRouter } from "../security/routes/SecurityRouter"
import { ProtectedLayout } from "../shared/components/ProtectedLayout"
import { AdminRouter } from "../Administration/routes"

export const AppRouter = () => {
  return (
    <Routes>
        <Route path="/security/*" element={<SecurityRouter />} />

        <Route element={<ProtectedLayout />}>
        <Route path="/administration/*" element={<AdminRouter />} />
      </Route>

        <Route path="*" element={<TiendaRoutes />} />
    </Routes>
  )
}

