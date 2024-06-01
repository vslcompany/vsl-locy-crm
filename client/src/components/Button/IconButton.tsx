import { TIconButtonProps } from "./types";

const IconButton = ({
    variant = "contained",
    color = "green",
    rounded = "full",
    icon,
    isLoading = false,
    ...props
}: TIconButtonProps) => {
    return (
        <>
            <button
                {...props}
                role="icon-button"
                b-variant={variant}
                b-color={color}
                data-rounded={rounded}
                disabled={isLoading}
            >
                {isLoading ? (
                    <>
                        <span b-icon=""></span>
                    </>
                ) : (
                    <span b-icon="">{icon}</span>
                )}
            </button>
        </>
    );
};

export default IconButton;
