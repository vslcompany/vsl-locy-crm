import { TProfileDto } from "./dto";

export type TAuthContextProps = {
    user: TProfileDto | null;
    updateData: (payload: TProfileDto | null) => void;
};
