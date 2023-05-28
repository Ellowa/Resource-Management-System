import { DeleteUser, GetAllUsers } from "@/fetchers/UserController";
import UserAdder from "../forms/UserAdderForm";
import UserChanger from "../forms/UserChangerForm";

function TableData() {
    const { users, isLoading, isError } = GetAllUsers();

    if (isLoading) return <tr><td>Loading...</td></tr>
    if (isError) return <tr><td>Error</td></tr>
    return (
        <>
            {
                users.map((user) => (
                    <tr key={user.id}>
                        <td>{user.id}</td>
                        <td>{user.login}</td>
                        <td>{user.lastName}</td>
                        <td>{user.roleName}</td>
                        <td><UserChanger data={user} /></td>
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
            <UserAdder />
            <div className="table">
                <div className="table__header">
                    <table cellPadding="0" cellSpacing="0" border="0">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Логін</th>
                                <th>Прізвище Користувача</th>
                                <th>Роль</th>
                                <th />
                                <th />
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