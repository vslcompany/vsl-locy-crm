import { privateInstance, publicInstance } from "@/configs";
import {
    TChangePasswordRequest,
    TForgotPasswordRequest,
    TLogInRequest,
} from "@/types";

export const logIn = (payload: TLogInRequest) => {
    return publicInstance.post("/auth/login", payload);
};

export const logOut = () => {
    return publicInstance.post("/auth/logout");
};

export const changePassword = async (payload: TChangePasswordRequest) => {
    return privateInstance.post("/auth/change-password", payload);
};

export const forgotPassword = (payload: TForgotPasswordRequest) => {
    return publicInstance.post("/auth/forgot-password", payload);
};

export const checkOtp = ({ otp, email }: { otp: string; email: string }) => {
    return publicInstance.post("/auth/check-otp", { otp, email });
};

export const resetPassword = ({
    email,
    password,
}: {
    email: string;
    password: string;
}) => {
    return publicInstance.post("/auth/reset-password", { email, password });
};
