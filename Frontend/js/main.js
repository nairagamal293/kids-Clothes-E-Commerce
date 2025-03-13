(function ($) {
    "use strict";

    /*==================================================================
    [ Focus Contact2 ]*/
    $('.input100').each(function () {
        $(this).on('blur', function () {
            if ($(this).val().trim() !== "") {
                $(this).addClass('has-val');
            } else {
                $(this).removeClass('has-val');
            }
        });
    });

    /*==================================================================
    [ Validate Form Fields ]*/
    var input = $('.validate-input .input100');

    $('.validate-form').on('submit', function () {
        var check = true;

        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) === false) {
                showValidate(input[i]);
                check = false;
            }
        }

        return check;
    });

    $('.validate-form .input100').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });

    function validate(input) {
        if ($(input).attr('type') === 'email' || $(input).attr('name') === 'email') {
            if (!$(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/)) {
                return false;
            }
        } else {
            if ($(input).val().trim() === '') {
                return false;
            }
        }
        return true;
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();
        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();
        $(thisAlert).removeClass('alert-validate');
    }

})(jQuery);


//========================================================
// SIGNUP FORM HANDLING
//========================================================
document.getElementById("signupForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    let name = document.getElementById("name").value.trim();
    let email = document.getElementById("email").value.trim();
    let password = document.getElementById("password").value.trim();

    let signupData = {
        name: name,
        email: email,
        password: password // Now matches RegisterDto
    };

    console.log("Sending data:", signupData); // Debugging

    try {
        let response = await fetch("https://localhost:7074/api/auth/signup", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(signupData)
        });

        if (!response.ok) {
            let errorMessage = await response.text();
            throw new Error(errorMessage || "Signup failed");
        }

        let result = await response.json();
        console.log("Signup successful:", result);
        alert("Signup successful! Please login.");
        window.location.href = "login.html";

    } catch (error) {
        console.error("Error:", error);
        document.getElementById("errorMessage").innerText = error.message;
        document.getElementById("errorMessage").style.display = "block";
    }
});

//========================================================
// LOGIN FORM HANDLING
//========================================================
document.addEventListener("DOMContentLoaded", function () {
    const loginForm = document.getElementById("loginForm");

    if (loginForm) {
        loginForm.addEventListener("submit", async function (event) {
            event.preventDefault();

            // Get input values
            const email = document.getElementById("email")?.value.trim();
            const password = document.getElementById("password")?.value.trim();
            const errorMessage = document.getElementById("errorMessage");

            if (!email || !password) {
                errorMessage.textContent = "Please enter both email and password!";
                errorMessage.style.display = "block";
                return;
            }

            // Create login data object
            const loginData = {
                email: email,
                passwordHash: password // Ensure this matches your API model
            };

            try {
                const response = await fetch("https://localhost:7074/api/auth/login", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(loginData)
                });

                const result = await response.json();

                if (response.ok) {
                    // ✅ Show success message
                    alert("Login successful! Redirecting to homepage...");

                    // ✅ Store token in localStorage
                    localStorage.setItem("token", result.token);

                    // ✅ Redirect to index.html
                    window.location.href = "index.html";
                } else {
                    // ❌ Display error message if login fails
                    errorMessage.textContent = result.message || "Invalid email or password!";
                    errorMessage.style.display = "block";
                }
            } catch (error) {
                console.error("Error:", error);
                errorMessage.textContent = "Something went wrong! Please try again.";
                errorMessage.style.display = "block";
            }
        });
    }
});

// Show error message function
function showErrorMessage(message) {
    const errorMessage = document.getElementById("errorMessage");
    errorMessage.innerText = message;
    errorMessage.style.display = "block";
}


//========================================================
// AUTHORIZATION CHECK (FOR PROTECTED PAGES ONLY)
//========================================================
document.addEventListener("DOMContentLoaded", function () {
    const token = localStorage.getItem("token");

    // Only enforce authentication on protected pages
    const protectedPages = ["admin-dashboard.html", "customer-dashboard.html"];

    if (!token && protectedPages.includes(window.location.pathname.split("/").pop())) {
        alert("Unauthorized! Please log in first.");
        window.location.href = "login.html";
    }
});
