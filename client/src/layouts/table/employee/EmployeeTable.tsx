import { useCallback, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useMutation, useQuery, useQueryClient } from "react-query";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    useMaterialReactTable,
} from "material-react-table";
import { IconButton, Tooltip } from "@mui/material";
import { Cached, MoreHoriz } from "@mui/icons-material";
import { Menu, MenuButton, MenuItem, MenuItems } from "@headlessui/react";

import { deleteEmployee, getEmployees, updateEmployee } from "@/api";
import { Button, GroupInput, GroupSelect, Modal } from "@/components";
import { gender } from "@/constants";
import { useAuth } from "@/contexts";
import { useCategory, useDebounced } from "@/hooks";
import {
    ChangePasswordAccountOfEmployeeModal,
    ChangePermissionAccountOfEmployeeModal,
    CreateAccountOfEmployeeModal,
    UpdateEmployeeModal,
} from "@/modals";
import {
    TAuthContextProps,
    TDeleteEmployeeRequest,
    TProfileDto,
    TTableColumn,
    TTableData,
    TUpdateEmployeeRequest,
} from "@/types";

type TColumn = TTableColumn<TProfileDto>;
type TData = TTableData<TProfileDto>;
type TQuery = {
    idChucVu: number | undefined;
    idVanPhong: number | undefined;
    idPhongBan: number | undefined;
    trangThai: boolean;
};

const EmployeeTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [query, setQuery] = useState<TQuery>({
        idChucVu: undefined,
        idVanPhong: undefined,
        idPhongBan: undefined,
        trangThai: true,
    });
    const [name, setName] = useState<string>("");

    const [isOpenUpdateModal, setIsOpenUpdateModal] = useState<boolean>(false);
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState<boolean>(false);
    const [isOpenUndeleteModal, setIsOpenUndeleteModal] =
        useState<boolean>(false);
    const [dataSelectedUpdate, setDataSelectedUpdate] =
        useState<TUpdateEmployeeRequest | null>(null);
    const [idSelected, setIdSelected] = useState<number | null>(null);

    const [isOpenCreateAccountModal, setIsOpenCreateAccountModal] =
        useState<boolean>(false);
    const [isOpenChangePasswordModal, setIsOpenChangePasswordModal] =
        useState<boolean>(false);
    const [isOpenChangePermissionModal, setIsOpenChangePermissionModal] =
        useState<boolean>(false);

    const { positions, departments, offices } = useCategory();
    const { user }: TAuthContextProps = useAuth();

    const debouncedSearch = useDebounced(name, 1000);

    const queryClient = useQueryClient();

    const navigate = useNavigate();

    const {
        data: employeeRes,
        isError,
        isFetching,
        isLoading,
        refetch,
    } = useQuery({
        queryKey: [
            "employees",
            pagination.pageIndex,
            pagination.pageSize,
            debouncedSearch,
            query.idChucVu,
            query.idVanPhong,
            query.idPhongBan,
            query.trangThai,
        ],
        queryFn: () =>
            getEmployees({
                start: pagination.pageIndex * pagination.pageSize,
                size: pagination.pageSize,
                name: debouncedSearch,
                idChucVu: query.idChucVu as number,
                idVanPhong: query.idVanPhong as number,
                idPhongBan: query.idPhongBan as number,
                trangThai: query.trangThai,
            }),
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            (user?.permission.includes("1048576") ||
                user?.permission.includes("5000")),
        keepPreviousData: true,
        refetchOnWindowFocus: false,
    });

    const updateMutation = useMutation({
        mutationFn: updateEmployee,
        onSuccess: (data) => {
            if (data.status) {
                queryClient.invalidateQueries({ queryKey: "employees" });
                closeUpdateModal();
            }
        },
    });

    const deleteMutation = useMutation({
        mutationFn: deleteEmployee,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                closeDeleteModal();
            }
        },
    });

    const undeleteMutation = useMutation({
        mutationFn: deleteEmployee,
        onSuccess: (data) => {
            if (data.status) {
                refetch();
                closeUndeleteModal();
            }
        },
    });

    const createAccountMutation = useMutation({
        mutationFn: updateEmployee,
        onSuccess: (data) => {
            if (data.status) {
                queryClient.invalidateQueries({ queryKey: "employees" });
                closeCreateAccountModal();
            }
        },
    });

    const changePasswordMutation = useMutation({
        mutationFn: updateEmployee,
        onSuccess: (data) => {
            if (data.status) {
                closeChangePasswordModal();
            }
        },
    });

    const changePermissionMutation = useMutation({
        mutationFn: updateEmployee,
        onSuccess: (data) => {
            if (data.status) {
                queryClient.invalidateQueries({ queryKey: "employees" });
                closeChangePermissionModal();
            }
        },
    });

    /**
     * * Handle events
     */
    const openUpdateModal = useCallback((data: TUpdateEmployeeRequest) => {
        setIsOpenUpdateModal(true);
        setDataSelectedUpdate(data);
    }, []);

    const closeUpdateModal = useCallback(() => {
        setIsOpenUpdateModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleUpdate = useCallback(
        async (data: TUpdateEmployeeRequest) => {
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
        async (data: TDeleteEmployeeRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await deleteMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [deleteMutation]
    );

    const openUndeleteModal = useCallback(() => {
        setIsOpenUndeleteModal(true);
    }, []);

    const closeUndeleteModal = useCallback(() => {
        setIdSelected(null);
        setIsOpenUndeleteModal(false);
    }, []);

    const handleUndeleteModal = useCallback(
        async (data: TDeleteEmployeeRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await undeleteMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [undeleteMutation]
    );

    const openCreateAccountModal = useCallback(
        (data: TUpdateEmployeeRequest) => {
            setIsOpenCreateAccountModal(true);
            setDataSelectedUpdate(data);
        },
        []
    );

    const closeCreateAccountModal = useCallback(() => {
        setIsOpenCreateAccountModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleCreateAccount = useCallback(
        async (data: TUpdateEmployeeRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await createAccountMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [createAccountMutation]
    );

    const openChangePasswordModal = useCallback(
        (data: TUpdateEmployeeRequest) => {
            setIsOpenChangePasswordModal(true);
            setDataSelectedUpdate(data);
        },
        []
    );

    const closeChangePasswordModal = useCallback(() => {
        setIsOpenChangePasswordModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleChangePassword = useCallback(
        async (data: TUpdateEmployeeRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await changePasswordMutation.mutateAsync(data);
            return result.status as boolean;
        },
        [changePasswordMutation]
    );

    const openChangePermissionModal = useCallback(
        (data: TUpdateEmployeeRequest) => {
            setIsOpenChangePermissionModal(true);
            setDataSelectedUpdate(data);
        },
        []
    );

    const closeChangePermissionModal = useCallback(() => {
        setIsOpenChangePermissionModal(false);
        setDataSelectedUpdate(null);
    }, []);

    const handleChangePermission = useCallback(
        async (data: TUpdateEmployeeRequest) => {
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            const result: any = await changePermissionMutation.mutateAsync(
                data
            );
            return result.status as boolean;
        },
        [changePermissionMutation]
    );

    useEffect(() => {
        if (employeeRes && employeeRes.status) {
            setTableData(employeeRes.data.data);
            setTotalRow(employeeRes.data.totalRow);
        }
    }, [employeeRes]);

    /**
     * * Material table configuration
     */
    const columns = useMemo<MRT_ColumnDef<TColumn>[]>(() => {
        return [
            {
                accessorKey: "manhanvien",
                header: "Mã nhân sự",
                maxSize: 80,
            },
            {
                accessorKey: "hoTenVI",
                header: "Tên nhân viên",
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
                maxSize: 180,
            },
            {
                accessorKey: "chucVu",
                header: "Chức vụ",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
                maxSize: 120,
            },
            {
                accessorKey: "phongban",
                header: "Phòng ban",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
                maxSize: 120,
            },
            {
                accessorKey: "vanPhong",
                header: "Văn phòng",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
                maxSize: 120,
            },
            {
                accessorKey: "namsinh",
                header: "Ngày sinh",
                maxSize: 120,
            },
            {
                accessorKey: "gioitinh",
                header: "Giới tính",
                accessorFn: (row) =>
                    gender.find((i) => row.gioitinh === i.id)?.nameVI,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
                maxSize: 60,
            },
            {
                accessorKey: "quequan",
                header: "Quê quán",
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
                maxSize: 180,
            },
            {
                accessorKey: "diachi",
                header: "Địa chỉ",
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
                maxSize: 180,
            },
            {
                accessorKey: "didong",
                header: "Số điện thoại",
                maxSize: 100,
            },
            {
                accessorKey: "email",
                header: "Thư điện tử",
                maxSize: 120,
            },
            {
                accessorKey: "soLuongKH",
                header: "Số lượng khách hàng",
                maxSize: 40,
            },
        ];
    }, []);

    const table = useMaterialReactTable({
        columns,
        data: tableData,
        enableTopToolbar: false,
        enableStickyFooter: true,
        enableGrouping: true,
        enableEditing: true,
        muiTableContainerProps: {
            sx: { maxHeight: "640px" },
        },
        renderEmptyRowsFallback: () => (
            <div className="px-2 py-6">
                <p className="subtitle first-letter:uppercase">
                    không có dữ liệu
                </p>
            </div>
        ),
        renderRowActions: ({ row }) => (
            <>
                <Menu>
                    <MenuButton>
                        <MoreHoriz />
                    </MenuButton>
                    <MenuItems
                        className="bg-white border border-gray-400 p-2 space-y-2 rounded-md z-[1000]"
                        anchor="bottom end"
                    >
                        {query.trangThai && (
                            <MenuItem>
                                <div
                                    className="first-letter:uppercase data-[focus]:bg-green-100 p-2 cursor-pointer"
                                    onClick={() => {
                                        const { idNhanVien } = row.original;
                                        const data = tableData.find(
                                            (e) => e.idNhanVien === idNhanVien
                                        ) as TProfileDto;
                                        const payload: TUpdateEmployeeRequest =
                                            {
                                                id: data.id,
                                                username: data.username,
                                                password: "",
                                                active: data.active,
                                                permission: data.permission,
                                                idNhanVien: data.idNhanVien,
                                                idChucVu: data.idChucVu,
                                                idPhongBan: data.idPhongBan,
                                                idVanPhong: data.idVanPhong,
                                                manhanvien: data.manhanvien,
                                                hoTenVI: data.hoTenVI,
                                                hoTenEN: data.hoTenEN,
                                                namsinh: data.namsinh,
                                                gioitinh: data.gioitinh,
                                                quequan: data.quequan,
                                                diachi: data.diachi,
                                                soCMT: data.soCMT,
                                                noiCapCMT: data.noiCapCMT,
                                                ngayCapCMT: data.ngayCapCMT,
                                                photoURL: data.photoURL,
                                                ghichu: data.ghichu,
                                                soLuongKH: data.soLuongKH,
                                            };
                                        openUpdateModal(payload);
                                    }}
                                >
                                    Chỉnh sửa
                                </div>
                            </MenuItem>
                        )}
                        {query.trangThai &&
                            row.original.id === 0 &&
                            row.original.username === "" && (
                                <MenuItem>
                                    <div
                                        className="first-letter:uppercase data-[focus]:bg-green-100 p-2 cursor-pointer"
                                        onClick={() => {
                                            const { idNhanVien } = row.original;
                                            const data = tableData.find(
                                                (e) =>
                                                    e.idNhanVien === idNhanVien
                                            ) as TProfileDto;
                                            const payload: TUpdateEmployeeRequest =
                                                {
                                                    id: data.id,
                                                    username: data.username,
                                                    password: "",
                                                    active: data.active,
                                                    permission: data.permission,
                                                    idNhanVien: data.idNhanVien,
                                                    idChucVu: data.idChucVu,
                                                    idPhongBan: data.idPhongBan,
                                                    idVanPhong: data.idVanPhong,
                                                    manhanvien: data.manhanvien,
                                                    hoTenVI: data.hoTenVI,
                                                    hoTenEN: data.hoTenEN,
                                                    namsinh: data.namsinh,
                                                    gioitinh: data.gioitinh,
                                                    quequan: data.quequan,
                                                    diachi: data.diachi,
                                                    soCMT: data.soCMT,
                                                    noiCapCMT: data.noiCapCMT,
                                                    ngayCapCMT: data.ngayCapCMT,
                                                    photoURL: data.photoURL,
                                                    ghichu: data.ghichu,
                                                    soLuongKH: data.soLuongKH,
                                                };
                                            openCreateAccountModal(payload);
                                        }}
                                    >
                                        tạo tài khoản
                                    </div>
                                </MenuItem>
                            )}
                        {query.trangThai && (
                            <MenuItem>
                                <div
                                    className="first-letter:uppercase data-[focus]:bg-green-100 p-2 cursor-pointer"
                                    onClick={() => {
                                        const { idNhanVien } = row.original;
                                        const data = tableData.find(
                                            (e) => e.idNhanVien === idNhanVien
                                        ) as TProfileDto;
                                        const payload: TUpdateEmployeeRequest =
                                            {
                                                id: data.id,
                                                username: data.username,
                                                password: "",
                                                active: data.active,
                                                permission: data.permission,
                                                idNhanVien: data.idNhanVien,
                                                idChucVu: data.idChucVu,
                                                idPhongBan: data.idPhongBan,
                                                idVanPhong: data.idVanPhong,
                                                manhanvien: data.manhanvien,
                                                hoTenVI: data.hoTenVI,
                                                hoTenEN: data.hoTenEN,
                                                namsinh: data.namsinh,
                                                gioitinh: data.gioitinh,
                                                quequan: data.quequan,
                                                diachi: data.diachi,
                                                soCMT: data.soCMT,
                                                noiCapCMT: data.noiCapCMT,
                                                ngayCapCMT: data.ngayCapCMT,
                                                photoURL: data.photoURL,
                                                ghichu: data.ghichu,
                                                soLuongKH: data.soLuongKH,
                                            };
                                        openChangePasswordModal(payload);
                                    }}
                                >
                                    thay đổi mật khẩu
                                </div>
                            </MenuItem>
                        )}
                        {query.trangThai && (
                            <MenuItem>
                                <div
                                    className="first-letter:uppercase data-[focus]:bg-green-100 p-2 cursor-pointer"
                                    onClick={() => {
                                        const { idNhanVien } = row.original;
                                        const data = tableData.find(
                                            (e) => e.idNhanVien === idNhanVien
                                        ) as TProfileDto;
                                        const payload: TUpdateEmployeeRequest =
                                            {
                                                id: data.id,
                                                username: data.username,
                                                password: "",
                                                active: data.active,
                                                permission: data.permission,
                                                idNhanVien: data.idNhanVien,
                                                idChucVu: data.idChucVu,
                                                idPhongBan: data.idPhongBan,
                                                idVanPhong: data.idVanPhong,
                                                manhanvien: data.manhanvien,
                                                hoTenVI: data.hoTenVI,
                                                hoTenEN: data.hoTenEN,
                                                namsinh: data.namsinh,
                                                gioitinh: data.gioitinh,
                                                quequan: data.quequan,
                                                diachi: data.diachi,
                                                soCMT: data.soCMT,
                                                noiCapCMT: data.noiCapCMT,
                                                ngayCapCMT: data.ngayCapCMT,
                                                photoURL: data.photoURL,
                                                ghichu: data.ghichu,
                                                soLuongKH: data.soLuongKH,
                                            };
                                        openChangePermissionModal(payload);
                                    }}
                                >
                                    cập nhật quyền
                                </div>
                            </MenuItem>
                        )}
                        {query.trangThai && (
                            <MenuItem>
                                <div
                                    className="first-letter:uppercase data-[focus]:bg-green-100 p-2 cursor-pointer"
                                    onClick={() => {
                                        const { idNhanVien } =
                                            row.original as TProfileDto;
                                        setIdSelected(idNhanVien);
                                        openDeleteModal();
                                    }}
                                >
                                    xoá
                                </div>
                            </MenuItem>
                        )}
                        {!query.trangThai && (
                            <MenuItem>
                                <div
                                    className="first-letter:uppercase data-[focus]:bg-green-100 p-2 cursor-pointer"
                                    onClick={() => {
                                        const { idNhanVien } =
                                            row.original as TProfileDto;
                                        setIdSelected(idNhanVien);
                                        openUndeleteModal();
                                    }}
                                >
                                    huỷ xoá
                                </div>
                            </MenuItem>
                        )}
                    </MenuItems>
                </Menu>
            </>
        ),
        initialState: {
            expanded: true,
            pagination,
            columnPinning: {
                right: ["mrt-row-actions"],
            },
            grouping: ["phongban"],
        },
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
            <div className="space-y-4">
                <div className="flex items-center justify-between gap-4">
                    <h3 className="subtitle">tất cả dữ liệu nhân viên</h3>
                    <div className="flex items-center gap-4">
                        <Button
                            label="tạo mới"
                            onClick={() => navigate("/employee/new")}
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
                <div className="grid md:grid-cols-5 gap-2">
                    <GroupInput
                        labelFor="name"
                        labelText="Tên nhân viên"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                    <GroupSelect
                        labelText="chức vụ"
                        options={
                            positions && positions.length > 0 ? positions : []
                        }
                        option={{
                            label: "nameVI",
                            value: "id",
                        }}
                        value={
                            query.idChucVu
                                ? query.idChucVu.toString()
                                : undefined
                        }
                        onValueChange={(val) =>
                            setQuery((prev) => ({
                                ...prev,
                                idChucVu: val ? parseInt(val) : undefined,
                            }))
                        }
                    />
                    <GroupSelect
                        labelText="phòng ban"
                        options={
                            departments && departments.length > 0
                                ? departments
                                : []
                        }
                        option={{
                            label: "nameVI",
                            value: "id",
                        }}
                        value={
                            query.idPhongBan
                                ? query.idPhongBan.toString()
                                : undefined
                        }
                        onValueChange={(val) =>
                            setQuery((prev) => ({
                                ...prev,
                                idPhongBan: val ? parseInt(val) : undefined,
                            }))
                        }
                    />
                    <GroupSelect
                        labelText="văn phòng"
                        options={offices && offices.length > 0 ? offices : []}
                        option={{
                            label: "nameVI",
                            value: "id",
                        }}
                        value={
                            query.idVanPhong
                                ? query.idVanPhong.toString()
                                : undefined
                        }
                        onValueChange={(val) =>
                            setQuery((prev) => ({
                                ...prev,
                                idVanPhong: val ? parseInt(val) : undefined,
                            }))
                        }
                    />
                    <GroupSelect
                        labelText="trạng thái"
                        options={[{ label: "chưa xoá" }, { label: "đã xoá" }]}
                        option={{
                            label: "label",
                            value: "label",
                        }}
                        value={query.trangThai ? "chưa xoá" : "đã xoá"}
                        onValueChange={(val) =>
                            setQuery((prev) => ({
                                ...prev,
                                trangThai: val
                                    ? val === "chưa xoá"
                                        ? true
                                        : false
                                    : true,
                            }))
                        }
                    />
                </div>
                <MaterialReactTable table={table} />
            </div>
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("5000")) && (
                <UpdateEmployeeModal
                    isOpen={isOpenUpdateModal}
                    onClose={closeUpdateModal}
                    onSubmit={handleUpdate}
                    title="Chỉnh sửa nhân viên"
                    width="7xl"
                    labelButtonCancel="huỷ"
                    labelButtonOk="cập nhật"
                    prevData={dataSelectedUpdate}
                    isLoading={updateMutation.isLoading}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("5000")) && (
                <Modal
                    isOpen={isOpenDeleteModal}
                    onClose={closeDeleteModal}
                    onSubmit={() => {
                        handleDeleteModal({
                            idNhanVien: idSelected as number,
                            flagDelete: true,
                            idUserDelete: user?.id as number,
                        });
                    }}
                    title="Xoá nhân viên"
                    description="bạn muốn thực hiện xoá nhân viên này?"
                    labelButtonCancel="huỷ"
                    labelButtonOk="yêu cầu xóa"
                    isButtonOkLoading={undeleteMutation.isLoading}
                    labelButtonOkLoading="đang xoá"
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("5000")) && (
                <Modal
                    isOpen={isOpenUndeleteModal}
                    onClose={closeUndeleteModal}
                    onSubmit={() => {
                        handleUndeleteModal({
                            idNhanVien: idSelected as number,
                            flagDelete: false,
                            idUserDelete: null,
                        });
                    }}
                    title="Huỷ xoá nhân viên"
                    description="bạn muốn thực hiện huỷ xoá nhân viên này?"
                    labelButtonCancel="huỷ"
                    labelButtonOk="huỷ xoá"
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("5000")) && (
                <CreateAccountOfEmployeeModal
                    isOpen={isOpenCreateAccountModal}
                    onClose={closeCreateAccountModal}
                    onSubmit={handleCreateAccount}
                    title="Tạo tài khoản nhân viên"
                    labelButtonCancel="huỷ"
                    labelButtonOk="tạo"
                    prevData={dataSelectedUpdate}
                    isLoading={createAccountMutation.isLoading}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("5000")) && (
                <ChangePasswordAccountOfEmployeeModal
                    isOpen={isOpenChangePasswordModal}
                    onClose={closeChangePasswordModal}
                    onSubmit={handleChangePassword}
                    title="Đổi mật khẩu mới"
                    labelButtonCancel="huỷ"
                    labelButtonOk="đổi mật khẩu"
                    prevData={dataSelectedUpdate}
                    isLoading={changePasswordMutation.isLoading}
                />
            )}
            {(user?.permission.includes("1048576") ||
                user?.permission.includes("5000")) && (
                <ChangePermissionAccountOfEmployeeModal
                    isOpen={isOpenChangePermissionModal}
                    onClose={closeChangePermissionModal}
                    onSubmit={handleChangePermission}
                    title="Phân quyền tài khoản"
                    labelButtonCancel="huỷ"
                    labelButtonOk="phân quyền"
                    prevData={dataSelectedUpdate}
                    isLoading={changePermissionMutation.isLoading}
                />
            )}
        </>
    );
};

export default EmployeeTable;
