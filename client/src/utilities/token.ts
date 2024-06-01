export function isExpiredToken(tokenExp: number): boolean {
    // Check if the token is expired
    const date = new Date();

    const isExpired = tokenExp < date.getTime() / 1000;

    return isExpired;
}
