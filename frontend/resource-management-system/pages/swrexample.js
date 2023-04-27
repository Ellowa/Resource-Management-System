import { GetAllResources } from '../fetchers/ResourceController';

function Resources() {
    const { resources, isLoading, isError } = GetAllResources();

    if (isLoading) return <div>Loading...</div>
    if (isError) return <div>Error</div>
    console.log(resources)
    return (
        <div>
            <h1>Resources</h1>
            <ul>
                {resources.map((resource) => (
                    <li key={resource.id}>{resource.name}</li>
                ))}
            </ul>
        </div>
    )
}

export default function Example() {
    return (
        <Resources />
    )
}