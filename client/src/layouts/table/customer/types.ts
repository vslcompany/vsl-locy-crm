import { TCustomerDto } from "@/types";
import { type MRT_PaginationState } from "material-react-table";

export type TCustomerTableProps = {
    data: TCustomerDto[];
    pagination: MRT_PaginationState;
    setPagination: React.Dispatch<React.SetStateAction<MRT_PaginationState>>;
    totalRow: number;
    isError: boolean;
    isFetching: boolean;
    isLoading: boolean;
    refetch: () => void;
};
