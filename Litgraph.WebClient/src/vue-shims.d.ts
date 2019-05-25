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

declare module '*.gql' {
    import { DocumentNode } from 'graphql'
    const value: DocumentNode;
    export default value;
}