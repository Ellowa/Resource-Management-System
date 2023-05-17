import { AddRequest } from "@/fetchers/RequestController";
import { useState } from "react";
import Modal from "../modal/Modal";

function RequestAdderForm() {
    const handleSubmit = async (e) => {
        e.preventDefault();

        const data = {
            start: e.target.start.value,
            end: e.target.end.value,
            purpose: e.target.purpose.value,
            resourceId: e.target.resourceId.value,
            userId: e.target.userId.value
        }

        const errormessage = await AddRequest(data);
        if (errormessage) {
            alert(errormessage);
        } else {
            e.target.reset();
            alert("Запит успішно доданий");
        }
    }

    return (
        <form className="formUserAdder" onSubmit={handleSubmit} style={{ position: "absolute, top: 50%, left: 50%" }} >
            <label htmlFor="start">Start</label>
            <input type="datetime-local" id="start" name="start" required />

            <label htmlFor="end">End</label>
            <input type="datetime-local" id="end" name="end" required />

            <label htmlFor="purpose">Purpose</label>
            <input type="text" id="purpose" name="purpose" required />

            <label htmlFor="resourceId">Resource ID</label>
            <input type="number" id="resourceId" name="resourceId" required />

            <label htmlFor="userId">User ID</label>
            <input type="number" id="userId" name="userId" required />

            <button type="submit">Submit</button>
        </form>
    )
}

export default function RequestAdder() {
    const [showForm, setShowForm] = useState(false);

    const handleButtonClick = () => {
        setShowForm(!showForm);
    }

    return (
        <div className="buttonAdd">
            <button className="buttonAdd__user" onClick={handleButtonClick}>{showForm ? "Закрити" : "Додати"}</button>
            {showForm && <Modal funcName = {RequestAdderForm}
                                closeModal = {setShowForm}/>}
        </div>
    )
}