import APIController from './APIController';
export function AddUser() {

}

export function ChangeUser() {

}

export function DeleteUser() {

}

export function GetAllUsers() {
    const { data, error, isLoading } = APIController('/api/Users')

    return {
        users: data,
        isLoading,
        isError: error
    }
}