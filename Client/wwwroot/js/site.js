var element = document.getElementById("btnChangeBg");
console.log(element);

element.addEventListener("click", changeBg,false)


function changeBg() {
    var x = document.getElementById("paraf2");
    x.className = (x.className === "activate") ? "" : "activate";
}