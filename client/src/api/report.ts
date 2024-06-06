import { privateInstance } from "@/configs";

type TReportWorkParams = {
    start: number;
    size: number;
    startDate: string;
    endDate: string;
};

export const getReportWork = (payload: TReportWorkParams) => {
    return privateInstance.get("/report/work", {
        params: {
            ...payload,
        },
    });
};
