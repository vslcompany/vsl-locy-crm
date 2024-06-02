import { ReactNode } from "react";
import { TColorProps } from "@/types";

export type TDropdownProps = {
    color?: TColorProps;
    label: string;
    options: ReactNode[];
};
