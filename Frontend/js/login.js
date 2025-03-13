document.addEventListener("DOMContentLoaded", function () {
    const loginForm = document.getElementById("loginForm");

    if (loginForm) {
        loginForm.addEventListener("submit", async function (event) {
            event.preventDefault();

            const email = document.getElementById("email")?.value.trim();
            const password = document.getElementById("password")?.value.trim();
            const errorMessage = document.getElementById("errorMessage");

            errorMessage.style.display = "none"; // Hide previous error

            if (!email || !password) {
                errorMessage.textContent = "Please enter both email and password!";
                errorMessage.style.display = "block";
                return;
            }

            const loginData = { email, password };

            console.log("📡 Sending request to:", "https://localhost:7074/api/auth/login");
            console.log("📝 Login data:", JSON.stringify(loginData));

            try {
                const response = await fetch("https://localhost:7074/api/auth/login", {
                    method: "POST",
                    headers: { 
                        "Content-Type": "application/json",
                        "Accept": "*/*" // ✅ Allow different response types
                    },
                    body: JSON.stringify(loginData),
                });

                if (!response.ok) {
                    const errorText = await response.text();
                    console.error("❌ Server error:", errorText);
                    throw new Error(errorText || `HTTP Error: ${response.status}`);
                }

                const result = await response.json();
                console.log("✅ Response received:", result);

                if (result.token) {
                    alert("🎉 Login successful! Redirecting...");
                    localStorage.setItem("token", result.token);
                    window.location.href = "index.html";
                } else {
                    throw new Error(result.message || "Invalid email or password!");
                }
            } catch (error) {
                console.error("⚠️ Error:", error);
                errorMessage.textContent = error.message || "Something went wrong! Please try again.";
                errorMessage.style.display = "block";
            }
        });
    }
});
