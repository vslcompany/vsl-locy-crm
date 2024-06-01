import { Suspense } from "react";
import { Routes, Route } from "react-router-dom";

import { Loading } from "@/components";
import { ForgotPassword, NotFound, SignIn } from "@/pages";

const LocyCrmRoutes = () => {
    return (
        <Suspense
            fallback={
                <div className="w-screen h-screen flex justify-center items-center">
                    <Loading size="3xl" />
                </div>
            }
        >
            <Routes>
                {/* Authentication routes */}
                <Route>
                    <Route path="/auth/signin" element={<SignIn />} />
                    <Route
                        path="/auth/forgot-password"
                        element={<ForgotPassword />}
                    />
                </Route>
                {/* Routes not found */}
                <Route path="*" element={<NotFound />} />
            </Routes>
        </Suspense>
    );
};

export default LocyCrmRoutes;
