const updateView = () =>{
    chrome.storage.sync.get('token', function (data) {
        let loginEl = document.getElementById("login-container");
        let logoutEl = document.getElementById("logout-container");
        console.log('token', data.token);
        loginEl.hidden = data.token !== undefined;
        logoutEl.hidden = data.token === undefined;
    });
}

updateView();

let loginBtn = document.getElementById("login");

loginBtn.addEventListener("click", () => {
    let userNameEl = document.getElementById("userName");
    let passwordEl = document.getElementById("password");

    let userName = userNameEl.value;
    let password = passwordEl.value

    let request = {
        userName,
        password
    }

    fetch('http://localhost:5000/users/login/chrome-extension', {
        method: 'PUT',
        body: JSON.stringify(request),
        headers: {
            'Content-type': 'application/json'
        },
    })
        .then(response => response.text())
        .then(response => JSON.parse(response))
        .then(response => {
            let token = response.token;
            chrome.storage.sync.set({ token: token }, function () {
                console.log('Value is set to ' + token);
                updateView();
            });
        })
        .catch(error => console.error(error));
});

let logoutBtn = document.getElementById("logout");
logoutBtn.addEventListener("click", () =>{
    chrome.storage.sync.remove('token', function () {
       console.log('token has been removed');
       updateView();
    });
});
// let selectedClassName = "current";
// const presetButtonColors = ["#3aa757", "#e8453c", "#f9bb2d", "#4688f1"];

// // Reacts to a button click by marking the selected button and saving
// // the selection
// function handleButtonClick(event) {
//     // Remove styling from the previously selected color
//     let current = event.target.parentElement.querySelector(
//         `.${selectedClassName}`
//     );
//     if (current && current !== event.target) {
//         current.classList.remove(selectedClassName);
//     }

//     // Mark the button as selected
//     let color = event.target.dataset.color;
//     event.target.classList.add(selectedClassName);
//     chrome.storage.sync.set({ color });
// }

// // Add a button to the page for each supplied color
// function constructOptions(buttonColors) {
//     chrome.storage.sync.get("color", (data) => {
//         let currentColor = data.color;
//         // For each color we were provided…
//         for (let buttonColor of buttonColors) {
//             // …create a button with that color…
//             let button = document.createElement("button");
//             button.dataset.color = buttonColor;
//             button.style.backgroundColor = buttonColor;

//             // …mark the currently selected color…
//             if (buttonColor === currentColor) {
//                 button.classList.add(selectedClassName);
//             }

//             // …and register a listener for when that button is clicked
//             button.addEventListener("click", handleButtonClick);
//             page.appendChild(button);
//         }
//     });
// }

// // Initialize the page by constructing the color options
// constructOptions(presetButtonColors);

function login() {
    console.log('test');
}