import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { getCitiesByIdCountry } from "@/api";
import { GroupInput, GroupSelect, GroupTextArea, Modal } from "@/components";
import { initPort } from "@/constants";
import { useCategory } from "@/hooks";
import { TCityDto, TCreatePortRequest } from "@/types";

import { TCreateModalProps } from "../../types";

const CreatePortModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "tạo",
}: TCreateModalProps<TCreatePortRequest>) => {
    const [data, setData] = useState<TCreatePortRequest>(initPort);
    const [cities, setCities] = useState<TCityDto[] | []>([]);

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
            data.idQuocGia !== undefined &&
            data.idQuocGia !== null &&
            data.idQuocGia !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result) setData(initPort);
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
                                    idQuocGia: val ? parseInt(val) : -1,
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

export default CreatePortModal;
