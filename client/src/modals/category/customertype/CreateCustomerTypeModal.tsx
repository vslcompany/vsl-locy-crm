import { useState } from "react";

import { GroupInput, GroupTextArea, Modal } from "@/components";
import { initCustomerType } from "@/constants";
import { TCreateCustomerTypeRequest } from "@/types";

import { TCreateModalProps } from "../../types";

const CreateCustomerTypeModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "tạo",
}: TCreateModalProps<TCreateCustomerTypeRequest>) => {
    const [data, setData] =
        useState<TCreateCustomerTypeRequest>(initCustomerType);

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result) setData(initCustomerType);
    };

    return (
        <>
            <Modal
                isOpen={isOpen}
                onClose={onClose}
                onSubmit={() => handleSubmit()}
                width={width}
                title={title}
                description={description}
                labelButtonCancel={labelButtonCancel}
                labelButtonOk={labelButtonOk}
                labelButtonOkLoading="đang tạo"
                isButtonOkLoading={isLoading}
            >
                <form className="grid gap-4" onSubmit={handleSubmit}>
                    <GroupInput
                        labelFor="code"
                        labelText="mã"
                        required={true}
                        value={data.code}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                code: e.target.value,
                            }))
                        }
                    />
                    <GroupTextArea
                        labelFor="nameVI"
                        labelText="tên (VI)"
                        required={true}
                        value={data.nameVI}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                nameVI: e.target.value,
                            }))
                        }
                    />
                    <GroupTextArea
                        labelFor="nameEN"
                        labelText="tên (EN)"
                        value={data.nameEN}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                nameEN: e.target.value,
                            }))
                        }
                    />
                </form>
            </Modal>
        </>
    );
};

export default CreateCustomerTypeModal;
