export const isNumber = (value: string) => {
    if (value === "") return false;
    return /^[0-9]*$/.test(value);
};
