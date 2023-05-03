import { GetAllRequests } from "@/fetchers/RequestController";

function TableData() {
    const { requests, isLoading, isError } = GetAllRequests();

    if (isLoading) return <tr><td>Loading...</td></tr>
    if (isError) return <tr><td>Error</td></tr>
    return (
        <>
            {
                requests.map((request) => (
                    <tr key={request.id}>
                        <td>{request.name}</td>
                        <td><button>Додати</button></td>
                    </tr>
                ))
            }
        </>
    );
}

export function RequestPage() {

    return (
        <div className="main-page">
            <h2 className="main-text">Мої запити</h2>

            <div className="table">
                <div className="table__header">
                    <table cellPadding="0" cellSpacing="0" border="0">
                        <thead>
                            <tr>
                                <th>Назва запиту</th>
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
