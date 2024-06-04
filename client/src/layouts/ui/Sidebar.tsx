import { Link, NavLink } from "react-router-dom";
import { FaTachometerAlt, FaUsers, FaHouseUser } from "react-icons/fa";
import { BiSolidCategory, BiSolidReport } from "react-icons/bi";

import { VslLogoSvg } from "@/assets";
import { useAuth } from "@/contexts";
import { TAuthContextProps } from "@/types";

const Sidebar = () => {
    const { user }: TAuthContextProps = useAuth();

    return (
        <>
            <aside className="flex-shrink-0 overflow-y-auto border-t sm:border-t-0 sm:border-r border-gray-300 dark:border-gray-600 p-2 flex flex-col gap-8">
                {/* Logo */}
                <Link
                    className="hidden sm:block w-11 mx-auto"
                    role="heading"
                    to="/"
                >
                    <img
                        className="w-full object-left-top"
                        src={VslLogoSvg}
                        alt="Vsl Logo"
                    />
                </Link>
                {/* Nav links */}
                <nav role="nav">
                    <ul role="nav-list">
                        <li role="nav-item">
                            <NavLink to="/" role="nav-link" end>
                                <FaTachometerAlt role="nav-icon" />
                                <span role="nav-label">dashboard</span>
                            </NavLink>
                        </li>
                        {(user?.permission.includes("1048576") ||
                            user?.permission.includes("5000") ||
                            user?.permission.includes("5020") ||
                            user?.permission.includes("5040")) && (
                            <li role="nav-item">
                                <NavLink to="/employee" role="nav-link" end>
                                    <FaUsers role="nav-icon" />
                                    <span role="nav-label">nhân viên</span>
                                </NavLink>
                            </li>
                        )}
                        <li role="nav-item">
                            <NavLink to="/customer" role="nav-link" end>
                                <FaHouseUser role="nav-icon" />
                                <span role="nav-label">khách hàng</span>
                            </NavLink>
                        </li>
                        <li role="nav-item">
                            <NavLink to="/report" role="nav-link" end>
                                <BiSolidReport role="nav-icon" />
                                <span role="nav-label">báo cáo</span>
                            </NavLink>
                        </li>
                        {(user?.permission.includes("1048576") ||
                            user?.permission.includes("6000") ||
                            user?.permission.includes("6020") ||
                            user?.permission.includes("6040")) && (
                            <li role="nav-item">
                                <NavLink to="/category" role="nav-link" end>
                                    <BiSolidCategory role="nav-icon" />
                                    <span role="nav-label">danh mục</span>
                                </NavLink>
                            </li>
                        )}
                    </ul>
                </nav>
            </aside>
        </>
    );
};

export default Sidebar;
