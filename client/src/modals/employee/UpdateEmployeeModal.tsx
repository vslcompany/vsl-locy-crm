import { useEffect, useState } from "react";
import { isEqual } from "lodash";

import { GroupInput, GroupSelect, Modal } from "@/components";
import { gender } from "@/constants";
import { useCategory } from "@/hooks";
import { TUpdateEmployeeRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateModalProps } from "../types";

const UpdateEmployeeModal = ({
    isOpen,
    onClose,
    onSubmit,
    title,
    description,
    width,
    labelButtonCancel,
    labelButtonOk,
    prevData,
    isLoading,
}: TUpdateModalProps<TUpdateEmployeeRequest>) => {
    const [data, setData] = useState<TUpdateEmployeeRequest | null>(null);

    const { positions, departments, offices } = useCategory();

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (isEqual(data, prevData)) {
            notification(false, "Thông tin nhân viên không thay đổi");
            return;
        }

        if (data) {
            const payload: TUpdateEmployeeRequest = {
                id: data.id,
                username: data.username,
                password: "",
                active: true,
                permission: data.permission,
                idNhanVien: data.idNhanVien,
                idChucVu: data.idChucVu,
                idPhongBan: data.idPhongBan,
                idVanPhong: data.idVanPhong,
                manhanvien: data.manhanvien,
                hoTenVI: data.hoTenVI,
                hoTenEN: data.hoTenEN,
                namsinh: data.namsinh,
                gioitinh: data.gioitinh,
                quequan: data.quequan,
                diachi: data.diachi,
                soCMT: data.soCMT,
                noiCapCMT: data.noiCapCMT,
                ngayCapCMT: data.ngayCapCMT,
                photoURL: data.photoURL,
                ghichu: data.ghichu,
                soLuongKH: data.soLuongKH,
            };

            await onSubmit(payload);
        }
    };

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
                    <div className="space-y-6">
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
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      manhanvien:
                                                          e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="hoTenVI"
                                    labelText="họ và tên (VI)"
                                    required={true}
                                    value={data.hoTenVI}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      hoTenVI: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="hoTenEN"
                                    labelText="họ và tên (EN)"
                                    value={data.hoTenEN}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      hoTenEN: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    type="date"
                                    labelFor="namsinh"
                                    labelText="năm sinh"
                                    required={true}
                                    value={data.namsinh}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      namsinh: e.target.value,
                                                  }
                                                : null
                                        )
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
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      gioitinh: val
                                                          ? parseInt(val)
                                                          : -1,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="quequan"
                                    labelText="quê quán"
                                    value={data.quequan}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      quequan: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="diachi"
                                    labelText="địa chỉ"
                                    value={data.diachi}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      diachi: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="soCMT"
                                    labelText="số chứng minh thư"
                                    value={data.soCMT}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      soCMT: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="noiCapCMT"
                                    labelText="nơi cấp chứng minh thư"
                                    value={data.noiCapCMT}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      noiCapCMT: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    type="date"
                                    labelFor="ngayCapCMT"
                                    labelText="ngày cấp chứng minh thư"
                                    value={data.ngayCapCMT}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      ngayCapCMT:
                                                          e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="ghichu"
                                    labelText="ghi chú"
                                    value={data.ghichu}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      ghichu: e.target.value,
                                                  }
                                                : null
                                        )
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
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      idChucVu: val
                                                          ? parseInt(val)
                                                          : -1,
                                                  }
                                                : null
                                        )
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
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      idPhongBan: val
                                                          ? parseInt(val)
                                                          : -1,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupSelect
                                    labelText="văn phòng"
                                    options={
                                        offices && offices.length > 0
                                            ? offices
                                            : []
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
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      idVanPhong: val
                                                          ? parseInt(val)
                                                          : -1,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    type="number"
                                    labelFor="soLuongKH"
                                    labelText="số lượng khách"
                                    value={data.soLuongKH}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      soLuongKH: parseInt(
                                                          e.target.value
                                                      ),
                                                  }
                                                : null
                                        )
                                    }
                                />
                            </div>
                        </div>
                    </div>
                )}
            </Modal>
        </>
    );
};

export default UpdateEmployeeModal;
