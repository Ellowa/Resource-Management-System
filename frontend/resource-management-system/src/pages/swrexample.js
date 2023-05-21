import { AddRequest } from "@/fetchers/RequestController";
import { AddResource, DeleteResource, GetAllResources, GetResourceByID } from "@/fetchers/ResourceController";
import { getSession } from "next-auth/react";
import useAuth from "../components/useAuth";
function Resources() {
    const { resources, isLoading, isError } = GetAllResources();

    if (isLoading) return <div>Loading...</div>
    if (isError) return <div>Error</div>
    return (
        <div>
            <h1>Resources</h1>
            <ul>
                {resources.map((resource) => (
                    <li key={resource.id}>{resource.id}: {resource.name}</li>
                ))}
            </ul>
        </div>
    )
}

function ResourceById() {
    const { resource, isLoading, isError } = GetResourceByID(2);

    if (isLoading) return <div>Loading...</div>
    if (isError) return <div>Error</div>
    return (
        <div>
            <h1>Resource 1</h1>
            <p>{resource.name}</p>
        </div>
    )
}

function ResourceAdder() {
    const handleSubmit = async (e) => {
        e.preventDefault();

        const data = {
            name: e.target.name.value,
            serialNumber: e.target.serialNumber.value,
            resourceTypeId: e.target.resourceTypeId.value
        }

        AddResource(data);
    }
    return (
        <form onSubmit={handleSubmit}>
            <label htmlFor="name">Name</label>
            <input type="text" id="name" name="name" required />

            <label htmlFor="serialNumber">Serial Number</label>
            <input type="text" id="serialNumber" name="serialNumber" required />

            <label htmlFor="resourceTypeId">Resource Type ID</label>
            <input type="number" id="resourceTypeId" name="resourceTypeId" required />

            <button type="submit">Submit</button>
        </form>
    )
}

function ResourceDeleter() {
    const handleSubmit = async (e) => {
        e.preventDefault();

        const id = e.target.id.value;

        DeleteResource(id);
    }
    return (
        <form onSubmit={handleSubmit}>
            <label htmlFor="id">ID</label>
            <input type="number" id="id" name="id" required />

            <button type="submit">Submit</button>
        </form>
    )
}

function ErrorTester() {
    const handleSubmit = async (e) => {
        e.preventDefault();

        const data = {
            start: e.target.start.value,
            end: e.target.end.value,
            purpose: e.target.purpose.value,
            resourceId: e.target.resourceId.value,
            userId: e.target.userId.value
        }

        const errormessage = await AddRequest(data);
        if (errormessage) alert(errormessage);
    }
    return (
        <form onSubmit={handleSubmit}>
            <label htmlFor="start">Start</label>
            <input type="datetime-local" id="start" name="start" required />

            <label htmlFor="end">End</label>
            <input type="datetime-local" id="end" name="end" required />

            <label htmlFor="purpose">Purpose</label>
            <input type="text" id="purpose" name="purpose" required />

            <label htmlFor="resourceId">Resource ID</label>
            <input type="number" id="resourceId" name="resourceId" required />

            <label htmlFor="userId">User ID</label>
            <input type="number" id="userId" name="userId" required />

            <button type="submit">Submit</button>
        </form>
    )
}

function IsAuthenticated() {
    const isAuthenticated = useAuth(true);

    const sessionReciever = async () => {
        const sess = await getSession();
        console.log(sess);
    }

    if (!isAuthenticated) {
        return <div>Not authenticated</div>
    }
    return (
        <>
            <div>Authenticated</div>
            <br />
            <button onClick={sessionReciever}>Session Reciever</button>
        </>
    )
}

export default function Example() {
    return (
        <>
            <Resources />
            <br />
            <ResourceById />
            <br />
            Changes made below should automatically update the resources above without requiring page refresh. (May have delays)
            <br />
            Add Resource:
            <ResourceAdder />
            <br />
            Delete Resource:
            <ResourceDeleter />
            <br />
            Error test (Request adder):
            <ErrorTester />
            <br />
            LoginTest:
            <IsAuthenticated />
        </>
    )
}