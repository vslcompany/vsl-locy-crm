import { useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getAllTransportations, getPortsByIdCountry } from "@/api";
import { GroupSelect, Modal } from "@/components";
import { useCategory } from "@/hooks";
import {
    TPortDto,
    TTransportationDto,
    TUpdateCustomerRouteRequest,
} from "@/types";
import { notification } from "@/utilities";

import { TUpdateCustomerInfoModalProps } from "../../types";

const UpdateCustomerRouteModal = ({
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
}: TUpdateCustomerInfoModalProps<TUpdateCustomerRouteRequest>) => {
    const [data, setData] = useState<TUpdateCustomerRouteRequest | null>(null);

    const [transportations, setTransportaions] = useState<
        TTransportationDto[] | []
    >([]);
    const [portsFrom, setPortsFrom] = useState<TPortDto[] | []>([]);
    const [portsTo, setPortsTo] = useState<TPortDto[] | []>([]);

    const { countries } = useCategory();

    const { data: portsFromRes } = useQuery({
        queryKey: ["portsFrom", data?.idQuocGiaDi],
        queryFn: () => getPortsByIdCountry(data?.idQuocGiaDi as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data != null &&
            data.idQuocGiaDi !== undefined &&
            data.idQuocGiaDi !== null &&
            data.idQuocGiaDi !== -1,
    });

    const { data: portsToRes } = useQuery({
        queryKey: ["portsTo", data?.idQuocGiaDen],
        queryFn: () => getPortsByIdCountry(data?.idQuocGiaDen as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data != null &&
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
            notification(false, "Thông tin tuyến hàng không thay đổi");
            return;
        }

        if (data) {
            await onSubmit(data);
        }
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
                                setData((prev) =>
                                    prev !== null
                                        ? {
                                              ...prev,
                                              idLoaiHinhVanChuyen: val
                                                  ? parseInt(val)
                                                  : undefined,
                                          }
                                        : null
                                )
                            }
                            required={true}
                        />
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupSelect
                                labelText="quốc gia đi"
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
                                    data.idQuocGiaDi
                                        ? data.idQuocGiaDi.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idQuocGiaDi: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
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
                                    data.idCangDi
                                        ? data.idCangDi.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idCangDi: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
                                }
                                required={true}
                            />
                        </div>
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupSelect
                                labelText="quốc gia đến"
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
                                    data.idQuocGiaDen
                                        ? data.idQuocGiaDen.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idQuocGiaDen: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
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
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idCangDen: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
                                }
                                required={true}
                            />
                        </div>
                    </div>
                )}
            </Modal>
        </>
    );
};

export default UpdateCustomerRouteModal;
