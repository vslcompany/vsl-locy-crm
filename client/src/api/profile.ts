import { privateInstance } from "@/configs";
import { TUpdateProfileRequest } from "@/types";

export const getProfileById = async (id: number) => {
    return privateInstance.get(`/profile/${id}`);
};

export const updateProfile = async (payload: TUpdateProfileRequest) => {
    return privateInstance.put(`/profile/${payload.id}`, payload);
};
