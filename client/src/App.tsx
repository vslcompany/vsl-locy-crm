import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "animate.css/animate.min.css";

import { LocyCrmRoutes } from "@/routes";

function App() {
    return (
        <>
            <LocyCrmRoutes />
            <ToastContainer />
        </>
    );
}

export default App;
