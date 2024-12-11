const carList = document.getElementById('cars')
const modal = document.getElementById('car-modal')
const modalClose = document.querySelector('.close-modal')
const modalImage = document.getElementById('modal-car-image')
const modalTitle = document.getElementById('modal-car-title')
const modalTransmission = document.getElementById('modal-car-transmission')
const modalSeats = document.getElementById('modal-car-seats')
const modalDoors = document.getElementById('modal-car-doors')
const modalFuel = document.getElementById('modal-car-fuel')
const modalYear = document.getElementById('modal-car-year')
const modalPrice = document.getElementById('modal-price-day')
const startDateInput = document.getElementById('start-date')
const endDateInput = document.getElementById('end-date')
const totalCostLabel = document.getElementById('total-cost')
const filter = document.getElementById('filter')
const reserveButton = document.getElementById('reserve-button') // Przycisk rezerwacji
const dateErrorLabel = document.getElementById('date-error') // Element do wyświetlania błędu walidacji daty
const userInfoModal = document.getElementById('user-info-modal')
const userInfoModalClose = document.querySelector('#user-info-modal .close-modal')
const userInfoForm = document.getElementById('user-info-form')
const totalPriceInput = document.getElementById('total-price')

const carsPerPage = 8 // Liczba samochodów na stronie
let currentPage = 1 // Aktualna strona
let rentalCost = 0 // Zmienna globalna do przechowywania kosztu wynajmu
let globalStartDate = null //zmienne do przechowywania dat
let globalEndDate = null

// Funkcja do wyciągania tylko roku z daty (jeśli data jest w pełnym formacie)
function getYearFromDate(dateString) {
	const date = new Date(dateString)
	return date.getFullYear() || 'Nieznany rok' // Zwróci rok lub "Nieznany rok" jeśli data jest niepoprawna
}

// Funkcja renderująca karty samochodów
function renderCars(cars, category = 'all', page = 1) {
	const filteredCars = cars.filter(car => category === 'all' || car.bodyType.toLowerCase() === category)
	const startIndex = (page - 1) * carsPerPage
	const paginatedCars = filteredCars.slice(startIndex, startIndex + carsPerPage)

	carList.innerHTML = ''
	if (paginatedCars.length === 0) {
		carList.innerHTML = '<p>Brak samochodów w wybranej kategorii.</p>'
		return
	}

	paginatedCars.forEach(car => {
		const carCard = document.createElement('div')
		carCard.classList.add('car-card')
		carCard.dataset.carId = car.id
		carCard.innerHTML = `
            <h3>${car.title}</h3>
            <p>Skrzynia: ${car.transmission}</p>
            <p>Miejsca: ${car.seatCount}</p>
            <p>Drzwi: ${car.doorCount}</p>
            <p>Cena za dobę: ${car.rentCostPerDay} zł</p>
            <img class="carphoto" src="${API_URL}/images/${car.imageIds ? 3 : car.imageIds[0]}/blob" alt="${car.title}">
        `
		carList.appendChild(carCard)
	})

	updatePagination(filteredCars.length, page)
}

// Funkcja otwierająca modal z pełnymi danymi samochodu
function openModal(car) {
	window.selectedCar = car // Zapisywanie wybranego samochodu do globalnej zmiennej
	modalImage.src = `${API_URL}/images/${car.imageIds ? 3 : car.imageIds[0]}/blob` || 'default.jpg'
	modalTitle.textContent = car.title
	modalTransmission.textContent = `Skrzynia: ${car.transmission}`
	modalSeats.textContent = `Miejsca: ${car.seatCount}`
	modalDoors.textContent = `Drzwi: ${car.doorCount}`
	modalFuel.textContent = `Paliwo: ${car.fuelType}`
	modalPrice.textContent = `Cena za dobę:${car.rentCostPerDay} zł`
	modalYear.textContent = `Rok produkcji: ${getYearFromDate(car.productionYear)}`

	// Pokazujemy modal
	modal.classList.remove('hidden')
}

// Funkcja zamykająca modal
// Funkcja zamykająca modal samochodu
function closeModal() {
	modal.classList.add('hidden')

	// Resetowanie danych w modalu
	modalImage.src = '' // Resetowanie obrazka
	modalTitle.textContent = ''
	modalTransmission.textContent = ''
	modalSeats.textContent = ''
	modalDoors.textContent = ''
	modalFuel.textContent = ''
	modalYear.textContent = ''
	modalPrice.textContent = ''

	// Resetowanie dat w formularzu rezerwacji
	startDateInput.value = ''
	endDateInput.value = ''
	totalCostLabel.textContent = 'Koszt wynajmu: 0 zł'
	totalPriceInput.value = '0 zł'

	// Resetowanie globalnych dat
	globalStartDate = null
	globalEndDate = null

	// Zablokowanie przycisku rezerwacji
	reserveButton.disabled = true

	// Usunięcie komunikatu o błędzie daty
	dateErrorLabel.textContent = ''
}

// Funkcja zamykająca modal z formularzem danych rezerwującego
function closeUserInfoModal() {
	userInfoModal.classList.add('hidden')

	// Resetowanie formularza użytkownika
	userInfoForm.reset() // Resetuje wszystkie pola formularza
	totalPriceInput.value = '' // Resetujemy pole z ceną
}

// Event listener do otwierania modalu po kliknięciu w kartę pojazdu
carList.addEventListener('click', e => {
	const carId = e.target.closest('.car-card')?.dataset.carId
	if (!carId) return
	const car = window.carsData.find(car => car.id === parseInt(carId))
	if (car) openModal(car)
})

// Event listener do zamknięcia modalu klikając w przycisk "krzyżyk"
modalClose.addEventListener('click', closeModal)

// Event listener do zamknięcia modalu klikając poza jego obszarem (na tło)
modal.addEventListener('click', e => {
	if (e.target === modal) {
		closeModal()
	}
})

// Nasłuchiwanie kliknięcia przycisku „Zarezerwuj” w modalu samochodu
reserveButton.addEventListener('click', function (e) {
	e.preventDefault()
	openUserInfoModal()
})

// Nasłuchiwanie kliknięcia przycisku zamknięcia modalu użytkownika
userInfoModalClose.addEventListener('click', closeUserInfoModal)

// Filtracja samochodów na podstawie kategorii
filter.addEventListener('change', () => renderCars(window.carsData, filter.value.toLowerCase(), 1))

// Funkcja do pobierania danych samochodów i filtrów
async function fetchCarsAndFilters() {
	try {
		const response = await fetch(`${API_URL}/Cars`)
		if (!response.ok) throw new Error(`HTTP error! Status: ${response.status}`)
		const cars = await response.json()
		window.carsData = cars

		const bodyTypes = [...new Set(cars.map(car => car.bodyType.toLowerCase()))]
		bodyTypes.forEach(bodyType => {
			const option = document.createElement('option')
			option.value = bodyType
			option.textContent = bodyType
			filter.appendChild(option)
		})

		renderCars(cars, 'all', 1) // Zmieniamy stronę na pierwszą
	} catch (error) {
		console.error('Błąd przy pobieraniu samochodów:', error)
		carList.innerHTML = '<p>Nie udało się załadować samochodów.</p>'
	}
}

// Ustawienie minimalnej daty na dzisiejszą
function setMinDate(input) {
	const today = new Date().toISOString().split('T')[0]
	input.min = today
}

// Funkcja aktualizująca minimalną datę zakończenia na podstawie daty rozpoczęcia
function updateMinEndDate() {
	if (globalStartDate) {
		endDateInput.min = globalStartDate
	}
}

// Funkcja do obliczania różnicy w dniach między datami
function calculateDays(startDate, endDate) {
	const start = new Date(startDate)
	const end = new Date(endDate)
	if (isNaN(start) || isNaN(end) || start >= end) return 0
	const diffTime = Math.abs(end - start)
	return Math.ceil(diffTime / (1000 * 60 * 60 * 24))
}

function updateTotalCost() {
	// Teraz używamy globalnych zmiennych
	const startDate = globalStartDate
	const endDate = globalEndDate

	// Sprawdzenie, czy obie daty są wprowadzone
	if (!startDate || !endDate) {
		dateErrorLabel.textContent = 'Proszę o zaznaczenie daty rozpoczęcia i zakończenia!'
		totalCostLabel.textContent = 'Koszt wynajmu: 0 zł'
		totalPriceInput.value = '0 zł'
		reserveButton.disabled = true
		return
	}

	const start = new Date(startDate)
	const end = new Date(endDate)

	// Sprawdzamy poprawność dat
	if (isNaN(start.getTime()) || isNaN(end.getTime()) || start >= end) {
		dateErrorLabel.textContent =
			'Daty są nieprawidłowe! Upewnij się, że data zakończenia jest późniejsza niż rozpoczęcia.'
		totalCostLabel.textContent = 'Koszt wynajmu: 0 zł'
		totalPriceInput.value = '0 zł'
		reserveButton.disabled = true
		return
	}

	// Obliczamy liczbę dni i koszt wynajmu
	const days = calculateDays(startDate, endDate)
	const selectedCar = window.selectedCar

	if (selectedCar && days > 0) {
		const totalCost = days * selectedCar.rentCostPerDay
		rentalCost = totalCost // Zapisujemy koszt do zmiennej globalnej
		totalCostLabel.textContent = `Koszt wynajmu: ${totalCost} zł`
		totalPriceInput.value = `${totalCost} zł`
		dateErrorLabel.textContent = ''
		reserveButton.disabled = false
	} else {
		totalCostLabel.textContent = 'Koszt wynajmu: 0 zł'
		totalPriceInput.value = '0 zł'
		reserveButton.disabled = true
	}
}

// Nasłuchiwanie zmian daty i aktualizacja globalnych zmiennych
;[startDateInput, endDateInput].forEach(input =>
	input.addEventListener('change', () => {
		if (input === startDateInput) {
			globalStartDate = startDateInput.value.trim() // Zapisujemy datę rozpoczęcia
		} else if (input === endDateInput) {
			globalEndDate = endDateInput.value.trim() // Zapisujemy datę zakończenia
		}
		updateTotalCost() // Aktualizujemy koszt
	})
)

// Funkcja otwierająca modal z formularzem danych rezerwującego
function openUserInfoModal() {
	closeModal() // Zamknięcie modalu samochodu, jeśli jest otwarty
	userInfoModal.classList.remove('hidden')

	// Wyczyść pola daty przed otwarciem formularza
	startDateInput.value = ''
	endDateInput.value = ''

	// Używamy globalnej zmiennej rentalCost do ustawienia wartości kosztu
	if (rentalCost > 0) {
		totalPriceInput.value = `${rentalCost} zł` // Ustawiamy koszt wynajmu w polu formularza
	} else {
		totalPriceInput.value = '0 zł'
	}

	// Zablokowanie przycisku rezerwacji na początku
	reserveButton.disabled = true
}

// Paginacja
function updatePagination(totalCars, currentPage) {
	const totalPages = Math.ceil(totalCars / carsPerPage)
	const pagination = document.getElementById('pagination')
	pagination.innerHTML = ''

	const createButton = (page, text, isDisabled = false) => {
		const button = document.createElement('button')
		button.textContent = text
		button.disabled = isDisabled
		button.classList.toggle('disabled', isDisabled)
		button.addEventListener('click', () => {
			renderCars(window.carsData, filter.value, page)
		})
		return button
	}

	pagination.appendChild(createButton(currentPage - 1, '« Poprzednia', currentPage === 1))
	for (let i = 1; i <= totalPages; i++) {
		pagination.appendChild(createButton(i, i, i === currentPage))
	}
	pagination.appendChild(createButton(currentPage + 1, 'Następna »', currentPage === totalPages))
}
// Funkcja do sortowania samochodów
function sortCars(cars, sortBy) {
	switch (sortBy) {
		case 'name-asc':
			return cars.sort((a, b) => a.title.localeCompare(b.title))
		case 'name-desc':
			return cars.sort((a, b) => b.title.localeCompare(a.title))
		case 'price-asc':
			return cars.sort((a, b) => a.rentCostPerDay - b.rentCostPerDay)
		case 'price-desc':
			return cars.sort((a, b) => b.rentCostPerDay - a.rentCostPerDay)
		default:
			return cars
	}
}

// Zaktualizowana funkcja renderująca samochody
function renderCars(cars, category = 'all', page = 1, sortBy = 'name-asc') {
	const filteredCars = cars.filter(car => category === 'all' || car.bodyType.toLowerCase() === category)
	const sortedCars = sortCars(filteredCars, sortBy)
	const startIndex = (page - 1) * carsPerPage
	const paginatedCars = sortedCars.slice(startIndex, startIndex + carsPerPage)

	carList.innerHTML = ''
	if (paginatedCars.length === 0) {
		carList.innerHTML = '<p>Brak samochodów w wybranej kategorii.</p>'
		return
	}

	paginatedCars.forEach(car => {
		const carCard = document.createElement('div')
		carCard.classList.add('car-card')
		carCard.dataset.carId = car.id
		carCard.innerHTML = `
            <h3>${car.title}</h3>
            <p>Skrzynia: ${car.transmission}</p>
            <p>Miejsca: ${car.seatCount}</p>
            <p>Drzwi: ${car.doorCount}</p>
            <p>Cena za dobę: ${car.rentCostPerDay} zł</p>
            <img class="carphoto" src="${API_URL}/images/${car.imageIds ? 3 : car.imageIds[0]}/blob" alt="${car.title}">
        `
		carList.appendChild(carCard)
	})

	updatePagination(filteredCars.length, page)
}

// Dodanie nasłuchiwania na zmianę sortowania
const sortFilter = document.getElementById('sort-filter')
sortFilter.addEventListener('change', () => {
	const sortBy = sortFilter.value
	renderCars(window.carsData, filter.value.toLowerCase(), currentPage, sortBy)
})
// Funkcja wysyłająca dane rezerwacji na serwer (API)
async function sendReservationEmail(userData, carData) {
	const emailData = {
		to: userData.email,
		subject: 'Potwierdzenie rezerwacji samochodu',
		message: `
            Dziękujemy za rezerwację samochodu ${
							carData.title
						} w naszym serwisie. Poniżej znajdziesz szczegóły rezerwacji:
            <ul>
                <li>Imię i nazwisko: ${userData.name}</li>
                <li>E-mail: ${userData.email}</li>
                <li>Telefon: ${userData.phone}</li>
                <li>Samochód: ${carData.title}</li>
                <li>Data rozpoczęcia: ${globalStartDate || 'Brak daty'}</li>
                <li>Data zakończenia: ${globalEndDate || 'Brak daty'}</li>
                <li>Cena całkowita: ${userData.totalPrice} zł</li>
            </ul>
        `,
	}

	try {
		const response = await fetch(`${API_URL}/Emails/send`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: JSON.stringify(emailData),
		})

		if (!response.ok) {
			throw new Error('Błąd podczas wysyłania e-maila!')
		}

		const responseData = await response.json()
		if (responseData.success) {
			alert('Rezerwacja została pomyślnie wysłana na e-mail.')
			closeUserInfoModal() // Zamykamy modal po sukcesie
		} else {
			alert('Wystąpił problem podczas wysyłania e-maila.')
		}
	} catch (error) {
		console.error('Błąd API:', error)
		alert('Wystąpił problem. Spróbuj ponownie później.')
	}
}

async function sendCarReservation(carId, userData) {
	try {
		const response = await fetch(`${API_URL}/api/cars/${carId}/rent`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: JSON.stringify(userData),
		})

		if (!response.ok) {
			throw new Error(`Błąd rezerwacji! Status: ${response.status}`)
		}

		const responseData = await response.json()
		if (responseData.success) {
			alert('Rezerwacja została pomyślnie wysłana.')
			closeUserInfoModal() // Zamykamy modal po sukcesie
		} else {
			alert('Wystąpił problem podczas rezerwacji.')
		}
	} catch (error) {
		console.error('Błąd API:', error)
		alert('Wystąpił problem. Spróbuj ponownie później.')
	}
}

// Nasłuchiwanie na kliknięcie przycisku „Potwierdź” w formularzu danych użytkownika
userInfoForm.addEventListener('submit', async function (e) {
	e.preventDefault()

	const userName = document.getElementById('name').value
	const userEmail = document.getElementById('email').value
	const userPhone = document.getElementById('phone').value

	const userData = {
		name: userName,
		email: userEmail,
		phone: userPhone,
		startDate: globalStartDate,
		endDate: globalEndDate,
		totalPrice: rentalCost,
	}

	// Wysyłanie danych rezerwacji do endpointu API
	await sendCarReservation(window.selectedCar.id, userData)

	// Wysłanie potwierdzenia e-mail (opcjonalne)
	await sendReservationEmail(userData, window.selectedCar)
})

document.addEventListener('DOMContentLoaded', () => {
	setMinDate(startDateInput)
	setMinDate(endDateInput)
	fetchCarsAndFilters()
	updateTotalCost() // Wywołanie funkcji przy inicjalizacji, by sprawdzić daty i zablokować przycisk
})
