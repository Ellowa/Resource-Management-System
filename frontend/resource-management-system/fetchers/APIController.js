import axios from 'axios'
import useSWR, { mutate } from 'swr'

const client = axios.create({
    headers: {
        // 'Authorisation': 'Bearer ' + localStorage.getItem('token'),
        'Content-Type': 'application/json',
    },
})

const fetcher = (...args) => client.get(...args).then((res) => res.data)

export function GETRequest(url) {
    const { data, error, isLoading } = useSWR(url, fetcher)
    return {
        data,
        isLoading,
        error
    }
}

export async function POSTRequest(url, data) {
    try {
        await client.post(url, data);
        mutate(url);
        return false;
    } catch (error) {
        console.error(error);
        return true;
    }
}

export async function PUTRequest(url, data) {
    try {
        await client.put(url, data);
        mutate(url);
        return false;
    } catch (error) {
        console.error(error);
        return true;
    }
}

export async function DELETERequest(url, id) {
    try {
        await client.delete(`${url}/${id.id}`);
        mutate(url);
        return false;
    } catch (error) {
        console.error(error);
        return true;
    }
}