import { GETRequest } from './APIController';
export function AddUser() {

}

export function ChangeUser() {

}

export function DeleteUser() {

}

export function GetAllUsers() {
    const { data, error, isLoading } = GETRequest('/api/User/')

    return {
        users: data,
        isLoading,
        isError: error
    }
}