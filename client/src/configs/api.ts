import axios from "axios";
import { jwtDecode } from "jwt-decode";

import { isExpiredToken, notification } from "@/utilities";
import { TResponseData } from "@/types";

const baseUrl =
    import.meta.env.VITE_APP_BASE_URL_API || "https://localhost:7221/api/v1";

export const publicInstance = axios.create({
    baseURL: baseUrl,
    headers: {
        "Content-Type": "application/json",
        "X-Custom-Headers": "foobar",
    },
    withCredentials: true,
});

export const privateInstance = axios.create({
    baseURL: baseUrl,
    headers: {
        "Content-Type": "application/json",
        "X-Custom-Headers": "foobar",
    },
    withCredentials: true,
});

export const importInstance = axios.create({
    baseURL: baseUrl,
    headers: {
        "Content-Type": "application/json",
        "X-Custom-Headers": "foobar",
    },
    withCredentials: true,
});

// Response interceptor when call APIs done
publicInstance.interceptors.response.use(
    (response) => {
        // Get status action
        const { status, message }: TResponseData = response.data;

        if (response.config.method !== "get") {
            // Notification modal
            notification(status, message);
        }

        // return data
        return response.data;
    },
    (error) => {
        if (axios.isAxiosError(error) && error.response) {
            const { status, message }: TResponseData = error.response.data;

            if (error.response.config.method === "get") {
                // Log error
                console.log(status, message);
            } else {
                // Notification modal
                notification(status, message);
            }

            return error.response.data;
        }
    }
);

// Request interceptor for APIs called
privateInstance.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem("token") || "";

        if (token !== "") {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const decoded: any = jwtDecode(token);

            const isExpired = isExpiredToken(decoded.Exp);

            if (isExpired) {
                // Handle when token is expired
            } else {
                config.headers["Authorization"] = `Bearer ${token}`;
            }
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Response interceptor when call APIs done
privateInstance.interceptors.response.use(
    (response) => {
        // Get status action
        const { status, message }: TResponseData = response.data;

        if (response.config.method !== "get") {
            // Notification modal
            notification(status, message);
        }

        // return data
        return response.data;
    },
    (error) => {
        if (axios.isAxiosError(error) && error.response) {
            const { status, message }: TResponseData = error.response.data;

            if (error.response.config.method === "get") {
                // Log error
                console.log(status, message);
            } else {
                // Notification modal
                notification(status, message);
            }

            return error.response.data;
        }
    }
);

// Request interceptor for APIs called
importInstance.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem("token") || "";

        if (token !== "") {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const decoded: any = jwtDecode(token);

            const isExpired = isExpiredToken(decoded.Exp);

            if (isExpired) {
                // Call api for refresh token
            } else {
                config.headers["Authorization"] = `Bearer ${token}`;
            }
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Response interceptor when call APIs done
importInstance.interceptors.response.use(
    (response) => {
        console.log(response.data);
        return response.data;
    },
    (error) => {
        if (axios.isAxiosError(error) && error.response) {
            console.log(error.response.data);
            return error.response.data;
        }
    }
);
