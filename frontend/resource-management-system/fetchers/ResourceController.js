import { DELETERequest, GETRequest, POSTRequest, PUTRequest } from './APIController';

// Добавить ресурс
export async function AddResource(data) {
    const [isError, errormessage] = await POSTRequest(`/api/Resource/`, data);
    if (isError) return errormessage;
}

// Добавить тип ресурса
export async function AddResourceType(data) {
    const [isError, errormessage] = await POSTRequest(`/api/Resource/type/`, data);
    if (isError) return errormessage;
}

// Изменить данные ресурса (имя, серийный номер, тип) по id (id изменить нельзя)
export async function ChangeResourceByID(id, data) {
    const [isError, errormessage] = await PUTRequest(`/api/Resource/${id}`, data);
    if (isError) return errormessage;

}

// Изменить данные типа ресурса
export async function ChangeResourceType(id, data) {
    const [isError, errormessage] = await PUTRequest(`/api/Resource/type/${id}`, data);
    if (isError) return errormessage;
}

// Удалить ресурс
export async function DeleteResource(id) {
    const [isError, errormessage] = await DELETERequest(`/api/Resource/`, id);
    if (isError) return errormessage;
}

// Удалить тип ресурса
export async function DeleteResourceType(id) {
    const [isError, errormessage] = await DELETERequest(`/api/Resource/type/${id}`);
    if (isError) return errormessage;
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