import { useCallback, useMemo, useState } from "react";
import { useMutation } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    useMaterialReactTable,
} from "material-react-table";
import { Box, IconButton, Tooltip } from "@mui/material";
import { Delete } from "@mui/icons-material";
import { SettingsBackupRestore } from "@mui/icons-material";

import { deleteCustomer, removeCustomer } from "@/api";
import { Modal } from "@/components";
import { useAuth } from "@/contexts";
import {
    TAuthContextProps,
    TDeleteCustomerRequest,
    TCustomerDto,
    TTableColumn,
} from "@/types";

type TColumn = TTableColumn<TCustomerDto>;

import { TCustomerTableProps } from "./types";

const CustomerDeleteTable = ({
    data,
    pagination,
    setPagination,
    totalRow,
    isError,
    isFetching,
    isLoading,
    refetch,
}: TCustomerTableProps) => {
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [isOpenUndeleteModal, setIsOpenUndeleteModal] =
        useState<boolean>(false);
    const [idSelectedDelete, setIdSelectedDelete] = useState<number | null>(
        null
    );

    const { user }: TAuthContextProps = useAuth();

    const deleteMutation = useMutation({
        mutationFn: removeCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                closeDeleteModal();
            }
        },
    });

    const undeleteMutation = useMutation({
        mutationFn: deleteCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                closeUndeleteModal();
            }
        },
    });

    /**
     * * Handle events
     */
    const openDeleteModal = useCallback(() => {
        setIsOpenDeleteModal(true);
    }, []);

    const closeDeleteModal = useCallback(() => {
        setIdSelectedDelete(null);
        setIsOpenDeleteModal(false);
    }, []);

    const handleDeleteModal = useCallback(
        async (id: number) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await deleteMutation.mutateAsync(id);
            return result.status as boolean;
        },
        [deleteMutation]
    );

    const openUndeleteModal = useCallback(() => {
        setIsOpenUndeleteModal(true);
    }, []);

    const closeUndeleteModal = useCallback(() => {
        setIdSelectedDelete(null);
        setIsOpenUndeleteModal(false);
    }, []);

    const handleUndeleteModal = useCallback(
        async (data: TDeleteCustomerRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await undeleteMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [undeleteMutation]
    );

    /**
     * * Material table configuration
     */
    const columns = useMemo<MRT_ColumnDef<TColumn>[]>(() => {
        return [
            {
                accessorKey: "code",
                header: "Mã",
                size: 120,
                muiTableBodyCellProps: ({ row }) => ({
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "taxCode",
                header: "Mã số thuế",
                size: 120,
                muiTableBodyCellProps: ({ row }) => ({
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "nameVI",
                header: "Tên khách hàng",
                size: 360,
                muiTableBodyCellProps: ({ row }) => ({
                    className: "capitalize",
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "addressVI",
                header: "Địa chỉ",
                size: 360,
                muiTableBodyCellProps: ({ row }) => ({
                    className: "first-letter:uppercase",
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "thanhPho",
                header: "Thành phố",
                size: 240,
                muiTableBodyCellProps: ({ row }) => ({
                    className: "capitalize",
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "quocGia",
                header: "Quốc gia",
                size: 240,
                muiTableBodyCellProps: ({ row }) => ({
                    className: "capitalize",
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "phone",
                header: "Số điện thoại",
                muiTableBodyCellProps: ({ row }) => ({
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "email",
                header: "Thư điện tử",
                muiTableBodyCellProps: ({ row }) => ({
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "website",
                header: "Website",
                muiTableBodyCellProps: ({ row }) => ({
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "note",
                header: "Ghi chú",
                muiTableBodyCellProps: ({ row }) => ({
                    className: "first-letter:uppercase",
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "nguoiXoa",
                header: "Người xoá",
                muiTableBodyCellProps: ({ row }) => ({
                    className: "capitalize",
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
                size: 240,
            },
            {
                accessorKey: "ngayXoa",
                header: "Ngày xoá",
                size: 180,
                muiTableBodyCellProps: ({ row }) => ({
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
            {
                accessorKey: "lyDoXoa",
                header: "Lý do xoá",
                size: 360,
                muiTableBodyCellProps: ({ row }) => ({
                    className: "first-letter:uppercase",
                    style: {
                        backgroundColor: row.original.mauTacNghiepCuoi,
                        color:
                            row.original.mauTacNghiepCuoi != ""
                                ? "white"
                                : "inherit",
                    },
                }),
            },
        ];
    }, []);

    const table = useMaterialReactTable({
        columns,
        data: data,
        enableStickyHeader: true,
        enableStickyFooter: true,
        muiTableContainerProps: { sx: { maxHeight: "640px" } },
        renderRowActions: ({ row }) => (
            <Box sx={{ display: "flex", gap: "0.25rem" }}>
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("7000") ||
                    user?.permission.includes("7020")) && (
                    <>
                        <Tooltip
                            arrow
                            placement="right"
                            title="Yêu cầu huỷ xoá"
                        >
                            <IconButton
                                color="info"
                                onClick={() => {
                                    const { id } = row.original as TCustomerDto;
                                    setIdSelectedDelete(id);
                                    openUndeleteModal();
                                }}
                            >
                                <SettingsBackupRestore />
                            </IconButton>
                        </Tooltip>
                    </>
                )}
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("7000") ||
                    user?.permission.includes("7020")) && (
                    <>
                        <Tooltip arrow placement="right" title="Xoá">
                            <IconButton
                                color="error"
                                onClick={() => {
                                    const { id } = row.original as TCustomerDto;
                                    setIdSelectedDelete(id);
                                    openDeleteModal();
                                }}
                            >
                                <Delete />
                            </IconButton>
                        </Tooltip>
                    </>
                )}
            </Box>
        ),
        renderEmptyRowsFallback: () => (
            <div className="px-2 py-6">
                <p className="section-subtitle first-letter:uppercase">
                    không có dữ liệu
                </p>
            </div>
        ),
        initialState: {
            pagination,
            columnPinning: {
                right: ["mrt-row-actions"],
            },
        },
        enableEditing: true,
        manualFiltering: true,
        manualPagination: true,
        onPaginationChange: setPagination,
        rowCount: totalRow,
        state: {
            isLoading,
            pagination,
            showAlertBanner: isError,
            showProgressBars: isFetching,
        },
    });

    return (
        <>
            <MaterialReactTable table={table} />
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7020")) && (
                <Modal
                    isOpen={isOpenDeleteModal}
                    onClose={closeDeleteModal}
                    onSubmit={() => {
                        handleDeleteModal(idSelectedDelete as number);
                    }}
                    title="Xoá khách hàng"
                    description="bạn chắc chắn muốn xoá hàng đã chọn"
                    labelButtonCancel="huỷ"
                    labelButtonOk="xoá"
                    isButtonOkLoading={deleteMutation.isLoading}
                    labelButtonOkLoading="đang xoá"
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7020")) && (
                <Modal
                    isOpen={isOpenUndeleteModal}
                    onClose={closeUndeleteModal}
                    onSubmit={() => {
                        handleUndeleteModal({
                            id: idSelectedDelete as number,
                            flagDel: false,
                            idUserDelete: null,
                            lyDoXoa: "",
                        });
                    }}
                    title="Huỷ xoá khách hàng"
                    description="bạn chắc chắn muốn huỷ xoá khách hàng đã chọn"
                    labelButtonCancel="huỷ"
                    labelButtonOk="huỷ xoá"
                    isButtonOkLoading={undeleteMutation.isLoading}
                    labelButtonOkLoading="đang huỷ xoá"
                />
            )}
        </>
    );
};

export default CustomerDeleteTable;
