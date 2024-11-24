// /js/rentals.js

function loadRentals() {
    clearContent();
    fetch(`${API_URL}/CarRentals`)
        .then(response => response.json())
        .then(rentals => {
            const contentDiv = document.getElementById('content');
            const template = document.getElementById('rentals-template');
            contentDiv.appendChild(template.content.cloneNode(true));

            const rentalsTableBody = document.querySelector('#rentals-table tbody');
            rentals.forEach(rental => {
                const row = document.createElement('tr');

                row.innerHTML = `
                    <td>${rental.carId}</td>
                    <td>${rental.renterId}</td>
                    <td>${new Date(rental.from).toLocaleDateString()}</td>
                    <td>${new Date(rental.to).toLocaleDateString()}</td>
                    <td>${rental.notes || ''}</td>
                    <td>
                        <button class="btn" onclick="editRental(${rental.id})">Edit</button>
                        <button class="btn" onclick="deleteRental(${rental.id})">Delete</button>
                    </td>
                `;
                rentalsTableBody.appendChild(row);
            });

            document.getElementById('add-rental-button').addEventListener('click', () => {
                openRentalForm();
            });
        })
        .catch(error => console.error('Error fetching rentals:', error));
}

// Attach functions to the global window object
window.loadRentals = loadRentals;

window.editRental = function (rentalId) {
    fetch(`${API_URL}/CarRentals/${rentalId}`)
        .then(response => response.json())
        .then(rental => {
            openRentalForm(rental);
        })
        .catch(error => console.error('Error fetching rental data:', error));
};

function openRentalForm(rental = {}) {
    clearContent();
    const contentDiv = document.getElementById('content');
    const template = document.getElementById('form-template');
    contentDiv.appendChild(template.content.cloneNode(true));

    document.getElementById('form-title').textContent = rental.id ? 'Edit Rental' : 'Add New Rental';

    // Fetch cars and renters for selection
    Promise.all([
        fetch(`${API_URL}/Cars`).then(res => res.json()),
        fetch(`${API_URL}/RenterInfos`).then(res => res.json())
    ]).then(([cars, renters]) => {
        const form = document.getElementById('data-form');
        form.innerHTML = `
          <label>Car:</label>
            <select name="carId" required>
                ${cars.map(car => `<option value="${car.id}" ${rental.carId === car.id ? 'selected' : ''}>${car.title}</option>`).join('')}
            </select>

            <label>Renter:</label>
            <select name="renterId" required>
                ${renters.map(renter => `<option value="${renter.id}" ${rental.renterId === renter.id ? 'selected' : ''}>${renter.firstName} ${renter.lastName}</option>`).join('')}
            </select>

            <label>From:</label>
            <input type="date" name="from" value="${rental.from ? rental.from.split('T')[0] : ''}" required>

            <label>To:</label>
            <input type="date" name="to" value="${rental.to ? rental.to.split('T')[0] : ''}" required>

            <label>Notes:</label>
            <textarea name="notes">${rental.notes || ''}</textarea>

            <button type="submit" class="btn">${rental.id ? 'Update Rental' : 'Add Rental'}</button>
            <button type="button" class="btn" onclick="loadRentals()">Cancel</button>
        `;
        form.addEventListener('submit', (e) => {
            e.preventDefault();
            const formData = new FormData(form);
            const rentalData = {
                carId: parseInt(formData.get('carId')),
                renterId: parseInt(formData.get('renterId')),
                from: formData.get('from'),
                to: formData.get('to'),
                notes: formData.get('notes'),
            };

            if (rental.id) {
                updateRental(rental.id, rentalData);
            } else {
                addRental(rentalData);
            }
        });
    }).catch(error => console.error('Error fetching cars or renters:', error));
}

function addRental(rentalData) {
    fetch(`${API_URL}/CarRentals`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(rentalData),
    })
        .then(response => {
            if (response.status === 201) {
                loadRentals();
            } else {
                console.error('Error adding rental:', response.statusText);
            }
        })
        .catch(error => console.error('Error adding rental:', error));
}

function updateRental(rentalId, rentalData) {
    fetch(`${API_URL}/CarRentals`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: rentalId, ...rentalData }),
    })
        .then(response => {
            if (response.status === 204) {
                loadRentals();
            } else {
                console.error('Error updating rental:', response.statusText);
            }
        })
        .catch(error => console.error('Error updating rental:', error));
}

window.deleteRental = function (rentalId) {
    if (confirm('Are you sure you want to delete this rental?')) {
        fetch(`${API_URL}/CarRentals/${rentalId}`, { method: 'DELETE' })
            .then(response => {
                if (response.status === 204) {
                    loadRentals();
                } else {
                    console.error('Error deleting rental:', response.statusText);
                }
            })
            .catch(error => console.error('Error deleting rental:', error));
    }
};
