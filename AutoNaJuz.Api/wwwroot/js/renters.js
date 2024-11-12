// /js/renters.js

function loadRenters() {
    clearContent();
    fetch(`${API_BASE_URL}/RenterInfos`)
        .then(response => response.json())
        .then(renters => {
            const contentDiv = document.getElementById('content');
            const template = document.getElementById('renters-template');
            contentDiv.appendChild(template.content.cloneNode(true));

            const rentersTableBody = document.querySelector('#renters-table tbody');
            renters.forEach(renter => {
                const row = document.createElement('tr');

                row.innerHTML = `
                    <td>${renter.firstName} ${renter.lastName}</td>
                    <td>${renter.drivingLicenseIdent}</td>
                    <td>${renter.pesel}</td>
                    <td>${new Date(renter.birthDate).toLocaleDateString()}</td>
                    <td>${renter.street} ${renter.buildingNumber}, ${renter.city}, ${renter.postalCode}</td>
                    <td>
                        <button class="btn" onclick="editRenter(${renter.id})">Edit</button>
                        <button class="btn" onclick="deleteRenter(${renter.id})">Delete</button>
                        <button class="btn" onclick="toggleRenterRentals(${renter.id}, this)">Show Rentals</button>
                    </td>
                `;
                rentersTableBody.appendChild(row);
            });

            document.getElementById('add-renter-button').addEventListener('click', () => {
                openRenterForm();
            });
        })
        .catch(error => console.error('Error fetching renters:', error));
}

// Attach functions to the global window object
window.loadRenters = loadRenters;

window.editRenter = function (renterId) {
    fetch(`${API_BASE_URL}/RenterInfos/${renterId}`)
        .then(response => response.json())
        .then(renter => {
            openRenterForm(renter);
        })
        .catch(error => console.error('Error fetching renter data:', error));
};

function openRenterForm(renter = {}) {
    clearContent();
    const contentDiv = document.getElementById('content');
    const template = document.getElementById('form-template');
    contentDiv.appendChild(template.content.cloneNode(true));

    document.getElementById('form-title').textContent = renter.id ? 'Edit Renter' : 'Add New Renter';

    const form = document.getElementById('data-form');
    form.innerHTML = `
        <label>First Name:</label>
        <input type="text" name="firstName" value="${renter.firstName || ''}" required>

        <label>Last Name:</label>
        <input type="text" name="lastName" value="${renter.lastName || ''}" required>

        <label>Driving License ID:</label>
        <input type="text" name="drivingLicenseIdent" value="${renter.drivingLicenseIdent || ''}" required>

        <label>PESEL:</label>
        <input type="text" name="pesel" value="${renter.pesel || ''}" required>

        <label>Birth Date:</label>
        <input type="date" name="birthDate" value="${renter.birthDate ? renter.birthDate.split('T')[0] : ''}" required>

        <label>Street:</label>
        <input type="text" name="street" value="${renter.street || ''}">

        <label>Building Number:</label>
        <input type="text" name="buildingNumber" value="${renter.buildingNumber || ''}">

        <label>Apartment Number:</label>
        <input type="text" name="apartmentNumber" value="${renter.apartmentNumber || ''}">

        <label>City:</label>
        <input type="text" name="city" value="${renter.city || ''}">

        <label>Postal Code:</label>
        <input type="text" name="postalCode" value="${renter.postalCode || ''}">

        <button type="submit" class="btn">${renter.id ? 'Update Renter' : 'Add Renter'}</button>
        <button type="button" class="btn" onclick="loadRenters()">Cancel</button>
    `;

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const formData = new FormData(form);
        const renterData = {
            firstName: formData.get('firstName'),
            lastName: formData.get('lastName'),
            drivingLicenseIdent: formData.get('drivingLicenseIdent'),
            pesel: formData.get('pesel'),
            birthDate: formData.get('birthDate'),
            street: formData.get('street'),
            buildingNumber: formData.get('buildingNumber'),
            apartmentNumber: formData.get('apartmentNumber'),
            city: formData.get('city'),
            postalCode: formData.get('postalCode'),
        };

        if (renter.id) {
            updateRenter(renter.id, renterData);
        } else {
            addRenter(renterData);
        }
    });
}

function addRenter(renterData) {
    fetch(`${API_BASE_URL}/RenterInfos`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(renterData),
    })
        .then(response => {
            if (response.status === 201) {
                loadRenters();
            } else {
                console.error('Error adding renter:', response.statusText);
            }
        })
        .catch(error => console.error('Error adding renter:', error));
}

function updateRenter(renterId, renterData) {
    fetch(`${API_BASE_URL}/RenterInfos`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: renterId, ...renterData }),
    })
        .then(response => {
            if (response.status === 204) {
                loadRenters();
            } else {
                console.error('Error updating renter:', response.statusText);
            }
        })
        .catch(error => console.error('Error updating renter:', error));
}

window.deleteRenter = function (renterId) {
    if (confirm('Are you sure you want to delete this renter?')) {
        fetch(`${API_BASE_URL}/RenterInfos/${renterId}`, { method: 'DELETE' })
            .then(response => {
                if (response.status === 204) {
                    loadRenters();
                } else {
                    console.error('Error deleting renter:', response.statusText);
                }
            })
            .catch(error => console.error('Error deleting renter:', error));
    }
};

window.toggleRenterRentals = function (renterId, button) {
    const row = button.closest('tr');
    const existingRentalRow = row.nextSibling;
    if (existingRentalRow && existingRentalRow.classList.contains('rental-row')) {
        existingRentalRow.remove();
        button.textContent = 'Show Rentals';
    } else {
        fetch(`${API_BASE_URL}/CarRentals/byRenter/${renterId}`)
            .then(response => response.json())
            .then(rentals => {
                const rentalRow = document.createElement('tr');
                rentalRow.classList.add('rental-row');
                const rentalCell = document.createElement('td');
                rentalCell.colSpan = 6;
                rentalCell.innerHTML = generateRentalTableHTML(rentals);
                rentalRow.appendChild(rentalCell);
                row.parentNode.insertBefore(rentalRow, row.nextSibling);
                button.textContent = 'Hide Rentals';
            })
            .catch(error => console.error('Error fetching renter rentals:', error));
    }
};

function generateRentalTableHTML(rentals) {
    if (rentals.length === 0) {
        return '<p>No rentals for this renter.</p>';
    }

    let html = `
        <table class="rental-table">
            <thead>
                <tr>
                    <th>Car ID</th>
                    <th>From</th>
                    <th>To</th>
                    <th>Notes</th>
                </tr>
            </thead>
            <tbody>
    `;
    rentals.forEach(rental => {
        html += `
            <tr>
                <td>${rental.carId}</td>
                <td>${new Date(rental.from).toLocaleDateString()}</td>
                <td>${new Date(rental.to).toLocaleDateString()}</td>
                <td>${rental.notes || ''}</td>
            </tr>
        `;
    });
    html += '</tbody></table>';
    return html;
}
