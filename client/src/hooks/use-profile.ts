import { useCallback } from "react";
import { jwtDecode } from "jwt-decode";

import { getProfileById, updateProfile } from "@/api";
import { TProfileDto, TUpdateProfileRequest } from "@/types";

export const useProfile = () => {
    const getUser = useCallback(async () => {
        const token = localStorage.getItem("token") || "";

        if (token !== "") {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const decoded: any = jwtDecode(token);

            const res = await getProfileById(decoded.Id);

            if (res) return res.data as unknown as TProfileDto;

            return null;
        }

        return null;
    }, []);

    const updateUser = useCallback(async (payload: TUpdateProfileRequest) => {
        const res = await updateProfile(payload);
        return res.data;
    }, []);

    return [getUser, updateUser] as const;
};
