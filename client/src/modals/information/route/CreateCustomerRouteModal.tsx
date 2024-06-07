import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { getAllTransportations, getPortsByIdCountry } from "@/api";
import { GroupSelect, Modal } from "@/components";
import { initCustomerRoute } from "@/constants";
import { useCategory } from "@/hooks";
import {
    TCreateCustomerRouteRequest,
    TTransportationDto,
    TPortDto,
} from "@/types";

import { TCreateCustomerInfoModalProps } from "../../types";

const CreateCustomerRouteModal = ({
    isOpen,
    onClose,
    width = "lg",
    title = "Modal title",
    description,
    isLoading,
    onSubmit,
    labelButtonCancel = "huỷ",
    labelButtonOk = "tạo",
    idCustomer,
}: TCreateCustomerInfoModalProps<TCreateCustomerRouteRequest>) => {
    const [data, setData] = useState<TCreateCustomerRouteRequest>({
        ...initCustomerRoute,
        idCustomer: idCustomer,
    });

    const [transportations, setTransportaions] = useState<
        TTransportationDto[] | []
    >([]);
    const [portsFrom, setPortsFrom] = useState<TPortDto[] | []>([]);
    const [portsTo, setPortsTo] = useState<TPortDto[] | []>([]);

    const { countries } = useCategory();

    const { data: portsFromRes } = useQuery({
        queryKey: ["portsFrom", data.idQuocGiaDi],
        queryFn: () => getPortsByIdCountry(data.idQuocGiaDi as number),
        onSuccess: (data) => {
            if (data.status) {
                setPortsFrom(data.data as unknown as TPortDto[]);
            }
        },
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data.idQuocGiaDi !== undefined &&
            data.idQuocGiaDi !== null &&
            data.idQuocGiaDi !== -1,
    });

    const { data: portsToRes } = useQuery({
        queryKey: ["portsTo", data.idQuocGiaDen],
        queryFn: () => getPortsByIdCountry(data.idQuocGiaDen as number),
        onSuccess: (data) => {
            if (data.status) {
                setPortsTo(data.data as unknown as TPortDto[]);
            }
        },
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data.idQuocGiaDen !== undefined &&
            data.idQuocGiaDen !== null &&
            data.idQuocGiaDen !== -1,
    });

    const { data: transportationsRes } = useQuery({
        queryKey: "transportations",
        queryFn: getAllTransportations,
        onSuccess: (data) => {
            if (data.status) {
                setTransportaions(data.data as unknown as TTransportationDto[]);
            }
        },
        staleTime: Infinity,
        cacheTime: Infinity,
        refetchOnWindowFocus: false,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "",
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCustomerRoute,
                idCustomer: idCustomer as number,
            });
    };

    useEffect(() => {
        if (portsFromRes && portsFromRes.status) {
            setPortsFrom(portsFromRes.data as unknown as TPortDto[]);
        }
    }, [portsFromRes]);

    useEffect(() => {
        if (portsToRes && portsToRes.status) {
            setPortsTo(portsToRes.data as unknown as TPortDto[]);
        }
    }, [portsToRes]);

    useEffect(() => {
        if (transportationsRes && transportationsRes.status) {
            setTransportaions(
                transportationsRes.data as unknown as TTransportationDto[]
            );
        }
    }, [transportationsRes]);

    return (
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
                <GroupSelect
                    labelText="loại hình vận chuyển"
                    options={transportations}
                    option={{
                        label: "nameVI",
                        value: "id",
                    }}
                    value={
                        data.idLoaiHinhVanChuyen
                            ? data.idLoaiHinhVanChuyen.toString()
                            : undefined
                    }
                    onValueChange={(val) =>
                        setData((prev) => ({
                            ...prev,
                            idLoaiHinhVanChuyen: val
                                ? parseInt(val)
                                : undefined,
                        }))
                    }
                    required={true}
                />
                <div className="grid md:grid-cols-2 gap-4">
                    <GroupSelect
                        labelText="quốc gia đi"
                        options={
                            countries && countries.length > 0 ? countries : []
                        }
                        option={{
                            label: "nameVI",
                            value: "id",
                        }}
                        value={
                            data.idQuocGiaDi
                                ? data.idQuocGiaDi.toString()
                                : undefined
                        }
                        onValueChange={(val) =>
                            setData((prev) => ({
                                ...prev,
                                idQuocGiaDi: val ? parseInt(val) : undefined,
                            }))
                        }
                        required={true}
                    />
                    <GroupSelect
                        labelText="cảng đi"
                        options={portsFrom}
                        option={{
                            label: "nameVI",
                            value: "id",
                        }}
                        value={
                            data.idCangDi ? data.idCangDi.toString() : undefined
                        }
                        onValueChange={(val) =>
                            setData((prev) => ({
                                ...prev,
                                idCangDi: val ? parseInt(val) : undefined,
                            }))
                        }
                        required={true}
                    />
                </div>
                <div className="grid md:grid-cols-2 gap-4">
                    <GroupSelect
                        labelText="quốc gia đến"
                        options={
                            countries && countries.length > 0 ? countries : []
                        }
                        option={{
                            label: "nameVI",
                            value: "id",
                        }}
                        value={
                            data.idQuocGiaDen
                                ? data.idQuocGiaDen.toString()
                                : undefined
                        }
                        onValueChange={(val) =>
                            setData((prev) => ({
                                ...prev,
                                idQuocGiaDen: val ? parseInt(val) : undefined,
                            }))
                        }
                        required={true}
                    />
                    <GroupSelect
                        labelText="cảng đến"
                        options={portsTo}
                        option={{
                            label: "nameVI",
                            value: "id",
                        }}
                        value={
                            data.idCangDen
                                ? data.idCangDen.toString()
                                : undefined
                        }
                        onValueChange={(val) =>
                            setData((prev) => ({
                                ...prev,
                                idCangDen: val ? parseInt(val) : undefined,
                            }))
                        }
                        required={true}
                    />
                </div>
            </div>
        </Modal>
    );
};

export default CreateCustomerRouteModal;
