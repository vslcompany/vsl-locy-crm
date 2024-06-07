import { Tab, TabGroup, TabList, TabPanel, TabPanels } from "@headlessui/react";

import {
    CustomerClassifyTable,
    CustomerContactTable,
    CustomerEvaluateTable,
    CustomerImExTable,
    CustomerMajorTable,
    CustomerOperationalTable,
    CustomerRouteTable,
} from "../table";

type TSectionProps = {
    id: number;
    isRefresh: boolean;
    onRefreshDone: () => void;
};

const CustomerRelatedListSection = ({
    id,
    isRefresh,
    onRefreshDone,
}: TSectionProps) => {
    return (
        <div className="space-y-4 md:p-2">
            <h3 className="subtitle">danh sách liên quan</h3>
            <div>
                <TabGroup className="space-y-6">
                    <TabList className="flex flex-wrap">
                        <Tab className="inline-block font-semibold text-sm py-2 px-4 border-b-2 border-transparent data-[hover]:text-gray-500 data-[hover]:border-gray-300 data-[selected]:border-[#70BC41] data-[selected]:text-[#70BC41] data-[focus]:text-[#70BC41]">
                            Danh sách người liên hệ
                        </Tab>
                        <Tab className="inline-block font-semibold text-sm py-2 px-4 border-b-2 border-transparent data-[hover]:text-gray-500 data-[hover]:border-gray-300 data-[selected]:border-[#70BC41] data-[selected]:text-[#70BC41] data-[focus]:text-[#70BC41]">
                            Danh sách tác nghiệp
                        </Tab>
                        <Tab className="inline-block font-semibold text-sm py-2 px-4 border-b-2 border-transparent data-[hover]:text-gray-500 data-[hover]:border-gray-300 data-[selected]:border-[#70BC41] data-[selected]:text-[#70BC41] data-[focus]:text-[#70BC41]">
                            Danh sách nghiệp vụ
                        </Tab>
                        <Tab className="inline-block font-semibold text-sm py-2 px-4 border-b-2 border-transparent data-[hover]:text-gray-500 data-[hover]:border-gray-300 data-[selected]:border-[#70BC41] data-[selected]:text-[#70BC41] data-[focus]:text-[#70BC41]">
                            Danh sách phân loại
                        </Tab>
                        <Tab className="inline-block font-semibold text-sm py-2 px-4 border-b-2 border-transparent data-[hover]:text-gray-500 data-[hover]:border-gray-300 data-[selected]:border-[#70BC41] data-[selected]:text-[#70BC41] data-[focus]:text-[#70BC41]">
                            Danh sách đánh giá
                        </Tab>
                        <Tab className="inline-block font-semibold text-sm py-2 px-4 border-b-2 border-transparent data-[hover]:text-gray-500 data-[hover]:border-gray-300 data-[selected]:border-[#70BC41] data-[selected]:text-[#70BC41] data-[focus]:text-[#70BC41]">
                            Danh sách tuyến hàng
                        </Tab>
                        <Tab className="inline-block font-semibold text-sm py-2 px-4 border-b-2 border-transparent data-[hover]:text-gray-500 data-[hover]:border-gray-300 data-[selected]:border-[#70BC41] data-[selected]:text-[#70BC41] data-[focus]:text-[#70BC41]">
                            Danh sách xuất nhập khẩu
                        </Tab>
                    </TabList>
                    <TabPanels>
                        <TabPanel>
                            <CustomerContactTable
                                id={id}
                                isRefresh={isRefresh}
                                onRefreshDone={onRefreshDone}
                            />
                        </TabPanel>
                        <TabPanel>
                            <CustomerOperationalTable
                                id={id}
                                isRefresh={isRefresh}
                                onRefreshDone={onRefreshDone}
                            />
                        </TabPanel>
                        <TabPanel>
                            <CustomerMajorTable
                                id={id}
                                isRefresh={isRefresh}
                                onRefreshDone={onRefreshDone}
                            />
                        </TabPanel>
                        <TabPanel>
                            <CustomerClassifyTable
                                id={id}
                                isRefresh={isRefresh}
                                onRefreshDone={onRefreshDone}
                            />
                        </TabPanel>
                        <TabPanel>
                            <CustomerEvaluateTable
                                id={id}
                                isRefresh={isRefresh}
                                onRefreshDone={onRefreshDone}
                            />
                        </TabPanel>
                        <TabPanel>
                            <CustomerRouteTable
                                id={id}
                                isRefresh={isRefresh}
                                onRefreshDone={onRefreshDone}
                            />
                        </TabPanel>
                        <TabPanel>
                            <CustomerImExTable
                                id={id}
                                isRefresh={isRefresh}
                                onRefreshDone={onRefreshDone}
                            />
                        </TabPanel>
                    </TabPanels>
                </TabGroup>
            </div>
        </div>
    );
};

export default CustomerRelatedListSection;
