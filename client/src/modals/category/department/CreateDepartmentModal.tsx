import { useState } from "react";

import { GroupSelect, GroupTextArea, Modal } from "@/components";
import { initDepartment } from "@/constants";
import { useCategory } from "@/hooks";
import { TCreateDepartmentRequest } from "@/types";

import { TCreateModalProps } from "../../types";

const CreateDepartmentModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "tạo",
}: TCreateModalProps<TCreateDepartmentRequest>) => {
    const [data, setData] = useState<TCreateDepartmentRequest>(initDepartment);

    const { offices } = useCategory();

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result) setData(initDepartment);
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
                    <GroupSelect
                        labelText="văn phòng"
                        options={offices && offices.length > 0 ? offices : []}
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
                            setData((prev) => ({
                                ...prev,
                                idVanPhong: val ? parseInt(val) : -1,
                            }))
                        }
                    />
                </div>
            </Modal>
        </>
    );
};

export default CreateDepartmentModal;
