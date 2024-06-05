import { useCallback, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useQuery, useMutation } from "react-query";
import { type MRT_PaginationState } from "material-react-table";
import { IconButton, Tooltip } from "@mui/material";
import { Cached, FilterAlt } from "@mui/icons-material";
import { BiExport } from "react-icons/bi";
import { Menu, MenuButton, MenuItem, MenuItems } from "@headlessui/react";

import {
    exportCustomerData,
    exportCustomerReceivedData,
    getCustomers,
} from "@/api";
import { Button, GroupInput, GroupSelect, Modal } from "@/components";
import { privateInstance } from "@/configs";
import { useAuth } from "@/contexts";
import { useDebounced } from "@/hooks";
import {
    CustomerAssignedTable,
    CustomerDeleteTable,
    CustomerDeliveredTable,
    CustomerReceivedTable,
    CustomerTable,
    CustomerUndeliveredTable,
} from "@/layouts";
import { TAuthContextProps, TCustomerDto, TTableData } from "@/types";

import { getListCustomerTable } from "./helper";
import { CustomerFilterModal } from "@/modals";

type TListType =
    | "all"
    | "assigned"
    | "delete"
    | "delivered"
    | "received"
    | "undelivered";

type TQuery = {
    idLoaiDoanhNghiep: number | undefined;
    idNghiepVu: number | undefined;
    idPhanLoaiKhachHang: number | undefined;
    idDanhGia: number | undefined;
    idLoaiTacNghiep: number | undefined;
    name: string;
    taxCode: string;
    listType: TListType;

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

type TData = TTableData<TCustomerDto>;

const listTypes = [
    {
        label: "Tất cả",
        value: "all",
    },
    {
        label: "Chưa nhận",
        value: "undelivered",
    },
    {
        label: "Đã nhận",
        value: "received",
    },
    {
        label: "Được giao",
        value: "delivered",
    },
    {
        label: "Đã giao",
        value: "assigned",
    },
    {
        label: "Đã xoá",
        value: "delete",
    },
];

const Customer = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [query, setQuery] = useState<TQuery>({
        idLoaiDoanhNghiep: undefined,
        idNghiepVu: undefined,
        idPhanLoaiKhachHang: undefined,
        idDanhGia: undefined,
        idLoaiTacNghiep: undefined,
        name: "",
        taxCode: "",
        listType: "all",
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
    });

    const [isOpenExportModal, setIsOpenExportModal] = useState<boolean>(false);
    const [isOpenExportReceivedModal, setIsOpenExportReceivedModal] =
        useState<boolean>(false);
    const [isOpenFilterModal, setIsOpenFilterModal] = useState<boolean>(false);

    const { user }: TAuthContextProps = useAuth();
    const debouncedName = useDebounced(query.name, 1000);
    const debouncedTaxCode = useDebounced(query.taxCode, 1000);

    const filterLists = useMemo(() => {
        const lists = getListCustomerTable(
            user?.permission as string,
            user?.username.toLowerCase() as string
        );
        console.log(lists);
        const result = listTypes.filter((item) => {
            return lists.includes(item.label.toLowerCase());
        });
        return result;
    }, [user]);

    const navigate = useNavigate();

    const {
        data: customerRes,
        isError,
        isFetching,
        isLoading,
        refetch,
    } = useQuery({
        queryKey: [
            "customer",
            pagination.pageIndex,
            pagination.pageSize,
            debouncedName,
            debouncedTaxCode,
            query.idLoaiDoanhNghiep,
            query.idNghiepVu,
            query.idPhanLoaiKhachHang,
            query.idDanhGia,
            query.idLoaiTacNghiep,
            query.listType,
            query.idQuocGiaDiTuyenHang,
            query.idQuocGiaDenTuyenHang,
            query.idCangDiTuyenHang,
            query.idCangDenTuyenHang,
            query.idQuocGiaDiXNK,
            query.idQuocGiaDenXNK,
            query.idCangDiXNK,
            query.idCangDenXNK,
            query.term,
            query.hsCode,
            query.type,
            user?.id,
        ],
        queryFn: () =>
            getCustomers({
                start: pagination.pageIndex * pagination.pageSize,
                size: pagination.pageSize,
                name: debouncedName,
                taxCode: debouncedTaxCode,
                idLoaiDoanhNghiep: query.idLoaiDoanhNghiep as number,
                idNghiepVu: query.idNghiepVu as number,
                idPhanLoaiKhachHang: query.idPhanLoaiKhachHang as number,
                idDanhGia: query.idDanhGia as number,
                idLoaiTacNghiep: query.idLoaiTacNghiep as number,
                listType: query.listType,
                idQuocGiaDiTuyenHang: query.idQuocGiaDiTuyenHang as number,
                idQuocGiaDenTuyenHang: query.idQuocGiaDenTuyenHang as number,
                idCangDiTuyenHang: query.idCangDiTuyenHang as number,
                idCangDenTuyenHang: query.idCangDenTuyenHang as number,
                idQuocGiaDiXNK: query.idQuocGiaDiXNK as number,
                idQuocGiaDenXNK: query.idQuocGiaDenXNK as number,
                idCangDiXNK: query.idCangDiXNK as number,
                idCangDenXNK: query.idCangDenXNK as number,
                term: query.term,
                hsCode: query.hsCode,
                type: query.type,
            }),
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
        keepPreviousData: true,
        refetchOnWindowFocus: false,
    });

    const exportMutation = useMutation({
        mutationFn: exportCustomerData,
    });

    const exportReceivedMutation = useMutation({
        mutationFn: exportCustomerReceivedData,
    });

    /**
     * * Handle events
     */
    const openExportModal = useCallback(() => {
        setIsOpenExportModal(true);
    }, []);

    const closeExportModal = useCallback(() => {
        setIsOpenExportModal(false);
    }, []);

    const handleExportData = useCallback(async () => {
        const data = await exportMutation.mutateAsync();

        if (data && data.status) {
            const baseUrl = privateInstance.defaults.baseURL;
            const fileName = data.data.downloadUrl.split("/").slice(-1)[0];
            const url = `${baseUrl}/export/download/${fileName}`;

            // Create a link element
            const link = document.createElement("a");
            link.href = url;
            link.download = fileName;

            // Trigger click event to initiate download
            link.click();

            closeExportModal();
        }
    }, [exportMutation, closeExportModal]);

    const openExportReceivedModal = useCallback(() => {
        setIsOpenExportReceivedModal(true);
    }, []);

    const closeExportReceivedModal = useCallback(() => {
        setIsOpenExportReceivedModal(false);
    }, []);

    const handleExportReceivedData = useCallback(async () => {
        const data = await exportReceivedMutation.mutateAsync();

        if (data && data.status) {
            const baseUrl = privateInstance.defaults.baseURL;
            const fileName = data.data.downloadUrl.split("/").slice(-1)[0];
            const url = `${baseUrl}/export/download/${fileName}`;

            // Create a link element
            const link = document.createElement("a");
            link.href = url;
            link.download = fileName;

            // Trigger click event to initiate download
            link.click();

            closeExportReceivedModal();
        }
    }, [exportReceivedMutation, closeExportReceivedModal]);

    const openFilterModal = useCallback(() => {
        setIsOpenFilterModal(true);
    }, []);

    const closeFilterModal = useCallback(() => {
        setIsOpenFilterModal(false);
    }, []);

    useEffect(() => {
        if (customerRes && customerRes.status) {
            setTableData(customerRes.data.data);
            setTotalRow(customerRes.data.totalRow);
        }
    }, [customerRes]);

    return (
        <>
            <h2 className="title">Quản lý khách hàng</h2>
            <div className="space-y-4">
                <div className="flex items-center justify-between flex-wrap gap-4">
                    <h3 className="subtitle">tất cả dữ liệu khách hàng</h3>
                    <div className="flex items-center gap-4">
                        <Button
                            label="tạo mới"
                            onClick={() => navigate("/customer/new")}
                        />
                        <Tooltip arrow placement="left" title="Tải lại dữ liệu">
                            <IconButton
                                color="primary"
                                onClick={() => refetch()}
                            >
                                <Cached />
                            </IconButton>
                        </Tooltip>
                    </div>
                </div>
                {/* Filter */}
                <div className="flex items-end justify-between gap-4 flex-wrap">
                    <div className="flex gap-2 items-center flex-wrap">
                        <div className="w-48">
                            <GroupInput
                                labelFor="name"
                                labelText="Tên khách hàng"
                                value={query.name}
                                onChange={(e) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        name: e.target.value,
                                    }))
                                }
                            />
                        </div>
                        <div className="w-48">
                            <GroupInput
                                labelFor="taxCode"
                                labelText="Mã số thuế"
                                value={query.taxCode}
                                onChange={(e) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        taxCode: e.target.value,
                                    }))
                                }
                            />
                        </div>
                        <div className="w-48">
                            <GroupSelect
                                labelText="trạng thái"
                                options={filterLists}
                                option={{
                                    label: "label",
                                    value: "value",
                                }}
                                value={query.listType}
                                onValueChange={(val) =>
                                    setQuery((prev) => ({
                                        ...prev,
                                        listType: val
                                            ? (val as TListType)
                                            : "all",
                                    }))
                                }
                            />
                        </div>
                    </div>
                    <div className="flex items-center gap-2 flex-wrap">
                        <Menu>
                            <MenuButton as="div">
                                <Tooltip
                                    arrow
                                    placement="left"
                                    title="xuất dữ liệu"
                                >
                                    <IconButton>
                                        <BiExport className="fill-green-500" />
                                    </IconButton>
                                </Tooltip>
                            </MenuButton>
                            <MenuItems
                                className="bg-white border border-gray-400 p-2 space-y-2 rounded-md"
                                anchor="bottom end"
                            >
                                <MenuItem>
                                    <div
                                        className="first-letter:uppercase data-[focus]:bg-green-100 p-2 cursor-pointer"
                                        onClick={openExportModal}
                                    >
                                        export khách hàng
                                    </div>
                                </MenuItem>
                                <MenuItem>
                                    <div
                                        className="first-letter:uppercase data-[focus]:bg-green-100 p-2 cursor-pointer"
                                        onClick={openExportReceivedModal}
                                    >
                                        export khách hàng đang quản lý
                                    </div>
                                </MenuItem>
                            </MenuItems>
                        </Menu>
                        <Tooltip arrow placement="left" title="Lọc dữ liệu">
                            <IconButton onClick={openFilterModal}>
                                <FilterAlt className="!fill-blue-500" />
                            </IconButton>
                        </Tooltip>
                    </div>
                </div>
                {/* Table */}
                {query.listType === "all" && (
                    <CustomerTable
                        data={tableData}
                        pagination={pagination}
                        setPagination={setPagination}
                        totalRow={totalRow}
                        isError={isError}
                        isFetching={isFetching}
                        isLoading={isLoading}
                        refetch={() => refetch()}
                    />
                )}
                {query.listType === "assigned" &&
                    (user?.permission.includes("1048576") ||
                        user?.permission.includes("7000") ||
                        user?.permission.includes("7080")) && (
                        <CustomerAssignedTable
                            data={tableData}
                            pagination={pagination}
                            setPagination={setPagination}
                            totalRow={totalRow}
                            isError={isError}
                            isFetching={isFetching}
                            isLoading={isLoading}
                            refetch={() => refetch()}
                        />
                    )}
                {query.listType === "received" && (
                    <CustomerReceivedTable
                        data={tableData}
                        pagination={pagination}
                        setPagination={setPagination}
                        totalRow={totalRow}
                        isError={isError}
                        isFetching={isFetching}
                        isLoading={isLoading}
                        refetch={() => refetch()}
                    />
                )}
                {query.listType === "undelivered" && (
                    <CustomerUndeliveredTable
                        data={tableData}
                        pagination={pagination}
                        setPagination={setPagination}
                        totalRow={totalRow}
                        isError={isError}
                        isFetching={isFetching}
                        isLoading={isLoading}
                        refetch={() => refetch()}
                    />
                )}
                {query.listType === "delivered" &&
                    user?.username !== "admin" && (
                        <CustomerDeliveredTable
                            data={tableData}
                            pagination={pagination}
                            setPagination={setPagination}
                            totalRow={totalRow}
                            isError={isError}
                            isFetching={isFetching}
                            isLoading={isLoading}
                            refetch={() => refetch()}
                        />
                    )}
                {query.listType === "delete" &&
                    (user?.permission.includes("1048576") ||
                        user?.permission.includes("7000") ||
                        user?.permission.includes("7020")) && (
                        <CustomerDeleteTable
                            data={tableData}
                            pagination={pagination}
                            setPagination={setPagination}
                            totalRow={totalRow}
                            isError={isError}
                            isFetching={isFetching}
                            isLoading={isLoading}
                            refetch={() => refetch()}
                        />
                    )}
            </div>
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7060")) && (
                <Modal
                    isOpen={isOpenExportModal}
                    onClose={closeExportModal}
                    title="Export dữ liệu khách hàng"
                    description="bạn chắc chắn muốn export dữ liệu khách hàng"
                    onSubmit={() => handleExportData()}
                    labelButtonCancel="huỷ"
                    labelButtonOk="xuất dữ liệu"
                    isButtonOkLoading={exportMutation.isLoading}
                    labelButtonOkLoading="đang xuất dữ liệu"
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7060")) && (
                <Modal
                    isOpen={isOpenExportReceivedModal}
                    onClose={closeExportReceivedModal}
                    onSubmit={() => handleExportReceivedData()}
                    title="Export dữ liệu khách hàng"
                    description="bạn chắc chắn muốn export dữ liệu khách hàng đã nhận"
                    labelButtonCancel="huỷ"
                    labelButtonOk="xuất dữ liệu"
                    isButtonOkLoading={exportReceivedMutation.isLoading}
                    labelButtonOkLoading="đang xuất dữ liệu"
                />
            )}
            <CustomerFilterModal
                isOpen={isOpenFilterModal}
                onClose={closeFilterModal}
                initQuery={query}
                updateQuery={setQuery}
            />
        </>
    );
};

export default Customer;
