import { useState } from "react";

import { GroupInput, GroupSelect, GroupTextArea, Modal } from "@/components";
import { initCity } from "@/constants";
import { useCategory } from "@/hooks";
import { TCreateCityRequest } from "@/types";

import { TCreateModalProps } from "../../types";

const CreateCityModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "tạo",
}: TCreateModalProps<TCreateCityRequest>) => {
    const [data, setData] = useState<TCreateCityRequest>(initCity);

    const { countries } = useCategory();

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result) setData(initCity);
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
                    <GroupSelect
                        labelText="quốc gia"
                        options={
                            countries && countries.length > 0 ? countries : []
                        }
                        option={{
                            label: "nameVI",
                            value: "id",
                        }}
                        value={
                            data.idQuocGia
                                ? data.idQuocGia.toString()
                                : undefined
                        }
                        onValueChange={(val) =>
                            setData((prev) => ({
                                ...prev,
                                idQuocGia: val ? parseInt(val) : -1,
                            }))
                        }
                    />
                </div>
            </Modal>
        </>
    );
};

export default CreateCityModal;
