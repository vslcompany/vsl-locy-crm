import { useCallback, useMemo, useState } from "react";
import { useMutation, useQuery, useQueryClient } from "react-query";
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
    createPosition,
    deletePosition,
    getPositions,
    updatePosition,
} from "@/api";
import { Button, Modal } from "@/components";
import { useAuth } from "@/contexts";
import { CreatePositionModal, UpdatePositionModal } from "@/modals";
import {
    TAuthContextProps,
    TCreatePositionRequest,
    TPositionDto,
    TTableColumn,
    TTableData,
    TUpdatePositionRequest,
} from "@/types";

type TColumn = TTableColumn<TPositionDto>;
type TData = TTableData<TPositionDto>;

const PositionTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState<string>("");
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenCreateModal, setIsOpenCreateModal] = useState<boolean>(false);
    const [isOpenUpdateModal, setIsOpenUpdateModal] = useState<boolean>(false);
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [dataSelectedUpdate, setDataSelectedUpdate] =
        useState<TUpdatePositionRequest | null>(null);
    const [idSelectedDelete, setIdSelectedDelete] = useState<number | null>(
        null
    );

    const { user }: TAuthContextProps = useAuth();

    const queryClient = useQueryClient();

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: [
            "positions",
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getPositions({
                start: pagination.pageIndex * pagination.pageSize,
                size: pagination.pageSize,
                search,
            }),
        onSuccess: (data) => {
            if (data.status) {
                setTableData(data.data.data);
                setTotalRow(data.data.totalRow);
            }
        },
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            (user?.permission.includes("1048576") ||
                user?.permission.includes("6000")),
        keepPreviousData: true,
        refetchOnWindowFocus: false,
    });

    const createMutation = useMutation({
        mutationFn: createPosition,
        onSuccess: (data) => {
            if (data.status) {
                closeCreateModal();
                refetch();
            }
        },
    });

    const updateMutation = useMutation({
        mutationFn: updatePosition,
        onSuccess: (data) => {
            if (data.status) {
                queryClient.invalidateQueries({ queryKey: "positions" });
                closeUpdateModal();
            }
        },
    });

    const deleteMutation = useMutation({
        mutationFn: deletePosition,
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
        async (data: TCreatePositionRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await createMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [createMutation]
    );

    const openUpdateModal = useCallback((data: TUpdatePositionRequest) => {
        setIsOpenUpdateModal(true);
        setDataSelectedUpdate(data);
    }, []);

    const closeUpdateModal = useCallback(() => {
        setIsOpenUpdateModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleUpdate = useCallback(
        async (data: TUpdatePositionRequest) => {
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
        async (index: number) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await deleteMutation.mutateAsync(index);
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
            },
            {
                accessorKey: "nameVI",
                header: "Tên VI",
                size: 360,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "nameEN",
                header: "Tên EN",
                size: 360,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
        ];
    }, []);

    const table = useMaterialReactTable({
        columns,
        data: tableData,
        enableStickyHeader: true,
        enableStickyFooter: true,
        muiTableContainerProps: { sx: { height: "640px" } },
        renderTopToolbarCustomActions: () => (
            <div className="flex-1 flex items-center gap-3">
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("6000")) && (
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
                    user?.permission.includes("6000")) && (
                    <>
                        <Tooltip arrow placement="left" title="Chỉnh sửa">
                            <IconButton
                                color="primary"
                                onClick={() => {
                                    const { id } = row.original;
                                    const data = tableData.find(
                                        (e) => e.id === id
                                    ) as TPositionDto;
                                    const payload: TUpdatePositionRequest = {
                                        id: data.id,
                                        code: data.code,
                                        nameVI: data.nameVI,
                                        nameEN: data.nameEN,
                                    };
                                    openUpdateModal(payload);
                                }}
                            >
                                <Edit />
                            </IconButton>
                        </Tooltip>
                    </>
                )}
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("6000")) && (
                    <>
                        <Tooltip arrow placement="right" title="Yêu cầu xoá">
                            <IconButton
                                color="error"
                                onClick={() => {
                                    const { id } = row.original as TPositionDto;
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

    return (
        <>
            <MaterialReactTable table={table} />
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("6000")) && (
                <CreatePositionModal
                    title="Thêm chức vụ"
                    isOpen={isOpenCreateModal}
                    onClose={closeCreateModal}
                    onSubmit={handleCreate}
                    isLoading={createMutation.isLoading}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("6000")) && (
                <UpdatePositionModal
                    title="Chỉnh sửa chức vụ"
                    isOpen={isOpenUpdateModal}
                    onClose={closeUpdateModal}
                    onSubmit={handleUpdate}
                    prevData={dataSelectedUpdate}
                    isLoading={updateMutation.isLoading}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("6000")) && (
                <Modal
                    isOpen={isOpenDeleteModal}
                    onClose={closeDeleteModal}
                    onSubmit={() => {
                        handleDeleteModal(idSelectedDelete as number);
                    }}
                    title="Xoá chức vụ"
                    description="bạn muốn thực hiện xoá chức vụ này?"
                    labelButtonCancel="huỷ"
                    labelButtonOk="xóa"
                    labelButtonOkLoading="đang xoá"
                    isButtonOkLoading={deleteMutation.isLoading}
                />
            )}
        </>
    );
};

export default PositionTable;
