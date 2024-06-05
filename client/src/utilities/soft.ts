export const sortStringCb = (a: string, b: string) => {
    let comparition = 0;

    if (a.toUpperCase() > b.toUpperCase()) {
        comparition = 1;
    }
    if (a.toUpperCase() < b.toUpperCase()) {
        comparition = -1;
    }

    return comparition;
};
