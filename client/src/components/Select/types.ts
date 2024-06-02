/* eslint-disable @typescript-eslint/no-explicit-any */
export type TOption = Record<string, any>;

export type TGroupSelectProps<T extends TOption> = {
    labelText: string;
    options: T[];
    option: {
        label: keyof T;
        value: keyof T;
    };
    required?: boolean;
    placeholder?: string;
    value?: string | undefined;
    onValueChange?: (value: string | undefined) => void;
};
