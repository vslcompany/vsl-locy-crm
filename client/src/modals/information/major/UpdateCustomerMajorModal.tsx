import { useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getAllMajors } from "@/api";
import { GroupSelect, Modal } from "@/components";
import { TMajorDto, TUpdateCustomerMajorRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateCustomerInfoModalProps } from "../../types";

const UpdateCustomerMajorModal = ({
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
}: TUpdateCustomerInfoModalProps<TUpdateCustomerMajorRequest>) => {
    const [data, setData] = useState<TUpdateCustomerMajorRequest | null>(null);
    const [majors, setMajors] = useState<TMajorDto[] | []>([]);

    const { data: majorsRes } = useQuery({
        queryKey: "majors",
        queryFn: getAllMajors,
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
            notification(false, "Thông tin nghiệp vụ không thay đổi");
            return;
        }

        if (data) {
            await onSubmit(data);
        }
    };

    useEffect(() => {
        if (majorsRes && majorsRes.status) {
            setMajors(majorsRes.data as unknown as TMajorDto[]);
        }
    }, [majorsRes]);

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
                            labelText="nghiệp vụ"
                            options={majors}
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idNghiepVu
                                    ? data.idNghiepVu.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) =>
                                    prev !== null
                                        ? {
                                              ...prev,
                                              idNghiepVu: val
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

export default UpdateCustomerMajorModal;
