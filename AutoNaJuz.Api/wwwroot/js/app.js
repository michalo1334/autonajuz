// app.js remains largely the same with an addition for the Images navigation

const API_URL = 'https://localhost:3123/api';

const COMMIT_HASH = '';
const COMMIT_NAME = '';

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

    document.getElementById('nav-images').addEventListener('click', (e) => {
        e.preventDefault();
        loadImages();
    });
    
    document.getElementById('nav-features').addEventListener('click', (e) => {
        e.preventDefault();
        loadFeatures();
    });
}

function clearContent() {
    document.getElementById('content').innerHTML = '';
}