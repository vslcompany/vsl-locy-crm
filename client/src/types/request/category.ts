export type TCreateCityRequest = {
    code: string;
    nameVI: string;
    nameEN: string;
    idQuocGia: number | undefined;
};

export type TUpdateCityRequest = TCreateCityRequest & {
    id: number;
};

export type TCreateCountryRequest = {
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TUpdateCountryRequest = TCreateCountryRequest & {
    id: number;
};

export type TCreatePortRequest = {
    idQuocGia: number | undefined;
    idCity: number | undefined;
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

export type TUpdatePortRequest = TCreatePortRequest & {
    id: number;
};

export type TCreatePositionRequest = {
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TUpdatePositionRequest = TCreatePositionRequest & { id: number };

export type TCreateDepartmentRequest = {
    nameVI: string;
    nameEN: string;
    idVanPhong: number | undefined;
};

export type TUpdateDepartmentRequest = TCreateDepartmentRequest & {
    id: number;
};

export type TCreateOfficeRequest = {
    code: string;
    idCountry: number | undefined;
    idCity: number | undefined;
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

export type TUpdateOfficeRequest = TCreateOfficeRequest & {
    id: number;
};

export type TCreateBusinessRequest = {
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TUpdateBusinessRequest = TCreateBusinessRequest & {
    id: number;
};

export type TCreateTransportationRequest = {
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TUpdateTransportationRequest = TCreateTransportationRequest & {
    id: number;
};

export type TCreateOperationalRequest = {
    name: string;
    r: number;
    g: number;
    b: number;
    ngayTuTraKhach: number;
};

export type TUpdateOperationalRequest = TCreateOperationalRequest & {
    id: number;
};

export type TCreateMajorRequest = {
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TUpdateMajorRequest = TCreateMajorRequest & {
    id: number;
};

export type TCreateTypeOfCustomerRequest = {
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TUpdateTypeOfCustomerRequest = TCreateTypeOfCustomerRequest & {
    id: number;
};

export type TCreateCustomerTypeRequest = {
    code: string;
    nameVI: string;
    nameEN: string;
};

export type TUpdateCustomerTypeRequest = TCreateCustomerTypeRequest & {
    id: number;
};
