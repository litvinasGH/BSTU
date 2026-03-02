const rpcWS = require('rpc-websockets').Client;
const async = require('async');

const client = new rpcWS("ws://localhost:4000");

client.on('open', () => {

  async.parallel({
    square1: (cb) => client.call('square', [3]).then(r => cb(null, r)).catch(cb),
    square2: (cb) => client.call('square', [5, 4]).then(r => cb(null, r)).catch(cb),
    sum1: (cb) => client.call('sum', [2]).then(r => cb(null, r)).catch(cb),
    sum5: (cb) => client.call('sum', [2, 4, 6, 8, 10]).then(r => cb(null, r)).catch(cb),
    mul1: (cb) => client.call('mul', [3]).then(r => cb(null, r)).catch(cb),
    mul6: (cb) => client.call('mul', [3, 5, 7, 9, 11, 13]).then(r => cb(null, r)).catch(cb)
  }, (err, results) => {
    if (err) {
      console.error("Error in parallel calls:", err);
      return;
    }

    console.log("Square with 1 parameter:", results.square1);
    console.log("Square with 2 parameters:", results.square2);
    console.log("Sum with 1 parameter:", results.sum1);
    console.log("Sum with 5 parameters:", results.sum5);
    console.log("Mul with 1 parameter:", results.mul1);
    console.log("Mul with 6 parameters:", results.mul6);

    client.login({ login: 'mnmd', password: '12345' })
      .then((logged) => {
        if (!logged) {
          console.error("Login failed");
          return;
        }

        async.parallel({
          fib1: (cb) => client.call('fib', [1]).then(r => cb(null, r)).catch(cb),
          fib2: (cb) => client.call('fib', [2]).then(r => cb(null, r)).catch(cb),
          fib7: (cb) => client.call('fib', [7]).then(r => cb(null, r)).catch(cb),
          fact0: (cb) => client.call('fact', [0]).then(r => cb(null, r)).catch(cb),
          fact5: (cb) => client.call('fact', [5]).then(r => cb(null, r)).catch(cb),
          fact10: (cb) => client.call('fact', [10]).then(r => cb(null, r)).catch(cb)
        }, (err, results) => {
          if (err) {
            console.error("Error in parallel auth calls:", err);
            return;
          }

          console.log("Fib for 1:", results.fib1);
          console.log("Fib for 2:", results.fib2);
          console.log("Fib for 7:", results.fib7);
          console.log("Fact for 0:", results.fact0);
          console.log("Fact for 5:", results.fact5);
          console.log("Fact for 10:", results.fact10);
        });
      })
      .catch(err => console.error("Login error:", err));
  });
});

client.on('error', (err) => {
  console.error("Client error:", err);
});
