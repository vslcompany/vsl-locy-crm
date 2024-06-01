import { forwardRef } from "react";
import Input from "./Input";

import { TGroupInputProps } from "./types";

const GroupInput = forwardRef<HTMLInputElement, TGroupInputProps>(
    ({ labelFor, labelText, children, fontSize, rounded, ...props }, ref) => {
        return (
            <div className="space-y-1">
                <label
                    className="inline-block first-letter:uppercase font-medium"
                    htmlFor={labelFor}
                    data-text={fontSize}
                >
                    {props.required ? `${labelText} *` : labelText}
                </label>
                <Input
                    {...props}
                    name={labelFor}
                    id={labelFor}
                    fontSize={fontSize}
                    rounded={rounded}
                    ref={ref}
                />
                {children && children}
            </div>
        );
    }
);

export default GroupInput;
