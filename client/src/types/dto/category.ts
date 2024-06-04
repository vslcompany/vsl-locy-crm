export type TPositionDto = {
    id: number;
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TCountryDto = {
    id: number;
    code: string;
    nameVI: string;
    nameEN: string;
    note: string;
    flagFavorite: boolean;
};

export type TCityDto = TCountryDto & {
    idQuocGia: number;
    quocGia: string;
};

export type TDepartmentDto = {
    id: number;
    nameVI: string;
    nameEN: string;
    ghiChu: string;
    flagFavorite: boolean;
    idVanPhong: number;
};

export type TOfficeDto = {
    id: number;
    code: string;
    idCountry: number;
    idCity: number;
    quocGia: string;
    thanhPho: string;
    nameVI: string;
    nameEN: string;
    addressVI: string;
    addressEN: string;
    phone: string;
    fax: string;
    email: string;
    website: string;
    note: string;
    taxCode: string;
};

export type TBusinessDto = {
    id: number;
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TPortDto = {
    id: number;
    idQuocGia: number;
    quocGia: string;
    idCity: number;
    thanhPho: string;
    code: string;
    taxCode: string;
    nameVI: string;
    nameEN: string;
    addressVI: string;
    addressEN: string;
    phone: string;
    fax: string;
    email: string;
    website: string;
    note: string;
};

export type TTransportationDto = {
    id: number;
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TOperationalDto = {
    id: number;
    name: string;
    r: number;
    g: number;
    b: number;
    ngayTuTraKhach: number;
};

export type TMajorDto = {
    id: number;
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TTypeOfCustomerDto = {
    id: number;
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TCustomerTypeDto = {
    id: number;
    code: string;
    nameVI: string;
    nameEN: string;
};
