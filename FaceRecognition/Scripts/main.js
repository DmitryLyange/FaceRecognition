function trainFirst(event) {
    apiCall('Api/Algorithm/PCA');
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
        console.log(await data.text());
    } else {
        console.error('FIX YOUR GD DMN SRVR... 1..2..3..4..');
    }
    
}