import { useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getAllTypeOfCustomers } from "@/api";
import { GroupSelect, Modal } from "@/components";
import { TTypeOfCustomerDto, TUpdateCustomerClassifyRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateCustomerInfoModalProps } from "../../types";

const UpdateCustomerClassifyModal = ({
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
}: TUpdateCustomerInfoModalProps<TUpdateCustomerClassifyRequest>) => {
    const [data, setData] = useState<TUpdateCustomerClassifyRequest | null>(
        null
    );

    const [types, setTypes] = useState<TTypeOfCustomerDto[] | []>([]);

    const { data: typesRes } = useQuery({
        queryKey: "types",
        queryFn: getAllTypeOfCustomers,
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
            notification(
                false,
                "Thông tin phân loại khách hàng không thay đổi"
            );
            return;
        }

        if (data) {
            await onSubmit(data);
        }
    };

    useEffect(() => {
        if (typesRes && typesRes.status) {
            setTypes(typesRes.data as unknown as TTypeOfCustomerDto[]);
        }
    }, [typesRes]);

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
                        <GroupSelect
                            labelText="phân loại khách hàng"
                            options={types}
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idPhanLoaiKhachHang
                                    ? data.idPhanLoaiKhachHang.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) =>
                                    prev !== null
                                        ? {
                                              ...prev,
                                              idPhanLoaiKhachHang: val
                                                  ? parseInt(val)
                                                  : undefined,
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

export default UpdateCustomerClassifyModal;
