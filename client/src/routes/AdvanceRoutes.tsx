import { Navigate, Outlet } from "react-router-dom";

import { useAuth } from "@/contexts";
import { TAuthContextProps } from "@/types";

export const AuthRoutes = () => {
    const { user }: TAuthContextProps = useAuth();
    return user == null ? <Outlet /> : <Navigate to="/" />;
};

export const ProtectedRoutes = () => {
    const { user }: TAuthContextProps = useAuth();
    return user != null ? <Outlet /> : <Navigate to="/auth/signin" />;
};
