import { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "react-query";
import { IconButton, Tooltip } from "@mui/material";
import { Cached } from "@mui/icons-material";

import { getCustomerById } from "@/api";
import { Loading } from "@/components";
import {
    CustomerDetailSection,
    CustomerRelatedListSection,
    NotFoundSection,
} from "@/layouts";
import { TCustomerDto } from "@/types";
import { isNumber } from "@/utilities";

const CustomerDetail = () => {
    const { id } = useParams();

    const [isRefresh, setIsRefresh] = useState<boolean>(false);

    const [customer, setCustomer] = useState<TCustomerDto | null>(null);

    const { data, isLoading, isRefetching, refetch } = useQuery({
        queryKey: ["customerDetail", id],
        queryFn: () => getCustomerById(Number(id)),
        staleTime: Infinity,
        cacheTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            id !== undefined &&
            isNumber(id),
    });

    const handleRefreshDone = useCallback(() => {
        setIsRefresh(false);
    }, []);

    useEffect(() => {
        if (data && data.status) {
            setCustomer(data.data as unknown as TCustomerDto);
        }
    }, [data]);

    if (isLoading) {
        return (
            <div className="w-full h-full flex-center">
                <Loading />
            </div>
        );
    }

    if (!customer && !isLoading) {
        return (
            <div className="w-full h-full">
                <NotFoundSection />
            </div>
        );
    }

    return (
        <>
            <div className="flex justify-between items-center gap-4">
                <h2 className="title">thông tin khách hàng</h2>
                <div className="flex gap-2 items-center">
                    <Tooltip arrow placement="left" title="Tải lại dữ liệu">
                        <IconButton
                            color="primary"
                            onClick={() => {
                                setIsRefresh(true);
                                refetch();
                            }}
                        >
                            <Cached />
                        </IconButton>
                    </Tooltip>
                </div>
            </div>
            <div className="flex flex-col sm:flex-row gap-4 items-start py-2 sm:py-8">
                {/* Basic information */}
                <div className="w-full sm:w-60 lg:w-[25rem] flex-shrink-0">
                    <CustomerDetailSection
                        data={customer as TCustomerDto}
                        isLoading={isRefetching}
                    />
                </div>
                {/* Major information */}
                <div className="flex-shrink-0 flex-1 w-full min-h-[30vh] border border-gray-200 shadow overflow-auto">
                    <CustomerRelatedListSection
                        id={Number(id)}
                        isRefresh={isRefresh}
                        onRefreshDone={handleRefreshDone}
                    />
                </div>
            </div>
        </>
    );
};

export default CustomerDetail;
