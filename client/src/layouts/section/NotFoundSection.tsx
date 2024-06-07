import { Link } from "react-router-dom";

const NotFoundSection = () => {
    return (
        <>
            <div className="w-full h-screen flex justify-center items-center">
                <div className="grid gap-6 bg-white dark:bg-gray-900 text-gray-900 dark:text-white">
                    <div className="relative">
                        <h2 className="text-9xl font-extrabold tracking-widest">
                            404
                        </h2>
                        <div className="bg-green-600 px-2 font-medium text-sm rounded rotate-12 absolute top-2/3 l-1/2 translate-x-2/3">
                            Trang không tồn tại
                        </div>
                    </div>
                    <button className="outline-none mx-auto">
                        <div className="relative inline-block text-sm font-medium text-green-600 group active:text-green-500 focus:outline-none focus:ring">
                            <span className="absolute inset-0 transition-transform translate-x-0.5 translate-y-0.5 bg-green-600 group-hover:translate-y-0 group-hover:translate-x-0"></span>
                            <span className="relative block px-8 py-3 bg-gray-800 border border-current">
                                <Link to="/">Về lại trang chủ</Link>
                            </span>
                        </div>
                    </button>
                </div>
            </div>
        </>
    );
};

export default NotFoundSection;
