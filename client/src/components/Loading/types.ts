export type TLoadingProps = {
    showMessage?: boolean;
    message?: string;
    color?: string;
    direction?: "row" | "row-reverse" | "col" | "col-reverse";
    size?: "xs" | "sm" | "base" | "lg" | "xl" | "2xl" | "3xl" | "4xl" | "5xl";
    type?:
        | "balls"
        | "bars"
        | "bubbles"
        | "cubes"
        | "cylon"
        | "spin"
        | "spinningBubbles"
        | "spokes";
};
