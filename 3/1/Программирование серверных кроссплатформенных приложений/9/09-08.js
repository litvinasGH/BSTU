const http = require('http');

const options = {
    host: 'localhost',
    path: '/download/file.txt',
    port: 5000,
    method: 'GET',
};

const request = http.request(options, (response) => {
    let data = '';
    
    response.setEncoding('utf8');

    response.on('data', (chunk) => {
        data += chunk;
    });

    response.on('end', () => {
        console.log(data);
    });
});

request.on('error', (err) => {
    console.log('Error: ', err);
});

request.end();