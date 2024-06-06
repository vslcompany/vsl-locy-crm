import { useEffect, useMemo, useState } from "react";
import { useQuery } from "react-query";
import moment from "moment";
import {
    MaterialReactTable,
    type MRT_ColumnDef,
    type MRT_PaginationState,
    useMaterialReactTable,
} from "material-react-table";

import { getReportWork } from "@/api";
import { Button, GroupInput } from "@/components";
import { useAuth } from "@/contexts";
import {
    TAuthContextProps,
    TReportWorkDto,
    TTableColumn,
    TTableData,
} from "@/types";

type TFilterFeatures = {
    startDate: string;
    endDate: string;
};
type TColumn = TTableColumn<TReportWorkDto>;
type TData = TTableData<TReportWorkDto>;

const ReportWorkTable = () => {
    const [tableData, setTableData] = useState<TData>([]);
    const [search, setSearch] = useState<string>("");
    const [pagination, setPagination] = useState<MRT_PaginationState>({
        pageIndex: 0,
        pageSize: 100,
    });
    const [totalRow, setTotalRow] = useState<number>(0);

    const [query, setQuery] = useState<TFilterFeatures>({
        startDate: moment().startOf("week").format("YYYY-MM-DD"),
        endDate: moment().endOf("week").format("YYYY-MM-DD"),
    });

    const [isFiltering, setIsFiltering] = useState<boolean>(true);

    const { user }: TAuthContextProps = useAuth();

    const {
        data: reportWorksRes,
        isError,
        isFetching,
        isLoading,
    } = useQuery({
        queryKey: [
            "reportWorks",
            pagination.pageIndex, //refetch when pagination.pageIndex changes
            pagination.pageSize, //refetch when pagination.pageSize changes
            query.startDate, //refetch when query changes
            query.endDate, //refetch when query changes
            user,
        ],
        queryFn: () =>
            getReportWork({
                start: pagination.pageIndex,
                size: pagination.pageSize,
                ...query,
            }),
        onError: () => {
            setIsFiltering(false);
        },
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            query.startDate.trim() !== "" &&
            query.endDate.trim() !== "" &&
            isFiltering,
    });

    /**
     * * Handle events
     */
    const handleSubmit = () => {
        console.log(query);
        // TODO: Set filtering status state to true
        setIsFiltering(true);
    };

    useEffect(() => {
        if (reportWorksRes && reportWorksRes.status) {
            setTableData(reportWorksRes.data.data);
            setTotalRow(reportWorksRes.data.totalRow);

            // TODO: Set finding status state to false
            setIsFiltering(false);
        }
    }, [reportWorksRes]);

    /**
     * * Material table configuration
     */
    const columns = useMemo<MRT_ColumnDef<TColumn>[]>(() => {
        return [
            {
                accessorKey: "nhanVienTao",
                header: "Nhân viên tác nghiệp",
                size: 360,
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "khachHang",
                header: "Khách hàng",
                size: 360,
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "nhanVien",
                header: "Nhân viên quản lý",
                size: 360,
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "loaiTacNghiep",
                header: "Loại tác nghiệp",
                size: 180,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "nguoiLienHe",
                header: "Người liên hệ",
                size: 360,
                muiTableBodyCellProps: () => ({
                    className: "capitalize",
                }),
            },
            {
                accessorKey: "noiDung",
                header: "Nội dung",
                size: 480,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "thoiGianThucHien",
                header: "Thời gian thực hiện",
                size: 180,
            },
            {
                accessorKey: "khachHangPhanHoi",
                header: "Khách hàng phản hồi",
                size: 480,
                muiTableBodyCellProps: () => ({
                    className: "first-letter:uppercase",
                }),
            },
            {
                accessorKey: "thoiGianPhanHoi",
                header: "Thời gian phản hồi",
                size: 180,
            },
        ];
    }, []);

    const table = useMaterialReactTable({
        columns,
        data: tableData,
        enableTopToolbar: false,
        enableStickyFooter: true,
        enableGrouping: true,
        enableExpanding: true,
        muiTableContainerProps: { sx: { maxHeight: "640px" } },
        renderEmptyRowsFallback: () => (
            <div className="px-2 py-6">
                <p className="section-subtitle first-letter:uppercase">
                    không có dữ liệu
                </p>
            </div>
        ),
        initialState: {
            expanded: true,
            pagination,
            grouping: ["nhanVienTao"],
        },
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
            <div className="space-y-4">
                <div className="flex items-end gap-4">
                    <div className="w-48 flex-grow-0">
                        <GroupInput
                            type="date"
                            labelFor="startDate"
                            labelText="từ ngày"
                            value={query.startDate}
                            onChange={(e) =>
                                setQuery((prev) => ({
                                    ...prev,
                                    startDate: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <div className="w-48 flex-grow-0">
                        <GroupInput
                            type="date"
                            className="w-64"
                            labelFor="endDate"
                            labelText="tới ngày"
                            value={query.endDate}
                            onChange={(e) =>
                                setQuery((prev) => ({
                                    ...prev,
                                    endDate: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <Button onClick={handleSubmit} label="lọc dữ liệu" />
                </div>
                <MaterialReactTable table={table} />
            </div>
        </>
    );
};

export default ReportWorkTable;
