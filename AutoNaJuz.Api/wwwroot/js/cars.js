// /js/cars.js

function loadCars() {
    clearContent();
    fetch(`${API_BASE_URL}/Cars`)
        .then(response => response.json())
        .then(cars => {
            const contentDiv = document.getElementById('content');
            const template = document.getElementById('cars-template');
            contentDiv.appendChild(template.content.cloneNode(true));

            const carsTableBody = document.querySelector('#cars-table tbody');
            cars.forEach(car => {
                const row = document.createElement('tr');

                row.innerHTML = `
                    <td>${car.title}</td>
                    <td>${car.transmission}</td>
                    <td>${car.fuelType}</td>
                    <td>${new Date(car.productionYear).getFullYear()}</td>
                    <td>${car.seatCount}</td>
                    <td>${car.doorCount}</td>
                    <td>${car.bodyType}</td>
                    <td>
                        <button class="btn" onclick="editCar(${car.id})">Edit</button>
                        <button class="btn" onclick="deleteCar(${car.id})">Delete</button>
                        <button class="btn" onclick="toggleCarRentals(${car.id}, this)">Show Rentals</button>
                    </td>
                `;
                carsTableBody.appendChild(row);
            });

            document.getElementById('add-car-button').addEventListener('click', () => {
                openCarForm();
            });
        })
        .catch(error => console.error('Error fetching cars:', error));
}

// Attach functions to the global window object
window.loadCars = loadCars;

window.editCar = function (carId) {
    fetch(`${API_BASE_URL}/Cars/${carId}`)
        .then(response => response.json())
        .then(car => {
            openCarForm(car);
        })
        .catch(error => console.error('Error fetching car data:', error));
};

function openCarForm(car = {}) {
    clearContent();
    const contentDiv = document.getElementById('content');
    const template = document.getElementById('form-template');
    contentDiv.appendChild(template.content.cloneNode(true));

    document.getElementById('form-title').textContent = car.id ? 'Edit Car' : 'Add New Car';

    const form = document.getElementById('data-form');
    form.innerHTML = `
        <label>Title:</label>
        <input type="text" name="title" value="${car.title || ''}" required>

        <label>Transmission:</label>
        <select name="transmission">
            <option value="Manual" ${car.transmission === 'Manual' ? 'selected' : ''}>Manual</option>
            <option value="Automatic" ${car.transmission === 'Automatic' ? 'selected' : ''}>Automatic</option>
        </select>

        <label>Fuel Type:</label>
        <select name="fuelType">
            <option value="Gas" ${car.fuelType === 'Gas' ? 'selected' : ''}>Gas</option>
            <option value="Diesel" ${car.fuelType === 'Diesel' ? 'selected' : ''}>Diesel</option>
            <option value="Electric" ${car.fuelType === 'Electric' ? 'selected' : ''}>Electric</option>
            <option value="HybridGasEv" ${car.fuelType === 'HybridGasEv' ? 'selected' : ''}>Hybrid: Gas + Electric</option>
            <option value="HybridDieselEv" ${car.fuelType === 'HybridDieselEv' ? 'selected' : ''}>Hybrid: Diesel + Electric</option>
            <option value="HybridLpgEv" ${car.fuelType === 'HybridLpgEv' ? 'selected' : ''}>Hybrid: Lpg + Electric</option>
        </select>

        <label>Production Year:</label>
        <input type="number" name="productionYear" value="${car.productionYear ? new Date(car.productionYear).getFullYear() : ''}" required>

        <label>Seat Count:</label>
        <input type="number" name="seatCount" value="${car.seatCount || ''}" required>

        <label>Door Count:</label>
        <input type="number" name="doorCount" value="${car.doorCount || ''}" required>

        <label>Body Type:</label>
        <select name="bodyType">
            <option value="Sedan" ${car.bodyType === 'Sedan' ? 'selected' : ''}>Sedan</option>
            <option value="Hatchback" ${car.bodyType === 'Hatchback' ? 'selected' : ''}>Hatchback</option>
            <option value="Suv" ${car.bodyType === 'Suv' ? 'selected' : ''}>SUV</option>
            <option value="Pickup" ${car.bodyType === 'Pickup' ? 'selected' : ''}>Pickup</option>
            <option value="Van" ${car.bodyType === 'Van' ? 'selected' : ''}>Van</option>
            <option value="Combi" ${car.bodyType === 'Combi' ? 'selected' : ''}>Combi</option>
        </select>
        <button type="submit" class="btn">${car.id ? 'Update Car' : 'Add Car'}</button>
        <button type="button" class="btn" onclick="loadCars()">Cancel</button>
    `;

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const formData = new FormData(form);
        const carData = {
            title: formData.get('title'),
            transmission: formData.get('transmission'),
            fuelType: formData.get('fuelType'),
            productionYear: new Date(`${formData.get('productionYear')}-01-01`).toISOString(),
            seatCount: parseInt(formData.get('seatCount')),
            doorCount: parseInt(formData.get('doorCount')),
            bodyType: formData.get('bodyType'),
        };

        if (car.id) {
            updateCar(car.id, carData);
        } else {
            addCar(carData);
        }
    });
}

function addCar(carData) {
    fetch(`${API_BASE_URL}/Cars`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(carData),
    })
        .then(response => {
            if (response.status === 201) {
                loadCars();
            } else {
                console.error('Error adding car:', response.statusText);
            }
        })
        .catch(error => console.error('Error adding car:', error));
}

function updateCar(carId, carData) {
    fetch(`${API_BASE_URL}/Cars/${carId}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: carId, ...carData }),
    })
        .then(response => {
            if (response.ok) {
                loadCars();
            } else {
                console.error('Error updating car:', response.statusText);
            }
        })
        .catch(error => console.error('Error updating car:', error));
}

window.deleteCar = function (carId) {
    if (confirm('Are you sure you want to delete this car?')) {
        fetch(`${API_BASE_URL}/Cars/${carId}`, { method: 'DELETE' })
            .then(response => {
                if (response.status === 204) {
                    loadCars();
                } else {
                    console.error('Error deleting car:', response.statusText);
                }
            })
            .catch(error => console.error('Error deleting car:', error));
    }
};

window.toggleCarRentals = function (carId, button) {
    const row = button.closest('tr');
    const existingRentalRow = row.nextSibling;
    if (existingRentalRow && existingRentalRow.classList.contains('rental-row')) {
        existingRentalRow.remove();
        button.textContent = 'Show Rentals';
    } else {
        fetch(`${API_BASE_URL}/CarRentals/byCar/${carId}`)
            .then(response => response.json())
            .then(rentals => {
                const rentalRow = document.createElement('tr');
                rentalRow.classList.add('rental-row');
                const rentalCell = document.createElement('td');
                rentalCell.colSpan = 8;
                rentalCell.innerHTML = generateRentalTableHTML(rentals);
                rentalRow.appendChild(rentalCell);
                row.parentNode.insertBefore(rentalRow, row.nextSibling);
                button.textContent = 'Hide Rentals';
            })
            .catch(error => console.error('Error fetching car rentals:', error));
    }
};

function generateRentalTableHTML(rentals) {
    if (rentals.length === 0) {
        return '<p>No rentals for this car.</p>';
    }

    let html = `
        <table class="rental-table">
            <thead>
                <tr>
                    <th>Renter ID</th>
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
                <td>${rental.renterId}</td>
                <td>${new Date(rental.from).toLocaleDateString()}</td>
                <td>${new Date(rental.to).toLocaleDateString()}</td>
                <td>${rental.notes || ''}</td>
            </tr>
        `;
    });
    html += '</tbody></table>';
    return html;
}
