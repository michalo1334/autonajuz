const API_BASE_URL = 'https://localhost:7092/api';

document.addEventListener('DOMContentLoaded', () => {
    setupNavigation();
    loadCars(); // Default view
});

function setupNavigation() {
    document.getElementById('nav-cars').addEventListener('click', (e) => {
        e.preventDefault();
        loadCars();
    });

    document.getElementById('nav-renters').addEventListener('click', (e) => {
        e.preventDefault();
        loadRenters();
    });

    document.getElementById('nav-rentals').addEventListener('click', (e) => {
        e.preventDefault();
        loadRentals();
    });
}

function clearContent() {
    document.getElementById('content').innerHTML = '';
}
