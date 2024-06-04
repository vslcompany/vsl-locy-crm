export type TResponseData = {
    status: boolean;
    message: string;
    data: unknown;
};

export type TApiGetWithPageProps = {
    start: number;
    size: number;
    search: string;
};
