document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");

    if (!form) return;

    const fields = form.querySelectorAll("input[data-val='true']");

    fields.forEach(field => {
        field.addEventListener("input", function () {
            validateField(field);
        });
    });

    function validateField(field) {
        let errorSpan = document.querySelector(`span[data-valmsg-for='${field.name}']`);
        if (!errorSpan) return;

        let errorMessage = ""
        let value = field.value.trim();

        if (field.hasAttribute("data-val-required")) {
            if (value === "") {
                errorMessage = "This field is required.";
            }
        }
        if (field.hasAttribute("data-val-regex")) {
            const regex = new RegExp(field.getAttribute("data-val-regex"));
            if (!regex.test(value)) {
                errorMessage = "Invalid format.";
            }
        }

        if (errorMessage) {
            field.classList.add("input-validation-error");
            errorSpan.classList.remove("field-validation-valid");
            errorSpan.classList.add("field-validation-error");
            errorSpawn.textContent = errorMessage;
        } else {
            field.classList.remove("input-validation-error");
            errorSpan.classList.remove("field-validation-error");
            errorSpan.classList.add("field-validation-valid");
            errorSpan.textContent = ""; }


    }
});