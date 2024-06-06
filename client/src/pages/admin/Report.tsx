import { Tab, TabGroup, TabList, TabPanel, TabPanels } from "@headlessui/react";

import { ReportWorkTable } from "@/layouts";

const Report = () => {
    return (
        <>
            <h2 className="title">báo cáo</h2>
            <div>
                <TabGroup className="space-y-6">
                    <TabList className="flex">
                        <Tab className="inline-block font-semibold py-2 px-4 border-b-2 border-transparent data-[hover]:text-gray-500 data-[hover]:border-gray-300 data-[selected]:border-[#70BC41] data-[selected]:text-[#70BC41] data-[focus]:text-[#70BC41]">
                            Công việc thực hiện
                        </Tab>
                    </TabList>
                    <TabPanels>
                        <TabPanel>
                            <ReportWorkTable />
                        </TabPanel>
                    </TabPanels>
                </TabGroup>
            </div>
        </>
    );
};

export default Report;
