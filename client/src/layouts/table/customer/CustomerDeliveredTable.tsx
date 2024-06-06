import { useCallback, useMemo, useState } from "react";
import { useMutation } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_RowSelectionState,
    useMaterialReactTable,
} from "material-react-table";

import { acceptCustomer, denyCustomer } from "@/api";
import { Button, GroupInput, Modal } from "@/components";
import { useAuth } from "@/contexts";
import {
    TAcceptCustomerRequest,
    TAuthContextProps,
    TCustomerDto,
    TDenyCustomerRequest,
    TTableColumn,
} from "@/types";

import { TCustomerTableProps } from "./types";

type TColumn = TTableColumn<TCustomerDto>;

const CustomerDeliveredTable = ({
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

    const [isOpenAcceptModal, setIsOpenAcceptModal] = useState<boolean>(false);
    const [isOpenDenyModal, setIsOpenDenyModal] = useState<boolean>(false);
    const [dataSelected, setDataSelected] = useState<number[] | []>([]);
    const [denyString, setDenyString] = useState<string>("");

    const { user }: TAuthContextProps = useAuth();

    const acceptMutation = useMutation({
        mutationFn: acceptCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeAcceptModal();
            }
        },
    });

    const denyMutation = useMutation({
        mutationFn: denyCustomer,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                // Unselected row
                setRowSelection({});
                // Reset data state
                setDataSelected([]);
                closeDenyModal();
            }
        },
    });

    /**
     * * Handle events
     */
    const openAcceptModal = useCallback(() => {
        setIsOpenAcceptModal(true);
    }, []);

    const closeAcceptModal = useCallback(() => {
        setIsOpenAcceptModal(false);
    }, []);

    const handleAccept = useCallback(
        async (data: TAcceptCustomerRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await acceptMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [acceptMutation]
    );

    const openDenyModal = useCallback(() => {
        setIsOpenDenyModal(true);
    }, []);

    const closeDenyModal = useCallback(() => {
        setIsOpenDenyModal(false);
    }, []);

    const handleDeny = useCallback(
        async (data: TDenyCustomerRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await denyMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [denyMutation]
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
                        <Button
                            variant="contained"
                            size="sm"
                            label="nhận"
                            rounded="lg"
                            color="green"
                            onClick={() => {
                                const items = table
                                    .getSelectedRowModel()
                                    .flatRows.map((row) => data[row.index].id);
                                setDataSelected(items);
                                openAcceptModal();
                            }}
                        />
                        <Button
                            variant="contained"
                            size="sm"
                            label="từ chối"
                            rounded="lg"
                            color="red"
                            onClick={() => {
                                const items = table
                                    .getSelectedRowModel()
                                    .flatRows.map((row) => data[row.index].id);
                                setDataSelected(items);
                                openDenyModal();
                            }}
                        />
                    </>
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
            <Modal
                isOpen={isOpenAcceptModal}
                onClose={closeAcceptModal}
                onSubmit={() => {
                    handleAccept({
                        idNhanVienSale: user?.idNhanVien as number,
                        idCustomers: dataSelected,
                    });
                }}
                title="Nhận khách hàng"
                description="bạn chắc chắn muốn nhận khách hàng đã chọn"
                labelButtonCancel="huỷ"
                labelButtonOk="nhận khách"
                isButtonOkLoading={acceptMutation.isLoading}
                labelButtonOkLoading="đang nhận"
            />
            <Modal
                isOpen={isOpenDenyModal}
                onClose={closeDenyModal}
                onSubmit={() => {
                    handleDeny({
                        idCustomers: dataSelected,
                        idNhanVienSale: user?.idNhanVien as number,
                        lyDoTuChoi: denyString,
                    });
                }}
                title="Từ chối nhận khách hàng"
                labelButtonCancel="huỷ"
                labelButtonOk="từ chối"
                isButtonOkLoading={denyMutation.isLoading}
                labelButtonOkLoading="đang từ chối"
            >
                <div className="grid gap-4">
                    <GroupInput
                        labelFor="lyDoTuChoi"
                        labelText="Lý do từ chối"
                        value={denyString}
                        onChange={(e) => setDenyString(e.target.value)}
                    />
                </div>
            </Modal>
        </>
    );
};

export default CustomerDeliveredTable;
