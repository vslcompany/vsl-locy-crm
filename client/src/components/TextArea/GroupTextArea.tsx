import { forwardRef } from "react";
import TextArea from "./TextArea";

import { TGroupTextAreaProps } from "./types";

const GroupTextArea = forwardRef<HTMLTextAreaElement, TGroupTextAreaProps>(
    ({ labelFor, labelText, children, fontSize, rounded, ...props }, ref) => {
        return (
            <div className="space-y-1">
                <label htmlFor={labelFor} data-text={fontSize}>
                    {props.required ? `${labelText} *` : labelText}
                </label>
                <TextArea
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

export default GroupTextArea;
