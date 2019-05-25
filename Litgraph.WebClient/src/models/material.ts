export interface IMaterial {
    title?: String,
    brief?: String
}

export class Material implements IMaterial {
    id?: String
    title?: String
    brief?: String
}