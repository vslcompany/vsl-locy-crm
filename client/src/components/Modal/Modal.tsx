import { Fragment, useCallback } from "react";
import { FaTimes } from "react-icons/fa";
import {
    Dialog,
    DialogPanel,
    DialogTitle,
    Transition,
    TransitionChild,
} from "@headlessui/react";

import { MAXWIDTH_CLASS } from "@/constants";

import { TDialogProps } from "./types";
import { Button } from "../Button";

const Modal = ({
    isOpen,
    onClose,
    onSubmit,
    title = "modal title",
    description,
    width = "xl",
    children,
    labelButtonCancel = "cancel",
    labelButtonOk = "Ok",
    labelButtonOkLoading,
    isButtonOkLoading = false,
    customDisplayFooter,
}: TDialogProps) => {
    /**
     * * Handle events
     */
    const handleSubmit = useCallback(() => {
        if (onSubmit) {
            onSubmit();
        }
    }, [onSubmit]);

    return (
        <>
            <Transition appear show={isOpen}>
                <Dialog
                    as="div"
                    className="relative z-[1000] focus:outline-none"
                    onClose={onClose}
                >
                    <TransitionChild
                        as={Fragment}
                        enter="ease-out duration-300"
                        enterFrom="opacity-0"
                        enterTo="opacity-100"
                        leave="ease-in duration-200"
                        leaveFrom="opacity-100"
                        leaveTo="opacity-0"
                    >
                        <div
                            className="fixed inset-0 bg-black/25"
                            role="modal-backdrop"
                        />
                    </TransitionChild>
                    <div className="fixed inset-0 z-[1000] w-screen overflow-hidden">
                        <div className="h-full flex items-center justify-center p-4">
                            <TransitionChild
                                enter="ease-out duration-300"
                                enterFrom="opacity-0 transform-[scale(95%)]"
                                enterTo="opacity-100 transform-[scale(100%)]"
                                leave="ease-in duration-200"
                                leaveFrom="opacity-100 transform-[scale(100%)]"
                                leaveTo="opacity-0 transform-[scale(95%)]"
                            >
                                <DialogPanel
                                    className={`w-full ${MAXWIDTH_CLASS[width]} rounded-xl bg-white overflow-hidden`}
                                >
                                    <div className="flex flex-col max-h-[90vh] overflow-auto">
                                        <div className="flex-shrink-0 flex justify-between items-center p-2 shadow-sm">
                                            <DialogTitle
                                                as="h3"
                                                className="first-letter:uppercase text-base font-medium text-gray-900"
                                            >
                                                {title}
                                            </DialogTitle>
                                            <button
                                                className="text-sm text-gray-400 hover:text-gray-900 transition-all"
                                                onClick={onClose}
                                            >
                                                <FaTimes />
                                            </button>
                                        </div>
                                        <div className="flex-1 px-2 py-4 space-y-4 overflow-x-hidden overflow-y-auto no-scrollbar">
                                            {description && (
                                                <p className="text-gray-900 first-letter:uppercase">
                                                    {description}
                                                </p>
                                            )}
                                            {children && children}
                                        </div>
                                        <div className="flex-shrink-0 flex gap-2 justify-end items-center p-2 shadow-sm">
                                            {customDisplayFooter ? (
                                                customDisplayFooter()
                                            ) : (
                                                <>
                                                    <Button
                                                        color="neutral"
                                                        label={
                                                            labelButtonCancel
                                                        }
                                                        onClick={onClose}
                                                    />
                                                    <Button
                                                        color="green"
                                                        label={labelButtonOk}
                                                        onClick={handleSubmit}
                                                        isLoading={
                                                            isButtonOkLoading
                                                        }
                                                        textIsLoading={
                                                            labelButtonOkLoading
                                                        }
                                                    />
                                                </>
                                            )}
                                        </div>
                                    </div>
                                </DialogPanel>
                            </TransitionChild>
                        </div>
                    </div>
                </Dialog>
            </Transition>
        </>
    );
};

export default Modal;
