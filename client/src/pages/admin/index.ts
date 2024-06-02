import { lazy } from "react";

export const Category = lazy(() => import("./Category"));
export const Dashboard = lazy(() => import("./Dashboard"));
export const Report = lazy(() => import("./Report"));
export const Settings = lazy(() => import("./Settings"));

export * from "./customer";
export * from "./hrm";
