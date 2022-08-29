const updateView = () => {
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

loginBtn.addEventListener("click", async () => {
    let userNameEl = document.getElementById("userName");
    let passwordEl = document.getElementById("password");

    let userName = userNameEl.value;
    let password = passwordEl.value

    let request = {
        userName,
        password
    }
    let host = await getHost();
    let url = host + '/users/login/chrome-extension';
    console.log(url);
    fetch(url, {
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
logoutBtn.addEventListener("click", () => {
    chrome.storage.sync.remove('token', function () {
        console.log('token has been removed');
        updateView();
    });
});

let hostInput = document.getElementById("host");
hostInput.addEventListener("blur", () => {
    const host = hostInput.value;
    console.log("setting host", host);
    chrome.storage.sync.set({ 'host': host }, function () {
    });
})


function getHost() {
    return new Promise((resolve, _) => {
        chrome.storage.sync.get('host', (data) => {
            resolve(data.host);
        });
    });
}