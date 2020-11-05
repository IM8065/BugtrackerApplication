const menuBtn = document.querySelector(".menu-btn");
const sidebar = document.querySelector(".sidebar");

menuBtn.addEventListener("click", () => {
    toggle();
});

function toggle() {
    sidebar.classList.toggle("close");
}