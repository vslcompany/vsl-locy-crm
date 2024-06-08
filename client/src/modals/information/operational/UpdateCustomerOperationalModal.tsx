import { useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getAllCustomerContacts, getAllOperationals } from "@/api";
import { GroupInput, GroupSelect, GroupTextArea, Modal } from "@/components";
import {
    TUpdateCustomerOperationalRequest,
    TCustomerContactDto,
    TOperationalDto,
} from "@/types";
import { notification } from "@/utilities";

import { TUpdateCustomerInfoModalProps } from "../../types";

const UpdateCustomerOperationalModal = ({
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
}: TUpdateCustomerInfoModalProps<TUpdateCustomerOperationalRequest>) => {
    const [data, setData] = useState<TUpdateCustomerOperationalRequest | null>(
        null
    );

    const [contacts, setContacts] = useState<TCustomerContactDto[] | []>([]);
    const [operationals, setOperationals] = useState<TOperationalDto[] | []>(
        []
    );

    const { data: contactsRes } = useQuery({
        queryKey: ["contacts", data?.idCustomer],
        queryFn: () => getAllCustomerContacts(data?.idCustomer as number),
        onSuccess: (data) => {
            if (data.status) {
                setContacts(data.data as unknown as TCustomerContactDto[]);
            }
        },
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data !== null &&
            data.idCustomer !== undefined &&
            data.idCustomer !== null &&
            data.idCustomer !== -1,
    });

    const { data: operationalsRes } = useQuery({
        queryKey: "operationals",
        queryFn: getAllOperationals,
        onSuccess: (data) => {
            if (data.status) {
                setOperationals(data.data as unknown as TOperationalDto[]);
            }
        },
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (isEqual(data, prevData)) {
            notification(false, "Thông tin tác nghiệp không thay đổi");
            return;
        }

        if (data) {
            await onSubmit(data);
        }
    };

    useEffect(() => {
        if (contactsRes && contactsRes.status) {
            setContacts(contactsRes.data as unknown as TCustomerContactDto[]);
        }
    }, [contactsRes]);

    useEffect(() => {
        if (operationalsRes && operationalsRes.status) {
            setOperationals(
                operationalsRes.data as unknown as TOperationalDto[]
            );
        }
    }, [operationalsRes]);

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
                        <div className="grid md:grid-cols-3 gap-4">
                            <GroupSelect
                                labelText="loại tác nghiệp"
                                options={operationals.map((i) => ({
                                    id: i.id,
                                    nameVI: i.name,
                                }))}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    data.idLoaiTacNghiep
                                        ? data.idLoaiTacNghiep.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  idLoaiTacNghiep: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupSelect
                                labelText="người liên hệ"
                                options={contacts}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    data.idNguoiLienHe
                                        ? data.idNguoiLienHe.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  idNguoiLienHe: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                type="date"
                                labelFor="thoiGianThucHien"
                                labelText="thời gian tác nghiệp"
                                value={data.thoiGianThucHien}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  thoiGianThucHien:
                                                      e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                        </div>
                        <GroupTextArea
                            labelFor="noiDung"
                            labelText="nội dung"
                            value={data.noiDung}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              noiDung: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupTextArea
                            labelFor="khachHangPhanHoi"
                            labelText="khách hàng phản hồi"
                            value={data.khachHangPhanHoi}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev != null
                                        ? {
                                              ...prev,
                                              khachHangPhanHoi: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <div className="grid md:grid-cols-3 gap-4">
                            <GroupInput
                                type="date"
                                labelFor="ngayPhanHoi"
                                labelText="thời gian phản hồi"
                                value={data.ngayPhanHoi}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  ngayPhanHoi: e.target.value,
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

export default UpdateCustomerOperationalModal;
