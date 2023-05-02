import { AddResource, DeleteResource, GetAllResources, GetResourceByID } from "@/fetchers/ResourceController";

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

export default function Example() {
    return (
        <>
            <Resources />
            <br />
            <ResourceById />
            <br />
            Add Resource:
            <ResourceAdder />
            <br />
            Delete Resource:
            <ResourceDeleter />
        </>
    )
}