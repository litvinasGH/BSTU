@echo off

start "Server X" cmd /k "node server.js X 3001"
start "Server Y" cmd /k "node server.js Y 3002"
start "Server Z" cmd /k "node server.js Z 3003"

echo Servers started
pause
