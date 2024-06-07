import { useEffect, useState } from "react";
import { isEqual } from "lodash";

import {
    GroupInput,
    GroupSelect,
    GroupTextArea,
    Modal,
    SwitchToggle,
} from "@/components";
import { gender } from "@/constants";
import { TUpdateCustomerContactRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateCustomerInfoModalProps } from "../../types";

const UpdateCustomerContactModal = ({
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
}: TUpdateCustomerInfoModalProps<TUpdateCustomerContactRequest>) => {
    const [data, setData] = useState<TUpdateCustomerContactRequest | null>(
        null
    );

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (isEqual(data, prevData)) {
            notification(false, "Thông tin người liên hệ không thay đổi");
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
                        <div className="grid gap-4">
                            <div className="grid md:grid-cols-5 gap-4">
                                <div className="col-span-2">
                                    <GroupInput
                                        labelFor="nameVI"
                                        labelText="họ và tên (VI)"
                                        required={true}
                                        value={data.nameVI}
                                        onChange={(e) =>
                                            setData((prev) =>
                                                prev !== null
                                                    ? {
                                                          ...prev,
                                                          nameVI: e.target
                                                              .value,
                                                      }
                                                    : null
                                            )
                                        }
                                    />
                                </div>
                                <div className="col-span-2">
                                    <GroupInput
                                        labelFor="nameEN"
                                        labelText="họ và tên (EN)"
                                        value={data.nameEN}
                                        onChange={(e) =>
                                            setData((prev) =>
                                                prev !== null
                                                    ? {
                                                          ...prev,
                                                          nameEN: e.target
                                                              .value,
                                                      }
                                                    : null
                                            )
                                        }
                                    />
                                </div>
                                <div
                                    className="flex items-center gap-2 leading-none cursor-pointer"
                                    onClick={() =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      flagDaiDien:
                                                          !prev?.flagDaiDien,
                                                  }
                                                : null
                                        )
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
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      addressVI: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupTextArea
                                    labelFor="addressEN"
                                    labelText="địa chỉ (EN)"
                                    value={data.addressEN}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      addressEN: e.target.value,
                                                  }
                                                : null
                                        )
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
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      enumGioiTinh: val
                                                          ? parseInt(val)
                                                          : undefined,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="handPhone"
                                    labelText="số điện thoại"
                                    value={data.handPhone}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      handPhone: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="chucVu"
                                    labelText="chức vụ"
                                    value={data.chucVu}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      chucVu: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                            </div>
                            <div className="grid gap-4 md:grid-cols-3">
                                <GroupInput
                                    labelFor="homePhone"
                                    labelText="số điện thoại bàn"
                                    value={data.homePhone}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      homePhone: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="email"
                                    labelText="thư điện tử"
                                    value={data.email}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      email: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="chat"
                                    labelText="chat"
                                    value={data.chat}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      chat: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                            </div>
                            <div className="grid gap-4 md:grid-cols-3">
                                <GroupInput
                                    labelFor="bankAccountNumber"
                                    labelText="số tài khoản ngân hàng"
                                    value={data.bankAccountNumber}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      bankAccountNumber:
                                                          e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="bankBranchName"
                                    labelText="tên ngân hàng"
                                    value={data.bankBranchName}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      bankBranchName:
                                                          e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="bankAddress"
                                    labelText="địa chỉ ngân hàng"
                                    value={data.bankAddress}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      bankAddress:
                                                          e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                            </div>
                            <GroupInput
                                labelFor="note"
                                labelText="ghi chú"
                                value={data.note}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  note: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                        </div>
                    </div>
                )}
            </Modal>
        </>
    );
};

export default UpdateCustomerContactModal;
