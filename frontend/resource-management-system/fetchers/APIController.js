import axios from 'axios';
import { getSession } from 'next-auth/react';
import useSWR, { mutate } from 'swr';

const client = axios.create();

const getToken = async () => {
    const session = await getSession();
    return session?.accessToken;
}

client.interceptors.request.use(async (config) => {
    config.headers["Content-Type"] = 'application/json';
    config.headers["Authorization"] = `Bearer ${await getToken()}`;
    return config;
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

export async function GETRequestSTD(url) {
    try {
        const res = await client.get(url);
        return [false, res.data];
    } catch (error) {
        console.error(error);
        return [true, error.response.data.message];
    }
}

export async function POSTRequest(url, data) {
    try {
        await client.post(url, data);
        mutate(url);
        return [false];
    } catch (error) {
        console.error(error);
        return [true, error.response.data.message];
    }
}

export async function PUTRequest(url, data) {
    try {
        await client.put(url, data);
        mutate(url);
        return [false];
    } catch (error) {
        console.error(error);
        return [true, error.response.data.message];
    }
}

export async function DELETERequest(url, id) {
    try {
        await client.delete(`${url}${id}`);
        mutate(url);
        return [false];
    } catch (error) {
        console.error(error);
        return [true, error.response.data.message];
    }
}