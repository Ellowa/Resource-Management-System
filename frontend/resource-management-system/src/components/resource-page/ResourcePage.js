import { DeleteResource, GetAllResources } from "@/fetchers/ResourceController";
import ResourceAdder from "../forms/ResourceAdderForm";
import ResourceChanger from "../forms/ResourceChangerForm";

function TableData() {
    const { resources, isLoading, isError } = GetAllResources();

    if (isLoading) return <tr><td>Loading...</td></tr>
    if (isError) return <tr><td>Error</td></tr>
    return (
        <>
            {
                resources.map((resource) => (
                    <tr key={resource.id}>
                        <td>{resource.id}</td>
                        <td>{resource.name}</td>
                        <td>{resource.serialNumber}</td>
                        <td>{resource.resourceTypeName}</td>
                        <td><ResourceChanger data={resource} /></td>
                        <td><button onClick={() => DeleteResource(resource.id)}>Видалити</button></td>
                    </tr>
                ))
            }
        </>
    )
}

export function ResourcePage() {

    return (
        <div className="main-page">
            <h2 className="main-text">Доступні ресурси</h2>
            <ResourceAdder />
            <div className="table">
                <div className="table__header">
                    <table cellPadding="0" cellSpacing="0" border="0"  >
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Назва ресурсу</th>
                                <th>Серійний номер</th>
                                <th>Тип ресурсу</th>
                                <th></th>
                                <th>Дія</th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div className="table__content">
                    <table cellPadding="0" cellSpacing="0" border="0">
                        <tbody>
                            <TableData />
                        </tbody>
                    </table>
                </div>

            </div>
        </div>

    );
}