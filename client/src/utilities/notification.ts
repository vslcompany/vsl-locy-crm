import { toast, cssTransition, ToastPosition, Theme } from "react-toastify";

const bounce = cssTransition({
    enter: "animate__animated animate__bounceIn",
    exit: "animate__animated animate__bounceOut",
});

const config = {
    position: "top-right" as ToastPosition,
    autoClose: 5000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "light" as Theme,
    transition: bounce,
};

export function notification(status: boolean, message: string) {
    if (!status) {
        return toast.error(message, config);
    }

    return toast.success(message, config);
}
