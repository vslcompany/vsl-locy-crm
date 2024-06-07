import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { getAllTypeOfCustomers } from "@/api";
import { GroupSelect, Modal } from "@/components";
import { initCustomerClassify } from "@/constants";
import { TCreateCustomerClassifyRequest, TTypeOfCustomerDto } from "@/types";

import { TCreateCustomerInfoModalProps } from "../../types";

const CreateCustomerClassifyModal = ({
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
}: TCreateCustomerInfoModalProps<TCreateCustomerClassifyRequest>) => {
    const [data, setData] = useState<TCreateCustomerClassifyRequest>({
        ...initCustomerClassify,
        idCustomer: idCustomer,
    });

    const [types, setTypes] = useState<TTypeOfCustomerDto[] | []>([]);

    const { data: typesRes } = useQuery({
        queryKey: "types",
        queryFn: getAllTypeOfCustomers,
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
        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCustomerClassify,
                idCustomer: idCustomer as number,
            });
    };

    useEffect(() => {
        if (typesRes && typesRes.status) {
            setTypes(typesRes.data as unknown as TTypeOfCustomerDto[]);
        }
    }, [typesRes]);

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
                        setData((prev) => ({
                            ...prev,
                            idPhanLoaiKhachHang: val
                                ? parseInt(val)
                                : undefined,
                        }))
                    }
                />
            </div>
        </Modal>
    );
};

export default CreateCustomerClassifyModal;
