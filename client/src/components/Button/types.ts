import { ComponentProps, ReactNode } from "react";

import { TColorProps, TFontSizeProps, TRoundedProps } from "@/types";

/**
 * * ===== Button =====
 */
export type Tvariant = "contained" | "outlined";

export type TBaseButtonProps = Omit<ComponentProps<"button">, "children">;

export type TButtonProps = TBaseButtonProps & {
    /**
     * * 1. Set button type
     * * 2. Set button text
     * * 3. Set font size text of button
     * * 4. Set button color
     * * 5. Set border radius of button
     */
    variant?: Tvariant; // 1
    label?: string; // 2
    size?: TFontSizeProps; // 3
    color?: TColorProps; // 4
    rounded?: TRoundedProps; // 5

    /**
     * * 1. Set icon in button component
     * * 2. Set position icon in button component
     */
    icon?: ReactNode; // 1
    iconPosition?: "start" | "end"; // 2

    /**
     * * 1. Set loading in button component
     * * 2. Set text loading in button component
     */
    isLoading?: boolean; // 1
    textIsLoading?: string; // 2
};

export type TIconButtonProps = TBaseButtonProps & {
    /**
     * * 1. Set button type
     * * 2. Set size icon of icon button
     * * 3. Set button color
     * * 4. Set border radius of icon button
     */
    variant?: Tvariant; // 1
    // size?: ; // 2
    color?: TColorProps; // 3
    rounded?: TRoundedProps; // 4

    /**
     * * Set icon in icon button component
     */
    icon: ReactNode;

    /**
     * * Set loading in icon button component
     */
    isLoading?: boolean;
};
