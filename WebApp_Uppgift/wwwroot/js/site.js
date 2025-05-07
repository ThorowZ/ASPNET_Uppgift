//ChatGPT
// Project-specific dropdowns
document.querySelectorAll('[data-type="project-dropdown"]').forEach(button => {
    button.addEventListener('click', () => {
        const card = button.closest('.project-card');
        const dropdown = card.querySelector('.project-dropdown-container');
        dropdown.classList.toggle('project-expand-show');
    });
});

// Open modal logic
document.querySelectorAll('[data-type="modal"]').forEach(button => {
    button.addEventListener('click', () => {
        const targetId = button.getAttribute('data-target');
        const modal = document.getElementById(targetId);

        if (modal.classList.contains('modal-show')) {
            modal.classList.remove('modal-show');
            modal.classList.add('modal-hide');
        } else {
            modal.classList.remove('modal-hide');
            modal.classList.add('modal-show');
        }

    });
});

// Close modal logic
document.querySelectorAll('.btn-close').forEach(button => {
    button.addEventListener('click', () => {
        const modal = button.closest('.modal');
        if (modal) {
            modal.classList.remove('modal-show');
            modal.classList.add('modal-hide');
        }
    });
});


//const dropdowns = document.querySelectorAll('[data-type="dropdown"]');


//dropdowns.forEach(dropdown => {
//    const targetId = dropdown.getAttribute('data-target');
//    const targetElement = document.querySelector(targetId);

//    if (targetElement) {
//        dropdown.addEventListener('click', () => {
//            if (targetElement.classList.contains('hide')) {
//                targetElement.classList.remove('hide');
//                targetElement.classList.add('show');
//            } else {
//                targetElement.classList.remove('show');
//                targetElement.classList.add('hide');
//            }
//        });
//    }
//});




const modals = document.querySelectorAll('[data-type="modal"]');

function openModal() {
    document.getElementById("add-project-modal").classList.add("modal-show");
}

function closeModal() {
    document.getElementById("add-project-modal").classList.remove("modal-show");
}