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
    createOperational,
    deleteOperational,
    getOperationals,
    updateOperational,
} from "@/api";
import { Button, Modal } from "@/components";
import { useAuth } from "@/contexts";
import { CreateOperationalModal, UpdateOperationalModal } from "@/modals";
import {
    TAuthContextProps,
    TCreateOperationalRequest,
    TOperationalDto,
    TTableColumn,
    TTableData,
    TUpdateOperationalRequest,
} from "@/types";
import { rgbToHex } from "@/utilities";

type TColumn = TTableColumn<TOperationalDto>;
type TData = TTableData<TOperationalDto>;

const OperationalTable = () => {
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
        useState<TUpdateOperationalRequest | null>(null);
    const [idSelectedDelete, setIdSelectedDelete] = useState<number | null>(
        null
    );

    const { user }: TAuthContextProps = useAuth();

    const queryClient = useQueryClient();

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: [
            "operationals",
            search, //refetch when search changes
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
        ],
        queryFn: () =>
            getOperationals({
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
        mutationFn: createOperational,
        onSuccess: (data) => {
            if (data.status) {
                closeCreateModal();
                refetch();
            }
        },
    });

    const updateMutation = useMutation({
        mutationFn: updateOperational,
        onSuccess: (data) => {
            if (data.status) {
                queryClient.invalidateQueries({ queryKey: "operationals" });
                closeUpdateModal();
            }
        },
    });

    const deleteMutation = useMutation({
        mutationFn: deleteOperational,
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
        async (data: TCreateOperationalRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await createMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [createMutation]
    );

    const openUpdateModal = useCallback((data: TUpdateOperationalRequest) => {
        setIsOpenUpdateModal(true);
        setDataSelectedUpdate(data);
    }, []);

    const closeUpdateModal = useCallback(() => {
        setIsOpenUpdateModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleUpdate = useCallback(
        async (data: TUpdateOperationalRequest) => {
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
                header: "Màu hiển thị",
                accessorFn: (row) => {
                    const colorToHex =
                        row.r !== undefined &&
                        row.g !== undefined &&
                        row.b !== undefined
                            ? rgbToHex(row.r, row.g, row.b)
                            : "";

                    return (
                        <div
                            className={`w-8 sm:w-16 aspect-square border border-gray-300 dark:border-gray-600 rounded ${
                                colorToHex !== "" ? colorToHex : ""
                            }`}
                            style={{
                                backgroundColor:
                                    colorToHex !== ""
                                        ? colorToHex
                                        : "transparent",
                            }}
                        ></div>
                    );
                },
                size: 200,
            },
            {
                accessorKey: "name",
                header: "Tên",
                size: 360,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "ngayTuTraKhach",
                header: "Ngày tự trả khách",
                size: 120,
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
                                    ) as TOperationalDto;
                                    const payload: TUpdateOperationalRequest = {
                                        id: data.id,
                                        name: data.name,
                                        r: data.r,
                                        g: data.g,
                                        b: data.b,
                                        ngayTuTraKhach: data.ngayTuTraKhach,
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
                                    const { id } =
                                        row.original as TOperationalDto;
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
                <CreateOperationalModal
                    title="Thêm loại tác nghiệp"
                    isOpen={isOpenCreateModal}
                    onClose={closeCreateModal}
                    onSubmit={handleCreate}
                    isLoading={createMutation.isLoading}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("6000")) && (
                <UpdateOperationalModal
                    title="Chỉnh sửa loại tác nghiệp"
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
                    title="Xoá loại tác nghiệp"
                    description="bạn muốn thực hiện xoá loại tác nghiệp này?"
                    labelButtonCancel="huỷ"
                    labelButtonOk="xóa"
                    labelButtonOkLoading="đang xoá"
                    isButtonOkLoading={deleteMutation.isLoading}
                />
            )}
        </>
    );
};

export default OperationalTable;
