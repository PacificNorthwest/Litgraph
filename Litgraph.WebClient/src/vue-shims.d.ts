declare module "*.vue" {
    import Vue from "vue";
    export default Vue;
}

declare module "*.svg" {
    const value: any;
    export = value;
}

declare module "*.js" {
    const value: any;
    export = value;
}