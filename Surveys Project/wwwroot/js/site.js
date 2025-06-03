document.addEventListener("DOMContentLoaded", function () {
    function calculateAge(dob) {
        if (!dob) return null;
        const today = new Date();
        const birthDate = new Date(dob);
        let age = today.getFullYear() - birthDate.getFullYear();
        const m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }
        return age;
    }

    const dobInput = document.getElementById("Survey_DOB");
    const ageValidation = document.getElementById("AgeValidation");
    const form = document.getElementById("surveyForm");

    if (!dobInput || !ageValidation || !form) return; 

    dobInput.addEventListener("change", function () {
        const dobValue = this.value;
        const age = calculateAge(dobValue);

        if (!dobValue) {
            ageValidation.textContent = "";
            return;
        }

        if (age === null || isNaN(age)) {
            ageValidation.textContent = "";
            return;
        }

        if (age < 5 || age > 120) {
            ageValidation.textContent = "Age must be between 5 and 120 based on the selected date.";
        } else {
            ageValidation.textContent = "";
        }
    });

    form.addEventListener("submit", function (e) {
        const dobValue = dobInput.value;
        const age = calculateAge(dobValue);

        if (!dobValue) {
            ageValidation.textContent = "";
            return;
        }

        if (age === null || isNaN(age) || age < 5 || age > 120) {
            e.preventDefault();
            ageValidation.textContent = "Please select a date of birth that results in an age between 5 and 120.";
            dobInput.focus();
        }
    });
});
