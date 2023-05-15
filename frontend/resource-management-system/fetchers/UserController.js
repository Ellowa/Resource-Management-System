import { DELETERequest, GETRequest, POSTRequest, PUTRequest } from './APIController';

//Добавить учетную запись
export async function AddUser(data) {
    console.log(data);
    const [isError, errormessage] = await POSTRequest(`/api/User/add`, data);
    if (isError) return errormessage;
}

//Изменить данные учентной записи по ID
export async function ChangeUser(id, data) {
    const [isError, errormessage] = await PUTRequest(`/api/User/change/${id}`, data);
    if (isError) return errormessage;
}

//Удалить учётную запись
export async function DeleteUser(id) {
    const [isError, errormessage] = await DELETERequest(`/api/User/delete/`, id);
    if (isError) return errormessage;
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
export function GetUserByID(id) {
    const { data, error, isLoading } = GETRequest(`/api/User/${id}`)

    return {
        user: data,
        isLoading,
        isError: error
    }
}