@echo off

echo ==========================
echo GET
echo ==========================
curl -X GET http://localhost:5035/api
echo.
echo.

echo ==========================
echo POST
echo ==========================
curl -X POST http://localhost:5035/api
echo.
echo.

echo ==========================
echo PUT
echo ==========================
curl -X PUT http://localhost:5035/api
echo.
echo.

echo ==========================
echo DELETE
echo ==========================
curl -X DELETE http://localhost:5035/api
echo.
echo.

pause
