import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { getAllCustomerTypes } from "@/api";
import { GroupInput, GroupSelect, Modal } from "@/components";
import { initCustomerEvaluate } from "@/constants";
import { useAuth } from "@/contexts";
import {
    TAuthContextProps,
    TCreateCustomerEvaluateRequest,
    TCustomerTypeDto,
} from "@/types";

import { TCreateCustomerInfoModalProps } from "../../types";

const CreateCustomerEvaluateModal = ({
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
}: TCreateCustomerInfoModalProps<TCreateCustomerEvaluateRequest>) => {
    const { user }: TAuthContextProps = useAuth();

    const [data, setData] = useState<TCreateCustomerEvaluateRequest>({
        ...initCustomerEvaluate,
        idCustomer: idCustomer as number,
        idUserCreate: user?.id as number,
    });

    const [types, setTypes] = useState<TCustomerTypeDto[] | []>([]);

    const { data: typesRes } = useQuery({
        queryKey: "customerTypes",
        queryFn: getAllCustomerTypes,
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
                ...initCustomerEvaluate,
                idCustomer: idCustomer as number,
                idUserCreate: user?.id as number,
            });
    };

    useEffect(() => {
        if (typesRes && typesRes.status) {
            setTypes(typesRes.data as unknown as TCustomerTypeDto[]);
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
                        setData((prev) => ({
                            ...prev,
                            idCustomerType: val ? parseInt(val) : undefined,
                        }))
                    }
                />
                <GroupInput
                    labelFor="ghiChu"
                    labelText="ghi chú"
                    value={data.ghiChu}
                    onChange={(e) =>
                        setData((prev) => ({
                            ...prev,
                            ghiChu: e.target.value,
                        }))
                    }
                />
            </div>
        </Modal>
    );
};

export default CreateCustomerEvaluateModal;
