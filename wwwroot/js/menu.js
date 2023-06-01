const buttonContent = document.querySelector('.button-content');
const buttonContainers = document.querySelectorAll('.button-container');

buttonContainers.forEach((container) => {
    const clone = buttonContent.cloneNode(true);
    container.appendChild(clone);
});