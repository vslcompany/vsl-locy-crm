import { TButtonProps } from "./types";

const Button = ({
    variant = "contained",
    label = "button",
    color = "green",
    rounded = "lg",
    size = "base",
    icon,
    iconPosition = "start",
    isLoading = false,
    textIsLoading = "loading",
    ...props
}: TButtonProps) => {
    return (
        <>
            <button
                {...props}
                role="button"
                b-variant={variant}
                b-color={color}
                b-icon-position={iconPosition}
                data-rounded={rounded}
                data-text={size}
                disabled={isLoading}
            >
                {isLoading && (
                    <>
                        {/* <span b-icon=""></span> */}
                        <span b-label="">{textIsLoading}...</span>
                    </>
                )}
                {!isLoading && (
                    <>
                        {icon && <span b-icon="">{icon}</span>}
                        <span b-label="">{label}</span>
                    </>
                )}
            </button>
        </>
    );
};

export default Button;
