export type TCreateCustomerRequest = {
    idLoaiDoanhNghiep: number | undefined;
    idQuocGia: number | undefined;
    idCity: number | undefined;
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
    idUserCreate: number | undefined;
};

export type TUpdateCustomerRequest = Omit<
    TCreateCustomerRequest,
    "idUserCreate"
> & {
    id: number;
};

export type TDeleteCustomerRequest = {
    id: number;
    idUserDelete: number | null;
    flagDel: boolean;
    lyDoXoa: string;
};

export type TChooseCustomerRequest = {
    idNhanVienSale: number;
    idCustomers: number[];
};

export type TDeliveryCustomerRequest = {
    idNhanVienSale: number;
    idUserGiaoViec: number;
    idCustomers: number[];
    thongTinGiaoViec: string;
};

export type TUndeliveryCustomerRequest = {
    idCustomers: number[];
};

export type TAcceptCustomerRequest = {
    idNhanVienSale: number;
    idCustomers: number[];
};

export type TDenyCustomerRequest = {
    idNhanVienSale: number;
    idCustomers: number[];
    lyDoTuChoi: string;
};

export type TCreateCustomerImExRequest = {
    date: string;
    type: string;
    vessel: string;
    term: string;
    code: string;
    commd: string;
    vol: string;
    unt: string;
    idUserCreate: number | undefined;
    idFromPort: number | undefined;
    idToPort: number | undefined;
    idFromCountry: number | undefined;
    idToCountry: number | undefined;
    idCustomer: number | undefined;
};

export type TUpdateCustomerImExRequest = Omit<
    TCreateCustomerImExRequest,
    "idUserCreate"
> & {
    id: number;
};

export type TCreateCustomerOperationalRequest = {
    idLoaiTacNghiep: number | undefined;
    noiDung: string;
    idUserCreate: number | undefined;
    idCustomer: number | undefined;
    thoiGianThucHien: string;
    idNguoiLienHe: number | undefined;
    khachHangPhanHoi: string;
    ngayPhanHoi: string;
};

export type TUpdateCustomerOperationalRequest = Omit<
    TCreateCustomerOperationalRequest,
    "idUserCreate"
> & {
    id: number;
};

export type TCreateCustomerContactRequest = {
    nameVI: string;
    nameEN: string;
    addressVI: string;
    addressEN: string;
    enumGioiTinh: number | undefined;
    handPhone: string;
    homePhone: string;
    email: string;
    note: string;
    idCustomer: number | undefined;
    bankAccountNumber: string;
    bankBranchName: string;
    bankAddress: string;
    chat: string;
    flagDaiDien: boolean;
    chucVu: string;
};

export type TUpdateCustomerContactRequest = TCreateCustomerContactRequest & {
    id: number;
};

export type TCreateCustomerEvaluateRequest = {
    idCustomer: number | undefined;
    idCustomerType: number | undefined;
    idUserCreate: number | undefined;
    ghiChu: string;
};

export type TUpdateCustomerEvaluateRequest = Omit<
    TCreateCustomerEvaluateRequest,
    "idUserCreate"
> & {
    id: number;
};

export type TCreateCustomerClassifyRequest = {
    idCustomer: number | undefined;
    idPhanLoaiKhachHang: number | undefined;
};

export type TUpdateCustomerClassifyRequest = TCreateCustomerClassifyRequest & {
    id: number;
};

export type TCreateCustomerMajorRequest = {
    idCustomer: number | undefined;
    idNghiepVu: number | undefined;
};

export type TUpdateCustomerMajorRequest = TCreateCustomerMajorRequest & {
    id: number;
};

export type TCreateCustomerRouteRequest = {
    idQuocGiaDi: number | undefined;
    idQuocGiaDen: number | undefined;
    idCangDi: number | undefined;
    idCangDen: number | undefined;
    idCustomer: number | undefined;
    idLoaiHinhVanChuyen: number | undefined;
};

export type TUpdateCustomerRouteRequest = TCreateCustomerRouteRequest & {
    id: number;
};

export type TImportCustomerRequest = {
    quocGia: string;
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
    idUserCreate: number;
};

export type TImportCustomerData = {
    Mã: string;
    "Mã số thuế": string;
    "Tên khách hàng (VI)": string;
    "Tên khách hàng (EN)": string;
    "Địa chỉ (VI)": string;
    "Địa chỉ (EN)": string;
    "Thành phố": string;
    "Quốc gia": string;
    "Số điện thoại": string;
    "Số FAX": string;
    "Thư điện tử": string;
    "Trang web": string;
    "Ghi chú": string;
};

export type TImportCustomerImExRequest = {
    date: string;
    taxID: string;
    codeCompany: string;
    company: string;
    add: string;
    tel: string;
    type: string;
    countryPOL: string;
    pol: string;
    countryPOD: string;
    pod: string;
    vessel: string;
    term: string;
    code: string;
    commd: string;
    vol: string;
    unt: string;
    idUserCreate: number;
};

export type TImportCustomerImExData = {
    Date: string;
    "Tax ID": string;
    "Code Company": string;
    Company: string;
    ADD: string;
    Tel: string;
    Type: string;
    "Country POL": string;
    POL: string;
    "_Country POD": string;
    POD: string;
    Vessel: string;
    Term: string;
    Code: string;
    Commd: string;
    VOL: string;
    UNT: string;
    BY: string;
};
