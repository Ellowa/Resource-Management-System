import { GETRequest } from './APIController';

// Добавить ресурс
export function AddResource() {

}

// Добавить тип ресурса
export function AddResourceType() {

}

// Изменить данные ресурса (имя, серийный номер, тип) по id (id изменить нельзя)
export function ChangeResourceByID() {

}

// Изменить данные типа ресурса
export function ChangeResourceType() {

}

// Удалить ресурс
export function DeleteResource() {

}

// Удалить тип ресурса
export function DeleteResourceType() {

}

// Просмотр списка всех ресурсов
export function GetAllResources() {
    const { data, error, isLoading } = GETRequest(`/api/Resource`)

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
export function GetScheduleByResourceID() {
    const { data, error, isLoading } = GETRequest(`/api/Resource/schedule/${id}`)

    return {
        schedule: data,
        isLoading,
        isError: error
    }
}

// Просмотр расписание ресурса по id пользователя
export function GetScheduleByUserID() {
    const { data, error, isLoading } = GETRequest(`/api/Resource/user/${id}`)

    return {
        schedule: data,
        isLoading,
        isError: error
    }
}