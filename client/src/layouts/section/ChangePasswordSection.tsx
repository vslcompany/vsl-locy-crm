import { FormEvent, useRef } from "react";
import { useMutation } from "react-query";

import { changePassword } from "@/api";
import { Button, GroupInput } from "@/components";
import { useAuth } from "@/contexts";
import { TAuthContextProps, TChangePasswordRequest } from "@/types";
import { notification } from "@/utilities";

const ChangePasswordSection = () => {
    const { user }: TAuthContextProps = useAuth();

    const passwordRef = useRef<HTMLInputElement | null>(null);
    const newPasswordRef = useRef<HTMLInputElement | null>(null);
    const reNewPasswordRef = useRef<HTMLInputElement | null>(null);

    const changePasswordMutation = useMutation({
        mutationFn: changePassword,
    });

    /**
     * * Handle events
     */
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const payloadData: TChangePasswordRequest = {
            id: user?.id as number,
            password: passwordRef.current?.value ?? "",
            newPassword: newPasswordRef.current?.value ?? "",
        };

        if (
            payloadData.password === "" ||
            payloadData.newPassword === "" ||
            reNewPasswordRef.current?.value === ""
        ) {
            notification(false, "Bạn cần nhập đủ thông tin!");
            return;
        }

        if (payloadData.newPassword !== reNewPasswordRef.current?.value) {
            notification(false, "Mật khẩu mới không khớp!");
            return;
        }

        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const res: any = await changePasswordMutation.mutateAsync(payloadData);

        if (
            res.status &&
            passwordRef.current &&
            newPasswordRef.current &&
            reNewPasswordRef.current
        ) {
            passwordRef.current.value = "";
            newPasswordRef.current.value = "";
            reNewPasswordRef.current.value = "";
        }
    };

    return (
        <section className="w-full h-full flex items-center justify-center">
            <form
                className="w-full max-w-[28rem] rounded-lg bg-white space-y-4 px-8 pt-4 pb-8 border border-gray-300 shadow-md"
                onSubmit={handleSubmit}
            >
                <div className="first-letter:uppercase text-xl text-gray-950 font-medium text-center">
                    đổi mật khẩu
                </div>
                <div className="space-y-2">
                    <GroupInput
                        type="password"
                        labelFor="password"
                        labelText="mật khẩu"
                        placeholder="Nhập mật khẩu"
                        ref={passwordRef}
                    />
                    <GroupInput
                        type="password"
                        labelFor="newPassword"
                        labelText="mật khẩu mới"
                        placeholder="Nhập mật khẩu mới"
                        autoComplete="off"
                        ref={newPasswordRef}
                    />
                    <GroupInput
                        type="password"
                        labelFor="reNewPassword"
                        labelText="xác nhận mật khẩu mới"
                        placeholder="Nhập lại mật khẩu mới"
                        autoComplete="off"
                        ref={reNewPasswordRef}
                    />
                </div>
                <Button
                    className="block w-full"
                    label="đổi mật khẩu"
                    isLoading={changePasswordMutation.isLoading}
                    textIsLoading="đang đổi mật khẩu"
                />
            </form>
        </section>
    );
};

export default ChangePasswordSection;
