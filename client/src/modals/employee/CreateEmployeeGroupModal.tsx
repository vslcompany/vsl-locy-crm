import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { getAllEmployeesNoGroup } from "@/api";
import { GroupInput, Modal } from "@/components";
import { initEmployeeGroup } from "@/constants";
import { TCreateEmployeeGroupRequest, TEmployeeDto } from "@/types";

import { TCreateEmployeeGroupProps } from "../types";

const CreateEmployeeGroupModal = ({
    isOpen,
    onClose,
    onSubmit,
    title,
    description,
    width,
    labelButtonCancel,
    labelButtonOk,
    isLoading,
    parentId,
}: TCreateEmployeeGroupProps<TCreateEmployeeGroupRequest>) => {
    const [data, setData] =
        useState<TCreateEmployeeGroupRequest>(initEmployeeGroup);
    const [employees, setEmployees] = useState<TEmployeeDto[] | []>([]);

    const { data: employeesRes, refetch } = useQuery({
        queryKey: "employeesGroup",
        queryFn: getAllEmployeesNoGroup,
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

        if (result) {
            setData(initEmployeeGroup);
            refetch();
        }
    };

    useEffect(() => {
        setData((prev) => ({
            ...prev,
            parentId: parentId ? parentId : 0,
        }));
    }, [parentId]);

    useEffect(() => {
        if (employeesRes && employeesRes.status) {
            setEmployees(employeesRes.data as unknown as TEmployeeDto[]);
        }
    }, [employeesRes]);

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
                labelButtonOkLoading="đang tạo"
                isButtonOkLoading={isLoading}
            >
                <div className="space-y-6">
                    {/* Tên nhóm */}
                    <GroupInput
                        labelFor="nameGroup"
                        labelText="tên nhóm"
                        value={data.nameGroup}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                nameGroup: e.target.value,
                            }))
                        }
                    />
                    {/* Chọn nhân viên */}
                    <div className="space-y-2">
                        <div className="first-letter:uppercase">
                            Chọn nhân viên
                        </div>
                        <div className="grid grid-cols-2 lg:grid-cols-3 gap-2">
                            {employees.length > 0 &&
                                employees.map((employee, index) => (
                                    <div
                                        key={index}
                                        className="flex items-center gap-2 leading-none cursor-pointer"
                                        onClick={() =>
                                            setData((prev) => {
                                                const filterData =
                                                    prev.idNhanVien.includes(
                                                        employee.id as never
                                                    )
                                                        ? prev.idNhanVien.filter(
                                                              (i) =>
                                                                  i !==
                                                                  employee.id
                                                          )
                                                        : [
                                                              ...prev.idNhanVien,
                                                              employee.id,
                                                          ];
                                                return {
                                                    ...prev,
                                                    idNhanVien: filterData,
                                                };
                                            })
                                        }
                                    >
                                        <input
                                            type="checkbox"
                                            value={employee.id}
                                            checked={data.idNhanVien.includes(
                                                employee.id as never
                                            )}
                                            readOnly
                                        />
                                        <p className="text-sm first-letter:uppercase">
                                            {employee.nameVI}
                                        </p>
                                    </div>
                                ))}
                        </div>
                    </div>
                </div>
            </Modal>
        </>
    );
};

export default CreateEmployeeGroupModal;
