import { forwardRef, useMemo } from "react";

import { TInputProps } from "./types";

const Input = forwardRef<HTMLInputElement, TInputProps>(
    ({ className, fontSize = "base", rounded = "md", ...props }, ref) => {
        const classes = useMemo(() => {
            let baseClasses =
                "bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-green-500 focus:border-green-500 block w-full py-1 px-2 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-green-500 dark:focus:border-green-500 outline-none focus:outline-none";

            if (className) {
                baseClasses += ` ${className}`;
            }

            return baseClasses;
        }, [className]);

        return (
            <>
                <input
                    {...props}
                    className={classes}
                    data-text={fontSize}
                    data-rounded={rounded}
                    ref={ref}
                />
            </>
        );
    }
);

export default Input;
