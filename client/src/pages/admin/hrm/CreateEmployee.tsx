import { FormEvent, useState } from "react";
import { useMutation } from "react-query";

import { createEmployee } from "@/api";
import { Button, GroupInput, GroupSelect } from "@/components";
import { gender, initEmployee } from "@/constants";
import { useCategory } from "@/hooks";
import { TCreateEmployeeRequest } from "@/types";

const CreateEmployee = () => {
    const [data, setData] = useState<TCreateEmployeeRequest>(initEmployee);

    const { positions, departments, offices } = useCategory();

    const createMutation = useMutation({
        mutationFn: createEmployee,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const result: any = await createMutation.mutateAsync(data);
        if (result.status) {
            setData(initEmployee);
        }
    };

    return (
        <>
            <h2 className="title">tạo mới nhân viên</h2>
            <form className="space-y-6" onSubmit={handleSubmit}>
                <div className="space-y-4">
                    <h4 className="!text-gray-900" role="title">
                        thông tin nhân viên
                    </h4>
                    <div className="grid gap-4 md:grid-cols-3">
                        <GroupInput
                            labelFor="manhanvien"
                            labelText="mã nhân viên"
                            required={true}
                            value={data.manhanvien}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    manhanvien: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="hoTenVI"
                            labelText="họ và tên (VI)"
                            required={true}
                            value={data.hoTenVI}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    hoTenVI: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="hoTenEN"
                            labelText="họ và tên (EN)"
                            value={data.hoTenEN}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    hoTenEN: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            type="date"
                            labelFor="namsinh"
                            labelText="năm sinh"
                            required={true}
                            value={data.namsinh}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    namsinh: e.target.value,
                                }))
                            }
                        />
                        <GroupSelect
                            labelText="giới tính"
                            options={gender}
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.gioitinh
                                    ? data.gioitinh.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) => ({
                                    ...prev,
                                    gioitinh: val ? parseInt(val) : -1,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="quequan"
                            labelText="quê quán"
                            value={data.quequan}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    quequan: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="diachi"
                            labelText="địa chỉ"
                            value={data.diachi}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    diachi: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="soCMT"
                            labelText="số chứng minh thư"
                            value={data.soCMT}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    soCMT: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="noiCapCMT"
                            labelText="nơi cấp chứng minh thư"
                            value={data.noiCapCMT}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    noiCapCMT: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            type="date"
                            labelFor="ngayCapCMT"
                            labelText="ngày cấp chứng minh thư"
                            value={data.ngayCapCMT}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    ngayCapCMT: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="ghichu"
                            labelText="ghi chú"
                            value={data.ghichu}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    ghichu: e.target.value,
                                }))
                            }
                        />
                    </div>
                </div>
                <div className="space-y-2">
                    <h4 className="!text-gray-900" role="title">
                        thông tin việc làm
                    </h4>
                    <div className="grid gap-4 md:grid-cols-4">
                        <GroupSelect
                            labelText="chức vụ"
                            options={
                                positions && positions.length > 0
                                    ? positions
                                    : []
                            }
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idChucVu
                                    ? data.idChucVu.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) => ({
                                    ...prev,
                                    idChucVu: val ? parseInt(val) : undefined,
                                }))
                            }
                        />
                        <GroupSelect
                            labelText="phòng ban"
                            options={
                                departments && departments.length > 0
                                    ? departments
                                    : []
                            }
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idPhongBan
                                    ? data.idPhongBan.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) => ({
                                    ...prev,
                                    idPhongBan: val ? parseInt(val) : undefined,
                                }))
                            }
                        />
                        <GroupSelect
                            labelText="văn phòng"
                            options={
                                offices && offices.length > 0 ? offices : []
                            }
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idVanPhong
                                    ? data.idVanPhong.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) => ({
                                    ...prev,
                                    idVanPhong: val ? parseInt(val) : undefined,
                                }))
                            }
                        />
                        <GroupInput
                            type="number"
                            labelFor="soLuongKH"
                            labelText="số lượng khách"
                            value={data.soLuongKH}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    soLuongKH: parseInt(e.target.value),
                                }))
                            }
                        />
                    </div>
                </div>
                <div className="flex items-center gap-4">
                    <Button
                        label="tạo"
                        isLoading={createMutation.isLoading}
                        textIsLoading="đang tạo"
                    />
                </div>
            </form>
        </>
    );
};

export default CreateEmployee;
