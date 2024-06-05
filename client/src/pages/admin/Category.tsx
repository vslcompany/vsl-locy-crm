import { ReactNode, useMemo, useState } from "react";
import { Tab, TabGroup, TabList, TabPanel, TabPanels } from "@headlessui/react";

import { categories } from "@/constants";
import {
    BusinessTable,
    CityTable,
    CountryTable,
    DepartmentTable,
    MajorTable,
    OfficeTable,
    OperationalTable,
    PortTable,
    PositionTable,
    TransportationTable,
    TypeOfCustomerTable,
    CustomerTypeTable,
} from "@/layouts";
import { sortStringCb } from "@/utilities";

const categoryComponent: { [key: string]: ReactNode } = {
    PositionTable: <PositionTable />,
    DepartmentTable: <DepartmentTable />,
    OfficeTable: <OfficeTable />,
    CityTable: <CityTable />,
    CountryTable: <CountryTable />,
    PortTable: <PortTable />,
    BusinessTable: <BusinessTable />,
    TransportationTable: <TransportationTable />,
    OperationalTable: <OperationalTable />,
    MajorTable: <MajorTable />,
    TypeOfCustomerTable: <TypeOfCustomerTable />,
    CustomerTypeTable: <CustomerTypeTable />,
};

const Category = () => {
    const [search, setSearch] = useState<string>("");

    const filterCategories = useMemo(() => {
        if (search === "") {
            return categories;
        }

        const filterRs = categories
            .filter((c) =>
                c.label
                    .toLowerCase()
                    .replace(/\s+/g, "")
                    .includes(search.toLowerCase().replace(/\s+/g, ""))
            )
            .sort((a, b) => sortStringCb(a.label, b.label));

        return filterRs;
    }, [search]);

    return (
        <>
            <h2 className="title">quản lý danh mục</h2>
            <div>
                <TabGroup>
                    <div className="flex flex-col md:flex-row w-full">
                        <div className="flex-shrink-0 relative md:w-64 max-h-[46rem] overflow-auto no-scrollbar border-b md:border-b-0 md:border-r border-gray-200 dark:border-gray-700 dark:text-gray-400 p-4">
                            <input
                                className="sticky mb-4 -top-4 left-0 bg-white dark:bg-gray-900 px-4 py-3 text-gray-900 dark:text-white rounded-lg w-full border border-gray-300 dark:border-gray-600 outline-none"
                                type="text"
                                placeholder="Tìm kiếm danh mục..."
                                value={search}
                                onChange={(e) => setSearch(e.target.value)}
                            />
                            <TabList className="flex flex-wrap md:flex-col gap-4 text-sm font-medium text-center text-gray-500">
                                {filterCategories.map((category, index) => (
                                    <Tab
                                        key={index}
                                        className={({ selected }) => {
                                            return selected
                                                ? "inline-block p-4 first-letter:uppercase text-green-600 bg-gray-100 active dark:bg-gray-800 dark:text-green-500 outline-none font-semibold text-left"
                                                : "inline-block p-4 first-letter:uppercase hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300 outline-none font-semibold text-left";
                                        }}
                                    >
                                        {category.label}
                                    </Tab>
                                ))}
                            </TabList>
                        </div>
                        <TabPanels className="flex-1 w-full overflow-auto">
                            {filterCategories.map((category, index) => (
                                <TabPanel key={index} className="p-4">
                                    {categoryComponent[category.component]}
                                </TabPanel>
                            ))}
                        </TabPanels>
                    </div>
                </TabGroup>
            </div>
        </>
    );
};

export default Category;
