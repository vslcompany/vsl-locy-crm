import { useEffect, useState } from "react";
import { useQuery } from "react-query";
import { isEqual } from "lodash";

import { getPortsByIdCountry } from "@/api";
import { GroupInput, GroupSelect, Modal } from "@/components";
import { useCategory } from "@/hooks";
import { TPortDto, TUpdateCustomerImExRequest } from "@/types";
import { notification } from "@/utilities";

import { TUpdateCustomerInfoModalProps } from "../../types";

const UpdateCustomerImExModal = ({
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
}: TUpdateCustomerInfoModalProps<TUpdateCustomerImExRequest>) => {
    const [data, setData] = useState<TUpdateCustomerImExRequest | null>(null);

    const [portsFrom, setPortsFrom] = useState<TPortDto[] | []>([]);
    const [portsTo, setPortsTo] = useState<TPortDto[] | []>([]);

    const { countries } = useCategory();

    const { data: portsFromRes } = useQuery({
        queryKey: ["portsFrom", data?.idFromCountry],
        queryFn: () => getPortsByIdCountry(data?.idFromCountry as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data != null &&
            data.idFromCountry !== undefined &&
            data.idFromCountry !== -1,
    });

    const { data: portsToRes } = useQuery({
        queryKey: ["portsTo", data?.idToCountry],
        queryFn: () => getPortsByIdCountry(data?.idToCountry as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data != null &&
            data.idToCountry !== undefined &&
            data.idToCountry !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        if (isEqual(data, prevData)) {
            notification(false, "Thông tin xuất nhập khẩu không thay đổi");
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
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupInput
                                type="date"
                                labelFor="date"
                                labelText="ngày thực hiện"
                                value={data.date}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  date: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                        </div>
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
                                    data.idFromCountry
                                        ? data.idFromCountry.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idFromCountry: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupSelect
                                labelText="cảng đi"
                                options={portsFrom}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    data.idFromPort
                                        ? data.idFromPort.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idFromPort: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
                                }
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
                                    data.idToCountry
                                        ? data.idToCountry.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idToCountry: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupSelect
                                labelText="cảng đến"
                                options={portsTo}
                                option={{
                                    label: "nameVI",
                                    value: "id",
                                }}
                                value={
                                    data.idToPort
                                        ? data.idToPort.toString()
                                        : undefined
                                }
                                onValueChange={(val) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  idToPort: val
                                                      ? parseInt(val)
                                                      : undefined,
                                              }
                                            : null
                                    )
                                }
                            />
                        </div>
                        <GroupInput
                            labelFor="type"
                            labelText="loại"
                            value={data.type}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev !== null
                                        ? {
                                              ...prev,
                                              type: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <div className="grid md:grid-cols-2 gap-4">
                            <GroupInput
                                labelFor="term"
                                labelText="term"
                                value={data.term}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  term: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="code"
                                labelText="Code (HS Code)"
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
                        </div>
                        <GroupInput
                            labelFor="commd"
                            labelText="commd"
                            value={data.commd}
                            onChange={(e) =>
                                setData((prev) =>
                                    prev !== null
                                        ? {
                                              ...prev,
                                              commd: e.target.value,
                                          }
                                        : null
                                )
                            }
                        />
                        <div className="grid md:grid-cols-4 gap-4">
                            <div className="col-span-2">
                                <GroupInput
                                    labelFor="vessel"
                                    labelText="vessel"
                                    value={data.vessel}
                                    onChange={(e) =>
                                        setData((prev) =>
                                            prev !== null
                                                ? {
                                                      ...prev,
                                                      vessel: e.target.value,
                                                  }
                                                : null
                                        )
                                    }
                                />
                            </div>
                            <GroupInput
                                labelFor="vol"
                                labelText="vol"
                                value={data.vol}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  vol: e.target.value,
                                              }
                                            : null
                                    )
                                }
                            />
                            <GroupInput
                                labelFor="unt"
                                labelText="unt"
                                value={data.unt}
                                onChange={(e) =>
                                    setData((prev) =>
                                        prev !== null
                                            ? {
                                                  ...prev,
                                                  unt: e.target.value,
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

export default UpdateCustomerImExModal;
