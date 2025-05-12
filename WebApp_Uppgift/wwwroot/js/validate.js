document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    if (!form) return;

    const fields = form.querySelectorAll("input[data-val='true']");

    fields.forEach(field => {
        field.addEventListener("input", () => {
            validateField(field);
        });
    });

    form.addEventListener("submit", function (event) {
        let isValid = true;

        fields.forEach(field => {
            if (!validateField(field)) {
                isValid = false;
            }
        });

        if (!isValid) {
            event.preventDefault();
            console.warn("Form submission prevented due to validation errors.");
        }
    });

    function validateField(field) {
        const errorSpan = document.querySelector(`span[data-valmsg-for='${field.name}']`);
        if (!errorSpan) return true;

        let errorMessage = "";
        const value = field.value.trim();

        if (field.hasAttribute("data-val-required") && value === "") {
            errorMessage = "This field is required.";
        }

        if (!errorMessage && field.hasAttribute("data-val-regex")) {
            const pattern = field.getAttribute("data-val-regex-pattern");
            const regex = new RegExp(pattern);
            if (!regex.test(value)) {
                errorMessage = "Invalid format.";
            }
        }

        if (errorMessage) {
            field.classList.add("input-validation-error");
            errorSpan.classList.remove("field-validation-valid");
            errorSpan.classList.add("field-validation-error");
            errorSpan.textContent = errorMessage;
            return false;
        } else {
            field.classList.remove("input-validation-error");
            errorSpan.classList.remove("field-validation-error");
            errorSpan.classList.add("field-validation-valid");
            errorSpan.textContent = "";
            return true;
        }
    }
});
