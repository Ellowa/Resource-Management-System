import { AddUser } from "@/fetchers/UserController";
import { useState } from "react";
import Modal from "../modal/Modal";

export function UserAdderForm() {
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
        <form className="formUserAdder" onSubmit={handleSubmit} >
            <label htmlFor="firstName">First Name</label>
            <input type="text" id="firstName" name="firstName" required />

            <label htmlFor="secondName">Second Name</label>
            <input type="text" id="secondName" name="secondName" required />

            <label htmlFor="lastName">Last Name</label>
            <input type="text" id="lastName" name="lastName" required />

            <label htmlFor="login">Login</label>
            <input type="text" id="login" name="login" required />

            <label htmlFor="roleId">Role ID</label>
            <select id="roleId" name="roleId" required >
                <option value="10">User</option>
                <option value="12">Manager</option>
                <option value="13">Admin</option>
            </select>

            <label htmlFor="password">Password</label>
            <input type="password" id="password" name="password" required />

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
        <div className="buttonAdd">
            <button className="buttonAdd__user" onClick={handleButtonClick}>{showForm ? "Закрити" : "Додати"}</button>
            {showForm && <Modal funcName={UserAdderForm}
                closeModal={setShowForm} />}
        </div>
    )
}