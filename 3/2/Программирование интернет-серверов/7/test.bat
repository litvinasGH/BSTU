
curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"sum\",\"params\":[5.05, 3.33],\"id\":1}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"sub\",\"params\":[5.05, 3.33],\"id\":2}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"mul\",\"params\":[5.05, 3.33],\"id\":3}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"div\",\"params\":[5.05, 3.33],\"id\":4}"




curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"sum\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":5}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"sub\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":6}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"mul\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":7}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"div\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":8}"


curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"pre\",\"params\":{\"N\":3}}"



curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"sum\",\"params\":[5.05, 3.33],\"id\":9}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"sub\",\"params\":[5.05, 3.33],\"id\":10}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"mul\",\"params\":[5.05, 3.33],\"id\":11}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"div\",\"params\":[5.05, 3.33],\"id\":12}"



curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"pre\",\"params\":{\"N\":1}}"



curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"sum\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":13}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"sub\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":14}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"mul\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":15}"

curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d "{\"jsonrpc\":\"2.0\",\"method\":\"div\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":16}"




curl.exe -X POST http://localhost:3000/rpc -H "Content-Type: application/json" -d ^
"[ ^
{\"jsonrpc\":\"2.0\",\"method\":\"pre\",\"params\":{\"N\":1}}, ^
{\"jsonrpc\":\"2.0\",\"method\":\"sum\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":17}, ^
{\"jsonrpc\":\"8.0\",\"method\":\"sub\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":18}, ^
{\"jsonrpc\":\"2.0\",\"method\":\"mu\",\"params\":{\"x\":5.05, \"y\":3.33},\"id\":19}, ^
{\"jsonrpc\":\"2.0\",\"method\":\"div\",\"params\":{\"x\":5.05, \"y\":0},\"id\":20}  ^
]"

 

