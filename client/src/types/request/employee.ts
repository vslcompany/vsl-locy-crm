export type TCreateEmployeeRequest = {
    // Thông tin tài khoản
    username: string;
    password: string;
    permission: string;
    // Thông tin cá nhân
    idChucVu: number | undefined;
    idPhongBan: number | undefined;
    idVanPhong: number | undefined;
    manhanvien: string;
    hoTenVI: string;
    hoTenEN: string;
    namsinh: string;
    gioitinh: number | undefined;
    quequan: string;
    diachi: string;
    soCMT: string;
    noiCapCMT: string;
    ngayCapCMT: string;
    photoURL: string;
    ghichu: string;
    soLuongKH: number | undefined;
};

export type TUpdateEmployeeRequest = {
    // Thông tin tài khoản
    id: number;
    username: string;
    password: string;
    active: boolean;
    permission: string;
    // Thông tin cá nhân
    idNhanVien: number;
    idChucVu: number | undefined;
    idPhongBan: number | undefined;
    idVanPhong: number | undefined;
    manhanvien: string;
    hoTenVI: string;
    hoTenEN: string;
    namsinh: string;
    gioitinh: number | undefined;
    quequan: string;
    diachi: string;
    soCMT: string;
    noiCapCMT: string;
    ngayCapCMT: string;
    photoURL: string;
    ghichu: string;
    soLuongKH: number;
};

export type TDeleteEmployeeRequest = {
    idNhanVien: number;
    flagDelete: boolean;
    idUserDelete: number | null;
};

export type TCreateEmployeeGroupRequest = {
    parentId: number | undefined;
    nameGroup: string;
    idNhanVien: number[] | [];
    flagViewAllGroup: boolean;
};
