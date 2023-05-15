import { ChangeResourceByID } from "@/fetchers/ResourceController";
import { useState } from "react";
function ResourceChangerForm(resource) {

    resource = resource.resource; //Найти почему так.

    const handleSubmit = async (e) => {
        e.preventDefault();

        const newResource = {
            id: resource.id,
            name: e.target.name.value,
            serialNumber: e.target.serialNumber.value,
            resourceTypeId: e.target.resourceTypeId.value
        }

        const errormessage = await ChangeResourceByID(resource.id, newResource);
        if (errormessage) {
            alert(errormessage);
        } else {
            e.target.reset();
            alert("Ресурс успішно змінено");
        }
    }
    return (
        <form onSubmit={handleSubmit} style={{ position: "absolute, top: 50%, left: 50%" }} >
            <label htmlFor="name">Name</label>
            <input type="text" id="name" name="name" defaultValue={resource.name} required />
            <br />
            <label htmlFor="serialNumber">Serial Number</label>
            <input type="text" id="serialNumber" name="serialNumber" defaultValue={resource.serialNumber} required />
            <br />
            <label htmlFor="resourceTypeId">Resource Type ID</label>
            <input type="number" id="resourceTypeId" name="resourceTypeId" defaultValue={resource.resourceTypeId} required />
            <br />
            <button type="submit">Submit</button>
        </form>
    )
}



export default function ResourceChanger(data) {
    const [showForm, setShowForm] = useState(false);

    const handleButtonClick = () => {
        setShowForm(!showForm);
    }

    return (
        <div>
            <button onClick={handleButtonClick}>{showForm ? "Закрити" : "Змінити"}</button>
            {showForm && <ResourceChangerForm resource={data.data} />}
        </div>
    )
}