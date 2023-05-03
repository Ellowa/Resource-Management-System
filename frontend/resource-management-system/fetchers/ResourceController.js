import { DELETERequest, GETRequest, POSTRequest, PUTRequest } from './APIController';

// Добавить ресурс
export function AddResource(data) {
    POSTRequest(`/api/Resource/`, data);
}

// Добавить тип ресурса
export function AddResourceType(data) {
    POSTRequest(`/api/Resource/type/`, data);
}

// Изменить данные ресурса (имя, серийный номер, тип) по id (id изменить нельзя)
export function ChangeResourceByID(id, data) {
    PUTRequest(`/api/Resource/${id}`, data);

}

// Изменить данные типа ресурса
export function ChangeResourceType(id, data) {
    PUTRequest(`/api/Resource/type/${id}`, data);
}

// Удалить ресурс
export function DeleteResource(id) {
    DELETERequest(`/api/Resource/`, id);
}

// Удалить тип ресурса
export function DeleteResourceType(id) {
    DELETERequest(`/api/Resource/type/${id}`);
}

// Просмотр списка всех ресурсов
export function GetAllResources() {
    const { data, error, isLoading } = GETRequest(`/api/Resource/`)

    return {
        resources: data,
        isLoading,
        isError: error
    }
}

// Просмотр списка всех типов ресурса
export function GetAllResourceTypes() {
    const { data, error, isLoading } = GETRequest(`/api/Resource/type`)

    return {
        resourceTypes: data,
        isLoading,
        isError: error
    }
}

// Просмотр ресурса по id
export function GetResourceByID(id) {
    const { data, error, isLoading } = GETRequest(`/api/Resource/${id}`)

    return {
        resource: data,
        isLoading,
        isError: error
    }
}

// Просмотр расписание ресурса по id ресурса
export function GetScheduleByResourceID(id) {
    const { data, error, isLoading } = GETRequest(`/api/Resource/schedule/${id}`)

    return {
        schedule: data,
        isLoading,
        isError: error
    }
}

// Просмотр расписание ресурса по id пользователя
export function GetScheduleByUserID(id) {
    const { data, error, isLoading } = GETRequest(`/api/Resource/user/${id}`)

    return {
        schedule: data,
        isLoading,
        isError: error
    }
}