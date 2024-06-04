import { TDeliveryCustomerRequest } from "@/types";

type TWidth =
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

export type TBaseModalProps = {
    isOpen: boolean;
    onClose: () => void;
    title?: string;
    description?: string;
    width?: TWidth;

    labelButtonCancel?: string;
    labelButtonOk?: string;
};

export type TCreateModalProps<T> = TBaseModalProps & {
    onSubmit: (payload: T) => Promise<boolean>;
    isLoading: boolean;
};

export type TUpdateModalProps<T> = TBaseModalProps & {
    onSubmit: (payload: T) => Promise<boolean>;
    prevData: T | null;
    isLoading: boolean;
};

export type TCreateEmployeeGroupProps<T> = TCreateModalProps<T> & {
    parentId?: number;
};

export type TDeliveryCustomerModalProps = TBaseModalProps & {
    onSubmit: (payload: TDeliveryCustomerRequest) => Promise<boolean>;
    idCustomers: number[] | [];
    isLoading: boolean;
};

export type TCreateCustomerInfoModalProps<T> = TCreateModalProps<T> & {
    idCustomer: number;
};

export type TUpdateCustomerInfoModalProps<T> = TUpdateModalProps<T>;
