import {
    Menu,
    MenuButton,
    MenuItem,
    MenuItems,
    Transition,
} from "@headlessui/react";
import { RiArrowDownSLine } from "react-icons/ri";

import { TDropdownProps } from "./types";

const Dropdown = ({ color = "gray", label, options }: TDropdownProps) => {
    return (
        <Menu>
            <MenuButton
                d-color={color}
                className={`inline-flex items-center gap-1 rounded-md px-4 py-2 text-sm/6 font-semibold text-white shadow-inner shadow-white/10 focus:outline-none data-[focus]:outline-1 data-[focus]:outline-white`}
            >
                <span className="first-letter:uppercase">{label}</span>
                <RiArrowDownSLine role="dropdown-icon" />
            </MenuButton>
            <Transition
                enter="transition ease-out duration-75"
                enterFrom="opacity-0 scale-95"
                enterTo="opacity-100 scale-100"
                leave="transition ease-in duration-100"
                leaveFrom="opacity-100 scale-100"
                leaveTo="opacity-0 scale-95"
            >
                <MenuItems
                    anchor="bottom end"
                    className="w-64 origin-top-right translate-y-1 rounded-xl border border-gray-300 dark:border-gray-600 bg-gray-50 dark:bg-gray-800 p-2 text-sm/6 text-gray-500 dark:text-gray-400 [--anchor-gap:var(--spacing-1)] focus:outline-none z-[1000]"
                >
                    {options.map((option, index) => (
                        <MenuItem key={index}>
                            <button className="w-full rounded-xl text-left px-4 py-2 data-[focus]:bg-black/10 dark:data-[focus]:bg-white/10 font-medium">
                                {option}
                            </button>
                        </MenuItem>
                    ))}
                </MenuItems>
            </Transition>
        </Menu>
    );
};

export default Dropdown;
