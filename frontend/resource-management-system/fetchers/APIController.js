import useSWR from 'swr'

const fetcher = (...args) => fetch(...args).then((res) => res.json())

export default function APICall(url) {
    const { data, error, isLoading } = useSWR(url, fetcher)
    return {
        data,
        isLoading,
        error
    }
}