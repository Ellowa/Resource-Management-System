import { GetAllResources } from "@/fetchers/ResourceController";

export function ResourcePage() {
    const { resources, isLoading, isError } = GetAllResources();

    if (isLoading) return <div>Loading...</div>
    if (isError) return <div>Error</div>

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
                            {
                                resources.map((resource) => (
                                    <tr key={resource.id}>
                                        <td>{resource.name}</td>
                                        <td><button>Додати</button></td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </table>
                </div>

            </div>
        </div>

    );
}