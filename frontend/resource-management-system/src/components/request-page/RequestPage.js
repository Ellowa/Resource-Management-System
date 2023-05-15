import { ConfirmRequest, DeleteRequest, DenyRequest, GetAllRequests } from "@/fetchers/RequestController";
import RequestAdder from "../forms/RequestAdderForm";

function TableData() {
    const { requests, isLoading, isError } = GetAllRequests();

    if (isLoading) return <tr><td>Loading...</td></tr>
    if (isError) return <tr><td>Error</td></tr>
    return (
        <>
            {
                requests.map((request) => (
                    <tr key={request.id}>
                        <td>{request.purpose}</td>
                        <td><button onClick={() => ConfirmRequest(request.id)}>Підтвердити</button></td>
                        <td><button onClick={() => DenyRequest(request.id)}>Відхилити</button></td>
                        <td><button onClick={() => DeleteRequest(request.id)}>Видалити</button></td>
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
            <RequestAdder />
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
