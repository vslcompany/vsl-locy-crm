import { useMemo } from "react";
import ReactLoading from "react-loading";

import { TLoadingProps } from "./types";

const Loading = ({
    showMessage = false,
    size = "xl",
    message = "Đang tải",
    type = "spinningBubbles",
    color = "#16A34A",
    direction = "row",
}: TLoadingProps) => {
    const loadingSize = useMemo(() => {
        let result = 0;

        switch (size) {
            case "base":
                result = 16;
                break;
            case "xs":
                result = 8;
                break;
            case "sm":
                result = 12;
                break;
            case "lg":
                result = 20;
                break;
            case "xl":
                result = 24;
                break;
            case "2xl":
                result = 28;
                break;
            case "3xl":
                result = 32;
                break;
            case "4xl":
                result = 48;
                break;
            case "5xl":
                result = 64;
                break;
            default:
                break;
        }

        return result;
    }, [size]);

    const classes = useMemo(() => {
        let baseClasses = "inline-flex gap-2 items-center max-w-72";

        if (direction == "col") baseClasses += " flex-col";
        if (direction == "col-reverse") baseClasses += " flex-col-reverse";
        if (direction == "row") baseClasses += " flex-row";
        if (direction == "row-reverse") baseClasses += " flex-row-reverse";

        return baseClasses;
    }, [direction]);

    return (
        <div className={classes}>
            <ReactLoading
                type={type}
                color={color}
                width={loadingSize}
                height={loadingSize}
            />
            {showMessage && (
                <span className="first-letter:uppercase font-medium">
                    {message}...
                </span>
            )}
        </div>
    );
};

export default Loading;
