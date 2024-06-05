import { ChangeEvent, useState } from "react";

import { GroupInput, GroupTextArea, Modal } from "@/components";
import { initOperational } from "@/constants";
import { TCreateOperationalRequest } from "@/types";
import { hexToRgb, rgbToHex } from "@/utilities";

import { TCreateModalProps } from "../../types";

const CreateOperationalModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "tạo",
}: TCreateModalProps<TCreateOperationalRequest>) => {
    const [data, setData] =
        useState<TCreateOperationalRequest>(initOperational);

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result) setData(initOperational);
    };

    const handleChangeColor = (e: ChangeEvent<HTMLInputElement>) => {
        const hexColor = e.target.value;

        const rgbColor = hexToRgb(hexColor);

        if (rgbColor) {
            setData((prev) => ({
                ...prev,
                r: rgbColor.r,
                g: rgbColor.g,
                b: rgbColor.b,
            }));
        }
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
                        type="color"
                        labelFor="color"
                        labelText="màu hiển thị"
                        value={rgbToHex(data.r, data.g, data.b)}
                        onChange={handleChangeColor}
                    />
                    <GroupTextArea
                        labelFor="name"
                        labelText="tên"
                        required={true}
                        value={data.name}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                name: e.target.value,
                            }))
                        }
                    />
                    <GroupInput
                        type="number"
                        labelFor="ngayTuTraKhach"
                        labelText="ngày tự trả khách"
                        value={data.ngayTuTraKhach}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                ngayTuTraKhach: parseInt(e.target.value),
                            }))
                        }
                    />
                </div>
            </Modal>
        </>
    );
};

export default CreateOperationalModal;
