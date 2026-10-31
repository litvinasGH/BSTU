@echo off

echo === CREATE ===
curl -X POST http://localhost:3000/graphql ^
-H "Content-Type: application/json" ^
-d "{\"query\":\"mutation { createCelebrity(id:1, fullName: \\\"Brad Pitt\\\", nationality: \\\"USA\\\", reqPhotoPath: \\\"/img/brad.jpg\\\") { id fullName nationality reqPhotoPath } }\"}"

echo.
echo === GET ALL ===
curl -X POST http://localhost:3000/graphql ^
-H "Content-Type: application/json" ^
-d "{\"query\":\"query { celebrities { id fullName nationality reqPhotoPath } }\"}"

echo.
echo === GET BY ID ===
curl -X POST http://localhost:3000/graphql ^
-H "Content-Type: application/json" ^
-d "{\"query\":\"query { celebrity(id:1) { id fullName nationality reqPhotoPath } }\"}"

echo.
echo === UPDATE ===
curl -X POST http://localhost:3000/graphql ^
-H "Content-Type: application/json" ^
-d "{\"query\":\"mutation { updateCelebrity(id:1, fullName: \\\"Brad Pitt Updated\\\", nationality: \\\"USA\\\", reqPhotoPath: \\\"/img/new.jpg\\\") { id fullName nationality reqPhotoPath } }\"}"

echo.
echo === DELETE ===
curl -X POST http://localhost:3000/graphql ^
-H "Content-Type: application/json" ^
-d "{\"query\":\"mutation { deleteCelebrity(id:1) }\"}"

pause