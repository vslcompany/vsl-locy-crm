import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "animate.css/animate.min.css";

import { AuthProvider } from "@/contexts";
import { LocyCrmRoutes } from "@/routes";

function App() {
    return (
        <AuthProvider>
            <LocyCrmRoutes />
            <ToastContainer />
        </AuthProvider>
    );
}

export default App;
