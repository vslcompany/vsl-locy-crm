import { useEffect, useState } from "react";
import { useQuery } from "react-query";

import { getPortsByIdCountry } from "@/api";
import { GroupInput, GroupSelect, Modal } from "@/components";
import { initCustomerImEx } from "@/constants";
import { useAuth } from "@/contexts";
import { useCategory } from "@/hooks";
import {
    TCreateCustomerImExRequest,
    TPortDto,
    TAuthContextProps,
} from "@/types";

import { TCreateCustomerInfoModalProps } from "../../types";

const CreateCustomerImExModal = ({
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
}: TCreateCustomerInfoModalProps<TCreateCustomerImExRequest>) => {
    const { user }: TAuthContextProps = useAuth();

    const [data, setData] = useState<TCreateCustomerImExRequest>({
        ...initCustomerImEx,
        idCustomer: idCustomer,
        idUserCreate: user?.id as number,
    });

    const [portsFrom, setPortsFrom] = useState<TPortDto[] | []>([]);
    const [portsTo, setPortsTo] = useState<TPortDto[] | []>([]);

    const { countries } = useCategory();

    const { data: portsFromRes } = useQuery({
        queryKey: ["portsFrom", data.idFromCountry],
        queryFn: () => getPortsByIdCountry(data.idFromCountry as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data.idFromCountry !== undefined &&
            data.idFromCountry !== -1,
    });

    const { data: portsToRes } = useQuery({
        queryKey: ["portsTo", data.idToCountry],
        queryFn: () => getPortsByIdCountry(data.idToCountry as number),
        cacheTime: Infinity,
        staleTime: Infinity,
        enabled:
            localStorage.getItem("token") != null &&
            localStorage.getItem("token") != "" &&
            data.idToCountry !== undefined &&
            data.idToCountry !== -1,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async () => {
        const result = await onSubmit(data);

        if (result)
            setData({
                ...initCustomerImEx,
                idCustomer: idCustomer as number,
                idUserCreate: user?.id as number,
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
                <div className="grid md:grid-cols-2 gap-4">
                    <GroupInput
                        type="date"
                        labelFor="date"
                        labelText="ngày thực hiện"
                        value={data.date}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                date: e.target.value,
                            }))
                        }
                    />
                </div>
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
                            data.idFromCountry
                                ? data.idFromCountry.toString()
                                : undefined
                        }
                        onValueChange={(val) =>
                            setData((prev) => ({
                                ...prev,
                                idFromCountry: val ? parseInt(val) : undefined,
                            }))
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
                            setData((prev) => ({
                                ...prev,
                                idFromPort: val ? parseInt(val) : undefined,
                            }))
                        }
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
                            data.idToCountry
                                ? data.idToCountry.toString()
                                : undefined
                        }
                        onValueChange={(val) =>
                            setData((prev) => ({
                                ...prev,
                                idToCountry: val ? parseInt(val) : undefined,
                            }))
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
                            data.idToPort ? data.idToPort.toString() : undefined
                        }
                        onValueChange={(val) =>
                            setData((prev) => ({
                                ...prev,
                                idToPort: val ? parseInt(val) : undefined,
                            }))
                        }
                    />
                </div>
                <GroupInput
                    labelFor="type"
                    labelText="loại"
                    value={data.type}
                    onChange={(e) =>
                        setData((prev) => ({
                            ...prev,
                            type: e.target.value,
                        }))
                    }
                />
                <div className="grid md:grid-cols-2 gap-4">
                    <GroupInput
                        labelFor="term"
                        labelText="term"
                        value={data.term}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                term: e.target.value,
                            }))
                        }
                    />
                    <GroupInput
                        labelFor="code"
                        labelText="Code (HS Code)"
                        value={data.code}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                code: e.target.value,
                            }))
                        }
                    />
                </div>
                <GroupInput
                    labelFor="commd"
                    labelText="commd"
                    value={data.commd}
                    onChange={(e) =>
                        setData((prev) => ({
                            ...prev,
                            commd: e.target.value,
                        }))
                    }
                />
                <div className="grid md:grid-cols-4 gap-4">
                    <div className="col-span-2">
                        <GroupInput
                            labelFor="vessel"
                            labelText="vessel"
                            value={data.vessel}
                            onChange={(e) =>
                                setData((prev) => ({
                                    ...prev,
                                    vessel: e.target.value,
                                }))
                            }
                        />
                    </div>
                    <GroupInput
                        labelFor="vol"
                        labelText="vol"
                        value={data.vol}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                vol: e.target.value,
                            }))
                        }
                    />
                    <GroupInput
                        labelFor="unt"
                        labelText="unt"
                        value={data.unt}
                        onChange={(e) =>
                            setData((prev) => ({
                                ...prev,
                                unt: e.target.value,
                            }))
                        }
                    />
                </div>
            </div>
        </Modal>
    );
};

export default CreateCustomerImExModal;
