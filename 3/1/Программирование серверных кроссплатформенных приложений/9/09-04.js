const http = require('http');


let requestObj = {
    "_comment":"lab9",
    "x":20,
    "y":10,
    "s":"Name",
    "m":["a","b","c"],
    "o":{"surename":"Kach","name":"Vac"}
};

let jsonRequest = JSON.stringify(requestObj);

let options = {
    host:'localhost',
    path:'/post-json',
    port:5000,
    method:'POST',
    headers:{
        'content-type':'application/json',
        'accept':'application/json',
    }
};

const request = http.request(options,(response)=>{
    console.log('Status code: ',response.statusCode);
    let responseBody = '';
    response.on('data',(chunk)=>{
        responseBody+=chunk;
    });
    response.on('end',()=>{
        console.log('Body: ',JSON.parse(responseBody));
    });
});


request.on('error',(err)=>{
    console.error(err);
});
request.end(jsonRequest);