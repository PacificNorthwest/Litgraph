import HttpClient from "./../tools/httpclient"
import { Config } from "./../tools/config"

export class UserService {

    async signIn(userName: string, password: string): Promise<string> {
        let tokenContainer = await HttpClient.Post<any>(Config.Api + '/auth/signin', { userName, password });
        return tokenContainer.token as string;
    }
}

const userServiceInstance = new UserService;
export default userServiceInstance;