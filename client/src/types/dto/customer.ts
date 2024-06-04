export type TCustomerDto = {
    id: number;
    idQuocGia: number;
    quocGia: string;
    idCity: number;
    thanhPho: string;
    code: string;
    nameVI: string;
    nameEN: string;
    addressVI: string;
    addressEN: string;
    taxCode: string;
    phone: string;
    fax: string;
    email: string;
    website: string;
    note: string;
    idNhanVienSale: number;
    nhanVien: string;
    idUserCreate: number;
    nguoiTao: string;
    dateCreate: string;
    flagActive: boolean;
    enumLoaiKhachHang: number;
    flagDel: boolean;
    idLoaiDoanhNghiep: number;
    loaiDoanhNghiep: string;
    idUserDelete: number;
    nguoiXoa: string;
    dateDelete: string;
    lyDoXoa: string;
    ngayTuongTac: string;
    ngayChonKhach: string;
    ngayTraVe: string;
    ngayChotKhach: string;
    sttmaxTacNghiep: number;
    ngayTacNghiep: string;
    enumGiaoNhan: number;
    ngayGiao: string;
    ngayNhan: string;
    idUserGiaoViec: number;
    nguoiGiaoViec: string;
    idUserTraKhach: number;
    nguoiTraKhach: string;
    ngayKetThucNhan: string;
    listTacNghiepText: string;
    listTuyenHangText: string;
    listPhanHoiText: string;
    ngayTuTraKhach: string;
    thongTinGiaoViec: string;
    lyDoTuChoi: string;
    idTacNghiepCuoi: number;
    mauTacNghiepCuoi: string;
};

export type TCustomerImExDto = {
    id: number;
    date: string;
    type: string;
    vessel: string;
    term: string;
    code: string;
    commd: string;
    vol: string;
    unt: string;
    createDate: string;
    idUserCreate: number;
    nguoiTao: string;
    idFromPort: number;
    cangDi: string;
    idToPort: number;
    cangDen: string;
    idFromCountry: number;
    quocGiaDi: string;
    idToCountry: number;
    quocGiaDen: string;
    idCustomer: number;
};

export type TCustomerOperationalDto = {
    id: number;
    idLoaiTacNghiep: number;
    loaiTacNghiep: string;
    noiDung: string;
    dateCreate: string;
    idUserCreate: number;
    nguoiTao: string;
    idCustomer: number;
    thoiGianThucHien: string;
    idNguoiLienHe: number;
    nguoiLienHe: number;
    khachHangPhanHoi: string;
    ngayPhanHoi: string;
};

export type TCustomerContactDto = {
    id: number;
    nameVI: string;
    nameEN: string;
    addressVI: string;
    addressEN: string;
    enumGioiTinh: number;
    handPhone: string;
    homePhone: string;
    email: string;
    note: string;
    idCustomer: number;
    flagFavorite: boolean;
    bankAccountNumber: string;
    bankBranchName: string;
    bankAddress: string;
    chat: string;
    flagActive: boolean;
    flagDaiDien: boolean;
    chucVu: string;
};

export type TCustomerEvaluateDto = {
    id: number;
    idCustomer: number;
    idCustomerType: number;
    loaiKhachHang: string;
    idUserCreate: number;
    nguoiTao: string;
    dateCreate: string;
    ghiChu: string;
};

export type TCustomerClassifyDto = {
    id: number;
    idCustomer: number;
    idPhanLoaiKhachHang: number;
    phanLoaiKhachHang: string;
};

export type TCustomerMajorDto = {
    id: number;
    idCustomer: number;
    idNghiepVu: number;
    nghiepVu: string;
};

export type TCustomerRouteDto = {
    id: number;
    idQuocGiaDi: number;
    quocGiaDi: string;
    idQuocGiaDen: number;
    quocGiaDen: string;
    idCangDi: number;
    cangDi: string;
    idCangDen: number;
    cangDen: string;
    idCustomer: number;
    idLoaiHinhVanChuyen: number;
    loaiHinhVanChuyen: string;
};
