import useSWR from 'swr'

const fetcher = (...args) => fetch(...args).then((res) => res.json())

export function AddUser() {

}

export function ChangeUser() {

}

export function DeleteUser() {

}

export function GetAllUsers() {
    const { data, error, isLoading } = useSWR('/api/Users', fetcher)

    return {
        users: data,
        isLoading,
        isError: error
    }
}