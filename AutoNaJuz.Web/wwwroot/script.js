const cars = [
	{ id: 1, name: 'Toyota Corolla', category: 'sedan', details: 'Komfortowy sedan z niskim spalaniem.' },
	{ id: 2, name: 'BMW X5', category: 'suv', details: 'Luksusowy SUV idealny na każdą podróż.' },
	{ id: 3, name: 'Porsche 911', category: 'sport', details: 'Sportowy samochód dla wymagających.' },
]

const carList = document.getElementById('cars')
const filter = document.getElementById('filter')
const form = document.getElementById('reservation-form')

// Funkcja do generowania listy samochodów
function renderCars(category = 'all') {
	carList.innerHTML = ''
	const filteredCars = cars.filter(car => category === 'all' || car.category === category)
	filteredCars.forEach(car => {
		const carCard = document.createElement('div')
		carCard.classList.add('car-card')
		carCard.dataset.carId = car.id // Przypisanie ID samochodu
		carCard.innerHTML = `
            <h3>${car.name}</h3>
            <p>${car.details}</p>
        `
		carList.appendChild(carCard)

		// Dodaj obsługę kliknięcia na kartę samochodu
		carCard.addEventListener('click', () => {
			// Usuń poprzednią klasę 'selected' z innych kart
			document.querySelectorAll('.car-card').forEach(card => card.classList.remove('selected'))
			// Dodaj klasę 'selected' do klikniętej karty
			carCard.classList.add('selected')
			// Ustaw ID wybranego samochodu w ukrytym polu formularza
			document.getElementById('selected-car-id').value = car.id
		})
	})
}

// Obsługa filtrowania
filter.addEventListener('change', () => {
	renderCars(filter.value)
})

// Obsługa formularza rezerwacji
form.addEventListener('submit', e => {
	e.preventDefault()
	const carId = document.getElementById('selected-car-id').value
	const startDate = document.getElementById('start-date').value
	const endDate = document.getElementById('end-date').value

	if (!carId || !startDate || !endDate) {
		alert('Proszę wypełnić wszystkie pola i wybrać samochód.')
		return
	}

	alert(`Samochód zarezerwowany! Szczegóły: 
        - Samochód: ${cars.find(car => car.id == carId).name}
        - Od: ${startDate} 
        - Do: ${endDate}`)
})

// Inicjalizacja
renderCars()
