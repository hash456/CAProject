function QtyMinus() {
    let num = document.getElementById("item1_input").value;
    if (num == 1)
        return;
    else
        num--;
    document.getElementById("item1_input").value = num;
}

function QtyPlus() {
    let num = document.getElementById("item1_input").value;
    num++;
    document.getElementById("item1_input").value = num;
}

