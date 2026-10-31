const JsonRPCServer = require('jsonrpc-server-http-nats');

const server = new JsonRPCServer();

let bin_validator = (param) => {

    if (!Array.isArray(param))
        throw new Error('Ожидается массив');

    if (param.length != 2)
        throw new Error('Ожидается 2 значения');

    param.forEach(element => {
        if (!isFinite(element))
            throw new Error('Ожидается число');
    });

    return param;
};

let arr_validator = (param) => {

    if (!Array.isArray(param))
        throw new Error('Ожидается массив');

    if (param.length === 0)
        throw new Error('Массив пуст');

    param.forEach(element => {
        if (!isFinite(element))
            throw new Error('Ожидается число');
    });
    

    return param;
};

server.on('mul', arr_validator, (params, _, response) => {
        let result = params.reduce((a, b) => a * b, 1);
    response(null, result);
});

server.on('sum', arr_validator, (params, _, response) => {
    let result = params.reduce((a, b) => a + b, 0);
    response(null, result);
});

server.on('div', bin_validator, (params, _, response) => {
    if (params[1] === 0) {
        response(new Error('Деление на ноль'), null);
        return;
    }
    let result = params[0] / params[1];
    response(null, result);
});

server.on('proc', bin_validator, (params, _, response) => {
    if (params[1] === 0) {
        response(new Error('Деление на ноль'), null);
        return;
    }
    let result = params[0] / params[1] * 100;
    response(null, result);
});

server.listenHttp({ host: '127.0.0.1', port: 3000 }, () => {
    console.log('JSON-RPC server is listening on http://localhost:3000');
});
