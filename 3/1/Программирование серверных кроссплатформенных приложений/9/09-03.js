const http = require('http');
const qs = require('querystring');

let postData = qs.stringify({ x: 10, y: 20, z: 30 });

let options = {
    host: 'localhost',
    port: 5000,
    path: '/post-query',
    method: 'POST',
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
        'Content-Length': Buffer.byteLength(postData)
    }
};

const request = http.request(options, (response) => {
    console.log('Status code:', response.statusCode);

    let responseBody = '';
    response.on('data', (chunk) => {
        responseBody += chunk;
    });

    response.on('end', () => {
        console.log('Body:', responseBody);
    });
});

request.on('error', (err) => {
    console.log('Error:', err);
});


request.write(postData);
request.end();
