import { DeleteUser, GetAllUsers } from "@/fetchers/UserController";


function TableData() {
    const { users, isLoading, isError } = GetAllUsers();

    if (isLoading) return <tr><td>Loading...</td></tr>
    if (isError) return <tr><td>Error</td></tr>
    return (
        <>
            {
                users.map((user) => (
                    <tr key={user.id}>
                        <td>{user.lastName}</td>
                        <td><button onClick={() => DeleteUser(user.id)}>Видалити</button></td>
                    </tr>
                ))
            }
        </>
    )
}

export function UserPage() {

    return (
        <div className="main-page">
            <h2 className="main-text">Користувачі</h2>
            <div className="table">
                <div className="table__header">
                    <table cellPadding="0" cellSpacing="0" border="0">
                        <thead>
                            <tr>
                                <th>Ім&apos;я Користувача</th>
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