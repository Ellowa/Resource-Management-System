import useSWR from 'swr'

const fetcher = (...args) => fetch(...args).then((res) => res.json())

export function AddRequest() {

}

export function ApproveRequest() {

}

export function DeleteRequest() {

}

export function DenyRequest() {

}

export function GetAllRequests() {
    const { data, error, isLoading } = useSWR('/api/Requests', fetcher)

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