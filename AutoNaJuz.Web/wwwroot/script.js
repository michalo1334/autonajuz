// Zmienna na modal i elementy, które będą zaktualizowane
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

// Funkcja do wyciągania tylko roku z daty (jeśli data jest w pełnym formacie)
function getYearFromDate(dateString) {
	const date = new Date(dateString)
	return date.getFullYear() || 'Nieznany rok' // Zwróci rok lub "Nieznany rok" jeśli data jest niepoprawna
}

// Funkcja renderująca karty samochodów
function renderCars(cars, category = 'all') {
	const filteredCars = cars.filter(car => category === 'all' || car.bodyType.toLowerCase() === category)
	carList.innerHTML = ''
	if (filteredCars.length === 0) {
		carList.innerHTML = '<p>Brak samochodów w wybranej kategorii.</p>'
		return
	}

	filteredCars.forEach(car => {
		const carCard = document.createElement('div')
		carCard.classList.add('car-card')
		carCard.dataset.carId = car.id
		carCard.innerHTML = `
      <h3>${car.title}</h3>
      <p>Skrzynia: ${car.transmission}</p>
      <p>Miejsca: ${car.seatCount}</p>
      <p>Drzwi: ${car.doorCount}</p>
      <img class="carphoto" src="${API_URL}/images/${car.imageIds ? 3 : car.imageIds[0]}/blob" alt="${car.title}">
    `
		carList.appendChild(carCard)
	})
}

// Funkcja otwierająca modal z pełnymi danymi samochodu
function openModal(car) {
	modalImage.src = `${API_URL}/images/${car.imageIds ? 3 : car.imageIds[0]}/blob` || 'default.jpg'
	modalTitle.textContent = car.title
	modalTransmission.textContent = `Skrzynia: ${car.transmission}`
	modalSeats.textContent = `Miejsca: ${car.seatCount}`
	modalDoors.textContent = `Drzwi: ${car.doorCount}`
	modalFuel.textContent = `Paliwo: ${car.fuelType}`

	// Używamy funkcji getYearFromDate, aby wyświetlić tylko rok
	modalYear.textContent = `Rok produkcji: ${getYearFromDate(car.productionYear)}`

	modal.classList.remove('hidden')
}

// Funkcja zamykająca modal
function closeModal() {
	modal.classList.add('hidden')
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
	// Sprawdzamy, czy kliknięcie miało miejsce na tło, a nie wewnętrzny kontener (modal-content)
	if (e.target === modal) {
		closeModal()
	}
})

// Filtracja samochodów na podstawie kategorii
filter.addEventListener('change', () => renderCars(window.carsData, filter.value.toLowerCase()))

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

		renderCars(cars)
	} catch (error) {
		console.error('Błąd przy pobieraniu samochodów:', error)
		carList.innerHTML = '<p>Nie udało się załadować samochodów.</p>'
	}
}

document.addEventListener('DOMContentLoaded', fetchCarsAndFilters)
