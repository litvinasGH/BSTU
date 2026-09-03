@echo off

start "Server X" cmd /k "node server.js X 3001 1000"
start "Server Y" cmd /k "node server.js Y 3002 2000"
start "Server Z" cmd /k "node server.js Z 3003 3000"

echo Servers started
pause
