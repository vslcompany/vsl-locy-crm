import { useEffect, useMemo, useState } from "react";
import {
    Combobox,
    ComboboxButton,
    ComboboxInput,
    ComboboxOption,
    ComboboxOptions,
    Transition,
} from "@headlessui/react";
import { FaAngleDown, FaCheck } from "react-icons/fa6";

import { TOption, TGroupSelectProps } from "./types";

const GroupSelect = <T extends TOption>({
    labelText,
    options,
    option,
    onValueChange,
    required,
    placeholder = "",
    value,
}: TGroupSelectProps<T>) => {
    const [query, setQuery] = useState("");
    const [selected, setSelected] = useState<string | undefined>();

    const filteredData = useMemo(() => {
        if (query === "") return options;

        return options.filter((item) =>
            item[option.label]
                ?.toLowerCase()
                ?.includes(query.toLocaleLowerCase())
        );
    }, [options, option, query]);

    /**
     * * Handle events
     */
    const handleValueChange = (value: string | null) => {
        setSelected(value !== null ? value.toString() : undefined);
        if (onValueChange) {
            onValueChange(value !== null ? value.toString() : undefined);
        }
    };

    useEffect(() => {
        setSelected(value);
    }, [value]);

    return (
        <>
            <div>
                <Combobox
                    value={selected}
                    onChange={(val) => handleValueChange(val)}
                >
                    <div className="relative w-full group space-y-1">
                        <label className="block first-letter:uppercase font-medium text-sm">
                            {required ? `${labelText} (*)` : labelText}
                        </label>
                        <ComboboxInput
                            className="border border-gray-300 bg-white p-2 text-sm text-gray-900 rounded-lg w-full focus:border-green-600 focus:outline-none focus:ring-0"
                            displayValue={() => {
                                if (selected) {
                                    const item = options.find(
                                        (item) =>
                                            item[option.value].toString() ===
                                            selected
                                    );
                                    return item
                                        ? item[option.label].toString()
                                        : placeholder;
                                }

                                return placeholder;
                            }}
                            onChange={(event) => {
                                setQuery(event.target.value);
                                if (event.target.value === "") {
                                    handleValueChange(null);
                                }
                            }}
                        />
                        <ComboboxButton className="group absolute top-1/2 -translate-y-50 right-2.5">
                            <FaAngleDown className="size-4 fill-black/60 group-data-[hover]:fill-black" />
                        </ComboboxButton>
                    </div>
                    <Transition
                        leave="transition ease-in duration-100"
                        leaveFrom="opacity-100"
                        leaveTo="opacity-0"
                        afterLeave={() => setQuery("")}
                    >
                        <ComboboxOptions
                            anchor="bottom"
                            className="w-[var(--input-width)] rounded-lg border border-gray-300 bg-white p-1 [--anchor-gap:var(--spacing-1)] empty:hidden z-[10000]"
                        >
                            {filteredData.map((item, index) => (
                                <ComboboxOption
                                    key={index}
                                    value={item[option.value]}
                                    className="flex cursor-default items-center gap-2 rounded-lg py-2.5 select-none data-[focus]:bg-white/60"
                                >
                                    <div className="pl-2">
                                        <FaCheck
                                            className={`${
                                                item[option.value] === selected
                                                    ? "visible"
                                                    : "invisible"
                                            } size-4 fill-gray-600`}
                                        />
                                    </div>
                                    <div className="text-sm text-gray-800">
                                        {item[option.label]}
                                    </div>
                                </ComboboxOption>
                            ))}
                        </ComboboxOptions>
                    </Transition>
                </Combobox>
            </div>
        </>
    );
};

export default GroupSelect;
