const uri = '/api/Clients';
let todos = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}



function sortAsc() {
    let resAsc = fetch(uri + "/" +  "direction?" + new URLSearchParams({
        direction: 'asc',
    })).then(res => res.json())
        .then((json) => {
            _displayItems(JSON.parse(JSON.stringify(json)))
        })
   
}

function sortDesc() {
    fetch(uri + "/" + "direction?" + new URLSearchParams({
        direction: 'desc',
    })).then(res => res.json())
        .then((json) => {
            _displayItems(JSON.parse(JSON.stringify(json)))
        })
}

function addItem() {
    const addlastname = document.getElementById('lastname');
    const addfirstname = document.getElementById('firstname');
    const addemail = document.getElementById('email');

    const item = { 
        lastname: addlastname.value.trim(),
        firstname: addfirstname.value.trim(),
        email: addemail.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then((res) => {
            if (res.response.statusCode === 201) {               
                $('#registration').modal('show');
            }
            getItems();
            addlastname.value = '';
            addfirstname.value = '';
            addemail.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}



function _displayItems(data) {
    const tBody = document.getElementById('todos');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(item => {
        let isCompleteCheckbox = document.createElement('input');
        isCompleteCheckbox.type = 'checkbox';
        isCompleteCheckbox.disabled = true;
        isCompleteCheckbox.checked = item.isComplete;

        let tr = tBody.insertRow();

        let td2 = tr.insertCell(0);
        let textNode = document.createTextNode(item.lastName);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(1);
        let textNode1 = document.createTextNode(item.firstName);
        td3.appendChild(textNode1);

        let td4 = tr.insertCell(2);
        let textNode2 = document.createTextNode(item.email);
        td4.appendChild(textNode2);


    });

    todos = data;
}