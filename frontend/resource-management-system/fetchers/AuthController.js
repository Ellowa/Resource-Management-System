import { GETRequest } from './APIController';

export function Login() {
    //Temp for preserving imports from auto deleting themselves
    const { data, error, isLoading } = GETRequest(`/api/Request/`)

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