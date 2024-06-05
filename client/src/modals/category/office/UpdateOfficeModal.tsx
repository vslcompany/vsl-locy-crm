import { useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getCitiesByIdCountry } from "@/api";
import { GroupInput, GroupSelect, GroupTextArea, Modal } from "@/components";
import { useCategory } from "@/hooks";
import { TCityDto, TUpdateOfficeRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateModalProps } from "../../types";

const UpdateOfficeModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    prevData,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "cập nhật",
}: TUpdateModalProps<TUpdateOfficeRequest>) => {
    const [data, setData] = useState<TUpdateOfficeRequest | null>(null);
    const [cities, setCities] = useState<TCityDto[] | []>([]);

    const { countries } = useCategory();

    const { data: citiesRes } = useQuery({
        queryKey: ["cities", data?.idCountry],
        queryFn: () => getCitiesByIdCountry(data?.idCountry as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data != null &&
            data.idCountry !== null &&
            data.idCountry !== -1,
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
                        <div className="grid md:grid-cols-3 gap-2">
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
                            <GroupInput
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
                            <GroupInput
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
                                    data.idCountry
                                        ? data.idCountry.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev != null
                                            ? {
                                                  ...prev,
                                                  idCountry: val
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
                                    cities && cities.length > 0 ? cities : []
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
                                labelText="fax"
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
                                labelText="thư điện tử"
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
                            <GroupInput
                                labelFor="website"
                                labelText="trang web"
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
                            <GroupInput
                                labelFor="note"
                                labelText="ghi chú"
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

export default UpdateOfficeModal;
