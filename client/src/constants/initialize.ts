import {
    TCreateBusinessRequest,
    TCreateCityRequest,
    TCreateCountryRequest,
    TCreateCustomerClassifyRequest,
    TCreateCustomerContactRequest,
    TCreateCustomerEvaluateRequest,
    TCreateCustomerImExRequest,
    TCreateCustomerMajorRequest,
    TCreateCustomerOperationalRequest,
    TCreateCustomerRequest,
    TCreateCustomerRouteRequest,
    TCreateCustomerTypeRequest,
    TCreateDepartmentRequest,
    TCreateEmployeeGroupRequest,
    TCreateEmployeeRequest,
    TCreateMajorRequest,
    TCreateOfficeRequest,
    TCreateOperationalRequest,
    TCreatePortRequest,
    TCreatePositionRequest,
    TCreateTransportationRequest,
    TCreateTypeOfCustomerRequest,
} from "@/types";

export const initEmployee: TCreateEmployeeRequest = {
    username: "",
    password: "",
    permission: "",
    idChucVu: undefined,
    idPhongBan: undefined,
    idVanPhong: undefined,
    manhanvien: "",
    hoTenVI: "",
    hoTenEN: "",
    namsinh: "",
    gioitinh: undefined,
    quequan: "",
    diachi: "",
    soCMT: "",
    noiCapCMT: "",
    ngayCapCMT: "",
    photoURL: "",
    ghichu: "",
    soLuongKH: 0,
};

export const initEmployeeGroup: TCreateEmployeeGroupRequest = {
    parentId: undefined,
    nameGroup: "",
    idNhanVien: [],
    flagViewAllGroup: false,
};

export const initPosition: TCreatePositionRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initDepartment: TCreateDepartmentRequest = {
    nameVI: "",
    nameEN: "",
    idVanPhong: undefined,
};

export const initOffice: TCreateOfficeRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
    addressVI: "",
    addressEN: "",
    phone: "",
    fax: "",
    email: "",
    website: "",
    note: "",
    taxCode: "",
    idCountry: undefined,
    idCity: undefined,
};

export const initCountry: TCreateCountryRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initCity: TCreateCityRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
    idQuocGia: undefined,
};

export const initPort: TCreatePortRequest = {
    code: "",
    taxCode: "",
    nameVI: "",
    nameEN: "",
    addressVI: "",
    addressEN: "",
    idQuocGia: undefined,
    idCity: undefined,
    phone: "",
    fax: "",
    email: "",
    website: "",
    note: "",
};

export const initBusiness: TCreateBusinessRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initTransportation: TCreateTransportationRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initMajor: TCreateMajorRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initTypeOfCustomer: TCreateTypeOfCustomerRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initCustomerType: TCreateCustomerTypeRequest = {
    code: "",
    nameVI: "",
    nameEN: "",
};

export const initOperational: TCreateOperationalRequest = {
    name: "",
    r: 0,
    g: 0,
    b: 0,
    ngayTuTraKhach: 0,
};

export const initCustomer: TCreateCustomerRequest = {
    idLoaiDoanhNghiep: undefined,
    idQuocGia: undefined,
    idCity: undefined,
    code: "",
    nameVI: "",
    nameEN: "",
    addressVI: "",
    addressEN: "",
    taxCode: "",
    phone: "",
    fax: "",
    email: "",
    website: "",
    note: "",
    idUserCreate: undefined,
};

export const initCustomerContact: TCreateCustomerContactRequest = {
    nameVI: "",
    nameEN: "",
    addressVI: "",
    addressEN: "",
    enumGioiTinh: undefined,
    handPhone: "",
    homePhone: "",
    email: "",
    note: "",
    idCustomer: undefined,
    bankAccountNumber: "",
    bankBranchName: "",
    bankAddress: "",
    chat: "",
    flagDaiDien: false,
    chucVu: "",
};

export const initCustomerOperational: TCreateCustomerOperationalRequest = {
    idLoaiTacNghiep: undefined,
    noiDung: "",
    idUserCreate: undefined,
    idCustomer: undefined,
    thoiGianThucHien: "",
    idNguoiLienHe: undefined,
    khachHangPhanHoi: "",
    ngayPhanHoi: "",
};

export const initCustomerMajor: TCreateCustomerMajorRequest = {
    idNghiepVu: undefined,
    idCustomer: undefined,
};

export const initCustomerClassify: TCreateCustomerClassifyRequest = {
    idCustomer: undefined,
    idPhanLoaiKhachHang: undefined,
};

export const initCustomerEvaluate: TCreateCustomerEvaluateRequest = {
    idCustomer: undefined,
    idCustomerType: undefined,
    idUserCreate: undefined,
    ghiChu: "",
};

export const initCustomerRoute: TCreateCustomerRouteRequest = {
    idCustomer: undefined,
    idCangDi: undefined,
    idCangDen: undefined,
    idQuocGiaDi: undefined,
    idQuocGiaDen: undefined,
    idLoaiHinhVanChuyen: undefined,
};

export const initCustomerImEx: TCreateCustomerImExRequest = {
    date: "",
    type: "",
    vessel: "",
    term: "",
    code: "",
    commd: "",
    vol: "",
    unt: "",
    idUserCreate: undefined,
    idFromPort: undefined,
    idToPort: undefined,
    idFromCountry: undefined,
    idToCountry: undefined,
    idCustomer: undefined,
};
