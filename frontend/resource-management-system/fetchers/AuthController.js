import useSWR from 'swr'

const fetcher = (...args) => fetch(...args).then((res) => res.json())

export function Login() {
    //Temp for preserving imports from auto deleting themselves
    const { data, error, isLoading } = useSWR('/api/Requests', fetcher)

    return {
        requests: data,
        isLoading,
        isError: error
    }
}

export function Logout() {

}

export function RefreshToken() {

}