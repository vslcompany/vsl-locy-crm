import { useCallback, useMemo, useState } from "react";
import { useMutation, useQuery } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_RowSelectionState,
    useMaterialReactTable,
} from "material-react-table";
import { Box, IconButton, Tooltip } from "@mui/material";
import { IoClose } from "react-icons/io5";
import { FaPlus } from "react-icons/fa6";

import {
    createEmployeeGroup,
    deleteEmployeeOfGroup,
    getEmployeeGroups,
} from "@/api";
import { Button, Modal } from "@/components";
import { useAuth } from "@/contexts";
import { CreateEmployeeGroupModal } from "@/modals";
import {
    TAuthContextProps,
    TCreateEmployeeGroupRequest,
    TEmployeeGroupDto,
    TTableColumn,
    TTableData,
} from "@/types";
import { buildTree, notification } from "@/utilities";

type TColumn = TTableColumn<
    TEmployeeGroupDto & { subRows: TEmployeeGroupDto[] }
>;
type TData = TTableData<TEmployeeGroupDto & { subRows: TEmployeeGroupDto[] }>;

const EmployeeGroupTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState<string>("");
    const [rowSelection, setRowSelection] = useState<MRT_RowSelectionState>({});
    const [totalRow, setTotalRow] = useState<number>(0);

    const [isOpenCreateModal, setIsOpenCreateModal] = useState<boolean>(false);
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [idSelectedDelete, setIdSelectedDelete] = useState<number | null>(
        null
    );
    const [selectedId, setSelectedId] = useState<number>();

    const { user }: TAuthContextProps = useAuth();

    const { isError, isFetching, isLoading, refetch } = useQuery({
        queryKey: "groups",
        queryFn: getEmployeeGroups,
        onSuccess: (data) => {
            if (data.status) {
                const dataTree = buildTree(data.data.data);
                setTableData(dataTree);
                setTotalRow(data.data.totalRow);
            }
        },
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            (user?.permission.includes("1048576") ||
                user?.permission.includes("5000")),
        keepPreviousData: true,
        refetchOnWindowFocus: false,
    });

    const createMutation = useMutation({
        mutationFn: createEmployeeGroup,
        onSuccess: (data) => {
            if (data.status) {
                closeCreateModal();
                setRowSelection({});
                refetch();
            }
        },
    });

    const deleteMutation = useMutation({
        mutationFn: deleteEmployeeOfGroup,
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
    const openCreateModal = useCallback((id: number | undefined) => {
        setSelectedId(id);
        setIsOpenCreateModal(true);
    }, []);

    const closeCreateModal = useCallback(() => {
        setIsOpenCreateModal(false);
    }, []);

    const handleCreate = useCallback(
        async (data: TCreateEmployeeGroupRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await createMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [createMutation]
    );

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

    /**
     * * Material table configuration
     */
    const columns = useMemo<MRT_ColumnDef<TColumn>[]>(() => {
        return [
            {
                accessorKey: "nameGroup",
                header: "Nhóm",
                size: 240,
            },
            {
                accessorKey: "nameVI",
                header: "Tên nhân viên",
                size: 240,
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "chucVu",
                header: "Chức vụ",
                size: 180,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "flagViewAllGroup",
                header: "Xem tất cả nhóm",
                size: 120,
                accessorFn: (row) => (
                    <input
                        type="checkbox"
                        id={row.id?.toString()}
                        name={row.id?.toString()}
                        defaultChecked={row.flagViewAllGroup}
                        disabled
                    />
                ),
            },
        ];
    }, []);

    const table = useMaterialReactTable({
        columns,
        data: tableData,
        enableStickyHeader: true,
        enableStickyFooter: true,
        enablePagination: false,
        enableRowSelection: true,
        enableSubRowSelection: false,
        enableExpanding: true,
        enableEditing: true,
        muiTableContainerProps: { sx: { maxHeight: "640px" } },
        renderTopToolbarCustomActions: ({ table }) => (
            <div className="flex-1 flex items-center gap-3">
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("5000")) && (
                    <Button
                        variant="contained"
                        size="sm"
                        label="tạo nhóm"
                        rounded="lg"
                        color="green"
                        onClick={() => openCreateModal(undefined)}
                        icon={<FaPlus />}
                    />
                )}
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("5000")) &&
                    (table.getIsSomePageRowsSelected() ||
                        table.getIsAllPageRowsSelected()) && (
                        <Button
                            variant="contained"
                            size="sm"
                            label="tạo nhóm nhỏ"
                            rounded="lg"
                            color="teal"
                            onClick={() => {
                                const data =
                                    table.getSelectedRowModel().flatRows[0]
                                        .original;
                                openCreateModal(data.id);
                            }}
                        />
                    )}
            </div>
        ),
        renderRowActions: ({ row }) => (
            <Box sx={{ display: "flex", gap: "0.25rem" }}>
                {(user?.permission.includes("1048576") ||
                    user?.permission.includes("5000")) && (
                    <>
                        <Tooltip arrow placement="right" title="Loại khỏi nhóm">
                            <IconButton
                                color="error"
                                onClick={() => {
                                    const { id, subRows, nameVI } =
                                        row.original;

                                    if (subRows && subRows.length > 0) {
                                        notification(
                                            false,
                                            `Nhân viên ${nameVI} đang quản lý nhân viên khác. Vui lòng loại bỏ hết nhân viên quản lý bởi nhân viên ${nameVI} rồi thực hiện loại bỏ nhân viên ${nameVI} ra khỏi nhóm`
                                        );
                                    }

                                    if (!subRows) {
                                        setIdSelectedDelete(id as number);
                                        openDeleteModal();
                                    }
                                }}
                            >
                                <IoClose />
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
        getSubRows: (row) => row.subRows, //default
        initialState: {
            expanded: true,
            columnPinning: {
                right: ["mrt-row-actions"],
            },
        },
        manualFiltering: true,
        onRowSelectionChange: setRowSelection,
        onGlobalFilterChange: setSearch,
        rowCount: totalRow,
        state: {
            globalFilter: search,
            isLoading,
            rowSelection: rowSelection,
            showAlertBanner: isError,
            showProgressBars: isFetching,
        },
    });
    return (
        <>
            <div className="space-y-4">
                <MaterialReactTable table={table} />
            </div>
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("5000")) && (
                <CreateEmployeeGroupModal
                    isOpen={isOpenCreateModal}
                    onClose={closeCreateModal}
                    onSubmit={handleCreate}
                    title="Thêm nhóm mới"
                    labelButtonCancel="huỷ"
                    labelButtonOk="tạo mới"
                    isLoading={createMutation.isLoading}
                    parentId={selectedId}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("5000")) && (
                <Modal
                    isOpen={isOpenDeleteModal}
                    onClose={closeDeleteModal}
                    onSubmit={() => {
                        handleDeleteModal(idSelectedDelete as number);
                    }}
                    title="Loại nhân viên khỏi nhóm"
                    description="bạn muốn thực hiện loại nhân viên khỏi nhóm?"
                    labelButtonCancel="huỷ"
                    labelButtonOk="loại bỏ"
                />
            )}
        </>
    );
};

export default EmployeeGroupTable;
