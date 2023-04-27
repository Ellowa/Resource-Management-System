import APIController from './APIController';

export function AddResource() {

}

export function AddResourceType() {

}

export function ChangeResourceByID() {

}

export function ChangeResourceType() {

}

export function DeleteResource() {

}

export function DeleteResourceType() {

}

export function GetAllResources() {
    const { data, error, isLoading } = APIController(`/api/Resource`)

    return {
        resources: data,
        isLoading,
        isError: error
    }
}

export function GetAllResourcesOfType() {

}

export function GetResourceByID(id) {
    const { data, error, isLoading } = APIController(`/api/Resource/${id}`)

    return {
        resource: data,
        isLoading,
        isError: error
    }
}

export function GetScheduleByResourceID() {

}

export function GetScheduleByUserID() {

}