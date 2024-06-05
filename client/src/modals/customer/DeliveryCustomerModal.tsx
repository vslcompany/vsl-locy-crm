import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { getEmployeesGroup } from "@/api";
import { useAuth } from "@/contexts";
import { GroupInput, GroupSelect, Modal } from "@/components";
import {
    TAuthContextProps,
    TDeliveryCustomerRequest,
    TEmployeeDto,
} from "@/types";
import { notification } from "@/utilities";

import { TDeliveryCustomerModalProps } from "../types";

const DeliveryCustomerModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "giao",
    idCustomers,
}: TDeliveryCustomerModalProps) => {
    const [infoMessage, setInfoMessage] = useState<string>("");

    const [idEmployee, setIdEmployee] = useState<number | undefined>(undefined);
    const [employees, setEmployees] = useState<TEmployeeDto[] | []>([]);

    const { user }: TAuthContextProps = useAuth();

    const { data } = useQuery({
        queryKey: ["employees", user?.id],
        queryFn: getEmployeesGroup,
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            (user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7080")),
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (idEmployee === undefined) {
            notification(false, "Vui lòng chọn nhân viên");
            return;
        }

        const payload: TDeliveryCustomerRequest = {
            idNhanVienSale: idEmployee,
            idUserGiaoViec: user?.id as number,
            idCustomers,
            thongTinGiaoViec: infoMessage,
        };

        const result = await onSubmit(payload);

        if (result) setIdEmployee(undefined);
    };

    useEffect(() => {
        if (data && data.status) {
            setEmployees(data.data as unknown as TEmployeeDto[]);
        }
    }, [data]);

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
                labelButtonOkLoading="đang giao"
                isButtonOkLoading={isLoading}
            >
                <div className="grid gap-4">
                    <GroupSelect
                        labelText="nhân viên"
                        options={employees}
                        option={{
                            label: "nameVI",
                            value: "id",
                        }}
                        value={idEmployee ? idEmployee.toString() : undefined}
                        onValueChange={(val) =>
                            setIdEmployee(val ? parseInt(val) : undefined)
                        }
                    />
                    <GroupInput
                        labelFor="thongTinGiaoViec"
                        labelText="Thông tin giao việc"
                        value={infoMessage}
                        onChange={(e) => setInfoMessage(e.target.value)}
                    />
                </div>
            </Modal>
        </>
    );
};

export default DeliveryCustomerModal;
