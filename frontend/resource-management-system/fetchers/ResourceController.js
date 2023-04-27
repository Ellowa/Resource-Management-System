import useSWR from 'swr'

const fetcher = (...args) => fetch(...args).then((res) => res.json())

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
    const { data, error, isLoading } = useSWR(`/api/Resource`, fetcher)

    return {
        resources: data,
        isLoading,
        isError: error
    }
}

export function GetAllResourcesOfType() {

}

export function GetResourceByID(id) {
    const { data, error, isLoading } = useSWR(`/api/Resource/${id}`, fetcher)

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