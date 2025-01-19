const apiBaseUrl = "api/contact"; 
const tableBody = document.getElementById("contactsTableBody");
const uploadCsvButton = document.getElementById("uploadCsvButton");
const csvFileInput = document.getElementById("csvFileInput");

async function loadContacts() {
    const response = await fetch(apiBaseUrl);
    const contacts = await response.json();
    renderTable(contacts);
}

function renderTable(contacts) {
    tableBody.innerHTML = contacts
        .map(contact => `
            <tr>
                <td>${contact.id}</td>
                <td contenteditable="true" data-id="${contact.id}" data-field="name">${contact.name}</td>
                <td contenteditable="true" data-id="${contact.id}" data-field="dateOfBirth">${contact.dateOfBirth}</td>
                <td contenteditable="true" data-id="${contact.id}" data-field="isMarried">${contact.isMarried}</td>
                <td contenteditable="true" data-id="${contact.id}" data-field="phone">${contact.phone}</td>
                <td contenteditable="true" data-id="${contact.id}" data-field="salary">${contact.salary}</td>
                <td>
                    <button class="btn btn-danger btn-sm delete" data-id="${contact.id}">Delete</button>
                </td>
            </tr>
        `)
        .join("");
    addEventListeners();
}

function addEventListeners() {
    document.querySelectorAll(".delete").forEach(button =>
        button.addEventListener("click", async (e) => {
            const id = e.target.dataset.id;
            await deleteContact(id);
            await loadContacts();
        })
    );

    document.querySelectorAll("[contenteditable]").forEach(cell =>
        cell.addEventListener("blur", async (e) => {
            const id = e.target.dataset.id;
            const field = e.target.dataset.field;
            const value = e.target.textContent.trim();
            await updateContact(id, { [field]: value });
        })
    );
}


async function deleteContact(id) {
    await fetch(`${apiBaseUrl}/${id}`, {
        method: "DELETE"
    });
}


async function updateContact(id, updatedField) {
    const row = document.querySelector(`tr td[data-id="${id}"]`).parentElement;
    const updatedContact = {
        id: id,
        name: row.querySelector('[data-field="name"]').textContent.trim(),
        dateOfBirth: row.querySelector('[data-field="dateOfBirth"]').textContent.trim(),
        isMarried: row.querySelector('[data-field="isMarried"]').textContent.trim() === "True",
        phone: row.querySelector('[data-field="phone"]').textContent.trim(),
        salary: parseFloat(row.querySelector('[data-field="salary"]').textContent.trim())
    };

    const response = await fetch(`${apiBaseUrl}/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(updatedContact)
    });

    if (!response.ok) {
        alert("Error updating contact");
    }
}


uploadCsvButton.addEventListener("click", () => {
    csvFileInput.click();
});

csvFileInput.addEventListener("change", async (e) => {
    const file = e.target.files[0];
    const formData = new FormData();
    formData.append("file", file);

    const response = await fetch(`${apiBaseUrl}/upload`, {
        method: "POST",
        body: formData
    });

    if (response.ok) {
        alert("File uploaded successfully");
        await loadContacts();
    } else {
        alert("Error uploading file");
    }
});

document.querySelectorAll(".sort").forEach(button =>
    button.addEventListener("click", (e) => {
        const column = e.target.dataset.column;
        const order = e.target.dataset.order;
        sortTable(column, order);
        e.target.dataset.order = order === "asc" ? "desc" : "asc";
    })
);

function sortTable(column, order) {
    const rows = Array.from(tableBody.rows);
    const sortedRows = rows.sort((a, b) => {
        const aValue = column === "id"
            ? Number(a.cells[0].textContent.trim())
            : a.querySelector(`[data-field="${column}"]`).textContent.trim();
        const bValue = column === "id"
            ? Number(b.cells[0].textContent.trim())
            : b.querySelector(`[data-field="${column}"]`).textContent.trim();
        return order === "asc" ? (aValue > bValue ? 1 : -1) : (aValue < bValue ? 1 : -1);
    });
    tableBody.innerHTML = "";
    sortedRows.forEach(row => tableBody.appendChild(row));
}

loadContacts();