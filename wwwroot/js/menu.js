const buttonContent = document.querySelector('.button-content');
const buttonContainers = document.querySelectorAll('.button-container');

buttonContainers.forEach((container) => {
    const clone = buttonContent.cloneNode(true);
    container.appendChild(clone);

    const idRootInputCreate = clone.querySelector('.id-root-create');
    if (idRootInputCreate) {
        idRootInputCreate.value = container.id;
    }

    const idRootInputDelete = clone.querySelector('.id-root-delete');
    if (idRootInputDelete) {
        idRootInputDelete.value = container.id;
    }

    const idRootInputEdit = clone.querySelector('a.id-root-edit');
    if (idRootInputEdit) {
        idRootInputEdit.id = container.id;
    }

    const contentCreate = clone.querySelector('input.content-create');
    if (contentCreate) {
        contentCreate.value = container.getAttribute('data');
    }

    const contentEdit = clone.querySelector('a.content-edit');
    if (contentEdit) {
        contentEdit.setAttribute('data', container.getAttribute('data').slice(0, -4).toString());
    }
});

const bindingValue = (e) => {
    console.log("test: ", e.target.getAttribute('id'));
    const idDst = document.querySelector('.id-root-edit-modal');
    const contentDst = document.querySelector('input.content-edit-modal');

    if (idDst) {
        idDst.value = e.target.getAttribute('id');
    }

    if (contentDst) {
        contentDst.value = e.target.getAttribute('data');
    }
};