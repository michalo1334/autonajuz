// /js/images.js

const API_IMAGES_URL = `${API_URL}/Images`;

function loadImages() {
    clearContent();
    fetch(`${API_IMAGES_URL}`)
        .then(response => response.json())
        .then(images => {
            const contentDiv = document.getElementById('content');
            const template = document.getElementById('images-template');
            contentDiv.appendChild(template.content.cloneNode(true));

            const imagesTableBody = document.querySelector('#images-table tbody');
            images.forEach(image => {
                const row = document.createElement('tr');

                row.innerHTML = `
                    <td>${image.id}</td>
                    <td>${image.mimeType}</td>
                    <td>
                        <button class="btn" onclick="viewImage(${image.id})">View</button>
                        <button class="btn" onclick="deleteImage(${image.id})">Delete</button>
                    </td>
                `;
                imagesTableBody.appendChild(row);
            });

            document.getElementById('upload-image-button').addEventListener('click', () => {
                openUploadImageForm();
            });
        })
        .catch(error => console.error('Error fetching images:', error));
}

// Attach loadImages to the global window object
window.loadImages = loadImages;

// Function to open the upload image form
function openUploadImageForm() {
    clearContent();
    const contentDiv = document.getElementById('content');
    const template = document.getElementById('upload-image-form-template');
    contentDiv.appendChild(template.content.cloneNode(true));

    const form = document.getElementById('upload-image-form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const fileInput = document.getElementById('image-file');
        const file = fileInput.files[0];
        if (!file) {
            alert('Please select an image file.');
            return;
        }

        const formData = new FormData();
        formData.append('file', file);

        fetch(`${API_IMAGES_URL}`, {
            method: 'POST',
            body: formData,
        })
            .then(response => {
                if (response.status === 201) {
                    loadImages();
                } else {
                    console.error('Error uploading image:', response.statusText);
                    alert('Failed to upload image.');
                }
            })
            .catch(error => {
                console.error('Error uploading image:', error);
                alert('An error occurred while uploading the image.');
            });
    });
}

// Function to view an image in a modal
window.viewImage = function(imageId) {
    // Fetch the image blob
    fetch(`${API_IMAGES_URL}/${imageId}/blob`)
        .then(async response => {
            if (response.ok) {
                return await response.blob();
            } else {
                throw new Error('Image not found');
            }
        })
        .then(blob => {
            const url = URL.createObjectURL(blob);
            openImageModal(url, `Image ID ${imageId}`);
        })
        .catch(error => {
            console.error('Error fetching image:', error);
            alert('Failed to load image.');
        });
};

// Function to open the image modal
function openImageModal(imageUrl, altText) {
    // Check if modal already exists
    let modal = document.getElementById('image-modal');
    if (!modal) {
        const modalTemplate = document.getElementById('image-modal-template');
        document.body.appendChild(modalTemplate.content.cloneNode(true));
        modal = document.getElementById('image-modal');
    }

    const modalImage = document.getElementById('modal-image');
    modalImage.src = imageUrl;
    modalImage.alt = altText;

    modal.style.display = 'block';

    // Close modal when clicking outside the image
    window.onclick = function(event) {
        if (event.target === modal) {
            closeImageModal();
        }
    };
}

// Function to close the image modal
window.closeImageModal = function() {
    const modal = document.getElementById('image-modal');
    if (modal) {
        modal.style.display = 'none';
        const modalImage = document.getElementById('modal-image');
        modalImage.src = '';
        modalImage.alt = '';
    }
};

// Function to delete an image
window.deleteImage = function(imageId) {
    if (confirm('Are you sure you want to delete this image?')) {
        fetch(`${API_IMAGES_URL}/${imageId}`, { method: 'DELETE' })
            .then(response => {
                if (response.status === 204) {
                    loadImages();
                } else {
                    console.error('Error deleting image:', response.statusText);
                    alert('Failed to delete image.');
                }
            })
            .catch(error => {
                console.error('Error deleting image:', error);
                alert('An error occurred while deleting the image.');
            });
    }
};

// Ensure that loadImages is available globally
window.loadImages = loadImages;

document.addEventListener('keydown', function(event) {
    if (event.key === 'Escape') {
        closeImageModal();
    }
});
