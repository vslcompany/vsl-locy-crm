import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { getAllCustomerContacts, getAllOperationals } from "@/api";
import { GroupInput, GroupSelect, GroupTextArea, Modal } from "@/components";
import { initCustomerOperational } from "@/constants";
import { useAuth } from "@/contexts";
import {
    TCreateCustomerOperationalRequest,
    TCustomerContactDto,
    TOperationalDto,
    TAuthContextProps,
} from "@/types";

import { TCreateCustomerInfoModalProps } from "../../types";

const CreateCustomerOperationalModal = ({
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
}: TCreateCustomerInfoModalProps<TCreateCustomerOperationalRequest>) => {
    const { user }: TAuthContextProps = useAuth();
    const [data, setData] = useState<TCreateCustomerOperationalRequest>({
        ...initCustomerOperational,
        idCustomer: idCustomer,
        idUserCreate: user?.id as number,
    });

    const [contacts, setContacts] = useState<TCustomerContactDto[] | []>([]);
    const [operationals, setOperationals] = useState<TOperationalDto[] | []>(
        []
    );

    const { data: contactsRes } = useQuery({
        queryKey: ["contacts", idCustomer],
        queryFn: () => getAllCustomerContacts(idCustomer as number),
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            idCustomer !== null,
    });

    const { data: operationalsRes } = useQuery({
        queryKey: "operationals",
        queryFn: getAllOperationals,
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCustomerOperational,
                idCustomer: idCustomer as number,
                idUserCreate: user?.id as number,
            });
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
                            setData((prev) => ({
                                ...prev,
                                idLoaiTacNghiep: val
                                    ? parseInt(val)
                                    : undefined,
                            }))
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
                            setData((prev) => ({
                                ...prev,
                                idNguoiLienHe: val ? parseInt(val) : undefined,
                            }))
                        }
                    />
                    <GroupInput
                        type="date"
                        labelFor="thoiGianThucHien"
                        labelText="thời gian tác nghiệp"
                        value={data.thoiGianThucHien}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                thoiGianThucHien: e.target.value,
                            }))
                        }
                    />
                </div>
                <GroupTextArea
                    labelFor="noiDung"
                    labelText="nội dung"
                    value={data.noiDung}
                    onChange={(e) =>
                        setData((prev) => ({
                            ...prev,
                            noiDung: e.target.value,
                        }))
                    }
                />
                <GroupTextArea
                    labelFor="khachHangPhanHoi"
                    labelText="khách hàng phản hồi"
                    value={data.khachHangPhanHoi}
                    onChange={(e) =>
                        setData((prev) => ({
                            ...prev,
                            khachHangPhanHoi: e.target.value,
                        }))
                    }
                />
                <div className="grid md:grid-cols-3 gap-4">
                    <GroupInput
                        type="date"
                        labelFor="ngayPhanHoi"
                        labelText="thời gian phản hồi"
                        value={data.ngayPhanHoi}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                ngayPhanHoi: e.target.value,
                            }))
                        }
                    />
                </div>
            </div>
        </Modal>
    );
};

export default CreateCustomerOperationalModal;
