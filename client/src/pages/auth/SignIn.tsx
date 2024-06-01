import { FormEvent, useRef } from "react";

import { VslLogo } from "@/assets";
import { Button, GroupInput } from "@/components";
import { notification } from "@/utilities";
import { Link } from "react-router-dom";

const SignIn = () => {
    const usernameRef = useRef<HTMLInputElement | null>(null);
    const passwordRef = useRef<HTMLInputElement | null>(null);

    /**
     * * Handle events
     */
    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();

        const payload = {
            username: usernameRef.current?.value.trim() || "",
            password: passwordRef.current?.value.trim() || "",
        };

        if (payload.username === "" || payload.password === "") {
            notification(false, "Bạn cần nhập đủ thông tin đăng nhập!");
            return;
        }

        console.log(payload);
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
                        <p className="first-letter:uppercase text-gray-950 font-medium text-center">
                            đăng nhập vào tài khoản của bạn.
                        </p>
                    </div>
                    <div className="space-y-4">
                        <GroupInput
                            labelFor="username"
                            labelText="tên đăng nhập"
                            placeholder="Nhập tên đăng nhập"
                            required={true}
                            ref={usernameRef}
                        />
                        <GroupInput
                            type="password"
                            labelFor="password"
                            labelText="mật khẩu"
                            placeholder="Nhập mật khẩu"
                            autoComplete="off"
                            required={true}
                            ref={passwordRef}
                        />
                        <div className="flex justify-end">
                            <Link
                                to="/auth/forgot-password"
                                className="text-sm first-line:text-green-600 first-letter:uppercase hover:underline font-medium"
                            >
                                quên mật khẩu?
                            </Link>
                        </div>
                    </div>
                    <Button
                        className="block w-full"
                        label="đăng nhập"
                        // isLoading={logInMutation.isLoading}
                        textIsLoading="đang đăng nhập"
                    />
                </form>
            </section>
        </>
    );
};

export default SignIn;
