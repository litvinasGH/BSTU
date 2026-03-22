    const inputs = document.querySelectorAll(".edit-input");
    const deleteBtn = document.getElementById("deleteBtn");

    inputs.forEach(input => {
        input.addEventListener("input", () => {
        deleteBtn.disabled = true;
    });
});