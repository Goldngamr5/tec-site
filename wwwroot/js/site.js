// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const wrapper = document.getElementById("bubble-wrapper");
const animateBubble = e => {
    const x = e.clientX;

    const p = e.clientY / window.innerHeight * 100;
    const body = document.getElementById("main-body")

    body.style.backgroundPositionY = `${p}%`;

    const bubble = document.createElement("div");

    bubble.className = "bubble";

    bubble.style.left = `${x}px`;

    wrapper.appendChild(bubble);

    setTimeout(() => wrapper.removeChild(bubble), 2000);
}

const animatePop = (x, y) => {
    const mainpop = document.createElement("div");
    const smallpop1 = document.createElement("div");
    const smallpop2 = document.createElement("div");
    const smallpop3 = document.createElement("div");
    const smallpop4 = document.createElement("div");

    mainpop.className = "mainpop";
    smallpop1.className = "smallpop1";
    smallpop2.className = "smallpop2";
    smallpop3.className = "smallpop3";
    smallpop4.className = "smallpop4";

    mainpop.style.left = `${x}px`;
    mainpop.style.top = `${y}px`;
    smallpop1.style.left = `${x}px`;
    smallpop1.style.top = `${y}px`;
    smallpop2.style.left = `${x}px`;
    smallpop2.style.top = `${y}px`;
    smallpop3.style.left = `${x}px`;
    smallpop3.style.top = `${y}px`;
    smallpop4.style.left = `${x}px`;
    smallpop4.style.top = `${y}px`;

    wrapper.appendChild(mainpop);
    wrapper.appendChild(smallpop1);
    wrapper.appendChild(smallpop2);
    wrapper.appendChild(smallpop3);
    wrapper.appendChild(smallpop4);

    setTimeout(() => wrapper.removeChild(mainpop), 1000);
    setTimeout(() => wrapper.removeChild(smallpop1), 2000);
    setTimeout(() => wrapper.removeChild(smallpop2), 2000);
    setTimeout(() => wrapper.removeChild(smallpop3), 2000);
    setTimeout(() => wrapper.removeChild(smallpop4), 2000);
}

const ButtonWait = () => {
    preventDefault();
     
    setTimeout(() => {
        return;
    }, 2000);
};

window.onclick = e => animatePop(e.clientX, e.clientY);

window.onmousemove = e => animateBubble(e);
window.ontouchmove = e => animateBubble(e.touches[0]);