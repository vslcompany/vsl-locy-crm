import { useEffect, useRef, useState } from "react";

import { GroupInput, Modal } from "@/components";
import { TUpdateEmployeeRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateModalProps } from "../types";

const ChangePasswordAccountOfEmployeeModal = ({
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

    const passwordRef = useRef<HTMLInputElement | null>(null);

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (passwordRef.current?.value === "") {
            notification(false, "Bạn cần nhập mật khẩu mới!");
            return;
        }

        if (data) {
            const payload: TUpdateEmployeeRequest = {
                id: data.id,
                username: data.username,
                password: passwordRef.current?.value ?? "",
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
                labelButtonOkLoading="đang đổi mật khẩu"
                isButtonOkLoading={isLoading}
            >
                {data !== null && (
                    <div className="space-y-2">
                        <GroupInput
                            type="password"
                            labelFor="password"
                            labelText="mật khẩu"
                            ref={passwordRef}
                        />
                    </div>
                )}
            </Modal>
        </>
    );
};

export default ChangePasswordAccountOfEmployeeModal;
