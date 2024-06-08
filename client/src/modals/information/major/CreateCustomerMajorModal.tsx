import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { GroupSelect, Modal } from "@/components";
import { initCustomerMajor } from "@/constants";
import { TCreateCustomerMajorRequest, TMajorDto } from "@/types";
import { getAllMajors } from "@/api";

import { TCreateCustomerInfoModalProps } from "../../types";

const CreateCustomerMajorModal = ({
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
}: TCreateCustomerInfoModalProps<TCreateCustomerMajorRequest>) => {
    const [data, setData] = useState<TCreateCustomerMajorRequest>({
        ...initCustomerMajor,
        idCustomer: idCustomer,
    });

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
        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCustomerMajor,
                idCustomer: idCustomer as number,
            });
    };

    useEffect(() => {
        if (majorsRes && majorsRes.status) {
            setMajors(majorsRes.data as unknown as TMajorDto[]);
        }
    }, [majorsRes]);

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
                <GroupSelect
                    labelText="nghiệp vụ"
                    options={majors}
                    option={{
                        label: "nameVI",
                        value: "id",
                    }}
                    value={
                        data.idNghiepVu ? data.idNghiepVu.toString() : undefined
                    }
                    onValueChange={(val) =>
                        setData((prev) => ({
                            ...prev,
                            idNghiepVu: val ? parseInt(val) : undefined,
                        }))
                    }
                />
            </div>
        </Modal>
    );
};

export default CreateCustomerMajorModal;
