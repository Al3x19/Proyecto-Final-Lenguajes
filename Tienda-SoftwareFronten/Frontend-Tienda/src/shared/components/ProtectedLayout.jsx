import { Navigate, Outlet } from "react-router-dom";
import { useAuthStore } from "../../security/store";


export const ProtectedLayout = () => {
  const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
  const roles = useAuthStore((state) => state.roles);

  if (!isAuthenticated || !roles.some(role => ["ADMIN", "PUBLISHER"].includes(role) )) {
    console.log(roles)
    return <Navigate to="/home" />;
  }

  return (
    <div className="grid grid-cols-[auto_1fr] grid-rows-[auto_1fr] gap-4 w-full">      
      <Outlet />
    </div>
  );
};
