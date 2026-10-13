// 1: Работа с DOM и событиями
// Изменение текста заголовка через DOM
document.querySelector('h1').textContent = 'Тестирование с DevTools';

// Логирование количества нажатий кнопки
let count = 0;
const button = document.querySelector('.button-class');

// Отправка данных формы
const form = document.querySelector('form');
form.addEventListener('submit', (event) => {
	event.preventDefault(); // Отменяем стандартное поведение формы
	window.location.href = 'https://www.google.com/search?q='+document.querySelector('#searchText').value; // Переход на Google
});

// 2: Отладка JavaScript
// Пошаговая отладка
function increaseCount() {
	count++;
	console.log("Текущее значение count:", count);
}

// 3: Сетевые запросы
const loadDataButton = document.querySelector('.load-data');
const dataContainer = document.querySelector('.data-container');
loadDataButton.addEventListener('click', () => {
	// Симуляция запроса
	//fetch('https://jsonplaceholder.typicode.com/posts')
	fetch('https://dummyjson.com/products')
		.then(response => response.json())
		.then(data => {
			console.log("Данные получены:", data);
			displayData(data.products); // Отображаем данные
		})
		.catch(error => {
			console.error("Ошибка при загрузке данных:", error);
		});
});

function displayData(products) {
	// Очищаем контейнер перед добавлением новых данных
	dataContainer.innerHTML = '';

	// Создаем элементы для каждого продукта и добавляем их в контейнер
	products.forEach(product => {
		const productElement = document.createElement('div');
		productElement.classList.add('product');
		productElement.innerHTML = `
			<hr>
			<img src="${product.images[0]}" alt="${product.title}" style="width: 100px;">
			<h3>${product.title}</h3>
			<p>Цена: ${product.price}</p>                    					
		`;
		dataContainer.appendChild(productElement);
	});
}

// 4: Динамическое добавление контента
// Эмуляция загрузки динамического контента через 2 секунды
setTimeout(() => {
	const dynamicElement = document.createElement('div');
	dynamicElement.classList.add('dynamic-element');
	dynamicElement.textContent = 'Этот элемент добавлен динамически через 2 секунды';
	document.body.appendChild(dynamicElement);
}, 2000);

 // Эмуляция асинхронной загрузки
setTimeout(() => {
	const dynamicElement2 = document.createElement('div');
	dynamicElement2.className = 'dynamic-element';
	dynamicElement2.textContent = 'Динамически загруженный элемент через 1 секунду';
	document.body.appendChild(dynamicElement2);

	// Изменение текста в параграфе
	const message = document.getElementById('message');
	message.textContent = 'Сообщение обновлено динамически';
}, 1000);


// 5: Логирование, ошибки и отладка
// Глобальная переменная для состояния


// Логирование состояния


// Симуляция ошибки
function simulateError() {
	try {
		console.log(nonExistentVariable); // Ошибка
	} catch (error) {
		console.error("Произошла ошибка через 3 секунды:", error.message);
		document.getElementById('error-message').textContent = `Ошибка через 3 секунды: ${error.message}`;
	}
}

// Симуляция ошибки через 3 секунды
setTimeout(simulateError, 3000);  // Ошибка через 3 секунды