import { useCallback, useEffect, useMemo, useState } from "react";

import { AuthContext } from "./auth.context";
import { Loading } from "@/components";
import { useProfile } from "@/hooks";
import { TAuthContextProps, TProfileDto } from "@/types";

import { TProviderProps } from "./types";

const AuthProvider = ({ children }: TProviderProps) => {
    const [getUser] = useProfile();

    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [user, setUser] = useState<TProfileDto | null>(null);

    const updateData = useCallback((payload: TProfileDto | null) => {
        if (payload) {
            setUser((prev) => ({ ...prev, ...payload }));
        } else {
            setUser(payload);
        }
    }, []);

    const getUserData = useCallback(async () => {
        const res = await getUser();
        setUser(res);
        setIsLoading(false);
    }, [getUser]);

    useEffect(() => {
        getUserData();
    }, [getUserData]);

    const value: TAuthContextProps = useMemo(
        () => ({
            user,
            updateData,
        }),
        [user, updateData]
    );

    return (
        <AuthContext.Provider value={value}>
            {isLoading && (
                <div className="w-screen h-screen flex-center">
                    <Loading size="3xl" />
                </div>
            )}
            {!isLoading && children}
        </AuthContext.Provider>
    );
};

export default AuthProvider;
