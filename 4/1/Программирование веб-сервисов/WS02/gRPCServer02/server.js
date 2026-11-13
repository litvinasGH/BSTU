const grpc = require("@grpc/grpc-js");
const protoLoader = require("@grpc/proto-loader");
const path = require("path");

const PROTO_PATH = path.join(__dirname, "protos", "calculator.proto");

const packageDefinition = protoLoader.loadSync(PROTO_PATH, {
    keepCase: true,
    longs: Number,
    enums: String,
    defaults: true,
    oneofs: true
});

const calculatorProto =
    grpc.loadPackageDefinition(packageDefinition).calculator;

// SUM
function sum(call, callback) {
    const { x, y } = call.request;
    callback(null, { result: x + y });
}

// SUB
function sub(call, callback) {
    const { x, y } = call.request;
    callback(null, { result: x - y });
}

// MUL
function mul(call, callback) {
    const { x, y } = call.request;
    callback(null, { result: x * y });
}

// DIV
function div(call, callback) {
    const { x, y } = call.request;

    if (y === 0) {
        return callback({
            code: grpc.status.INVALID_ARGUMENT,
            details: "Division by zero"
        });
    }

    callback(null, { result: x / y });
}

// FACT
function fact(call, callback) {
    const x = call.request.x;

    if (x < 0) {
        return callback({
            code: grpc.status.INVALID_ARGUMENT,
            details: "Factorial of a negative number is undefined"
        });
    }

    let result = 1n;

    for (let i = 2; i <= x; i++) {
        result *= BigInt(i);

        if (result > 2147483647n) {
            return callback({
                code: grpc.status.OUT_OF_RANGE,
                details: "Factorial exceeds Int32 range"
            });
        }
    }

    callback(null, { result: Number(result) });
}


const server = new grpc.Server();

server.addService(calculatorProto.Calculator.service, {
    sum,
    sub,
    mul,
    div,
    fact
});

server.bindAsync(
    "127.0.0.1:5002",
    grpc.ServerCredentials.createInsecure(),
    (error, port) => {
        if (error) {
            console.error("Server error:", error);
            return;
        }

        server.start();
        console.log(`gRPC Node.js Server listening on port ${port}`);
    }
);