window.onload = function () {
    let elemList = document.getElementsByClassName("btn btn-primary");

    for (let i = 0; i < elemList.length; i++) {
        elemList[i].addEventListener("click", AddToCart);
    }
}

function AddToCart(event) {
    let elem = event.currentTarget;
    let productId = elem.getAttribute("product_id");
    SendToCart(productId);
}

function SendToCart(productId) {
    let xhr = new XMLHttpRequest();

    xhr.open("POST", "/ShoppingCart/UpdateCart");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    xhr.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE) {
            // receive response from server
            if (this.status === 200 || this.status === 302) {
                let data = JSON.parse(this.responseText);

                if (this.status === 200) {
                    console.log("Successful operation: " + data.success);
                }
                else if (this.status === 302) {
                    window.location = data.redirect_url;
                }
            }
        }
    };

    xhr.send(JSON.stringify({
        ProductId: productId
    }));
}