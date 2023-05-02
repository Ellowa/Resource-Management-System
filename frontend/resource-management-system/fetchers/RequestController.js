import { GETRequest } from './APIController';

export function AddRequest() {

}

export function ApproveRequest() {

}

export function DeleteRequest() {

}

export function DenyRequest() {

}

export function GetAllRequests() {
    const { data, error, isLoading } = GETRequest('/api/Request/')

    return {
        requests: data,
        isLoading,
        isError: error
    }
}

export function GetRequestByID(id) {

}

export function GetRequestByUserID(id) {

}