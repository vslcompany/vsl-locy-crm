import { privateInstance } from "@/configs";
import {
    TAcceptCustomerRequest,
    TApiGetWithPageProps,
    TChooseCustomerRequest,
    TCreateCustomerClassifyRequest,
    TCreateCustomerContactRequest,
    TCreateCustomerEvaluateRequest,
    TCreateCustomerImExRequest,
    TCreateCustomerMajorRequest,
    TCreateCustomerOperationalRequest,
    TCreateCustomerRequest,
    TCreateCustomerRouteRequest,
    TDeleteCustomerRequest,
    TDeliveryCustomerRequest,
    TDenyCustomerRequest,
    TUndeliveryCustomerRequest,
    TUpdateCustomerClassifyRequest,
    TUpdateCustomerContactRequest,
    TUpdateCustomerEvaluateRequest,
    TUpdateCustomerImExRequest,
    TUpdateCustomerMajorRequest,
    TUpdateCustomerOperationalRequest,
    TUpdateCustomerRequest,
    TUpdateCustomerRouteRequest,
} from "@/types";

type TGetParams = {
    start: number;
    size: number;

    idLoaiDoanhNghiep: number;
    idNghiepVu: number;
    idPhanLoaiKhachHang: number;
    idDanhGia: number;
    idLoaiTacNghiep: number;
    name: string;
    taxCode: string;
    listType: string;

    idQuocGiaDiTuyenHang: number;
    idQuocGiaDenTuyenHang: number;
    idCangDiTuyenHang: number;
    idCangDenTuyenHang: number;

    idQuocGiaDiXNK: number;
    idQuocGiaDenXNK: number;
    idCangDiXNK: number;
    idCangDenXNK: number;
    term: string;
    hsCode: string;
    type: string;
};

export const getCustomers = (params: TGetParams) => {
    return privateInstance.get("/customer", {
        params,
    });
};

export const createCustomer = (payload: TCreateCustomerRequest) => {
    return privateInstance.post("customer", payload);
};

export const updateCustomer = (payload: TUpdateCustomerRequest) => {
    return privateInstance.put(`/customer/${payload.id}`, payload);
};

export const deleteCustomer = (payload: TDeleteCustomerRequest) => {
    return privateInstance.put(`/customer/${payload.id}/delete`, payload);
};

export const removeCustomer = (id: number) => {
    return privateInstance.delete(`/customer/${id}`);
};

export const chooseCustomer = (payload: TChooseCustomerRequest) => {
    return privateInstance.put("/customer/choose", payload);
};

export const deliveryCustomer = (payload: TDeliveryCustomerRequest) => {
    return privateInstance.put("/customer/delivery", payload);
};

export const undeliveryCustomer = (payload: TUndeliveryCustomerRequest) => {
    return privateInstance.put("/customer/undelivery", payload);
};

export const acceptCustomer = (payload: TAcceptCustomerRequest) => {
    return privateInstance.put("/customer/accept", payload);
};

export const denyCustomer = (payload: TDenyCustomerRequest) => {
    return privateInstance.put("/customer/deny", payload);
};

// Information
// Detail
export const getCustomerById = (id: number) => {
    return privateInstance.get(`/customerinfo/${id}`);
};

// List ImEx of customer id
export const getCustomerImExs = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    return privateInstance.get(`/customerinfo/imex/${idCustomer}`, {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createCustomerImEx = async (
    payload: TCreateCustomerImExRequest
) => {
    return privateInstance.post("/customerinfo/imex", payload);
};

export const updateCustomerImEx = async (
    payload: TUpdateCustomerImExRequest
) => {
    return privateInstance.put(`/customerinfo/imex/${payload.id}`, payload);
};

export const deleteCustomerImEx = async (id: number) => {
    return privateInstance.delete(`/customerinfo/imex/${id}`);
};

// List Operational of customer id
export const getCustomerOperationals = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    return privateInstance.get(`/customerinfo/operational/${idCustomer}`, {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createCustomerOperational = async (
    payload: TCreateCustomerOperationalRequest
) => {
    return privateInstance.post("/customerinfo/operational", payload);
};

export const updateCustomerOperational = async (
    payload: TUpdateCustomerOperationalRequest
) => {
    return privateInstance.put(
        `/customerinfo/operational/${payload.id}`,
        payload
    );
};

export const deleteCustomerOperational = async (id: number) => {
    return privateInstance.delete(`/customerinfo/operational/${id}`);
};

// List Contact of customer id
export const getAllCustomerContacts = async (idCustomer: number) => {
    return privateInstance.get(`/customerinfo/contact/${idCustomer}/all`);
};

export const getCustomerContacts = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    return privateInstance.get(`/customerinfo/contact/${idCustomer}`, {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createCustomerContact = async (
    payload: TCreateCustomerContactRequest
) => {
    return privateInstance.post("/customerinfo/contact", payload);
};

export const updateCustomerContact = async (
    payload: TUpdateCustomerContactRequest
) => {
    return privateInstance.put(`/customerinfo/contact/${payload.id}`, payload);
};

export const deleteCustomerContact = async (id: number) => {
    return privateInstance.delete(`/customerinfo/contact/${id}`);
};

// List Evaluate of customer id
export const getCustomerEvaluates = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    return privateInstance.get(`/customerinfo/evaluate/${idCustomer}`, {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createCustomerEvaluate = async (
    payload: TCreateCustomerEvaluateRequest
) => {
    return privateInstance.post("/customerinfo/evaluate", payload);
};

export const updateCustomerEvaluate = async (
    payload: TUpdateCustomerEvaluateRequest
) => {
    return privateInstance.put(`/customerinfo/evaluate/${payload.id}`, payload);
};

export const deleteCustomerEvaluate = async (id: number) => {
    return privateInstance.delete(`/customerinfo/evaluate/${id}`);
};

// List Classify of customer id
export const getCustomerClassifies = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    return privateInstance.get(`/customerinfo/classify/${idCustomer}`, {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createCustomerClassify = async (
    payload: TCreateCustomerClassifyRequest
) => {
    return privateInstance.post("/customerinfo/classify", payload);
};

export const updateCustomerClassify = async (
    payload: TUpdateCustomerClassifyRequest
) => {
    return privateInstance.put(`/customerinfo/classify/${payload.id}`, payload);
};

export const deleteCustomerClassify = async (id: number) => {
    return privateInstance.delete(`/customerinfo/classify/${id}`);
};

// List Major of customer id
export const getCustomerMajors = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    return privateInstance.get(`/customerinfo/major/${idCustomer}`, {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createCustomerMajor = async (
    payload: TCreateCustomerMajorRequest
) => {
    return privateInstance.post("/customerinfo/major", payload);
};

export const updateCustomerMajor = async (
    payload: TUpdateCustomerMajorRequest
) => {
    return privateInstance.put(`/customerinfo/major/${payload.id}`, payload);
};

export const deleteCustomerMajor = async (id: number) => {
    return privateInstance.delete(`/customerinfo/major/${id}`);
};

// List Route of customer id
export const getCustomerRoutes = async ({
    start,
    size,
    search,
    idCustomer,
}: TApiGetWithPageProps & { idCustomer: number }) => {
    return privateInstance.get(`/customerinfo/route/${idCustomer}`, {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createCustomerRoute = async (
    payload: TCreateCustomerRouteRequest
) => {
    return privateInstance.post("/customerinfo/route", payload);
};

export const updateCustomerRoute = async (
    payload: TUpdateCustomerRouteRequest
) => {
    return privateInstance.put(`/customerinfo/route/${payload.id}`, payload);
};

export const deleteCustomerRoute = async (id: number) => {
    return privateInstance.delete(`/customerinfo/route/${id}`);
};

export const exportCustomerData = async () => {
    return privateInstance.get("/export/customer");
};

export const exportCustomerReceivedData = async () => {
    return privateInstance.get("/export/customer/received");
};
