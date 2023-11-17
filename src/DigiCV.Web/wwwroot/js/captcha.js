document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("#form");
    const msg = document.getElementById("captcha_msg");

    form.addEventListener("submit", function (event) {
        const recaptchaResponse = grecaptcha.getResponse();
        if (recaptchaResponse.length === 0) {
            event.preventDefault();
            msg.textContent = "Please complete the reCAPTCHA";
            msg.style.color = "red";
            setTimeout(function () {
                msg.textContent = "";
            }, 5000);
        }
    });
});