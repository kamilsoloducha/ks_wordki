const apiUrl = "http://localhost:5000";

function getToken() {
    return new Promise((resolve, reject) => {
        chrome.storage.sync.get('token', (data) => {
            resolve(data.token);
        });
    });
}

chrome.runtime.onInstalled.addListener(() => {
    chrome.contextMenus.create({
        title: 'Wordki',
        id: 'wordki_menu',
        contexts: ['selection'],
    });

    chrome.contextMenus.create({ title: "Add word", parentId: "wordki_menu", id: 'wordki_menu_add_word', contexts: ["selection"] });
});

chrome.contextMenus.onClicked.addListener(async function (info, tab) {
    if (info.menuItemId === "wordki_menu_add_word" && info.selectionText) {
        const selectionText = info.selectionText;
        const request = { value: selectionText };
        const token = await getToken();
        await fetch(apiUrl + "/cards/add/chrome-extension", {
            body: JSON.stringify(request),
            method: 'POST',
            headers: new Headers({
                'Authorization': 'Bearer ' + token,
                'Content-Type': 'application/json'
            })
        })
            .catch(error => console.error(error));
    }
});

