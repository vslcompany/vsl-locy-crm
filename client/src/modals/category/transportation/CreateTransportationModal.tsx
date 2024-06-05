import { useState } from "react";

import { GroupInput, GroupTextArea, Modal } from "@/components";
import { initTransportation } from "@/constants";
import { TCreateTransportationRequest } from "@/types";

import { TCreateModalProps } from "../../types";

const CreateTransportationModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "tạo",
}: TCreateModalProps<TCreateTransportationRequest>) => {
    const [data, setData] =
        useState<TCreateTransportationRequest>(initTransportation);

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result) setData(initTransportation);
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
                <div className="grid gap-4">
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
                </div>
            </Modal>
        </>
    );
};

export default CreateTransportationModal;
