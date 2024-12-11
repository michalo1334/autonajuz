const carList = document.getElementById('cars');
const modal = document.getElementById('car-modal');
const modalClose = document.querySelector('.close-modal');
const modalImage = document.getElementById('modal-car-image');
const modalTitle = document.getElementById('modal-car-title');
const modalTransmission = document.getElementById('modal-car-transmission');
const modalSeats = document.getElementById('modal-car-seats');
const modalDoors = document.getElementById('modal-car-doors');
const modalFuel = document.getElementById('modal-car-fuel');
const modalYear = document.getElementById('modal-car-year');
const modalPrice = document.getElementById('modal-price-day');
const startDateInput = document.getElementById('start-date');
const endDateInput = document.getElementById('end-date');
const totalCostLabel = document.getElementById('total-cost');
const filter = document.getElementById('filter');
const reserveButton = document.getElementById('reserve-button');
const dateErrorLabel = document.getElementById('date-error');
const userInfoModal = document.getElementById('user-info-modal');
const userInfoModalClose = document.querySelector('#user-info-modal .close-modal');
const userInfoForm = document.getElementById('user-info-form');
const totalPriceInput = document.getElementById('total-price');

const carsPerPage = 8;
let currentPage = 1;
let rentalCost = 0;
let globalStartDate = null;
let globalEndDate = null;

function getYearFromDate(dateString) {
	const date = new Date(dateString);
	return date.getFullYear() || 'Nieznany rok';
}

function renderCars(cars, category = 'all', page = 1, sortBy = 'name-asc') {
	const filteredCars = cars.filter(car => category === 'all' || car.bodyType.toLowerCase() === category);
	const sortedCars = sortCars(filteredCars, sortBy);
	const startIndex = (page - 1) * carsPerPage;
	const paginatedCars = sortedCars.slice(startIndex, startIndex + carsPerPage);

	carList.innerHTML = '';
	if (paginatedCars.length === 0) {
		carList.innerHTML = '<p>Brak samochodów w wybranej kategorii.</p>';
		return;
	}

	paginatedCars.forEach(car => {
		const carCard = document.createElement('div');
		carCard.classList.add('car-card');
		carCard.dataset.carId = car.id;
		carCard.innerHTML = `
            <h3>${car.title}</h3>
            <p>Skrzynia: ${car.transmission}</p>
            <p>Miejsca: ${car.seatCount}</p>
            <p>Drzwi: ${car.doorCount}</p>
            <p>Cena za dobę: ${car.rentCostPerDay} zł</p>
            <img class="carphoto" src="${API_URL}/images/${(car.imageIds && car.imageIds.length > 0) ? car.imageIds[0] : 3}/blob" alt="${car.title}">
        `;
		carList.appendChild(carCard);
	});

	updatePagination(filteredCars.length, page);
}

function openModal(car) {
	window.selectedCar = car;
	modalImage.src = `${API_URL}/images/${(car.imageIds && car.imageIds.length > 0) ? car.imageIds[0] : 3}/blob` || 'default.jpg';
	modalTitle.textContent = car.title;
	modalTransmission.textContent = `Skrzynia: ${car.transmission}`;
	modalSeats.textContent = `Miejsca: ${car.seatCount}`;
	modalDoors.textContent = `Drzwi: ${car.doorCount}`;
	modalFuel.textContent = `Paliwo: ${car.fuelType}`;
	modalPrice.textContent = `Cena za dobę: ${car.rentCostPerDay} zł`;
	modalYear.textContent = `Rok produkcji: ${getYearFromDate(car.productionYear)}`;

	modal.classList.remove('hidden');
}

function closeModal() {
	modal.classList.add('hidden');

	// Do not reset globalStartDate and globalEndDate
	modalImage.src = '';
	modalTitle.textContent = '';
	modalTransmission.textContent = '';
	modalSeats.textContent = '';
	modalDoors.textContent = '';
	modalFuel.textContent = '';
	modalYear.textContent = '';
	modalPrice.textContent = '';

	startDateInput.value = '';
	endDateInput.value = '';
	totalCostLabel.textContent = 'Koszt wynajmu: 0 zł';
	totalPriceInput.value = '0 zł';
	dateErrorLabel.textContent = '';
}

function closeUserInfoModal() {
	userInfoModal.classList.add('hidden');
	userInfoForm.reset();
	totalPriceInput.value = '0 zł';
}

carList.addEventListener('click', e => {
	const carId = e.target.closest('.car-card')?.dataset.carId;
	if (!carId) return;
	const car = window.carsData.find(car => car.id === parseInt(carId));
	if (car) openModal(car);
});

modalClose.addEventListener('click', closeModal);

modal.addEventListener('click', e => {
	if (e.target === modal) {
		closeModal();
	}
});

reserveButton.addEventListener('click', function (e) {
	e.preventDefault();
	openUserInfoModal();
});

userInfoModalClose.addEventListener('click', closeUserInfoModal);

filter.addEventListener('change', () => {
	currentPage = 1;
	renderCars(window.carsData, filter.value.toLowerCase(), currentPage, sortFilter.value);
});

async function fetchCarsAndFilters() {
	try {
		const response = await fetch(`${API_URL}/Cars`);
		if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`);
		const cars = await response.json();
		window.carsData = cars;

		const bodyTypes = [...new Set(cars.map(car => car.bodyType.toLowerCase()))];
		bodyTypes.forEach(bodyType => {
			const option = document.createElement('option');
			option.value = bodyType;
			option.textContent = bodyType.charAt(0).toUpperCase() + bodyType.slice(1);
			filter.appendChild(option);
		});

		renderCars(cars, 'all', 1, sortFilter.value);
	} catch (error) {
		console.error('Błąd przy pobieraniu samochodów:', error);
		carList.innerHTML = '<p>Nie udało się załadować samochodów.</p>';
	}
}

function setMinDate(input) {
	const today = new Date().toISOString().split('T')[0];
	input.min = today;
}

function updateMinEndDate() {
	if (globalStartDate) {
		endDateInput.min = globalStartDate;
	}
}

function calculateDays(startDate, endDate) {
	const start = new Date(startDate);
	const end = new Date(endDate);
	if (isNaN(start) || isNaN(end) || start >= end) return 0;
	const diffTime = Math.abs(end - start);
	return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
}

function updateTotalCost() {
	const startDate = globalStartDate;
	const endDate = globalEndDate;

	if (!startDate || !endDate) {
		dateErrorLabel.textContent = 'Proszę o zaznaczenie daty rozpoczęcia i zakończenia!';
		totalCostLabel.textContent = 'Koszt wynajmu: 0 zł';
		totalPriceInput.value = '0 zł';
		reserveButton.disabled = true;
		return;
	}

	const start = new Date(startDate);
	const end = new Date(endDate);

	if (isNaN(start.getTime()) || isNaN(end.getTime()) || start >= end) {
		dateErrorLabel.textContent =
			'Daty są nieprawidłowe! Upewnij się, że data zakończenia jest późniejsza niż rozpoczęcia.';
		totalCostLabel.textContent = 'Koszt wynajmu: 0 zł';
		totalPriceInput.value = '0 zł';
		reserveButton.disabled = true;
		return;
	}

	const days = calculateDays(startDate, endDate);
	const selectedCar = window.selectedCar;

	if (selectedCar && days > 0) {
		const totalCost = days * selectedCar.rentCostPerDay;
		rentalCost = totalCost;
		totalCostLabel.textContent = `Koszt wynajmu: ${totalCost} zł`;
		totalPriceInput.value = `${totalCost} zł`;
		dateErrorLabel.textContent = '';
		reserveButton.disabled = false;
	} else {
		totalCostLabel.textContent = 'Koszt wynajmu: 0 zł';
		totalPriceInput.value = '0 zł';
		reserveButton.disabled = true;
	}
}

[startDateInput, endDateInput].forEach(input =>
	input.addEventListener('change', () => {
		if (input === startDateInput) {
			globalStartDate = startDateInput.value.trim();
			updateMinEndDate();
		} else if (input === endDateInput) {
			globalEndDate = endDateInput.value.trim();
		}
		updateTotalCost();
	})
);

function openUserInfoModal() {
	closeModal();
	userInfoModal.classList.remove('hidden');

	totalPriceInput.value = rentalCost > 0 ? `${rentalCost} zł` : '0 zł';
}

function updatePagination(totalCars, currentPage) {
	const totalPages = Math.ceil(totalCars / carsPerPage);
	const pagination = document.getElementById('pagination');
	pagination.innerHTML = '';

	const createButton = (page, text, isDisabled = false) => {
		const button = document.createElement('button');
		button.textContent = text;
		button.disabled = isDisabled;
		button.classList.toggle('disabled', isDisabled);
		button.addEventListener('click', () => {
			currentPage = page;
			renderCars(window.carsData, filter.value, page, sortFilter.value);
		});
		return button;
	};

	pagination.appendChild(createButton(currentPage - 1, '« Poprzednia', currentPage === 1));
	for (let i = 1; i <= totalPages; i++) {
		pagination.appendChild(createButton(i, i, i === currentPage));
	}
	pagination.appendChild(createButton(currentPage + 1, 'Następna »', currentPage === totalPages));
}

function sortCars(cars, sortBy) {
	switch (sortBy) {
		case 'name-asc':
			return cars.sort((a, b) => a.title.localeCompare(b.title));
		case 'name-desc':
			return cars.sort((a, b) => b.title.localeCompare(a.title));
		case 'price-asc':
			return cars.sort((a, b) => a.rentCostPerDay - b.rentCostPerDay);
		case 'price-desc':
			return cars.sort((a, b) => b.rentCostPerDay - a.rentCostPerDay);
		default:
			return cars;
	}
}

const sortFilter = document.getElementById('sort-filter');
sortFilter.addEventListener('change', () => {
	currentPage = 1;
	renderCars(window.carsData, filter.value.toLowerCase(), currentPage, sortFilter.value);
});

async function sendCarReservation(carId, userData) {
	try {
		const reservationData = {
			renterId: userData.renterId,
			startDate: userData.startDate,
			endDate: userData.endDate,
			totalPrice: userData.totalPrice
		};

		const response = await fetch(`${API_URL}/cars/${carId}/rent`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: JSON.stringify(reservationData),
		});

		if (!response.ok) {
			const errorText = await response.text();
			throw new Error(`Błąd rezerwacji! Status: ${response.status}, ${errorText}`);
		}

		// If we reached here, it means success
		alert('Samochód został zarezerwowany pomyślnie!');
		closeUserInfoModal();
	} catch (error) {
		console.error('Błąd rezerwacji:', error);
		alert('Wystąpił problem podczas rezerwacji. Spróbuj ponownie później.');
	}
}

async function sendReservationEmail(userData, carData) {
	const emailData = {
		to: userData.email,
		subject: 'Potwierdzenie rezerwacji samochodu',
		startDate: userData.startDate,
		endDate: userData.endDate,
		message: `
            Dziękujemy za rezerwację samochodu ${carData.title}. Oto szczegóły:
            <ul>
                <li>Imię i nazwisko: ${userData.name}</li>
                <li>E-mail: ${userData.email}</li>
                <li>Telefon: ${userData.phone}</li>
                <li>Samochód: ${carData.title}</li>
                <li>Data rozpoczęcia: ${userData.startDate || 'Brak daty'}</li>
                <li>Data zakończenia: ${userData.endDate || 'Brak daty'}</li>
                <li>Cena całkowita: ${userData.totalPrice} zł</li>
            </ul>
        `,
	};

	try {
		const response = await fetch(`${API_URL}/Emails/send`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: JSON.stringify(emailData),
		});

		if (!response.ok) {
			throw new Error('Błąd podczas wysyłania e-maila!');
		}

		// If we reached here, it's a success response
		alert('Rezerwacja została pomyślnie wysłana na e-mail.');
		closeUserInfoModal();
	} catch (error) {
		console.error('Błąd API:', error);
		alert('Wystąpił problem. Spróbuj ponownie później.');
	}
}

async function sendRenterInfo(userData) {
	try {
		const response = await fetch(`${API_URL}/RenterInfos`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: JSON.stringify({
				id: 0,
				fullname: userData.name,
				email: userData.email,
				phone: userData.phone
			}),
		});

		if (!response.ok) {
			const errorText = await response.text();
			throw new Error(`Błąd przy wysyłaniu danych użytkownika: ${errorText}`);
		}

		// If we reached here, response is ok. The API returns just a number (renterId)
		const renterId = await response.json();
		return renterId;
	} catch (error) {
		console.error('Błąd API:', error);
		alert('Wystąpił problem. Spróbuj ponownie później.');
		return null;
	}
}

userInfoForm.addEventListener('submit', async function (e) {
	e.preventDefault();

	const userName = document.getElementById('name').value.trim();
	const userEmail = document.getElementById('email').value.trim();
	const userPhone = document.getElementById('phone').value.trim();

	const userData = {
		name: userName,
		email: userEmail,
		phone: userPhone,
		startDate: globalStartDate,
		endDate: globalEndDate,
		totalPrice: rentalCost
	};

	const renterId = await sendRenterInfo(userData);

	if (!renterId) {
		return;
	}

	// Reservation
	await sendCarReservation(window.selectedCar.id, { ...userData, renterId });

	// Email Confirmation
	await sendReservationEmail({ ...userData, renterId }, window.selectedCar);
});

document.addEventListener('DOMContentLoaded', () => {
	setMinDate(startDateInput);
	setMinDate(endDateInput);
	fetchCarsAndFilters();
	updateTotalCost();
});
