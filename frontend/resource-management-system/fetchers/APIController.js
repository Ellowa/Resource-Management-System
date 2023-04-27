import useSWR from 'swr'

const fetcher = (...args) => fetch(...args).then((res) => res.json())

export function GETRequest(url) {
    const { data, error, isLoading } = useSWR(url, fetcher)
    return {
        data,
        isLoading,
        error
    }
}

export function POSTRequest(url, data) {

}

export function PUTRequest(url, data) {

}

export function DELETERequest(url) {

}