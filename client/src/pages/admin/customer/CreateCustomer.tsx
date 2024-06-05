import { FormEvent, useEffect, useState } from "react";
import { useMutation, useQuery } from "react-query";

import { createCustomer, getAllBusinesses, getCitiesByIdCountry } from "@/api";
import { Button, GroupInput, GroupSelect, GroupTextArea } from "@/components";
import { useAuth } from "@/contexts";
import { initCustomer } from "@/constants";
import { useCategory } from "@/hooks";
import {
    TAuthContextProps,
    TBusinessDto,
    TCityDto,
    TCreateCustomerRequest,
} from "@/types";

const CreateCustomer = () => {
    const [data, setData] = useState<TCreateCustomerRequest>(initCustomer);
    const [cities, setCities] = useState<TCityDto[] | []>([]);
    const [businesses, setBusinesses] = useState<TBusinessDto[] | []>([]);

    const { user }: TAuthContextProps = useAuth();
    const { countries } = useCategory();

    const { data: citiesRes } = useQuery({
        queryKey: ["cities", data.idQuocGia],
        queryFn: () => getCitiesByIdCountry(data.idQuocGia as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data.idQuocGia !== undefined,
    });

    const { data: businessesRes } = useQuery({
        queryKey: ["businesses"],
        queryFn: getAllBusinesses,
        cacheTime: Infinity,
        staleTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    const createMutation = useMutation({
        mutationFn: createCustomer,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        data.idUserCreate = user?.id as number;

        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const result: any = await createMutation.mutateAsync(data);
        if (result.status) {
            setData(initCustomer);
        }
    };

    useEffect(() => {
        if (citiesRes && citiesRes.status) {
            setCities(citiesRes.data as unknown as TCityDto[]);
        }
    }, [citiesRes]);

    useEffect(() => {
        if (businessesRes && businessesRes.status) {
            setBusinesses(businessesRes.data as unknown as TBusinessDto[]);
        }
    }, [businessesRes]);

    return (
        <>
            <h2 className="title">tạo mới khách hàng</h2>
            <form className="space-y-6" onSubmit={handleSubmit}>
                <div className="space-y-4">
                    <div className="grid md:grid-cols-3 gap-4">
                        <GroupInput
                            labelFor="code"
                            labelText="mã"
                            required={true}
                            value={data.code}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    code: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="taxCode"
                            labelText="mã số thuế"
                            value={data.taxCode}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    taxCode: e.target.value,
                                }))
                            }
                        />
                        <GroupSelect
                            labelText="loại doanh nghiệp"
                            options={businesses}
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idLoaiDoanhNghiep
                                    ? data.idLoaiDoanhNghiep.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) => ({
                                    ...prev,
                                    idLoaiDoanhNghiep: val
                                        ? parseInt(val)
                                        : undefined,
                                }))
                            }
                        />
                    </div>
                    <div className="grid md:grid-cols-2 gap-4">
                        <GroupTextArea
                            labelFor="nameVI"
                            labelText="tên (VI)"
                            required={true}
                            value={data.nameVI}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    nameVI: e.target.value,
                                }))
                            }
                        />
                        <GroupTextArea
                            labelFor="nameEN"
                            labelText="tên (EN)"
                            value={data.nameEN}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    nameEN: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <div className="grid md:grid-cols-2 gap-4">
                        <GroupTextArea
                            labelFor="addressVI"
                            labelText="địa chỉ (VI)"
                            required={true}
                            value={data.addressVI}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    addressVI: e.target.value,
                                }))
                            }
                        />
                        <GroupTextArea
                            labelFor="addressEN"
                            labelText="địa chỉ (EN)"
                            value={data.addressEN}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    addressEN: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <div className="grid md:grid-cols-2 gap-4">
                        <GroupSelect
                            labelText="quốc gia"
                            options={
                                countries && countries.length > 0
                                    ? countries
                                    : []
                            }
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idQuocGia
                                    ? data.idQuocGia.toString()
                                    : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) => ({
                                    ...prev,
                                    idQuocGia: val ? parseInt(val) : undefined,
                                }))
                            }
                        />
                        <GroupSelect
                            labelText="thành phố"
                            options={cities}
                            option={{
                                label: "nameVI",
                                value: "id",
                            }}
                            value={
                                data.idCity ? data.idCity.toString() : undefined
                            }
                            onValueChange={(val) =>
                                setData((prev) => ({
                                    ...prev,
                                    idCity: val ? parseInt(val) : undefined,
                                }))
                            }
                        />
                    </div>
                    <div className="grid md:grid-cols-4 gap-4">
                        <GroupInput
                            labelFor="phone"
                            labelText="số điện thoại"
                            value={data.phone}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    phone: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="fax"
                            labelText="FAX"
                            value={data.fax}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    fax: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="email"
                            labelText="Thư điện tử"
                            value={data.email}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    email: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="website"
                            labelText="Trang web"
                            value={data.website}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    website: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <GroupTextArea
                        labelFor="note"
                        labelText="Ghi chú"
                        value={data.note}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                note: e.target.value,
                            }))
                        }
                    />
                </div>
                <div className="flex items-center gap-4">
                    <Button
                        label="tạo"
                        isLoading={createMutation.isLoading}
                        textIsLoading="đang tạo"
                    />
                </div>
            </form>
        </>
    );
};

export default CreateCustomer;
