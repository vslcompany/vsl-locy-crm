import { Dispatch, useEffect, useState } from "react";
import { useQuery } from "react-query";

import {
    getAllBusinesses,
    getAllCustomerTypes,
    getAllMajors,
    getAllOperationals,
    getAllTypeOfCustomers,
    getPortsByIdCountry,
} from "@/api";
import {
    Button,
    GroupInput,
    GroupSelect,
    GroupTextArea,
    Modal,
} from "@/components";
import { useCategory } from "@/hooks";
import {
    TBusinessDto,
    TCustomerTypeDto,
    TMajorDto,
    TOperationalDto,
    TPortDto,
    TTypeOfCustomerDto,
} from "@/types";

type TQueryProps = {
    idLoaiDoanhNghiep: number | undefined;
    idNghiepVu: number | undefined;
    idPhanLoaiKhachHang: number | undefined;
    idDanhGia: number | undefined;
    idLoaiTacNghiep: number | undefined;
    name: string;
    taxCode: string;
    listType:
        | "all"
        | "assigned"
        | "delete"
        | "delivered"
        | "received"
        | "undelivered";

    idQuocGiaDiTuyenHang: number | undefined;
    idQuocGiaDenTuyenHang: number | undefined;
    idCangDiTuyenHang: number | undefined;
    idCangDenTuyenHang: number | undefined;

    idQuocGiaDiXNK: number | undefined;
    idQuocGiaDenXNK: number | undefined;
    idCangDiXNK: number | undefined;
    idCangDenXNK: number | undefined;
    term: string;
    hsCode: string;
    type: string;
};

type TModalProps = {
    isOpen: boolean;
    onClose: () => void;
    initQuery: TQueryProps;
    updateQuery: Dispatch<React.SetStateAction<TQueryProps>>;
};

const CustomerFilterModal = ({
    isOpen,
    onClose,
    initQuery,
    updateQuery,
}: TModalProps) => {
    const [query, setQuery] = useState<TQueryProps>(initQuery);

    const [businesses, setBusinesses] = useState<TBusinessDto[] | []>([]);
    const [majors, setMajors] = useState<TMajorDto[] | []>([]);
    const [types, setTypes] = useState<TTypeOfCustomerDto[] | []>([]);
    const [customerTypes, setCustomerTypes] = useState<TCustomerTypeDto[] | []>(
        []
    );
    const [operationals, setOperationals] = useState<TOperationalDto[] | []>(
        []
    );
    const [portsRouteFrom, setPortsRouteFrom] = useState<TPortDto[] | []>([]);
    const [portsRouteTo, setPortsRouteTo] = useState<TPortDto[] | []>([]);
    const [portsImExFrom, setPortsImExFrom] = useState<TPortDto[] | []>([]);
    const [portsImExTo, setPortsImExTo] = useState<TPortDto[] | []>([]);

    const { countries } = useCategory();

    // Loại doanh nghiệp
    const { data: businessesRes } = useQuery({
        queryKey: ["businessesQuery"],
        queryFn: getAllBusinesses,
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    // Nghiệp vụ
    const { data: majorsRes } = useQuery({
        queryKey: "majorsQuery",
        queryFn: getAllMajors,
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    // Phân loại khách hàng
    const { data: typesRes } = useQuery({
        queryKey: "types",
        queryFn: getAllTypeOfCustomers,
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    // Đánh giá
    const { data: customerTypesRes } = useQuery({
        queryKey: "customerTypesQuery",
        queryFn: getAllCustomerTypes,
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    // Tác nghiệp
    const { data: operationalsRes } = useQuery({
        queryKey: "operationalsQuery",
        queryFn: getAllOperationals,
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    // Xuất tuyến hàng
    const { data: portsFromRouteRes } = useQuery({
        queryKey: ["portsFromRouteQuery", query?.idQuocGiaDiTuyenHang],
        queryFn: () =>
            getPortsByIdCountry(query?.idQuocGiaDiTuyenHang as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            query.idQuocGiaDiTuyenHang !== undefined,
    });

    const { data: portsToRouteRes } = useQuery({
        queryKey: ["portsToRouteQuery", query?.idQuocGiaDenTuyenHang],
        queryFn: () =>
            getPortsByIdCountry(query?.idQuocGiaDenTuyenHang as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            query.idQuocGiaDenTuyenHang !== undefined,
    });

    // Xuất nhập khẩu
    const { data: portsFromImExRes } = useQuery({
        queryKey: ["portsFromImExQuery", query?.idQuocGiaDiXNK],
        queryFn: () => getPortsByIdCountry(query?.idQuocGiaDiXNK as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            query.idQuocGiaDiXNK !== undefined,
    });

    const { data: portsToImExRes } = useQuery({
        queryKey: ["portsToImExQuery", query?.idQuocGiaDenXNK],
        queryFn: () => getPortsByIdCountry(query?.idQuocGiaDenXNK as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            query.idQuocGiaDenXNK !== undefined,
    });

    /**
     * * Handle events
     */
    const handleSubmit = () => {
        updateQuery(query);
        onClose();
    };

    const handleRefesh = () => {
        setQuery((prev) => ({
            ...prev,
            idLoaiDoanhNghiep: undefined,
            idNghiepVu: undefined,
            idPhanLoaiKhachHang: undefined,
            idDanhGia: undefined,
            idLoaiTacNghiep: undefined,
            idQuocGiaDiTuyenHang: undefined,
            idQuocGiaDenTuyenHang: undefined,
            idCangDiTuyenHang: undefined,
            idCangDenTuyenHang: undefined,
            idQuocGiaDiXNK: undefined,
            idQuocGiaDenXNK: undefined,
            idCangDiXNK: undefined,
            idCangDenXNK: undefined,
            term: "",
            hsCode: "",
            type: "",
        }));
    };

    useEffect(() => {
        if (businessesRes && businessesRes.status) {
            setBusinesses(businessesRes.data as unknown as TBusinessDto[]);
        }
    }, [businessesRes]);

    useEffect(() => {
        if (majorsRes && majorsRes.status) {
            setMajors(majorsRes.data as unknown as TMajorDto[]);
        }
    }, [majorsRes]);

    useEffect(() => {
        if (customerTypesRes && customerTypesRes.status) {
            setCustomerTypes(
                customerTypesRes.data as unknown as TCustomerTypeDto[]
            );
        }
    }, [customerTypesRes]);

    useEffect(() => {
        if (typesRes && typesRes.status) {
            setTypes(typesRes.data as unknown as TTypeOfCustomerDto[]);
        }
    }, [typesRes]);

    useEffect(() => {
        if (operationalsRes && operationalsRes.status) {
            setOperationals(
                operationalsRes.data as unknown as TOperationalDto[]
            );
        }
    }, [operationalsRes]);

    useEffect(() => {
        if (portsFromRouteRes && portsFromRouteRes.status) {
            setPortsRouteFrom(portsFromRouteRes.data as unknown as TPortDto[]);
        }
    }, [portsFromRouteRes]);

    useEffect(() => {
        if (portsToRouteRes && portsToRouteRes.status) {
            setPortsRouteTo(portsToRouteRes.data as unknown as TPortDto[]);
        }
    }, [portsToRouteRes]);

    useEffect(() => {
        if (portsFromImExRes && portsFromImExRes.status) {
            setPortsImExFrom(portsFromImExRes.data as unknown as TPortDto[]);
        }
    }, [portsFromImExRes]);

    useEffect(() => {
        if (portsToImExRes && portsToImExRes.status) {
            setPortsImExTo(portsToImExRes.data as unknown as TPortDto[]);
        }
    }, [portsToImExRes]);

    useEffect(() => {
        if (isOpen) {
            setQuery(initQuery);
        }
    }, [isOpen, initQuery, setQuery]);

    return (
        <>
            <Modal
                isOpen={isOpen}
                onClose={onClose}
                title="Lọc dữ liệu khách hàng"
                width="7xl"
                customDisplayFooter={() => (
                    <div className="flex items-center gap-4">
                        <Button
                            label="lọc dữ liệu"
                            variant="contained"
                            color="cyan"
                            rounded="xl"
                            onClick={handleSubmit}
                        />
                        <Button
                            type="button"
                            label="huỷ"
                            variant="contained"
                            color="neutral"
                            rounded="xl"
                            onClick={onClose}
                        />
                        <Button
                            type="button"
                            label="làm mới"
                            variant="contained"
                            color="green"
                            rounded="xl"
                            onClick={handleRefesh}
                        />
                    </div>
                )}
            >
                <div className="grid gap-2">
                    <div className="space-y-2">
                        <h4
                            className="text-gray-900 dark:text-white text-base font-medium"
                            role="title"
                        >
                            thông tin khách hàng
                        </h4>
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupSelect
                                labelText="loại doanh nghiệp"
                                options={businesses}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idLoaiDoanhNghiep
                                        ? query.idLoaiDoanhNghiep.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idLoaiDoanhNghiep: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                            <GroupSelect
                                labelText="nghiệp vụ"
                                options={majors}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idNghiepVu
                                        ? query.idNghiepVu.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idNghiepVu: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                        </div>
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupSelect
                                labelText="phân loại khách hàng"
                                options={types}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idPhanLoaiKhachHang
                                        ? query.idPhanLoaiKhachHang.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idPhanLoaiKhachHang: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                            <GroupSelect
                                labelText="đánh giá khách hàng"
                                options={customerTypes}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idDanhGia
                                        ? query.idDanhGia.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idDanhGia: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                        </div>
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupSelect
                                labelText="loại tác nghiệp"
                                options={operationals}
                                option={{
                                    label: "name",
                                    value: "id",
                                }}
                                value={
                                    query.idLoaiTacNghiep
                                        ? query.idLoaiTacNghiep.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idLoaiTacNghiep: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                        </div>
                    </div>
                    <div className="space-y-2">
                        <h4
                            className="text-gray-900 dark:text-white text-base font-medium"
                            role="title"
                        >
                            thông tin tuyến hàng
                        </h4>
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupSelect
                                labelText="quốc gia đi"
                                options={
                                    countries && countries.length > 0
                                        ? countries
                                        : []
                                }
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idQuocGiaDiTuyenHang
                                        ? query.idQuocGiaDiTuyenHang.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idQuocGiaDiTuyenHang: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                            <GroupSelect
                                labelText="cảng đi"
                                options={portsRouteFrom}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idCangDiTuyenHang
                                        ? query.idCangDiTuyenHang.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idCangDiTuyenHang: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                        </div>
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupSelect
                                labelText="quốc gia đến"
                                options={
                                    countries && countries.length > 0
                                        ? countries
                                        : []
                                }
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idQuocGiaDenTuyenHang
                                        ? query.idQuocGiaDenTuyenHang.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idQuocGiaDenTuyenHang: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                            <GroupSelect
                                labelText="cảng đến"
                                options={portsRouteTo}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idCangDenTuyenHang
                                        ? query.idCangDenTuyenHang.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idCangDenTuyenHang: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                        </div>
                    </div>
                    <div className="space-y-2">
                        <h4
                            className="text-gray-900 dark:text-white text-base font-medium"
                            role="title"
                        >
                            thông tin xuất nhập khẩu
                        </h4>
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupSelect
                                labelText="quốc gia đi"
                                options={
                                    countries && countries.length > 0
                                        ? countries
                                        : []
                                }
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idQuocGiaDiXNK
                                        ? query.idQuocGiaDiXNK.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idQuocGiaDiXNK: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                            <GroupSelect
                                labelText="cảng đi"
                                options={portsImExFrom}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idCangDiXNK
                                        ? query.idCangDiXNK.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idCangDiXNK: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                        </div>
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupSelect
                                labelText="quốc gia đến"
                                options={
                                    countries && countries.length > 0
                                        ? countries
                                        : []
                                }
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idQuocGiaDenXNK
                                        ? query.idQuocGiaDenXNK.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idQuocGiaDenXNK: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                            <GroupSelect
                                labelText="cảng đến"
                                options={portsImExTo}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    query.idCangDenXNK
                                        ? query.idCangDenXNK.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        idCangDenXNK: val
                                            ? parseInt(val)
                                            : undefined,
                                    }))
                                }
                            />
                        </div>
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupInput
                                labelFor="term"
                                labelText="term"
                                value={query.term}
                                onChange={(e) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        term: e.target.value,
                                    }))
                                }
                            />
                            <GroupInput
                                labelFor="hscode"
                                labelText="hscode"
                                value={query.hsCode}
                                onChange={(e) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        hsCode: e.target.value,
                                    }))
                                }
                            />
                        </div>
                        <GroupTextArea
                            labelFor="type"
                            labelText="type"
                            value={query.type}
                            onChange={(e) =>
                                setQuery((prev) => ({
                                    ...prev,
                                    type: e.target.value,
                                }))
                            }
                        />
                    </div>
                </div>
            </Modal>
        </>
    );
};

export default CustomerFilterModal;
