import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { getCitiesByIdCountry } from "@/api";
import { GroupInput, GroupSelect, GroupTextArea, Modal } from "@/components";
import { initOffice } from "@/constants";
import { useCategory } from "@/hooks";
import { TCityDto, TCreateOfficeRequest } from "@/types";

import { TCreateModalProps } from "../../types";

const CreateOfficeModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "tạo",
}: TCreateModalProps<TCreateOfficeRequest>) => {
    const [data, setData] = useState<TCreateOfficeRequest>(initOffice);
    const [cities, setCities] = useState<TCityDto[] | []>([]);

    const { countries } = useCategory();

    const { data: citiesRes } = useQuery({
        queryKey: ["cities", data.idCountry],
        queryFn: () => getCitiesByIdCountry(data.idCountry as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data.idCountry != undefined &&
            data.idCountry !== null &&
            data.idCountry !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result) {
            setData(initOffice);
            setCities([]);
        }
    };

    useEffect(() => {
        if (citiesRes && citiesRes.status) {
            setCities(citiesRes.data as unknown as TCityDto[]);
        }
    }, [citiesRes]);

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
                labelButtonOkLoading="đang tạo"
                isButtonOkLoading={isLoading}
            >
                <div className="grid gap-4">
                    <div className="grid md:grid-cols-3 gap-2">
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
                        <GroupInput
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
                        <GroupInput
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
                                setData((prev) => ({
                                    ...prev,
                                    idCountry: val ? parseInt(val) : -1,
                                }))
                            }
                        />
                        <GroupSelect
                            labelText="thành phố"
                            options={cities && cities.length > 0 ? cities : []}
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
                                    idCity: val ? parseInt(val) : -1,
                                }))
                            }
                        />
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
                            labelText="fax"
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
                            labelText="thư điện tử"
                            value={data.email}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    email: e.target.value,
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
                        <GroupInput
                            labelFor="website"
                            labelText="trang web"
                            value={data.website}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    website: e.target.value,
                                }))
                            }
                        />
                        <GroupInput
                            labelFor="note"
                            labelText="ghi chú"
                            value={data.note}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    note: e.target.value,
                                }))
                            }
                        />
                    </div>
                </div>
            </Modal>
        </>
    );
};

export default CreateOfficeModal;
