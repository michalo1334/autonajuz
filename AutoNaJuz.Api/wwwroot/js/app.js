// app.js remains largely the same with an addition for the Images navigation

const API_URL = 'https://localhost:3123/api';

const COMMIT_HASH = '';
const COMMIT_NAME = '';
const V_MAJOR = '';
const V_MINOR = '';
const V_PATCH = '';

document.addEventListener('DOMContentLoaded', () => {
    setupNavigation();
    loadCars(); // Default view

    let versionElement = document.getElementById('version');
    versionElement.innerHTML = `v${V_MAJOR}.${V_MINOR}.${V_PATCH}`;

    let commitElement = document.getElementById('commit');
    commitElement.innerHTML = `Commit: ${COMMIT_HASH} (${COMMIT_NAME})`;
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