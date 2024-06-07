import { useCallback, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useMutation } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_RowSelectionState,
    useMaterialReactTable,
} from "material-react-table";
import { Box, IconButton, Tooltip } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";
import { BiSolidInfoCircle } from "react-icons/bi";

import {
    deleteCustomer,
    deliveryCustomer,
    undeliveryCustomer,
    updateCustomer,
} from "@/api";
import { Button, GroupInput, Modal } from "@/components";
import { useAuth } from "@/contexts";
import { DeliveryCustomerModal, UpdateCustomerModal } from "@/modals";
import {
    TAuthContextProps,
    TCustomerDto,
    TDeleteCustomerRequest,
    TDeliveryCustomerRequest,
    TTableColumn,
    TUndeliveryCustomerRequest,
    TUpdateCustomerRequest,
} from "@/types";

import { TCustomerTableProps } from "./types";

type TColumn = TTableColumn<TCustomerDto>;

const CustomerReceivedTable = ({
    data,
    pagination,
    setPagination,
    totalRow,
    isError,
    isFetching,
    isLoading,
    refetch,
}: TCustomerTableProps) => {
    const [rowSelection, setRowSelection] = useState<MRT_RowSelectionState>({});

    const [isOpenReturnModal, setIsOpenReturnModal] = useState<boolean>(false);
    const [isOpenUndeliveryModal, setIsOpenUndeliveryModal] =
        useState<boolean>(false);
    const [isOpenDeliveryModal, setIsOpenDeliveryModal] =
        useState<boolean>(false);
    const [isOpenUpdateModal, setIsOpenUpdateModal] = useState<boolean>(false);
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [dataSelectedUpdate, setDataSelectedUpdate] =
        useState<TUpdateCustomerRequest | null>(null);
    const [dataSelected, setDataSelected] = useState<number[] | []>([]);
    const [idSelectedDelete, setIdSelectedDelete] = useState<number | null>(
        null
    );
    const [deleteString, setDeleteString] = useState<string>("");

    const { user }: TAuthContextProps = useAuth();

    const navigate = useNavigate();

    const undeliveryMutation = useMutation({
        mutationFn: undeliveryCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeUndeliveryModal();
            }
        },
    });

    const deliveryMutation = useMutation({
        mutationFn: deliveryCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeDeliveryModal();
            }
        },
    });

    const returnMutation = useMutation({
        mutationFn: undeliveryCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeReturnModal();
            }
        },
    });

    const updateMutation = useMutation({
        mutationFn: updateCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                closeUpdateModal();
            }
        },
    });

    const deleteMutation = useMutation({
        mutationFn: deleteCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                closeDeleteModal();
            }
        },
    });

    /**
     * * Handle events
     */
    const openUndeliveryModal = useCallback(() => {
        setIsOpenUndeliveryModal(true);
    }, []);

    const closeUndeliveryModal = useCallback(() => {
        setIsOpenUndeliveryModal(false);
    }, []);

    const handleUndelivery = useCallback(
        async (data: TUndeliveryCustomerRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await undeliveryMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [undeliveryMutation]
    );

    const openDeliveryModal = useCallback(() => {
        setIsOpenDeliveryModal(true);
    }, []);

    const closeDeliveryModal = useCallback(() => {
        setIsOpenDeliveryModal(false);
    }, []);

    const handleDelivery = useCallback(
        async (data: TDeliveryCustomerRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await deliveryMutation.mutateAsync(data);

            return result.status as boolean;
        },
        [deliveryMutation]
    );

    const openReturnModal = useCallback(() => {
        setIsOpenReturnModal(true);
    }, []);

    const closeReturnModal = useCallback(() => {
        setIsOpenReturnModal(false);
    }, []);

    const handleReturn = useCallback(
        async (data: TUndeliveryCustomerRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await returnMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [returnMutation]
    );

    const openUpdateModal = useCallback((data: TUpdateCustomerRequest) => {
        setIsOpenUpdateModal(true);
        setDataSelectedUpdate(data);
    }, []);

    const closeUpdateModal = useCallback(() => {
        setIsOpenUpdateModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleUpdate = useCallback(
        async (data: TUpdateCustomerRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await updateMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [updateMutation]
    );

    const openDeleteModal = useCallback(() => {
        setIsOpenDeleteModal(true);
    }, []);

    const closeDeleteModal = useCallback(() => {
        setIdSelectedDelete(null);
        setIsOpenDeleteModal(false);
    }, []);

    const handleDeleteModal = useCallback(
        async (data: TDeleteCustomerRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await deleteMutation.mutateAsync(data);

            return result.status as boolean;
        },
        [deleteMutation]
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
                accessorKey: "nhanVien",
                header: "Nhân viên quản lý",
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
                accessorKey: "nguoiGiaoViec",
                header: "Người giao",
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
                accessorKey: "ngayNhan",
                header: "Thời gian nhận",
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
                accessorKey: "thongTinGiaoViec",
                header: "Thông tin giao việc",
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
        renderTopToolbar: ({ table }) => (
            <div className="flex-1 flex items-center gap-3 p-4">
                {(table.getIsSomePageRowsSelected() ||
                    table.getIsAllPageRowsSelected()) && (
                    <>
                        {(user?.permission.includes("1048576") ||
                            user?.permission.includes("7000") ||
                            user?.permission.includes("7080")) && (
                            <>
                                <Button
                                    variant="contained"
                                    size="sm"
                                    label="chuyển khách"
                                    rounded="lg"
                                    color="green"
                                    onClick={() => {
                                        const items = table
                                            .getSelectedRowModel()
                                            .flatRows.map(
                                                (row) => data[row.index].id
                                            );
                                        setDataSelected(items);
                                        openDeliveryModal();
                                    }}
                                />
                            </>
                        )}
                        {(user?.permission.includes("1048576") ||
                            user?.permission.includes("7000") ||
                            user?.permission.includes("7080")) &&
                            !table
                                .getSelectedRowModel()
                                .flatRows.every(
                                    (row) =>
                                        user?.idNhanVien ===
                                        row.original.idNhanVienSale
                                ) && (
                                <Button
                                    variant="contained"
                                    size="sm"
                                    label="huỷ giao"
                                    rounded="lg"
                                    color="red"
                                    onClick={() => {
                                        const items = table
                                            .getSelectedRowModel()
                                            .flatRows.map(
                                                (row) => data[row.index].id
                                            );
                                        setDataSelected(items);
                                        openUndeliveryModal();
                                    }}
                                />
                            )}
                        {!user?.permission.includes("1048576") &&
                            table
                                .getSelectedRowModel()
                                .flatRows.every(
                                    (row) =>
                                        user?.idNhanVien ===
                                        row.original.idNhanVienSale
                                ) && (
                                <Button
                                    variant="contained"
                                    size="sm"
                                    label="trả khách"
                                    rounded="lg"
                                    color="orange"
                                    onClick={() => {
                                        const items = table
                                            .getSelectedRowModel()
                                            .flatRows.map(
                                                (row) => data[row.index].id
                                            );
                                        setDataSelected(items);
                                        openReturnModal();
                                    }}
                                />
                            )}
                    </>
                )}
            </div>
        ),
        renderRowActions: ({ row }) => (
            <Box sx={{ display: "flex", gap: "0.25rem" }}>
                <Tooltip arrow placement="left" title="Chỉnh sửa">
                    <IconButton
                        color="primary"
                        onClick={() => {
                            const { id } = row.original;
                            const item = data.find(
                                (e) => e.id === id
                            ) as TCustomerDto;
                            const payload: TUpdateCustomerRequest = {
                                id: item.id,
                                idLoaiDoanhNghiep: item.idLoaiDoanhNghiep,
                                idQuocGia: item.idQuocGia,
                                idCity: item.idCity,
                                code: item.code,
                                nameVI: item.nameVI,
                                nameEN: item.nameEN,
                                addressVI: item.addressVI,
                                addressEN: item.addressEN,
                                taxCode: item.taxCode,
                                phone: item.phone,
                                fax: item.fax,
                                email: item.email,
                                website: item.website,
                                note: item.note,
                            };
                            openUpdateModal(payload);
                        }}
                    >
                        <Edit />
                    </IconButton>
                </Tooltip>
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("7000") ||
                    user?.permission.includes("7020")) && (
                    <>
                        <Tooltip arrow placement="left" title="Yêu cầu xoá">
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
                <Tooltip arrow placement="left" title="Thông tin chi tiết">
                    <IconButton
                        color="info"
                        onClick={() => {
                            const { id } = row.original as TCustomerDto;
                            navigate(`/customer/${id}`);
                        }}
                    >
                        <BiSolidInfoCircle />
                    </IconButton>
                </Tooltip>
            </Box>
        ),
        renderEmptyRowsFallback: () => (
            <div className="px-2 py-6">
                <p className="subtitle first-letter:uppercase">
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
        enableRowSelection: true,
        manualFiltering: true,
        manualPagination: true,
        onPaginationChange: setPagination,
        onRowSelectionChange: setRowSelection,
        rowCount: totalRow,
        state: {
            isLoading,
            pagination,
            rowSelection: rowSelection,
            showAlertBanner: isError,
            showProgressBars: isFetching,
        },
    });

    return (
        <>
            <MaterialReactTable table={table} />
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7080")) && (
                <DeliveryCustomerModal
                    isOpen={isOpenDeliveryModal}
                    onClose={closeDeliveryModal}
                    onSubmit={handleDelivery}
                    title="Giao khách hàng"
                    labelButtonCancel="huỷ"
                    labelButtonOk="giao khách"
                    isLoading={deliveryMutation.isLoading}
                    idCustomers={dataSelected}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7080")) && (
                <Modal
                    isOpen={isOpenUndeliveryModal}
                    onClose={closeUndeliveryModal}
                    onSubmit={() => {
                        handleUndelivery({
                            idCustomers: dataSelected,
                        });
                    }}
                    title="Huỷ giao khách hàng"
                    description="bạn chắc chắn muốn huỷ giao khách hàng đã chọn"
                    labelButtonCancel="huỷ"
                    labelButtonOk="huỷ giao"
                    isButtonOkLoading={undeliveryMutation.isLoading}
                    labelButtonOkLoading="đang huỷ giao"
                />
            )}
            <Modal
                isOpen={isOpenReturnModal}
                onClose={closeReturnModal}
                onSubmit={() => {
                    handleReturn({
                        idCustomers: dataSelected,
                    });
                }}
                title="trả khách hàng"
                description="bạn chắc chắn muốn trả khách hàng đã chọn"
                labelButtonCancel="huỷ"
                labelButtonOk="trả khách"
                isButtonOkLoading={returnMutation.isLoading}
                labelButtonOkLoading="đang trả khách"
            />
            <UpdateCustomerModal
                isOpen={isOpenUpdateModal}
                onClose={closeUpdateModal}
                onSubmit={handleUpdate}
                title="Chỉnh sửa khách hàng"
                width="7xl"
                labelButtonCancel="huỷ"
                labelButtonOk="cập nhật"
                prevData={dataSelectedUpdate}
                isLoading={updateMutation.isLoading}
            />
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7020")) && (
                <Modal
                    isOpen={isOpenDeleteModal}
                    onClose={closeDeleteModal}
                    onSubmit={() => {
                        handleDeleteModal({
                            id: idSelectedDelete as number,
                            flagDel: true,
                            idUserDelete: user?.id as number,
                            lyDoXoa: deleteString,
                        });
                    }}
                    title="Xoá khách hàng"
                    description="bạn muốn thực hiện xoá khách hàng này?"
                    labelButtonCancel="huỷ"
                    labelButtonOk="yêu cầu xóa"
                    isButtonOkLoading={deleteMutation.isLoading}
                    labelButtonOkLoading="đang xoá"
                >
                    <div className="grid gap-4">
                        <GroupInput
                            labelFor="lyDoXoa"
                            labelText="Lý do xoá"
                            value={deleteString}
                            onChange={(e) => setDeleteString(e.target.value)}
                        />
                    </div>
                </Modal>
            )}
        </>
    );
};

export default CustomerReceivedTable;
