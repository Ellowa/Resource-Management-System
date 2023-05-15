import { AddUser } from "@/fetchers/UserController";
import { useState } from "react";

function UserAdderForm() {
    const handleSubmit = async (e) => {
        e.preventDefault();

        const data = {
            firstName: e.target.firstName.value,
            secondName: e.target.secondName.value,
            lastName: e.target.lastName.value,
            login: e.target.login.value,
            roleId: e.target.roleId.value,
            roleName: "",
            password: e.target.password.value
        }

        const errormessage = await AddUser(data);
        if (errormessage) {
            alert(errormessage);
        } else {
            e.target.reset();
            alert("Користувач успішно доданий");
        }
    }

    return (
        <form onSubmit={handleSubmit} style={{ position: "absolute, top: 50%, left: 50%" }} >
            <label htmlFor="firstName">First Name</label>
            <input type="text" id="firstName" name="firstName" required />
            <br />
            <label htmlFor="secondName">Second Name</label>
            <input type="text" id="secondName" name="secondName" required />
            <br />
            <label htmlFor="lastName">Last Name</label>
            <input type="text" id="lastName" name="lastName" required />
            <br />
            <label htmlFor="login">Login</label>
            <input type="text" id="login" name="login" required />
            <br />
            <label htmlFor="roleId">Role ID</label>
            <select id="roleId" name="roleId" required >
                <option value="10">User</option>
                <option value="12">Manager</option>
                <option value="13">Admin</option>
            </select>
            <br />
            <label htmlFor="password">Password</label>
            <input type="password" id="password" name="password" required />
            <br />
            <button type="submit">Submit</button>
        </form>
    )
}

export default function UserAdder() {
    const [showForm, setShowForm] = useState(false);

    const handleButtonClick = () => {
        setShowForm(!showForm);
    }

    return (
        <div>
            <button onClick={handleButtonClick}>{showForm ? "Закрити" : "Додати"}</button>
            {showForm && <UserAdderForm />}
        </div>
    )
}