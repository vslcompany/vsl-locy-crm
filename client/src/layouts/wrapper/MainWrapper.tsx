import { Outlet } from "react-router-dom";

import { Header, Sidebar } from "../ui";

const MainWrapper = () => {
    return (
        <main className="flex flex-col-reverse sm:flex-row w-full h-screen">
            <Sidebar />
            <section className="section flex-1 flex flex-col overflow-hidden">
                <Header />
                <div className="flex-1 space-y-4 overflow-x-hidden overflow-y-auto">
                    <Outlet />
                </div>
            </section>
        </main>
    );
};

export default MainWrapper;
