// /js/cars.js

// Variables to track selected image IDs
let selectedImageIds = [];

// Variables to keep track of the current slide in the carousel
let currentSlide = 0;
let totalSlides = 0;

// Function to load cars
function loadCars() {
    clearContent();
    fetch(`${API_URL}/Cars`)
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
                        <button class="btn" onclick="showCarImages(${car.id}, '${car.title}')">Show Images</button> <!-- Show Images Button -->
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

// Function to edit a car
window.editCar = function (carId) {
    fetch(`${API_URL}/Cars/${carId}`)
        .then(response => response.json())
        .then(car => {
            openCarForm(car);
        })
        .catch(error => console.error('Error fetching car data:', error));
};

// Function to open the car form (Create or Edit)
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

        <!-- Select Images Button and Selected Images Container -->
        <div class="form-group">
            <label>Associated Images:</label>
            <button type="button" id="select-images-button" class="btn">Select Images</button>
            <div id="selected-images-container">
                <!-- Selected image thumbnails will be displayed here -->
            </div>
        </div>

        <button type="submit" class="btn">${car.id ? 'Update Car' : 'Add Car'}</button>
        <button type="button" class="btn" onclick="loadCars()">Cancel</button>
    `;

    // Initialize selectedImageIds with existing image associations (if editing)
    if (car.imageIds && Array.isArray(car.imageIds)) {
        selectedImageIds = [...car.imageIds];
        displaySelectedImages();
    } else {
        selectedImageIds = [];
    }

    // Event listener for "Select Images" button
    document.getElementById('select-images-button').addEventListener('click', () => {
        openImageSelectionModal();
    });

    // Handle form submission
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
            imageIds: selectedImageIds.length > 0 ? selectedImageIds : [], // Associate images
        };

        if (car.id) {
            updateCar(car.id, carData);
        } else {
            addCar(carData);
        }
    });
}

// Function to add a new car
function addCar(carData) {
    fetch(`${API_URL}/Cars`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(carData),
    })
        .then(response => {
            if (response.status === 201) {
                loadCars();
            } else {
                console.error('Error adding car:', response.statusText);
                alert('Failed to add car.');
            }
        })
        .catch(error => console.error('Error adding car:', error));
}

// Function to update an existing car
function updateCar(carId, carData) {
    fetch(`${API_URL}/Cars/${carId}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: carId, ...carData }),
    })
        .then(response => {
            if (response.ok) {
                loadCars();
            } else {
                console.error('Error updating car:', response.statusText);
                alert('Failed to update car.');
            }
        })
        .catch(error => console.error('Error updating car:', error));
}

// Function to delete a car
window.deleteCar = function (carId) {
    if (confirm('Are you sure you want to delete this car?')) {
        fetch(`${API_URL}/Cars/${carId}`, { method: 'DELETE' })
            .then(response => {
                if (response.status === 204) {
                    loadCars();
                } else {
                    console.error('Error deleting car:', response.statusText);
                    alert('Failed to delete car.');
                }
            })
            .catch(error => console.error('Error deleting car:', error));
    }
};

// Function to toggle rentals (existing functionality)
window.toggleCarRentals = function (carId, button) {
    const row = button.closest('tr');
    const existingRentalRow = row.nextSibling;
    if (existingRentalRow && existingRentalRow.classList.contains('rental-row')) {
        existingRentalRow.remove();
        button.textContent = 'Show Rentals';
    } else {
        fetch(`${API_URL}/Cars/${carId}/Rentals`)
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

// Function to show images of a specific car in a carousel modal (existing functionality)
window.showCarImages = function(carId, carTitle) {
    // Fetch images for the specific car
    fetch(`${API_URL}/Cars/${carId}/images`)
        .then(response => response.json())
        .then(images => {
            if (images.length === 0) {
                alert('No images available for this car.');
                return;
            }
            openCarouselModal(images, carTitle);
        })
        .catch(error => {
            console.error('Error fetching car images:', error);
            alert('Failed to load car images.');
        });
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

// Function to open the carousel modal with images (existing functionality)
function openCarouselModal(images, carTitle) {
    // Check if modal already exists
    let modal = document.getElementById('carousel-modal');
    if (!modal) {
        const modalTemplate = document.getElementById('carousel-modal-template');
        document.body.appendChild(modalTemplate.content.cloneNode(true));
        modal = document.getElementById('carousel-modal');
    }

    const carouselSlide = modal.querySelector('.carousel-slide');
    const carouselIndicators = modal.querySelector('.carousel-indicators');

    // Clear previous images and indicators
    carouselSlide.innerHTML = '';
    carouselIndicators.innerHTML = '';

    // Populate carousel with images
    images.forEach((image, index) => {
        const imgElement = document.createElement('img');
        imgElement.src = `${API_URL}/Images/${image.id}/blob`;
        imgElement.alt = `Image ${index + 1} of ${carTitle}`;
        imgElement.classList.add('carousel-image');
        carouselSlide.appendChild(imgElement);

        // Create indicators
        const indicator = document.createElement('span');
        indicator.classList.add('indicator');
        if (index === 0) indicator.classList.add('active');
        indicator.setAttribute('data-slide', index);
        indicator.addEventListener('click', () => {
            currentSlide = index;
            updateCarousel();
        });
        carouselIndicators.appendChild(indicator);
    });

    // Initialize carousel state
    currentSlide = 0;
    totalSlides = images.length;
    updateCarousel();

    modal.style.display = 'block';

    // Set focus to the close button
    const closeButton = modal.querySelector('.close-button');
    closeButton.focus();

    // Close modal when clicking outside the carousel content
    window.onclick = function(event) {
        if (event.target === modal) {
            closeCarouselModal();
        }
    };
}

// Function to update the carousel display
function updateCarousel() {
    const modal = document.getElementById('carousel-modal');
    const carouselSlide = modal.querySelector('.carousel-slide');
    const carouselIndicators = modal.querySelector('.carousel-indicators');
    const images = carouselSlide.querySelectorAll('.carousel-image');
    const indicators = carouselIndicators.querySelectorAll('.indicator');

    // Ensure currentSlide is within bounds
    if (currentSlide >= totalSlides) currentSlide = 0;
    if (currentSlide < 0) currentSlide = totalSlides - 1;

    // Update carousel position
    carouselSlide.style.transform = `translateX(-${currentSlide * 100}%)`;

    // Update indicators
    indicators.forEach((indicator, index) => {
        indicator.classList.toggle('active', index === currentSlide);
    });
}

// Function to go to the next slide
function nextSlide() {
    currentSlide++;
    updateCarousel();
}

// Function to go to the previous slide
function prevSlide() {
    currentSlide--;
    updateCarousel();
}

// Function to close the carousel modal
window.closeCarouselModal = function() {
    const modal = document.getElementById('carousel-modal');
    if (modal) {
        modal.style.display = 'none';
        const carouselSlide = modal.querySelector('.carousel-slide');
        const carouselIndicators = modal.querySelector('.carousel-indicators');
        // Optionally, reset carousel position
        carouselSlide.style.transform = 'translateX(0)';
        currentSlide = 0;
        totalSlides = 0;
    }
};

// Function to display selected images in the form
function displaySelectedImages() {
    const container = document.getElementById('selected-images-container');
    container.innerHTML = ''; // Clear existing thumbnails

    if (selectedImageIds.length === 0) {
        container.innerHTML = '<p>No images selected.</p>';
        return;
    }

    selectedImageIds.forEach(imageId => {
        const imgElement = document.createElement('img');
        imgElement.src = `${API_URL}/Images/${imageId}/blob`;
        imgElement.alt = `Image ID ${imageId}`;
        imgElement.classList.add('selected-image-thumbnail');
        container.appendChild(imgElement);
    });
}

// Function to open the Image Selection Modal
function openImageSelectionModal() {
    // Check if modal already exists
    let modal = document.getElementById('image-selection-modal');
    if (!modal) {
        const modalTemplate = document.getElementById('image-selection-modal-template');
        document.body.appendChild(modalTemplate.content.cloneNode(true));
        modal = document.getElementById('image-selection-modal');
    }

    const imageListContainer = modal.querySelector('#image-list-container');
    imageListContainer.innerHTML = ''; // Clear previous images

    // Fetch all available images
    fetch(`${API_URL}/Images`)
        .then(response => response.json())
        .then(images => {
            if (images.length === 0) {
                imageListContainer.innerHTML = '<p>No images available. Please upload images first.</p>';
                return;
            }

            images.forEach(image => {
                const imageItem = document.createElement('div');
                imageItem.classList.add('image-item');

                // Create thumbnail image
                const img = document.createElement('img');
                img.src = `${API_URL}/Images/${image.id}/blob`;
                img.alt = `Image ID ${image.id}`;

                // Create checkbox
                const checkbox = document.createElement('input');
                checkbox.type = 'checkbox';
                checkbox.value = image.id;
                checkbox.checked = selectedImageIds.includes(image.id);

                // Event listener to track selection
                checkbox.addEventListener('change', (e) => {
                    if (e.target.checked) {
                        if (!selectedImageIds.includes(image.id)) {
                            selectedImageIds.push(image.id);
                        }
                    } else {
                        selectedImageIds = selectedImageIds.filter(id => id !== image.id);
                    }
                    displaySelectedImages();
                });

                imageItem.appendChild(img);
                imageItem.appendChild(checkbox);
                imageListContainer.appendChild(imageItem);
            });
        })
        .catch(error => {
            console.error('Error fetching images:', error);
            imageListContainer.innerHTML = '<p>Error loading images.</p>';
        });

    modal.style.display = 'block';
}

// Function to close the Image Selection Modal
window.closeImageSelectionModal = function() {
    const modal = document.getElementById('image-selection-modal');
    if (modal) {
        modal.style.display = 'none';
    }
};

// Function to confirm image selection and close the modal
window.confirmImageSelection = function() {
    closeImageSelectionModal();
    displaySelectedImages();
};

// Keyboard accessibility for modals (optional but recommended)
document.addEventListener('keydown', function(event) {
    const imageModal = document.getElementById('image-selection-modal');
    const carouselModal = document.getElementById('carousel-modal');

    if (imageModal && imageModal.style.display === 'block') {
        if (event.key === 'Escape') {
            closeImageSelectionModal();
        }
    }

    if (carouselModal && carouselModal.style.display === 'block') {
        if (event.key === 'Escape') {
            closeCarouselModal();
        }
    }
});
