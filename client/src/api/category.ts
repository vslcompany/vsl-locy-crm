import { privateInstance } from "@/configs";
import {
    TApiGetWithPageProps,
    TCreateBusinessRequest,
    TCreateCityRequest,
    TCreateCountryRequest,
    TCreateCustomerTypeRequest,
    TCreateDepartmentRequest,
    TCreateMajorRequest,
    TCreateOfficeRequest,
    TCreateOperationalRequest,
    TCreatePortRequest,
    TCreatePositionRequest,
    TCreateTransportationRequest,
    TCreateTypeOfCustomerRequest,
    TUpdateBusinessRequest,
    TUpdateCityRequest,
    TUpdateCountryRequest,
    TUpdateCustomerTypeRequest,
    TUpdateDepartmentRequest,
    TUpdateMajorRequest,
    TUpdateOfficeRequest,
    TUpdateOperationalRequest,
    TUpdatePortRequest,
    TUpdatePositionRequest,
    TUpdateTransportationRequest,
    TUpdateTypeOfCustomerRequest,
} from "@/types";

/**
 * * ===== Chức vụ =====
 */
export const getAllPositions = () => {
    return privateInstance.get("/position/all");
};

export const getPositions = ({ start, size, search }: TApiGetWithPageProps) => {
    return privateInstance.get("/position", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createPosition = (payload: TCreatePositionRequest) => {
    return privateInstance.post("/position", payload);
};

export const updatePosition = (payload: TUpdatePositionRequest) => {
    return privateInstance.put(`/position/${payload.id}`, payload);
};

export const deletePosition = (id: number) => {
    return privateInstance.delete(`/position/${id}`);
};

/**
 * * ===== Loại doanh nghiệp =====
 */
export const getAllBusinesses = () => {
    return privateInstance.get("/business/all");
};

export const getBusinesses = ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    return privateInstance.get("/business", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createBusiness = (payload: TCreateBusinessRequest) => {
    return privateInstance.post("/business", payload);
};

export const updateBusiness = (payload: TUpdateBusinessRequest) => {
    return privateInstance.put(`/business/${payload.id}`, payload);
};

export const deleteBusiness = (id: number) => {
    return privateInstance.delete(`/business/${id}`);
};

/**
 * * ===== Phòng ban =====
 */
export const getAllDepartments = () => {
    return privateInstance.get("/department/all");
};

export const getDepartments = ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    return privateInstance.get("/department", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createDepartment = (payload: TCreateDepartmentRequest) => {
    return privateInstance.post("/department", payload);
};

export const updateDepartment = (payload: TUpdateDepartmentRequest) => {
    return privateInstance.put(`/department/${payload.id}`, payload);
};

export const deleteDepartment = (id: number) => {
    return privateInstance.delete(`/department/${id}`);
};

/**
 * * ===== Văn phòng =====
 */
export const getAllOffices = () => {
    return privateInstance.get("/office/all");
};

export const getOffices = ({ start, size, search }: TApiGetWithPageProps) => {
    return privateInstance.get("/office", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createOffice = (payload: TCreateOfficeRequest) => {
    return privateInstance.post("/office", payload);
};

export const updateOffice = (payload: TUpdateOfficeRequest) => {
    return privateInstance.put(`/office/${payload.id}`, payload);
};

export const deleteOffice = (id: number) => {
    return privateInstance.delete(`/office/${id}`);
};

/**
 * * ===== Quốc gia =====
 */
export const getAllCountries = () => {
    return privateInstance.get("/country/all");
};

export const getCountries = ({ start, size, search }: TApiGetWithPageProps) => {
    return privateInstance.get("/country", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createCountry = (payload: TCreateCountryRequest) => {
    return privateInstance.post("/country", payload);
};

export const updateCountry = (payload: TUpdateCountryRequest) => {
    return privateInstance.put(`/country/${payload.id}`, payload);
};

export const deleteCountry = (id: number) => {
    return privateInstance.delete(`/country/${id}`);
};

/**
 * * ===== Thành phố =====
 */
export const getCities = ({ start, size, search }: TApiGetWithPageProps) => {
    return privateInstance.get("/city", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const getCitiesByIdCountry = (id: number) => {
    return privateInstance.get(`/city/${id}/country`);
};

export const createCity = (payload: TCreateCityRequest) => {
    return privateInstance.post("/city", payload);
};

export const updateCity = (payload: TUpdateCityRequest) => {
    return privateInstance.put(`/city/${payload.id}`, payload);
};

export const deleteCity = (id: number) => {
    return privateInstance.delete(`/city/${id}`);
};

/**
 * * ===== Cảng =====
 */
export const getPortsByIdCountry = (idCountry: number) => {
    return privateInstance.get(`port/${idCountry}/all`);
};

export const getPorts = ({ start, size, search }: TApiGetWithPageProps) => {
    return privateInstance.get("/port", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createPort = (payload: TCreatePortRequest) => {
    return privateInstance.post("/port", payload);
};

export const updatePort = (payload: TUpdatePortRequest) => {
    return privateInstance.put(`/port/${payload.id}`, payload);
};

export const deletePort = (id: number) => {
    return privateInstance.delete(`/port/${id}`);
};

/**
 * * ===== Loại hình vận chuyển =====
 */
export const getAllTransportations = async () => {
    return privateInstance.get("/transportation/all");
};

export const getTransportations = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    return privateInstance.get("/transportation", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createTransportation = async (
    payload: TCreateTransportationRequest
) => {
    return privateInstance.post("/transportation", payload);
};

export const updateTransportation = async (
    payload: TUpdateTransportationRequest
) => {
    return privateInstance.put(`/transportation/${payload.id}`, payload);
};

export const deleteTransportation = async (id: number) => {
    return privateInstance.delete(`/transportation/${id}`);
};

/**
 * * ===== loại tác nghiệp =====
 */
export const getAllOperationals = async () => {
    return privateInstance.get("/operational/all");
};

export const getOperationals = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    return privateInstance.get("/operational", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createOperational = async (payload: TCreateOperationalRequest) => {
    return privateInstance.post("/operational", payload);
};

export const updateOperational = async (payload: TUpdateOperationalRequest) => {
    return privateInstance.put(`/operational/${payload.id}`, payload);
};

export const deleteOperational = async (id: number) => {
    return privateInstance.delete(`/operational/${id}`);
};

/**
 * * ===== Nghiệp vụ =====
 */
export const getAllMajors = async () => {
    return privateInstance.get("/major/all");
};

export const getMajors = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    return privateInstance.get("/major", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createMajor = async (payload: TCreateMajorRequest) => {
    return privateInstance.post("/major", payload);
};

export const updateMajor = async (payload: TUpdateMajorRequest) => {
    return privateInstance.put(`/major/${payload.id}`, payload);
};

export const deleteMajor = async (id: number) => {
    return privateInstance.delete(`/major/${id}`);
};

/**
 * * ===== Phân loại khách hàng =====
 */
export const getAllTypeOfCustomers = async () => {
    return privateInstance.get("/typeofcustomer/all");
};

export const getTypeOfCustomers = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    return privateInstance.get("/typeofcustomer", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createTypeOfCustomer = async (
    payload: TCreateTypeOfCustomerRequest
) => {
    return privateInstance.post("/typeofcustomer", payload);
};

export const updateTypeOfCustomer = async (
    payload: TUpdateTypeOfCustomerRequest
) => {
    return privateInstance.put(`/typeofcustomer/${payload.id}`, payload);
};

export const deleteTypeOfCustomer = async (id: number) => {
    return privateInstance.delete(`/typeofcustomer/${id}`);
};

/**
 * * ===== Phân loại khách hàng đánh giá =====
 */
export const getAllCustomerTypes = async () => {
    return privateInstance.get("/customertypes/all");
};

export const getCustomerTypes = async ({
    start,
    size,
    search,
}: TApiGetWithPageProps) => {
    return privateInstance.get("/customertypes", {
        params: {
            start,
            size,
            search,
        },
    });
};

export const createCustomerType = async (
    payload: TCreateCustomerTypeRequest
) => {
    return privateInstance.post("/customertypes", payload);
};

export const updateCustomerType = async (
    payload: TUpdateCustomerTypeRequest
) => {
    return privateInstance.put(`/customertypes/${payload.id}`, payload);
};

export const deleteCustomerType = async (id: number) => {
    return privateInstance.delete(`/customertypes/${id}`);
};
