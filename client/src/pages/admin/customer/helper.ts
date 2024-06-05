export const getListCustomerTable = (permission: string, username: string) => {
    if (permission.includes("1048576") && username === "admin") {
        return ["tất cả", "chưa nhận", "đã nhận", "đã giao", "đã xoá"];
    } else if (
        (permission.includes("1048576") && username !== "admin") ||
        permission.includes("7000") ||
        (permission.includes("7020") && permission.includes("7080"))
    ) {
        return [
            "tất cả",
            "chưa nhận",
            "đã nhận",
            "được giao",
            "đã giao",
            "đã xoá",
        ];
    } else if (permission.includes("7020") && !permission.includes("7080")) {
        return ["tất cả", "chưa nhận", "đã nhận", "được giao", "đã xoá"];
    } else if (!permission.includes("7020") && permission.includes("7080")) {
        return ["tất cả", "chưa nhận", "đã nhận", "được giao", "đã giao"];
    } else {
        return ["tất cả", "chưa nhận", "đã nhận", "được giao"];
    }
};
