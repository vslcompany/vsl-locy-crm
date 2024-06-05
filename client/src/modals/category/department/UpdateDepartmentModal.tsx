import { useEffect, useState } from "react";
import { isEqual } from "lodash";

import { GroupSelect, GroupTextArea, Modal } from "@/components";
import { useCategory } from "@/hooks";
import { TUpdateDepartmentRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateModalProps } from "../../types";

const UpdateDepartmentModal = ({
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
}: TUpdateModalProps<TUpdateDepartmentRequest>) => {
    const [data, setData] = useState<TUpdateDepartmentRequest | null>(null);

    const { offices } = useCategory();

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
                    <div className="grid gap-4">
                        <GroupTextArea
                            labelFor="nameVI"
                            labelText="tên (VI)"
                            required={true}
                            value={data.nameVI}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev
                                        ? {
                                              ...prev,
                                              nameVI: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupTextArea
                            labelFor="nameEN"
                            labelText="tên (EN)"
                            value={data.nameEN}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev
                                        ? {
                                              ...prev,
                                              nameEN: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupSelect
                            labelText="văn phòng"
                            options={
                                offices && offices.length > 0 ? offices : []
                            }
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idVanPhong
                                    ? data.idVanPhong.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) =>
                                    prev
                                        ? {
                                              ...prev,
                                              idVanPhong: val
                                                  ? parseInt(val)
                                                  : -1,
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

export default UpdateDepartmentModal;
