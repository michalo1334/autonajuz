// /js/features.js

function loadFeatures() {
    clearContent();
    fetch(`${API_URL}/Cars/Features`)
        .then(response => response.json())
        .then(features => {
            const contentDiv = document.getElementById('content');
            const template = document.getElementById('features-template');
            contentDiv.appendChild(template.content.cloneNode(true));

            const featuresTableBody = document.querySelector('#features-table tbody');
            features.forEach(feature => {
                const row = document.createElement('tr');

                row.innerHTML = `
                    <td>${feature.id}</td>
                    <td>${feature.title}</td>
                    <td>
                        <button class="btn" onclick="editFeature(${feature.id})">Edit</button>
                        <button class="btn" onclick="deleteFeature(${feature.id})">Delete</button>
                    </td>
                `;
                featuresTableBody.appendChild(row);
            });

            document.getElementById('add-feature-button').addEventListener('click', () => {
                openFeatureForm();
            });
        })
        .catch(error => console.error('Error fetching features:', error));
}

// Attach functions to the global window object
window.loadFeatures = loadFeatures;

window.editFeature = function (featureId) {
    fetch(`${API_URL}/Cars/Features/${featureId}`)
        .then(response => response.json())
        .then(feature => {
            openFeatureForm(feature);
        })
        .catch(error => console.error('Error fetching feature data:', error));
};

function openFeatureForm(feature = {}) {
    clearContent();
    const contentDiv = document.getElementById('content');
    const template = document.getElementById('form-template');
    contentDiv.appendChild(template.content.cloneNode(true));

    document.getElementById('form-title').textContent = feature.id ? 'Edit Feature' : 'Add New Feature';

    const form = document.getElementById('data-form');
    form.innerHTML = `
        <label>Title</label>
        <input type="text" name="title" value="${feature.title || ''}" required>
        
        <button type="submit" class="btn">${feature.id ? 'Update Feature' : 'Add Feature'}</button>
        <button type="button" class="btn" onclick="loadFeatures()">Cancel</button>
    `;

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const formData = new FormData(form);
        const featureData = {
            title: formData.get('title'),
        };

        if (feature.id) {
            updateFeature(feature.id, featureData);
        } else {
            addFeature(featureData);
        }
    });
}

function addFeature(featureData) {
    fetch(`${API_URL}/Cars/Features`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(featureData),
    })
        .then(response => {
            if (response.status === 201) {
                loadFeatures();
            } else {
                console.error('Error adding feature:', response.statusText);
            }
        })
        .catch(error => console.error('Error adding feature:', error));
}

function updateFeature(featureId, featureData) {
    fetch(`${API_URL}/Cars/Features/${featureId}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: featureId, ...featureData }),
    })
        .then(response => {
            if (response.status === 204) {
                loadFeatures();
            } else {
                console.error('Error updating feature:', response.statusText);
            }
        })
        .catch(error => console.error('Error updating feature:', error));
}

window.deleteFeature = function (featureId) {
    if (confirm('Are you sure you want to delete this feature?')) {
        fetch(`${API_URL}/Cars/Features/${featureId}`, { method: 'DELETE' })
            .then(response => {
                if (response.status === 204) {
                    loadFeatures();
                } else {
                    console.error('Error deleting feature:', response.statusText);
                }
            })
            .catch(error => console.error('Error deleting feature:', error));
    }
};