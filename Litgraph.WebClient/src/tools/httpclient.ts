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

    async Get<T>(url: string): Promise<T | undefined> {
        try {
            var response = await Axios.get(url);
            return this.processResult<T>(response);
        } catch (e) {
            throw this.wrapError(e);
        }
    }

    async Post<T>(url: string, data: any) : Promise<T | undefined> {
        try {
            var response = await Axios.post(url, data);
            return this.processResult<T>(response);
        } catch (e) {
            throw this.wrapError(e);
        }
    }

    async Delete<T>(url: string) : Promise<T | undefined> {
        try {
        var response = await Axios.delete(url);
        return this.processResult<T>(response);
        } catch (e) {
            throw this.wrapError(e);
        }
    }

    private processResult<T>(result: AxiosResponse): T {
        if (result.status !== 200) {
            throw new RequestError(result.status, result.statusText);
        }

        return result.data as T;
    }

    private wrapError(e: any): RequestError {
        return new RequestError(e.response.status, e.response.data);
    }
}

const httpClientInstance = new HttpClient
export default httpClientInstance;