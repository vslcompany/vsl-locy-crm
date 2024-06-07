import { useEffect, useState } from "react";
import { isEqual } from "lodash";

import { Modal, SwitchToggle } from "@/components";
import { permissionAccount } from "@/constants";
import { TUpdateEmployeeRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateModalProps } from "../types";

const ChangePermissionAccountOfEmployeeModal = ({
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

    const [isPermissionCheckAll, setIsPermissionCheckAll] =
        useState<boolean>(false);

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (isEqual(data, prevData)) {
            notification(false, "Thông tin quyền không thay đổi");
            return;
        }

        if (data) {
            const payload: TUpdateEmployeeRequest = {
                id: data.id,
                username: data.username,
                active: data.active,
                password: "",
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

    const handlePermissionChange = (checked: boolean, value: string) => {
        let permissionsArr = data?.permission?.split(";") || [];
        permissionsArr?.shift();

        if (checked) {
            permissionsArr.push(value);
        }

        if (!checked) {
            permissionsArr = permissionsArr.filter((e) => e !== value);
        }

        const permissionStr =
            permissionsArr.length > 0 ? `;${permissionsArr.join(";")}` : "";

        setData((prev) => {
            if (prev) return { ...prev, permission: permissionStr };
            return null;
        });
    };

    const handlePermissionCheckAll = (checked: boolean) => {
        if (checked) {
            const rs = permissionAccount.map((e) => e.value);
            const permissionStr = `;${rs.join(";")}`;
            setData((prev) => {
                if (prev) return { ...prev, permission: permissionStr };
                return null;
            });
        } else {
            setData((prev) => {
                if (prev) return { ...prev, permission: "" };
                return null;
            });
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
                labelButtonOkLoading="đang phân quyền"
                isButtonOkLoading={isLoading}
            >
                {data !== null && (
                    <div className="space-y-2">
                        <div className="flex justify-end">
                            <div className="flex items-center">
                                <input
                                    checked={isPermissionCheckAll}
                                    readOnly
                                    id="default-checkbox"
                                    type="checkbox"
                                    value=""
                                    className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                                    onClick={() => {
                                        handlePermissionCheckAll(
                                            !isPermissionCheckAll
                                        );
                                        setIsPermissionCheckAll(
                                            (prev) => !prev
                                        );
                                    }}
                                />
                                <label
                                    htmlFor="default-checkbox"
                                    className="ms-2 text-sm font-medium cursor-pointer"
                                    onClick={() => {
                                        handlePermissionCheckAll(
                                            !isPermissionCheckAll
                                        );
                                        setIsPermissionCheckAll(
                                            (prev) => !prev
                                        );
                                    }}
                                >
                                    chọn tất cả
                                </label>
                            </div>
                        </div>
                        <div className="grid grid-cols-2 lg:grid-cols-3 gap-2">
                            {permissionAccount.map((item) => (
                                <div
                                    key={item.label}
                                    className="flex items-center gap-2 col-span-2 md:col-span-1 leading-none cursor-pointer"
                                    onClick={() =>
                                        handlePermissionChange(
                                            !data.permission.includes(
                                                item.value
                                            ),
                                            item.value
                                        )
                                    }
                                >
                                    <SwitchToggle
                                        checked={data.permission.includes(
                                            item.value
                                        )}
                                        isSetEnabled={false}
                                        size="sm"
                                    />
                                    <p className="text-sm first-letter:uppercase">
                                        {item.label}
                                    </p>
                                </div>
                            ))}
                        </div>
                    </div>
                )}
            </Modal>
        </>
    );
};

export default ChangePermissionAccountOfEmployeeModal;
