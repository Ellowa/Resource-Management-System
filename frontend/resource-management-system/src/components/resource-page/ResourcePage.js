import { GetAllResources } from "@/fetchers/ResourceController";

function TableData() {
    const { resources, isLoading, isError } = GetAllResources();

    if (isLoading) return <tr><td>Loading...</td></tr>
    if (isError) return <tr><td>Error</td></tr>
    return (
        <>
            {
                resources.map((resource) => (
                    <tr key={resource.id}>
                        <td>{resource.name}</td>
                        <td><button>Додати</button></td>
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

            <div className="table">
                <div className="table__header">
                    <table cellPadding="0" cellSpacing="0" border="0">
                        <thead>
                            <tr>
                                <th>Назва ресурсу</th>
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