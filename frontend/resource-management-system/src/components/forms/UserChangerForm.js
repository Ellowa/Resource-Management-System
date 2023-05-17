import { ChangeUserByID } from "@/fetchers/UserController";
import { useState } from "react";
import Modal from "../modal/Modal";

function UserChangerForm(user) {

    user = user.user

    const handleSubmit = async (e) => {
        e.preventDefault();

        const newData = {
            id: user.id,
            firstName: e.target.firstName.value,
            secondName: e.target.secondName.value,
            lastName: e.target.lastName.value,
            login: e.target.login.value,
            roleId: e.target.roleId.value,
            roleName: "",
            password: e.target.password.value
        }

        const errormessage = await ChangeUserByID(user.id, newData);
        if (errormessage) {
            alert(errormessage);
        } else {
            e.target.reset();
            alert("Користувач успішно змінений");
        }
    }

    return (
        <form className="formUserAdder formUserChange" onSubmit={handleSubmit} >
            <label htmlFor="firstName">First Name</label>
            <input type="text" id="firstName" name="firstName" defaultValue={user.firstName} required />
            <label htmlFor="secondName">Second Name</label>
            <input type="text" id="secondName" name="secondName" defaultValue={user.secondName} required />
            <label htmlFor="lastName">Last Name</label>
            <input type="text" id="lastName" name="lastName" defaultValue={user.lastName} required />
            <label htmlFor="login">Login</label>
            <input type="text" id="login" name="login" defaultValue={user.login} required />
            <label htmlFor="roleId">Role</label>
            <select id="roleId" name="roleId" defaultValue={user.roleId} required >
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

export default function UserChanger(data) {
    const [showForm, setShowForm] = useState(false);

    const handleButtonClick = () => {
        setShowForm(!showForm);
    }

    return (
        <div>
            <button onClick={handleButtonClick}>{showForm ? "Закрити" : "Змінити"}</button>
            {showForm && <Modal funcName = {UserChangerForm}
                                closeModal = {setShowForm}
                                user={data.data}/>}
        </div>
    )
}