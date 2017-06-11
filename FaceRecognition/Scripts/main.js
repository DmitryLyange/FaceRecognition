async function trainFirst(event) {
    try {
        const responseData = await apiCall('Api/Algorithm/PCA');
        createNewStatisticsCard(responseData);
    } catch (e) {
        console.log(e);
    } 
}

function trainSecond (event) {
    debugger
}

function trainThird (event) {
    debugger
}

async function apiCall(url) {
    const data = await fetch(url,
        {
            method: 'get',
            contentType: "json",
            dataType: "json"
        });
    if (data) {
        return await data.text();
    } else {
        throw new Error('FIX YOUR GD DMN SRVR... 1..2..3..4..');
    }
}

function createNewStatisticsCard(plot) {
    const nodePrototype = document.getElementById('prototypeCard');
    const clenedElement = nodePrototype.cloneNode(true);
    debugger 
    document.getElementById('cardContainer').appendChild(clenedElement);
}