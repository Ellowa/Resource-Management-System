import { DELETERequest, GETRequest, POSTRequest, PUTRequest } from './APIController';

//Добавить учетную запись
export function AddUser(data) {
    POSTRequest(`/api/User/add`, data);
}

//Изменить данные учентной записи по ID
export function ChangeUser(id, data) {
    PUTRequest(`/api/User/change/${id}`, data);
}

//Удалить учётную запись
export function DeleteUser() {
    DELETERequest(`/api/User/delete/${id}`);
}

//Просмотр списка всех учётных записей
export function GetAllUsers() {
    const { data, error, isLoading } = GETRequest(`/api/User/`)

    return {
        users: data,
        isLoading,
        isError: error
    }
}

//Просмотр учётной записи по ID
export function GetUserByID() {
    const { data, error, isLoading } = GETRequest(`/api/User/${id}`)

    return {
        user: data,
        isLoading,
        isError: error
    }
}