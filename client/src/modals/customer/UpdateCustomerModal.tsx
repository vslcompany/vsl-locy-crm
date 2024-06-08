import { useEffect, useState } from "react";
import { isEqual } from "lodash";
import { useQuery } from "react-query";

import { getAllBusinesses, getCitiesByIdCountry } from "@/api";
import { GroupInput, GroupSelect, GroupTextArea, Modal } from "@/components";
import { useCategory } from "@/hooks";
import { TBusinessDto, TCityDto, TUpdateCustomerRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateModalProps } from "../types";

const UpdateCustomerModal = ({
    isOpen,
    onClose,
    onSubmit,
    title,
    description,
    width,
    labelButtonCancel,
    labelButtonOk,
    prevData,
    isLoading,
}: TUpdateModalProps<TUpdateCustomerRequest>) => {
    const [data, setData] = useState<TUpdateCustomerRequest | null>(null);
    const [cities, setCities] = useState<TCityDto[] | []>([]);
    const [businesses, setBusinesses] = useState<TBusinessDto[] | []>([]);

    const { countries } = useCategory();

    const { data: citiesRes } = useQuery({
        queryKey: ["cities", data?.idQuocGia],
        queryFn: () => getCitiesByIdCountry(data?.idQuocGia as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data != null &&
            data.idQuocGia !== null &&
            data.idQuocGia !== -1,
    });

    const { data: businessesRes } = useQuery({
        queryKey: ["businesses", data?.idQuocGia],
        queryFn: getAllBusinesses,
        refetchOnWindowFocus: true,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (isEqual(data, prevData)) {
            notification(false, "Thông tin không thay đổi");
            return;
        }

        if (data) {
            await onSubmit(data);
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

    useEffect(() => {
        setData(prevData);
    }, [prevData]);

    return (
        <>
            <Modal
                isOpen={isOpen}
                onClose={onClose}
                onSubmit={() => handleSubmit()}
                width={width}
                title={title}
                description={description}
                labelButtonCancel={labelButtonCancel}
                labelButtonOk={labelButtonOk}
                labelButtonOkLoading="đang cập nhật"
                isButtonOkLoading={isLoading}
            >
                {data !== null && (
                    <div className="grid gap-4">
                        <div className="space-y-4">
                            <div className="grid md:grid-cols-3 gap-4">
                                <GroupInput
                                    labelFor="code"
                                    labelText="mã"
                                    required={true}
                                    value={data.code}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      code: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="taxCode"
                                    labelText="mã số thuế"
                                    value={data.taxCode}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      taxCode: e.target.value,
                                                  }
                                                : null
                                        )
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
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      idLoaiDoanhNghiep: val
                                                          ? parseInt(val)
                                                          : -1,
                                                  }
                                                : null
                                        )
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
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      nameVI: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupTextArea
                                    labelFor="nameEN"
                                    labelText="tên (EN)"
                                    value={data.nameEN}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      nameEN: e.target.value,
                                                  }
                                                : null
                                        )
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
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      addressVI: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupTextArea
                                    labelFor="addressEN"
                                    labelText="địa chỉ (EN)"
                                    value={data.addressEN}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      addressEN: e.target.value,
                                                  }
                                                : null
                                        )
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
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      idQuocGia: val
                                                          ? parseInt(val)
                                                          : -1,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupSelect
                                    labelText="thành phố"
                                    options={
                                        cities && cities.length > 0
                                            ? cities
                                            : []
                                    }
                                    option={{
                                        label: "nameVI",
                                        value: "id",
                                    }}
                                    value={
                                        data.idCity
                                            ? data.idCity.toString()
                                            : undefined
                                    }
                                    onValueChange={(val) =>
                                        setData((prev) =>
                                            prev != null
                                                ? {
                                                      ...prev,
                                                      idCity: val
                                                          ? parseInt(val)
                                                          : -1,
                                                  }
                                                : null
                                        )
                                    }
                                />
                            </div>
                            <div className="grid md:grid-cols-4 gap-4">
                                <GroupInput
                                    labelFor="phone"
                                    labelText="số điện thoại"
                                    value={data.phone}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      phone: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="fax"
                                    labelText="FAX"
                                    value={data.fax}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      fax: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="email"
                                    labelText="Thư điện tử"
                                    value={data.email}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      email: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                                <GroupInput
                                    labelFor="website"
                                    labelText="Trang web"
                                    value={data.website}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      website: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                            </div>
                            <GroupTextArea
                                labelFor="note"
                                labelText="Ghi chú"
                                value={data.note}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  note: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                        </div>
                    </div>
                )}
            </Modal>
        </>
    );
};

export default UpdateCustomerModal;
