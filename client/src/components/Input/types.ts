import { ComponentProps, ReactNode } from "react";

import { TFontSizeProps, TRoundedProps } from "@/types";

export type TInputProps = ComponentProps<"input"> & {
    fontSize?: TFontSizeProps;
    rounded?: TRoundedProps;
};

export type TGroupInputProps = TInputProps & {
    labelFor?: string;
    labelText?: string;

    children?: ReactNode;
};
