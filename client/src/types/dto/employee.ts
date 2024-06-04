export type TEmployeeDto = {
    id: number;
    nameVI: string;
    nameEN: string;
};

export type TEmployeeGroupDto = {
    id: number;
    parentId: number;
    nameGroup: string;
    idNhanVien: number;
    nameVI: string;
    chucVu: string;
    flagViewAllGroup: boolean;
};
