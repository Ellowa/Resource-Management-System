import { GETRequest } from './APIController';

export function Login() {
    //Temp for preserving imports from auto deleting themselves
    const { data, error, isLoading } = GETRequest('/api/Requests')

    return {
        requests: data,
        isLoading,
        isError: error
    }
}

export function Logout() {

}

export function RefreshToken() {

}