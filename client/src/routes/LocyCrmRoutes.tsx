import { Suspense } from "react";
import { Routes, Route } from "react-router-dom";

import { Loading } from "@/components";
import { MainWrapper } from "@/layouts";
import {
    Category,
    CreateCustomer,
    CreateEmployee,
    Customer,
    CustomerDetail,
    Dashboard,
    Employee,
    ForgotPassword,
    NotFound,
    Report,
    Settings,
    SignIn,
} from "@/pages";

import { AuthRoutes, ProtectedRoutes } from "./AdvanceRoutes";

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
                <Route element={<AuthRoutes />}>
                    <Route path="/auth/signin" element={<SignIn />} />
                    <Route
                        path="/auth/forgot-password"
                        element={<ForgotPassword />}
                    />
                </Route>
                {/* Main routes */}
                <Route element={<ProtectedRoutes />}>
                    <Route element={<MainWrapper />}>
                        <Route path="/" element={<Dashboard />} />

                        <Route path="/settings" element={<Settings />} />

                        <Route path="/employee" element={<Employee />} />
                        <Route
                            path="/employee/new"
                            element={<CreateEmployee />}
                        />

                        <Route path="/customer" element={<Customer />} />
                        <Route
                            path="/customer/new"
                            element={<CreateCustomer />}
                        />
                        <Route
                            path="/customer/:id"
                            element={<CustomerDetail />}
                        />

                        <Route path="/category" element={<Category />} />

                        <Route path="/report" element={<Report />} />
                    </Route>
                </Route>
                {/* Routes not found */}
                <Route path="*" element={<NotFound />} />
            </Routes>
        </Suspense>
    );
};

export default LocyCrmRoutes;
