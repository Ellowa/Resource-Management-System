import { DELETERequest, GETRequest, POSTRequest, PUTRequest } from './APIController';

// Добавить запрос
export async function AddRequest(data) {
    const [isError, errormessage] = await POSTRequest(`/api/Request/`, data);
    if (isError) return errormessage;
}

// Подтвердить запрос
export async function ConfirmRequest(id) {
    const [isError, errormessage] = await PUTRequest(`/api/Request/confirm/${id}`, null);
    if (isError) return errormessage;
}

// Удалить запрос
export async function DeleteRequest(id) {
    const [isError, errormessage] = await DELETERequest(`/api/Request/${id}`);
    if (isError) return errormessage;
}

// Отклонить запрос
export async function DenyRequest(id) {
    const [isError, errormessage] = await DELETERequest(`/api/Request/deny/${id}`);
    if (isError) return errormessage;
}

// Просмотр списка запросов
export function GetAllRequests() {
    const { data, error, isLoading } = GETRequest(`/api/Request/`)

    return {
        requests: data,
        isLoading,
        isError: error
    }
}

// Просмотр запроса по id
export function GetRequestByID(id) {
    const { data, error, isLoading } = GETRequest(`/api/Request/${id}`)

    return {
        request: data,
        isLoading,
        isError: error
    }
}

// Просмотр списка запросов по пользователю
export function GetRequestByUserID(id) {
    const { data, error, isLoading } = GETRequest(`/api/Request/user/${id}`)

    return {
        requests: data,
        isLoading,
        isError: error
    }
}