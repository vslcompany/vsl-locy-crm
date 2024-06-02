export const gender = [
    {
        id: 0,
        nameVI: "nam",
    },
    {
        id: 1,
        nameVI: "nữ",
    },
];

export const categories = [
    {
        label: "chức vụ",
        component: "PositionTable",
    },
    {
        label: "phòng ban",
        component: "DepartmentTable",
    },
    {
        label: "văn phòng",
        component: "OfficeTable",
    },
    {
        label: "thành phố",
        component: "CityTable",
    },
    {
        label: "quốc gia",
        component: "CountryTable",
    },
    {
        label: "cảng",
        component: "PortTable",
    },
    {
        label: "loại doanh nghiệp",
        component: "BusinessTable",
    },
    {
        label: "loại hình vận chuyển",
        component: "TransportationTable",
    },
    {
        label: "loại tác nghiệp",
        component: "OperationalTable",
    },
    {
        label: "nghiệp vụ",
        component: "MajorTable",
    },
    {
        label: "phân loại khách hàng",
        component: "TypeOfCustomerTable",
    },
    {
        label: "đánh giá khách hàng",
        component: "CustomerTypeTable",
    },
].sort((a, b) => {
    let comparition = 0;

    if (a.label.toUpperCase() > b.label.toUpperCase()) {
        comparition = 1;
    }
    if (a.label.toUpperCase() < b.label.toUpperCase()) {
        comparition = -1;
    }

    return comparition;
});

export const statusCustomer = ["chưa giao", "đã giao", "đã nhận", "từ chối"];
