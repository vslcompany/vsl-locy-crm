import { useState, useMemo, useCallback, useEffect } from "react";
import { useQuery, useMutation, useQueryClient } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    useMaterialReactTable,
} from "material-react-table";
import { Box, IconButton, Tooltip } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";
import { FaPlus } from "react-icons/fa6";

import {
    createCustomerEvaluate,
    deleteCustomerEvaluate,
    getCustomerEvaluates,
    updateCustomerEvaluate,
} from "@/api";
import { Button, Modal } from "@/components";
import { useAuth } from "@/contexts";
import {
    CreateCustomerEvaluateModal,
    UpdateCustomerEvaluateModal,
} from "@/modals";
import {
    TAuthContextProps,
    TCreateCustomerEvaluateRequest,
    TCustomerEvaluateDto,
    TTableColumn,
    TTableData,
    TUpdateCustomerEvaluateRequest,
} from "@/types";

import { TTableProps } from "./types";

type TColumn = TTableColumn<TCustomerEvaluateDto>;
type TData = TTableData<TCustomerEvaluateDto>;

const CustomerEvaluateTable = ({
    id,
    isRefresh,
    onRefreshDone,
}: TTableProps) => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState("");
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenCreateModal, setIsOpenCreateModal] = useState<boolean>(false);
    const [isOpenUpdateModal, setIsOpenUpdateModal] = useState<boolean>(false);
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [dataSelectedUpdate, setDataSelectedUpdate] =
        useState<TUpdateCustomerEvaluateRequest | null>(null);
    const [idSelected, setIdSelected] = useState<number | null>(null);

    const { user }: TAuthContextProps = useAuth();

    const queryClient = useQueryClient();

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: [
            ["evaluates", id],
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getCustomerEvaluates({
                start: pagination.pageIndex * pagination.pageSize,
                size: pagination.pageSize,
                search,
                idCustomer: id,
            }),
        onSuccess: (data) => {
            if (data.status) {
                setTableData(data.data.data);
                setTotalRow(data.data.totalRow);
            }
        },
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
        keepPreviousData: true,
        refetchOnWindowFocus: false,
    });

    const createMutation = useMutation({
        mutationFn: createCustomerEvaluate,
        onSuccess: (data) => {
            if (data.status) {
                closeCreateModal();
                refetch();
            }
        },
    });

    const updateMutation = useMutation({
        mutationFn: updateCustomerEvaluate,
        onSuccess: (data) => {
            if (data.status) {
                queryClient.invalidateQueries({
                    queryKey: [
                        ["evaluates", id],
                        search, //refetch when search changes
                        pagination.pageIndex, //refetch when pagination.pageIndex changes
                        pagination.pageSize, //refetch when pagination.pageSize changes
                    ],
                });
                closeUpdateModal();
            }
        },
    });

    const deleteMutation = useMutation({
        mutationFn: deleteCustomerEvaluate,
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
    const openCreateModal = useCallback(() => {
        setIsOpenCreateModal(true);
    }, []);

    const closeCreateModal = useCallback(() => {
        setIsOpenCreateModal(false);
    }, []);

    const handleCreate = useCallback(
        async (data: TCreateCustomerEvaluateRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await createMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [createMutation]
    );

    const openUpdateModal = useCallback(
        (data: TUpdateCustomerEvaluateRequest) => {
            setIsOpenUpdateModal(true);
            setDataSelectedUpdate(data);
        },
        []
    );

    const closeUpdateModal = useCallback(() => {
        setIsOpenUpdateModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleUpdate = useCallback(
        async (data: TUpdateCustomerEvaluateRequest) => {
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
        setIdSelected(null);
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

    /**
     * * Material table configuration
     */
    const columns = useMemo<MRT_ColumnDef<TColumn>[]>(() => {
        return [
            {
                accessorKey: "loaiKhachHang",
                header: "Loại khách hàng",
                size: 360,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "ghiChu",
                header: "Ghi chú",
            },
            {
                accessorKey: "nguoiTao",
                header: "Người tạo",
                size: 240,
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "dateCreate",
                header: "Ngày tạo",
                size: 180,
            },
        ];
    }, []);

    const table = useMaterialReactTable({
        columns,
        data: tableData,
        enableStickyHeader: true,
        enableStickyFooter: true,
        muiTableContainerProps: { sx: { maxHeight: "640px" } },
        renderTopToolbarCustomActions: () => (
            <div className="flex-1 flex items-center gap-3">
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("7000") ||
                    user?.permission.includes("7020")) && (
                    <Button
                        variant="contained"
                        size="sm"
                        label="tạo mới"
                        rounded="lg"
                        color="green"
                        onClick={openCreateModal}
                        icon={<FaPlus />}
                    />
                )}
            </div>
        ),
        renderRowActions: ({ row }) => (
            <Box sx={{ display: "flex", gap: "0.25rem" }}>
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("7000") ||
                    user?.permission.includes("7020")) && (
                    <Tooltip arrow placement="left" title="Chỉnh sửa">
                        <IconButton
                            color="primary"
                            onClick={() => {
                                const { id } = row.original;
                                const data = tableData.find(
                                    (e) => e.id === id
                                ) as TCustomerEvaluateDto;
                                const payload: TUpdateCustomerEvaluateRequest =
                                    {
                                        id: data.id,
                                        idCustomer: data.idCustomer,
                                        idCustomerType: data.idCustomerType,
                                        ghiChu: data.ghiChu,
                                    };
                                openUpdateModal(payload);
                            }}
                        >
                            <Edit />
                        </IconButton>
                    </Tooltip>
                )}
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("7000") ||
                    user?.permission.includes("7020")) && (
                    <Tooltip arrow placement="right" title="Yêu cầu xoá">
                        <IconButton
                            color="error"
                            onClick={() => {
                                const { id } =
                                    row.original as TCustomerEvaluateDto;
                                setIdSelected(id);
                                openDeleteModal();
                            }}
                        >
                            <Delete />
                        </IconButton>
                    </Tooltip>
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
        onGlobalFilterChange: setSearch,
        onPaginationChange: setPagination,
        rowCount: totalRow,
        state: {
            globalFilter: search,
            isLoading,
            pagination,
            showAlertBanner: isError,
            showProgressBars: isFetching,
        },
    });

    useEffect(() => {
        if (isRefresh) {
            refetch();
            onRefreshDone();
        }
    }, [isRefresh, onRefreshDone, refetch]);

    return (
        <>
            <MaterialReactTable table={table} />
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7020")) && (
                <CreateCustomerEvaluateModal
                    title="Thêm đánh giá"
                    isOpen={isOpenCreateModal}
                    onClose={closeCreateModal}
                    onSubmit={handleCreate}
                    isLoading={createMutation.isLoading}
                    idCustomer={id}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7020")) && (
                <UpdateCustomerEvaluateModal
                    title="Chỉnh sửa đánh giá"
                    isOpen={isOpenUpdateModal}
                    onClose={closeUpdateModal}
                    onSubmit={handleUpdate}
                    prevData={dataSelectedUpdate}
                    isLoading={updateMutation.isLoading}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("7000") ||
                user?.permission.includes("7020")) && (
                <Modal
                    isOpen={isOpenDeleteModal}
                    onClose={closeDeleteModal}
                    onSubmit={() => {
                        handleDeleteModal(idSelected as number);
                    }}
                    title="Xoá đánh giá"
                    description="bạn muốn thực hiện xoá đánh giá này?"
                    labelButtonCancel="huỷ"
                    labelButtonOk="xóa"
                    labelButtonOkLoading="đang xoá"
                    isButtonOkLoading={deleteMutation.isLoading}
                />
            )}
        </>
    );
};

export default CustomerEvaluateTable;
