import { FormEvent, useRef } from "react";
import { Link } from "react-router-dom";

import { VslLogo } from "@/assets";
import { Button, GroupInput } from "@/components";
import { notification } from "@/utilities";

const ForgotPassword = () => {
    const emailRef = useRef<HTMLInputElement | null>(null);

    /**
     * * Handle events
     */
    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();

        if (emailRef.current?.value === "") {
            notification(false, "Bạn cần nhập email để lấy lại mật khẩu!");
        }

        console.log("forgot password");
    };

    return (
        <>
            <section className="h-screen flex justify-center items-center">
                <form
                    className="w-full max-w-[28rem] rounded-lg bg-white space-y-6 p-8 border border-gray-300 shadow-md"
                    onSubmit={handleSubmit}
                >
                    <div className="space-y-2">
                        <div className="w-full">
                            <img
                                className="w-full h-auto object-cover"
                                src={VslLogo}
                                alt="Vsl Logo"
                            />
                        </div>
                        <p className="first-letter:uppercase font-medium text-center">
                            nhập email của bạn để lấy lại mật khẩu.
                        </p>
                    </div>
                    <div className="space-y-2">
                        <GroupInput
                            labelFor="email"
                            labelText="email"
                            placeholder="Nhập email"
                            ref={emailRef}
                        />
                        <div className="flex justify-end ">
                            <Link
                                to="/auth/signin"
                                className="text-sm text-green-600 first-letter:uppercase hover:underline font-medium"
                            >
                                quay lại trang đăng nhập
                            </Link>
                        </div>
                    </div>
                    <Button
                        className="block w-full"
                        label="tìm kiếm"
                        // isLoading={logInMutation.isLoading}
                        textIsLoading="đang tìm kiếm"
                    />
                </form>
            </section>
        </>
    );
};

export default ForgotPassword;
