import Axios, { AxiosResponse } from "axios";

export class RequestError extends Error {
    code!: number;
    message!: string;

    constructor(code: number, message: string) {
        super(message);
        this.code = code;
        this.message = message;
    }
}

export class HttpClient {

    async Get<T>(url: string, token?: string): Promise<T> {
        var response = await Axios.get(url, token == null ? undefined : { headers: { 'Authorization': "Bearer " + token } });
        return this.processResult<T>(response);
    }

    async Post<T>(url: string, data: any, token?: string) : Promise<T> {
        var response = await Axios.post(url, data, token == null ? undefined : { headers: { 'Authorization': "Bearer " + token } });
        return this.processResult<T>(response);
    }

    async Delete<T>(url: string, token?: string) : Promise<T> {
        var response = await Axios.delete(url, token == null ? undefined : { headers: { 'Authorization': "Bearer " + token } });
        return this.processResult<T>(response);
    }

    private processResult<T>(result: AxiosResponse): T {
        if (result.status !== 200) {
            throw new RequestError(result.status, result.statusText);
        }

        return result.data as T;
    }
}

const httpClientInstance = new HttpClient
export default httpClientInstance;