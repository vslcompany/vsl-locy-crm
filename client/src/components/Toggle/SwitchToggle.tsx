import { useEffect, useMemo, useState } from "react";
import { Switch } from "@headlessui/react";

import { TSwitchToggleProps } from "./types";

const SwitchToggle = ({
    checked,
    isSetEnabled = true,
    size = "base",
}: TSwitchToggleProps) => {
    const [enabled, setEnabled] = useState<boolean>(checked);

    const sizeClass = useMemo(() => {
        const props = {
            switchSizeClass: "",
            spanSizeClass: "",
            spanTransformEnabled: "",
            spanTransformUnenabled: "",
        };

        switch (size) {
            case "sm":
                props.switchSizeClass = "h-4 w-7";
                props.spanSizeClass = "h-2 w-2";
                props.spanTransformEnabled = "translate-x-4";
                props.spanTransformUnenabled = "translate-x-1";
                break;
            case "base":
                props.switchSizeClass = "h-6 w-11";
                props.spanSizeClass = "h-4 w-4";
                props.spanTransformEnabled = "translate-x-6";
                props.spanTransformUnenabled = "translate-x-1";
                break;
            case "lg":
                props.switchSizeClass = "h-8 w-16";
                props.spanSizeClass = "h-6 w-6";
                props.spanTransformEnabled = "translate-x-9";
                props.spanTransformUnenabled = "translate-x-1";
                break;
            default:
                props.switchSizeClass = "h-6 w-11";
                props.spanSizeClass = "h-4 w-4";
                props.spanTransformEnabled = "translate-x-6";
                props.spanTransformUnenabled = "translate-x-1";
                break;
        }

        return props;
    }, [size]);

    /**
     * * Handle events
     */
    const handleChange = (checked: boolean) => {
        if (isSetEnabled) {
            setEnabled(checked);
        }
    };

    useEffect(() => {
        setEnabled(checked);
    }, [checked]);

    return (
        <>
            <Switch
                checked={enabled}
                onChange={handleChange}
                className={`${
                    enabled ? "bg-green-600" : "bg-gray-400"
                } relative inline-flex ${
                    sizeClass.switchSizeClass
                } items-center rounded-full`}
            >
                <span
                    aria-hidden="true"
                    className={`${
                        enabled
                            ? sizeClass.spanTransformEnabled
                            : sizeClass.spanTransformUnenabled
                    } inline-block ${
                        sizeClass.spanSizeClass
                    } transform rounded-full bg-white transition`}
                />
            </Switch>
        </>
    );
};

export default SwitchToggle;
