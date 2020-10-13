window.onload = function () {
    let form = document.getElementById("form");
    form.onsubmit = function () {
        let email = document.getElementById("email").value;
        let password = document.getElementById("password").value;

        if (email.length === 0) {
            document.getElementById("errEmail").innerHTML = "Email not entered";
            return false;
        }

        if (password.length === 0) {
            document.getElementById("errPassword").innerHTML = "Password not entered";
            return false;
        }
        
        return true;
    }

    let inputfields = document.getElementsByClassName("form-control");
    for (let i = 0; i < inputfields.length; i++) {
        let idname = ["errEmail", "errPassword"];
        inputfields[i].onclick = function () {
            let change = document.getElementById(idname[i]);
            change.innerHTML = "";
        }
    }
}