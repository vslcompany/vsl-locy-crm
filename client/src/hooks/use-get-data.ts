import { useQuery, QueryKey, QueryFunction } from "react-query";

type TUseGetDataProps = {
    queryKey: QueryKey;
    queryFn: QueryFunction;
    refetchOnWindowFocus?: boolean;
    isSaveCached?: boolean;
    keepPreviousData?: boolean;
    enabled?: boolean;
};

export const useGetData = ({
    queryKey,
    queryFn,
    isSaveCached = false,
    refetchOnWindowFocus = false,
    keepPreviousData,
    enabled,
}: TUseGetDataProps) => {
    const query = useQuery({
        queryKey,
        queryFn,
        staleTime: isSaveCached ? Infinity : undefined,
        cacheTime: isSaveCached ? Infinity : undefined,
        refetchOnWindowFocus,
        enabled,
        keepPreviousData,
    });

    return query;
};
