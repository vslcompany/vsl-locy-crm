import { Fragment, useCallback, useState } from "react";
import { Link } from "react-router-dom";
import {
    Menu,
    MenuButton,
    MenuItem,
    MenuItems,
    Transition,
} from "@headlessui/react";

import { FiChevronDown } from "react-icons/fi";
import { BiLogOut } from "react-icons/bi";
import { IoMdSettings } from "react-icons/io";

import { logOut } from "@/api";
import { Modal } from "@/components";
import { useAuth } from "@/contexts";
import { TAuthContextProps } from "@/types";

const Header = () => {
    const { user, updateData }: TAuthContextProps = useAuth();

    const [isOpenLogOutModal, setIsOpenLogOutModal] = useState<boolean>(false);

    const openLogOutModal = useCallback(() => {
        setIsOpenLogOutModal(true);
    }, []);

    const closeLogOutModal = useCallback(() => {
        setIsOpenLogOutModal(false);
    }, []);

    const handleLogOut = useCallback(async () => {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const result: any = await logOut();

        if (result.status) {
            localStorage.removeItem("token");
            updateData(null);
        }
    }, [updateData]);

    return (
        <>
            <header className="border-b border-gray-300 dark:border-gray-600 z-20 shadow-sm flex-shrink-0">
                <div className="w-full flex justify-end items-center gap-2">
                    {/* Notification toggle */}
                    {/* <div className="p-2 rounded-full hover:bg-gray-200 hover:dark:bg-gray-600 cursor-pointer">
                        <span className="text-base">
                            <BsBellFill />
                        </span>
                    </div> */}
                    {/* Profile toggle */}
                    <div className="header-profile">
                        <Menu as="div" className="relative inline-block">
                            <MenuButton className="flex items-center gap-1 sm:gap-2">
                                <span className="hidden text-right sm:block">
                                    <span
                                        className="block text-sm capitalize font-semibold"
                                        role="heading"
                                    >
                                        {user?.hoTenVI}
                                    </span>
                                    <span className="block text-xs first-letter:uppercase font-semibold">
                                        {user?.chucVu || user?.hoTenVI}
                                    </span>
                                </span>
                                <span className="w-8 h-8 rounded-full overflow-hidden">
                                    <img
                                        className="w-full h-full"
                                        src={
                                            user?.photoURL ||
                                            "https://cdn-icons-png.flaticon.com/512/3135/3135715.png"
                                        }
                                        alt={user?.username.toLowerCase()}
                                    />
                                </span>
                                <FiChevronDown className="text-lg" />
                            </MenuButton>
                            <Transition
                                as={Fragment}
                                enter="transition ease-out duration-100"
                                enterFrom="transform opacity-0 scale-95"
                                enterTo="transform opacity-100 scale-100"
                                leave="transition ease-in duration-75"
                                leaveFrom="transform opacity-100 scale-100"
                                leaveTo="transform opacity-0 scale-95"
                            >
                                <MenuItems className="absolute top-full right-0 mt-6 w-56 origin-top-right divide-y rounded-md bg-white shadow-lg focus:outline-none">
                                    <MenuItem>
                                        <Link
                                            className="flex items-center w-full gap-2 p-4 text-sm lg:text-base font-medium duration-300 ease-in-out text-gray-500 hover:text-black cursor-pointer"
                                            to="/settings"
                                        >
                                            <IoMdSettings className="text-xl" />
                                            <span className="capitalize">
                                                cài đặt
                                            </span>
                                        </Link>
                                    </MenuItem>
                                    <MenuItem>
                                        <button
                                            className="flex items-center w-full gap-2 p-4 text-sm lg:text-base font-medium duration-300 ease-in-out text-gray-500 hover:text-black cursor-pointer"
                                            onClick={openLogOutModal}
                                        >
                                            <BiLogOut className="text-lg" />
                                            <span className="capitalize">
                                                đăng xuất
                                            </span>
                                        </button>
                                    </MenuItem>
                                </MenuItems>
                            </Transition>
                        </Menu>
                    </div>
                </div>
            </header>
            <Modal
                isOpen={isOpenLogOutModal}
                onClose={closeLogOutModal}
                onSubmit={handleLogOut}
                title="đăng xuất"
                description="bạn có muốn đăng xuất"
                labelButtonCancel="huỷ"
                labelButtonOk="đăng xuất"
            />
        </>
    );
};

export default Header;
