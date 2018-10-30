import HttpClient from "./../tools/httpclient"
import store from "./../vuexStore"
import { Config } from "./../tools/config"

export class UserService {

    async signIn(userName: string, password: string): Promise<boolean> {
        let tokenContainer = await HttpClient.Post<any>(Config.Api + '/auth/signin', { userName, password });
        if (tokenContainer.token) {
            store.commit('identity/enrollToken', tokenContainer.token);
        } else {
            return false;
        }
        return true;
    }

    async signUp(userName: string, password: string, email: string): Promise<boolean> {
        let tokenContainer = await HttpClient.Post<any>(Config.Api + '/auth/signup', { userName, password, email });
        if (tokenContainer.token) {
            store.commit('identity/enrollToken', tokenContainer.token);
        } else {
            return false;
        }

        return true;
    }
}

const userServiceInstance = new UserService;
export default userServiceInstance;