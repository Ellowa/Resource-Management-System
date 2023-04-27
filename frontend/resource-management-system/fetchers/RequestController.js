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
    const { data, error, isLoading } = GETRequest('/api/Requests')

    return {
        requests: data,
        isLoading,
        isError: error
    }
}

export function GetRequestByID() {

}

export function GetRequestByUserID() {

}