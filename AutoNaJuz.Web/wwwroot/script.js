const carList = document.getElementById('cars')
const filter = document.getElementById('filter')
const modal = document.getElementById('car-modal')
const modalClose = document.querySelector('.close-modal')
const modalImage = document.getElementById('modal-car-image')
const modalTitle = document.getElementById('modal-car-title')
const modalTransmission = document.getElementById('modal-car-transmission')
const modalSeats = document.getElementById('modal-car-seats')
const modalDoors = document.getElementById('modal-car-doors')
const modalFuel = document.getElementById('modal-car-fuel')
const modalYear = document.getElementById('modal-car-year')

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

function renderCars(cars, category = 'all') {
	const filteredCars = cars.filter(car => category === 'all' || car.category.toLowerCase() === category)
	carList.innerHTML = ''
	if (!filteredCars.length) {
		carList.innerHTML = '<p>Brak samochodów w wybranej kategorii.</p>'
		return
	}

	filteredCars.forEach(car => {
		const carCard = document.createElement('div')
		carCard.classList.add('car-card')
		carCard.dataset.carId = car.id
		carCard.innerHTML = `
            <h3>${car.title}</h3>
            <p>Skrzynia: ${car.Transmission}</p>
            <p>Miejsca: ${car.seatCount}</p>
            <p>Drzwi: ${car.doorCount}</p>
        `
		carList.appendChild(carCard)
	})
}

function openModal(car) {
	modalImage.src = car.imageUrl || 'default.jpg'
	modalTitle.textContent = car.title
	modalTransmission.textContent = `Skrzynia: ${car.Transmission}`
	modalSeats.textContent = `Miejsca: ${car.seatCount}`
	modalDoors.textContent = `Drzwi: ${car.doorCount}`
	modalFuel.textContent = `Paliwo: ${car.fuelType}`
	modalYear.textContent = `Rok produkcji: ${car.productionYear}`
	modal.classList.remove('hidden')
}

function closeModal() {
	modal.classList.add('hidden')
}

carList.addEventListener('click', e => {
	const carId = e.target.closest('.car-card')?.dataset.carId
	if (!carId) return

	const car = window.carsData.find(car => car.id === parseInt(carId))
	if (car) openModal(car)
})

modalClose.addEventListener('click', closeModal)
filter.addEventListener('change', () => renderCars(window.carsData, filter.value.toLowerCase()))

document.addEventListener('DOMContentLoaded', fetchCarsAndFilters)
