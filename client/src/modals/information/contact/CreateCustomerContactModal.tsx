import { useState } from "react";

import {
    GroupInput,
    GroupSelect,
    GroupTextArea,
    Modal,
    SwitchToggle,
} from "@/components";
import { gender, initCustomerContact } from "@/constants";
import { TCreateCustomerContactRequest } from "@/types";

import { TCreateCustomerInfoModalProps } from "../../types";

const CreateCustomerContactModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "tạo",
    idCustomer,
}: TCreateCustomerInfoModalProps<TCreateCustomerContactRequest>) => {
    const [data, setData] = useState<TCreateCustomerContactRequest>({
        ...initCustomerContact,
        idCustomer: idCustomer,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCustomerContact,
                idCustomer: idCustomer as number,
            });
    };

    return (
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
                <div className="grid gap-4">
                    <div className="grid md:grid-cols-5 gap-4">
                        <div className="col-span-2">
                            <GroupInput
                                labelFor="nameVI"
                                labelText="họ và tên (VI)"
                                required={true}
                                value={data.nameVI}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        nameVI: e.target.value,
                                    }))
                                }
                            />
                        </div>
                        <div className="col-span-2">
                            <GroupInput
                                labelFor="nameEN"
                                labelText="họ và tên (EN)"
                                value={data.nameEN}
                                onChange={(e) =>
                                    setData((prev) => ({
                                        ...prev,
                                        nameEN: e.target.value,
                                    }))
                                }
                            />
                        </div>
                        <div
                            className="flex items-center gap-2 leading-none cursor-pointer"
                            onClick={() =>
                                setData((prev) => ({
                                    ...prev,
                                    flagDaiDien: !prev?.flagDaiDien,
                                }))
                            }
                        >
                            <SwitchToggle
                                checked={data.flagDaiDien}
                                isSetEnabled={false}
                                size="sm"
                            />
                            <p className="text-sm first-letter:uppercase">
                                người đại diện
                            </p>
                        </div>
                    </div>
                    <div className="grid gap-4 md:grid-cols-2">
                        <GroupTextArea
                            labelFor="addressVI"
                            labelText="địa chỉ (VI)"
                            value={data.addressVI}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    addressVI: e.target.value,
                                }))
                            }
                        />
                        <GroupTextArea
                            labelFor="addressEN"
                            labelText="địa chỉ (EN)"
                            value={data.addressEN}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    addressEN: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <div className="grid gap-4 md:grid-cols-3">
                        <GroupSelect
                            labelText="giới tính"
                            options={gender}
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.enumGioiTinh
                                    ? data.enumGioiTinh.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) => ({
                                    ...prev,
                                    enumGioiTinh: val
                                        ? parseInt(val)
                                        : undefined,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="handPhone"
                            labelText="số điện thoại"
                            value={data.handPhone}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    handPhone: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="chucVu"
                            labelText="chức vụ"
                            value={data.chucVu}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    chucVu: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <div className="grid gap-4 md:grid-cols-3">
                        <GroupInput
                            labelFor="homePhone"
                            labelText="số điện thoại bàn"
                            value={data.homePhone}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    homePhone: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="email"
                            labelText="thư điện tử"
                            value={data.email}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    email: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="chat"
                            labelText="chat"
                            value={data.chat}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    chat: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <div className="grid gap-4 md:grid-cols-3">
                        <GroupInput
                            labelFor="bankAccountNumber"
                            labelText="số tài khoản ngân hàng"
                            value={data.bankAccountNumber}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    bankAccountNumber: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="bankBranchName"
                            labelText="tên ngân hàng"
                            value={data.bankBranchName}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    bankBranchName: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="bankAddress"
                            labelText="địa chỉ ngân hàng"
                            value={data.bankAddress}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    bankAddress: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <GroupInput
                        labelFor="note"
                        labelText="ghi chú"
                        value={data.note}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                note: e.target.value,
                            }))
                        }
                    />
                </div>
            </div>
        </Modal>
    );
};

export default CreateCustomerContactModal;
