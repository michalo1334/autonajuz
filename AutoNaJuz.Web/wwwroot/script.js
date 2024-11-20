const cars = [
	{ id: 1, name: 'Toyota Corolla', category: 'sedan', details: 'Komfortowy sedan z niskim spalaniem.' },
	{ id: 2, name: 'BMW X5', category: 'suv', details: 'Luksusowy SUV idealny na każdą podróż.' },
	{ id: 3, name: 'Porsche 911', category: 'sport', details: 'Sportowy samochód dla wymagających.' },
]

const carList = document.getElementById('cars')
const filter = document.getElementById('filter')
const carSelect = document.getElementById('car-select')
const form = document.getElementById('reservation-form')

// Funkcja do generowania listy samochodów
function renderCars(category = 'all') {
	carList.innerHTML = ''
	const filteredCars = cars.filter(car => category === 'all' || car.category === category)
	filteredCars.forEach(car => {
		const carCard = document.createElement('div')
		carCard.classList.add('car-card')
		carCard.innerHTML = `
            <h3>${car.name}</h3>
            <p>${car.details}</p>
        `
		carList.appendChild(carCard)
	})

	// Aktualizacja opcji w formularzu rezerwacji
	carSelect.innerHTML = ''
	filteredCars.forEach(car => {
		const option = document.createElement('option')
		option.value = car.id
		option.textContent = car.name
		carSelect.appendChild(option)
	})
}

// Obsługa filtrowania
filter.addEventListener('change', () => {
	renderCars(filter.value)
})

// Obsługa formularza rezerwacji
form.addEventListener('submit', e => {
	e.preventDefault()
	const carId = carSelect.value
	const startDate = document.getElementById('start-date').value
	const endDate = document.getElementById('end-date').value

	if (!carId || !startDate || !endDate) {
		alert('Proszę wypełnić wszystkie pola.')
		return
	}

	alert(`Samochód zarezerwowany! Szczegóły: 
        - Samochód: ${cars.find(car => car.id == carId).name}
        - Od: ${startDate} 
        - Do: ${endDate}`)
})

// Inicjalizacja
renderCars()
