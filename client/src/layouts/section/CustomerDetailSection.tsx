import { Loading } from "@/components";
import { TCustomerDto } from "@/types";

type TSectionProps = {
    data: TCustomerDto;
    isLoading: boolean;
};

const CustomerDetailSection = ({ data, isLoading }: TSectionProps) => {
    if (isLoading) {
        return (
            <div className="w-full min-h-96 flex justify-center items-center">
                <Loading />
            </div>
        );
    }

    return (
        <div className="space-y-4">
            <h3 className="subtitle">thông tin chi tiết</h3>
            <div className="grid md:grid-cols-2 gap-2">
                <div className="space-y-2 min-h-[3.25rem]">
                    <small className="block text-sm first-letter:uppercase">
                        mã khách hàng
                    </small>
                    <p className="text-gray-950">{data.code}</p>
                </div>
                <div className="space-y-2 min-h-[3.25rem]">
                    <small className="block text-sm first-letter:uppercase">
                        mã số thuế
                    </small>
                    <p className="text-gray-950">{data.taxCode}</p>
                </div>
            </div>
            <div className="space-y-2 min-h-[3.25rem]">
                <small className="block text-sm first-letter:uppercase">
                    tên khách hàng
                </small>
                <p className="text-gray-950">{data.nameVI}</p>
            </div>
            <div className="space-y-2 min-h-[3.25rem]">
                <small className="block text-sm first-letter:uppercase">
                    địa chỉ
                </small>
                <p className="text-gray-950">{data.addressVI}</p>
            </div>
            <div className="grid md:grid-cols-2 gap-2">
                <div className="space-y-2 min-h-[3.25rem]">
                    <small className="block text-sm first-letter:uppercase">
                        số điện thoại
                    </small>
                    <p className="text-gray-950">{data.phone}</p>
                </div>
                <div className="space-y-2 min-h-[3.25rem]">
                    <small className="block text-sm first-letter:uppercase">
                        số FAX
                    </small>
                    <p className="text-gray-950">{data.fax}</p>
                </div>
            </div>
            <div className="grid md:grid-cols-2 gap-2">
                <div className="space-y-2 min-h-[3.25rem]">
                    <small className="block text-sm first-letter:uppercase">
                        thư điện tử
                    </small>
                    <p className="text-gray-950">{data.email}</p>
                </div>
                <div className="space-y-2 min-h-[3.25rem]">
                    <small className="block text-sm first-letter:uppercase">
                        website
                    </small>
                    <p className="text-gray-950">{data.website}</p>
                </div>
            </div>
            <div className="space-y-2 min-h-[3.25rem]">
                <small className="block text-sm first-letter:uppercase">
                    ghi chú
                </small>
                <p className="text-gray-950">{data.note}</p>
            </div>
        </div>
    );
};

export default CustomerDetailSection;
