import React, { useContext } from "react";

import { TAuthContextProps } from "@/types";

export const AuthContext = React.createContext<TAuthContextProps>(
    {} as TAuthContextProps
);

export const useAuth = () => {
    return useContext(AuthContext);
};
