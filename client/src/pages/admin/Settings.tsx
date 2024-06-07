import { Tab, TabGroup, TabList, TabPanel, TabPanels } from "@headlessui/react";

import { ChangePasswordSection } from "@/layouts";

const Settings = () => {
    return (
        <>
            <h2 className="title">Cài đặt</h2>
            <div>
                <TabGroup className="space-y-6">
                    <TabList className="flex">
                        <Tab className="inline-block font-semibold py-2 px-4 border-b-2 border-transparent data-[hover]:text-gray-500 data-[hover]:border-gray-300 data-[selected]:border-[#70BC41] data-[selected]:text-[#70BC41] data-[focus]:text-[#70BC41]">
                            Đổi mật khẩu
                        </Tab>
                    </TabList>
                    <TabPanels>
                        <TabPanel>
                            <ChangePasswordSection />
                        </TabPanel>
                    </TabPanels>
                </TabGroup>
            </div>
        </>
    );
};

export default Settings;
