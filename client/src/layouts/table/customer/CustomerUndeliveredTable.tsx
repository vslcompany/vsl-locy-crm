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
import { BiSolidInfoCircle } from "react-icons/bi";

import { chooseCustomer, deliveryCustomer } from "@/api";
import { Button, Modal } from "@/components";
import { useAuth } from "@/contexts";
import { DeliveryCustomerModal } from "@/modals";
import {
    TAuthContextProps,
    TChooseCustomerRequest,
    TCustomerDto,
    TDeliveryCustomerRequest,
    TTableColumn,
} from "@/types";

import { TCustomerTableProps } from "./types";

type TColumn = TTableColumn<TCustomerDto>;

const CustomerUndeliveredTable = ({
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

    const [isOpenDeliveryModal, setIsOpenDeliveryModal] =
        useState<boolean>(false);
    const [isOpenChooseModal, setIsOpenChooseModal] = useState<boolean>(false);
    const [dataSelected, setDataSelected] = useState<number[] | []>([]);

    const { user }: TAuthContextProps = useAuth();

    const navigate = useNavigate();

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

    const chooseMutation = useMutation({
        mutationFn: chooseCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeChooseModal();
            }
        },
    });

    /**
     * * Handle events
     */
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

    const openChooseModal = useCallback(() => {
        setIsOpenChooseModal(true);
    }, []);

    const closeChooseModal = useCallback(() => {
        setIsOpenChooseModal(false);
    }, []);

    const handleChoose = useCallback(
        async (data: TChooseCustomerRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await chooseMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [chooseMutation]
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
                accessorKey: "nguoiTao",
                header: "Người tạo",
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
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("7000") ||
                    user?.permission.includes("7080")) &&
                    (table.getIsSomePageRowsSelected() ||
                        table.getIsAllPageRowsSelected()) && (
                        <Button
                            variant="contained"
                            size="sm"
                            label="giao khách"
                            rounded="lg"
                            color="cyan"
                            onClick={() => {
                                const items = table
                                    .getSelectedRowModel()
                                    .flatRows.map((row) => data[row.index].id);
                                setDataSelected(items);
                                openDeliveryModal();
                            }}
                        />
                    )}
                {user?.username.toLowerCase() !== "admin" &&
                    (table.getIsSomePageRowsSelected() ||
                        table.getIsAllPageRowsSelected()) && (
                        <Button
                            variant="contained"
                            size="sm"
                            label="nhận khách"
                            rounded="lg"
                            color="teal"
                            onClick={() => {
                                const items = table
                                    .getSelectedRowModel()
                                    .flatRows.filter(
                                        (row) =>
                                            data[row.index].enumGiaoNhan ===
                                                0 ||
                                            data[row.index].enumGiaoNhan === 3
                                    )
                                    .map((row) => data[row.index].id);
                                setDataSelected(items);
                                openChooseModal();
                            }}
                        />
                    )}
            </div>
        ),
        renderRowActions: ({ row }) => (
            <Box sx={{ display: "flex", gap: "0.25rem" }}>
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
            {user?.username.toLowerCase() !== "admin" && (
                <Modal
                    isOpen={isOpenChooseModal}
                    onClose={closeChooseModal}
                    onSubmit={() => {
                        handleChoose({
                            idNhanVienSale: user?.idNhanVien as number,
                            idCustomers: dataSelected,
                        });
                    }}
                    title="Nhận khách hàng"
                    description="bạn chắc chắn muốn nhận khách hàng đã chọn"
                    labelButtonCancel="huỷ"
                    labelButtonOk="nhận khách"
                    isButtonOkLoading={chooseMutation.isLoading}
                    labelButtonOkLoading="đang nhận"
                />
            )}
        </>
    );
};

export default CustomerUndeliveredTable;
