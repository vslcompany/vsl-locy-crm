import { useCallback, useMemo, useState } from "react";
import { useMutation } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_RowSelectionState,
    useMaterialReactTable,
} from "material-react-table";

import { undeliveryCustomer } from "@/api";
import { Button, Modal } from "@/components";
import { useAuth } from "@/contexts";
import {
    TAuthContextProps,
    TCustomerDto,
    TTableColumn,
    TUndeliveryCustomerRequest,
} from "@/types";

import { TCustomerTableProps } from "./types";

type TColumn = TTableColumn<TCustomerDto>;

const CustomerAssignedTable = ({
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

    const [isOpenUndeliveryModal, setIsOpenUndeliveryModal] =
        useState<boolean>(false);
    const [dataSelected, setDataSelected] = useState<number[] | []>([]);

    const { user }: TAuthContextProps = useAuth();

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
                accessorKey: "nhanVien",
                header: "Người được giao",
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
                accessorKey: "ngayGiao",
                header: "Thời gian giao",
                size: 180,
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
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("7000") ||
                    user?.permission.includes("7080")) &&
                    (table.getIsSomePageRowsSelected() ||
                        table.getIsAllPageRowsSelected()) && (
                        <Button
                            variant="contained"
                            size="sm"
                            label="huỷ giao khách"
                            rounded="lg"
                            color="cyan"
                            onClick={() => {
                                const items = table
                                    .getSelectedRowModel()
                                    .flatRows.map((row) => data[row.index].id);
                                setDataSelected(items);
                                openUndeliveryModal();
                            }}
                        />
                    )}
            </div>
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
        </>
    );
};

export default CustomerAssignedTable;
