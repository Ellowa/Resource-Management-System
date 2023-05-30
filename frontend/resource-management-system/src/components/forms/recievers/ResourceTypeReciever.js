import { GetAllResourceTypes } from "@/fetchers/ResourceController";

export default function ResourceTypeReciever(defaultValue) {
    const { resourceTypes, isLoading, isError } = GetAllResourceTypes();
    if (isLoading) return <tr><td>Loading...</td></tr>
    if (isError) return <tr><td>Error</td></tr>
    return (
        <select name="resourceTypeId" id="resourceTypeId" defaultValue={defaultValue.defaultValue}>
            {
                resourceTypes.map((resourceType) => (
                    <option key={resourceType.id} value={resourceType.id}>{resourceType.typeName}</option>
                ))
            }
        </select>
    )
}