# ===== ARRAY PARAMS =====
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"sum","params":[5.05,3.33],"id":1}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"sub","params":[5.05,3.33],"id":2}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"mul","params":[5.05,3.33],"id":3}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"div","params":[5.05,3.33],"id":4}'

# ===== OBJECT PARAMS =====
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"sum","params":{"x":5.05,"y":3.33},"id":5}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"sub","params":{"x":5.05,"y":3.33},"id":6}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"mul","params":{"x":5.05,"y":3.33},"id":7}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"div","params":{"x":5.05,"y":3.33},"id":8}'

# ===== SET PRECISION =====
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"pre","params":{"N":3}}'

# ===== AFTER PRECISION =====
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"sum","params":[5.05,3.33],"id":9}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"sub","params":[5.05,3.33],"id":10}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"mul","params":[5.05,3.33],"id":11}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"div","params":[5.05,3.33],"id":12}'

# ===== SET PRECISION AGAIN =====
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"pre","params":{"N":1}}'

# ===== OBJECT AGAIN =====
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"sum","params":{"x":5.05,"y":3.33},"id":13}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"sub","params":{"x":5.05,"y":3.33},"id":14}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"mul","params":{"x":5.05,"y":3.33},"id":15}'
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '{"jsonrpc":"2.0","method":"div","params":{"x":5.05,"y":3.33},"id":16}'

# ===== BATCH =====
curl -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d '[
{"jsonrpc":"2.0","method":"pre","params":{"N":4}},
{"jsonrpc":"2.0","method":"sum","params":{"x":5.05,"y":3.33},"id":17},
{"jsonrpc":"2.0","method":"sub","params":{"x":5.05,"y":3.33},"id":18},
{"jsonrpc":"2.0","method":"mul","params":{"x":5.05,"y":3.33},"id":19},
{"jsonrpc":"2.0","method":"div","params":{"x":5.05,"y":3.33},"id":20}
]'