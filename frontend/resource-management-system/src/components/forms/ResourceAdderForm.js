import { AddResource } from "@/fetchers/ResourceController";
import { useState } from "react";
import Modal from "../modal/Modal";
import ResourceTypeReciever from "./recievers/ResourceTypeReciever";

function ResourceAdderForm() {
    const handleSubmit = async (e) => {
        e.preventDefault();

        const data = {
            name: e.target.name.value,
            serialNumber: e.target.serialNumber.value,
            resourceTypeId: e.target.resourceTypeId.value
        }

        const errormessage = await AddResource(data);
        if (errormessage) {
            alert(errormessage);
        } else {
            e.target.reset();
            alert("Ресурс успішно доданий");
        }
    }
    return (
        <form className="formUserAdder" onSubmit={handleSubmit} style={{ position: "absolute, top: 50%, left: 50%" }} >
            <label htmlFor="name">Name</label>
            <input type="text" id="name" name="name" required />

            <label htmlFor="serialNumber">Serial Number</label>
            <input type="text" id="serialNumber" name="serialNumber" required />

            <label htmlFor="resourceTypeId">Resource Type</label>
            {/* <input type="number" id="resourceTypeId" name="resourceTypeId" required /> */}
            <ResourceTypeReciever />

            <button type="submit">Submit</button>
        </form>
    )
}

export default function ResourceAdder() {
    const [showForm, setShowForm] = useState(false);

    const handleButtonClick = () => {
        setShowForm(!showForm);
    }

    return (
        <div className="buttonAdd">
            <button className="buttonAdd__user" onClick={handleButtonClick}>{showForm ? "Закрити" : "Додати"}</button>
            {showForm && <Modal funcName={ResourceAdderForm}
                closeModal={setShowForm} />}
        </div>
    )
}