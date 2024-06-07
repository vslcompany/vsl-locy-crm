import { useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getAllCustomerTypes } from "@/api";
import { GroupInput, GroupSelect, Modal } from "@/components";
import { TCustomerTypeDto, TUpdateCustomerEvaluateRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateCustomerInfoModalProps } from "../../types";

const UpdateCustomerEvaluateModal = ({
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
}: TUpdateCustomerInfoModalProps<TUpdateCustomerEvaluateRequest>) => {
    const [data, setData] = useState<TUpdateCustomerEvaluateRequest | null>(
        null
    );

    const [types, setTypes] = useState<TCustomerTypeDto[] | []>([]);

    const { data: typesRes } = useQuery({
        queryKey: "customerTypes",
        queryFn: getAllCustomerTypes,
        onSuccess: (data) => {
            if (data.status) {
                setTypes(data.data as unknown as TCustomerTypeDto[]);
            }
        },
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (isEqual(data, prevData)) {
            notification(false, "Thông tin đánh giá không thay đổi");
            return;
        }

        if (data) {
            await onSubmit(data);
        }
    };

    useEffect(() => {
        if (typesRes && typesRes.status) {
            setTypes(typesRes.data as unknown as TCustomerTypeDto[]);
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
                            labelText="loại đánh giá"
                            options={types}
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idCustomerType
                                    ? data.idCustomerType.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) =>
                                    prev !== null
                                        ? {
                                              ...prev,
                                              idCustomerType: val
                                                  ? parseInt(val)
                                                  : undefined,
                                          }
                                        : null
                                )
                            }
                        />
                        <GroupInput
                            labelFor="ghiChu"
                            labelText="ghi chú"
                            value={data.ghiChu}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev !== null
                                        ? {
                                              ...prev,
                                              ghiChu: e.target.value,
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

export default UpdateCustomerEvaluateModal;
