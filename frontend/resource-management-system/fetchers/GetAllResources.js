import useSWR from 'swr'

const fetcher = (...args) => fetch(...args).then((res) => res.json())

export default function GetAllResources() {
    const { data, error, isLoading } = useSWR('/api/Resource', fetcher)

    return {
        resources: data,
        isLoading,
        isError: error
    }
}