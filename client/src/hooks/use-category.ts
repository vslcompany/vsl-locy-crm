import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import {
    getAllCountries,
    getAllDepartments,
    getAllOffices,
    getAllPositions,
} from "@/api";
import { TCountryDto, TDepartmentDto, TOfficeDto, TPositionDto } from "@/types";

type TCategoryHookProps = {
    positions: TPositionDto[] | undefined | null;
    departments: TDepartmentDto[] | undefined | null;
    offices: TOfficeDto[] | undefined | null;
    countries: TCountryDto[] | undefined | null;
};

export const useCategory = () => {
    const [category, setCategory] = useState<TCategoryHookProps>({
        countries: null,
        departments: null,
        offices: null,
        positions: null,
    });

    const { data: positionRes } = useQuery({
        queryKey: "position",
        queryFn: getAllPositions,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    const { data: departmentsRes } = useQuery({
        queryKey: "departments",
        queryFn: getAllDepartments,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    const { data: officesRes } = useQuery({
        queryKey: "offices",
        queryFn: getAllOffices,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    const { data: countriesRes } = useQuery({
        queryKey: "countries",
        queryFn: getAllCountries,
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    useEffect(() => {
        if (positionRes && positionRes.status) {
            setCategory((prev) => ({
                ...prev,
                positions: positionRes.data as unknown as TPositionDto[],
            }));
        }
    }, [positionRes]);

    useEffect(() => {
        if (departmentsRes && departmentsRes.status) {
            setCategory((prev) => ({
                ...prev,
                departments: departmentsRes.data as unknown as TDepartmentDto[],
            }));
        }
    }, [departmentsRes]);

    useEffect(() => {
        if (officesRes && officesRes.status) {
            setCategory((prev) => ({
                ...prev,
                offices: officesRes.data as unknown as TOfficeDto[],
            }));
        }
    }, [officesRes]);

    useEffect(() => {
        if (countriesRes && countriesRes.status) {
            setCategory((prev) => ({
                ...prev,
                countries: countriesRes.data as unknown as TCountryDto[],
            }));
        }
    }, [countriesRes]);

    return {
        positions: category.positions,
        departments: category.departments,
        offices: category.offices,
        countries: category.countries,
    } as const;
};
