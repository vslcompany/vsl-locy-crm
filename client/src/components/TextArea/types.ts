import { ComponentProps, ReactNode } from "react";

import { TFontSizeProps, TRoundedProps } from "@/types";

export type TTextAreaProps = ComponentProps<"textarea"> & {
    fontSize?: TFontSizeProps;
    rounded?: TRoundedProps;
};

export type TGroupTextAreaProps = TTextAreaProps & {
    labelFor?: string;
    labelText?: string;

    children?: ReactNode;
};
