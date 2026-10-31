const http = require('http');

let options = {
    host:'localhost',
    path:'/get',
    port:5000,
    method:'GET'
}

const request = http.request(options,(response)=>{
    console.log('Status code: ',response.statusCode);
    console.log('Message: ',response.statusMessage);
    console.log('Server IP: ',response.socket.remoteAddress);
    console.log('Server port: ',response.socket.remotePort);

    let responseBody = '';
    response.on('data',(chunk)=>{
        responseBody+=chunk;
    });
    response.on('end',()=>{
        console.log('Body: ',responseBody);
    });
});

request.on('error',(err)=>{
    console.error('Error: ',err);
});
request.end();