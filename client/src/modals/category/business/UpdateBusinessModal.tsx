import { useEffect, useState } from "react";
import { isEqual } from "lodash";

import { GroupInput, GroupTextArea, Modal } from "@/components";
import { TUpdateBusinessRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateModalProps } from "../../types";

const UpdateBusinessModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "cập nhật",
}: TUpdateModalProps<TUpdateBusinessRequest>) => {
    const [data, setData] = useState<TUpdateBusinessRequest | null>(null);

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (isEqual(data, prevData)) {
            notification(false, "Thông tin không thay đổi");
            return;
        }

        if (data) {
            await onSubmit(data);
        }
    };

    useEffect(() => {
        setData(prevData);
    }, [prevData]);

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
                labelButtonOkLoading="đang cập nhật"
                isButtonOkLoading={isLoading}
            >
                {data !== null && (
                    <div className="grid gap-4" onSubmit={handleSubmit}>
                        <GroupInput
                            labelFor="code"
                            labelText="mã"
                            required={true}
                            value={data.code}
                            onChange={(e) =>
                                setData((prev) => {
                                    if (prev) {
                                        return {
                                            ...prev,
                                            code: e.target.value,
                                        };
                                    }
                                    return null;
                                })
                            }
                        />
                        <GroupTextArea
                            labelFor="nameVI"
                            labelText="tên (VI)"
                            required={true}
                            value={data.nameVI}
                            onChange={(e) =>
                                setData((prev) => {
                                    if (prev) {
                                        return {
                                            ...prev,
                                            nameVI: e.target.value,
                                        };
                                    }

                                    return null;
                                })
                            }
                        />
                        <GroupTextArea
                            labelFor="nameEN"
                            labelText="tên (EN)"
                            value={data.nameEN}
                            onChange={(e) =>
                                setData((prev) => {
                                    if (prev) {
                                        return {
                                            ...prev,
                                            nameEN: e.target.value,
                                        };
                                    }

                                    return null;
                                })
                            }
                        />
                    </div>
                )}
            </Modal>
        </>
    );
};

export default UpdateBusinessModal;
