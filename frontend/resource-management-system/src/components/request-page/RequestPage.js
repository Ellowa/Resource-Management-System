import { GetAllRequests } from "@/fetchers/RequestController";

export function RequestPage() {
    const { requests, isLoading, isError } = GetAllRequests();
    console.log(isError);

    if (isLoading) return <div>Loading...</div>
    if (isError) return <div>Error</div>

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
                            {
                                requests.map((request) => (
                                    <tr key={request.id}>
                                        <td>{request.name}</td>
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
