import { ReactNode } from "react";

export type TDialogProps = {
    isOpen: boolean;
    onClose: () => void;
    onSubmit?: () => void;

    title?: string;
    description?: string;
    width?:
        | "xs"
        | "sm"
        | "md"
        | "lg"
        | "xl"
        | "2xl"
        | "3xl"
        | "4xl"
        | "5xl"
        | "6xl"
        | "7xl";

    children?: ReactNode | ReactNode[];

    labelButtonCancel?: string;
    labelButtonOk?: string;
    labelButtonOkLoading?: string;
    isButtonOkLoading?: boolean;

    customDisplayFooter?: () => ReactNode;
};
