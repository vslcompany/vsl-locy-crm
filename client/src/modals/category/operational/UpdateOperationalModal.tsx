import { ChangeEvent, useEffect, useState } from "react";
import { isEqual } from "lodash";

import { GroupInput, GroupTextArea, Modal } from "@/components";
import { TUpdateOperationalRequest } from "@/types";
import { hexToRgb, notification, rgbToHex } from "@/utilities";

import { TUpdateModalProps } from "../../types";

const UpdateOperationalModal = ({
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
}: TUpdateModalProps<TUpdateOperationalRequest>) => {
    const [data, setData] = useState<TUpdateOperationalRequest | null>(null);

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

    const handleChangeColor = (e: ChangeEvent<HTMLInputElement>) => {
        const hexColor = e.target.value;

        const rgbColor = hexToRgb(hexColor);

        if (rgbColor) {
            setData((prev) =>
                prev !== null
                    ? {
                          ...prev,
                          r: rgbColor.r,
                          g: rgbColor.g,
                          b: rgbColor.b,
                      }
                    : null
            );
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
                                setData((prev) =>
                                    prev !== null
                                        ? {
                                              ...prev,
                                              name: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            type="number"
                            labelFor="ngayTuTraKhach"
                            labelText="ngày tự trả khách"
                            value={data.ngayTuTraKhach}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev !== null
                                        ? {
                                              ...prev,
                                              ngayTuTraKhach: parseInt(
                                                  e.target.value
                                              ),
                                          }
                                        : null
                                )
                            }
                        />
                    </div>
                )}
            </Modal>
        </>
    );
};

export default UpdateOperationalModal;
