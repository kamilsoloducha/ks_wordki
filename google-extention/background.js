function getToken() {
    return new Promise((resolve, reject) => {
        chrome.storage.sync.get('token', (data) => {
            resolve(data.token);
        });
    });
}

function getHost() {
    return new Promise((resolve, _) => {
        chrome.storage.sync.get('host', (data) => {
            resolve(data.host);
        });
    });
}

chrome.runtime.onInstalled.addListener(() => {
    chrome.contextMenus.create({
        title: 'Wordki',
        id: 'wordki_menu',
        contexts: ['selection'],
    });

    chrome.contextMenus.create({
        title: "Add word",
        parentId: "wordki_menu",
        id: 'wordki_menu_add_word',
        contexts: ["selection"]
    });
});

chrome.contextMenus.onClicked.addListener(async function (info, tab) {
    if (info.menuItemId === "wordki_menu_add_word" && info.selectionText) {
        const selectionText = info.selectionText;
        const request = {
            value: selectionText
        };
        const token = await getToken();
        const host = await getHost();
        const url = host + "/cards/add/extension";
        console.log(url);
        await fetch(url, {
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