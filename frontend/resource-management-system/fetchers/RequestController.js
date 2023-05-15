import { DELETERequest, GETRequest, POSTRequest, PUTRequest } from './APIController';

// Добавить запрос
export async function AddRequest(data) {
    const [isError, errormessage] = await POSTRequest(`/api/Request/`, data);
    if (isError) return errormessage;
}

// Подтвердить запрос
export function ConfirmRequest(id) {
    PUTRequest(`/api/Request/confirm/${id}`, null);
}

// Удалить запрос
export function DeleteRequest(id) {
    DELETERequest(`/api/Request/${id}`);
}

// Отклонить запрос
export function DenyRequest(id) {
    DELETERequest(`/api/Request/deny/${id}`);
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