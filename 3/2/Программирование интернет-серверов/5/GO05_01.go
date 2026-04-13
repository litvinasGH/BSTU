package main

import (
	"database/sql"
	"encoding/json"
	"log"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
	_ "github.com/jackc/pgx/v5/stdlib"
)

type Celebrity struct {
	Id           int    `json:"id"`
	FullName     string `json:"fullName"`
	Nationality  string `json:"nationality"`
	ReqPhotoPath string `json:"reqPhotoPath"`
}

var db *sql.DB

func main() {
	initDB()

	r := mux.NewRouter()

	r.HandleFunc("/Celebrities/All", getAllCelebrities).Methods(http.MethodGet)
	r.HandleFunc("/Celebrities/{id}", getCelebrityByID).Methods(http.MethodGet)
	r.HandleFunc("/Celebrities", createCelebrity).Methods(http.MethodPost)
	r.HandleFunc("/Celebrities/{id}", updateCelebrity).Methods(http.MethodPut)
	r.HandleFunc("/Celebrities/{id}", deleteCelebrity).Methods(http.MethodDelete)

	log.Println("GO05_01 started on :3000")
	log.Fatal(http.ListenAndServe(":3000", loggingMiddleware(r)))
}

func initDB() {
	var err error

	//connStr := "postgres://postgres:postgres@localhost:5432/gisdb?sslmode=disable"

	connStr := "host=localhost user=postgres password=postgres dbname=gisdb port=5432 sslmode=disable"
	db, err = sql.Open("pgx", connStr)
	if err != nil {
		log.Fatal(err)
	}

	err = db.Ping()
	if err != nil {
		log.Fatal(err)
	}

	createTable := `
	CREATE TABLE IF NOT EXISTS celebrities (
		id INTEGER PRIMARY KEY,
		fullName TEXT,
		nationality TEXT,
		reqPhotoPath TEXT
	);`

	_, err = db.Exec(createTable)
	if err != nil {
		log.Fatal(err)
	}
}

func loggingMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		log.Printf("%s %s from %s", r.Method, r.URL.Path, r.RemoteAddr)
		next.ServeHTTP(w, r)
	})
}

// GET ALL
func getAllCelebrities(w http.ResponseWriter, r *http.Request) {
	rows, err := db.Query("SELECT id, fullName, nationality, reqPhotoPath FROM celebrities")
	if err != nil {
		http.Error(w, err.Error(), 500)
		return
	}
	defer rows.Close()

	var items []Celebrity

	for rows.Next() {
		var c Celebrity
		rows.Scan(&c.Id, &c.FullName, &c.Nationality, &c.ReqPhotoPath)
		items = append(items, c)
	}

	writeJSON(w, 200, items)
}

// GET BY ID
func getCelebrityByID(w http.ResponseWriter, r *http.Request) {
	id, err := getIDFromRequest(r)
	if err != nil {
		http.Error(w, "invalid id", 400)
		return
	}

	var c Celebrity
	err = db.QueryRow(
		"SELECT id, fullName, nationality, reqPhotoPath FROM celebrities WHERE id = $1", id).
		Scan(&c.Id, &c.FullName, &c.Nationality, &c.ReqPhotoPath)

	if err == sql.ErrNoRows {
		http.Error(w, "celebrity not found", 404)
		return
	}

	writeJSON(w, 200, c)
}

// POST
func createCelebrity(w http.ResponseWriter, r *http.Request) {
	var c Celebrity
	if err := json.NewDecoder(r.Body).Decode(&c); err != nil {
		http.Error(w, "invalid json", 400)
		return
	}

	_, err := db.Exec(
		"INSERT INTO celebrities (id, fullName, nationality, reqPhotoPath) VALUES ($1,$2,$3,$4)",
		c.Id, c.FullName, c.Nationality, c.ReqPhotoPath)

	if err != nil {
		http.Error(w, "duplicate id", 409)
		return
	}

	writeJSON(w, 201, c)
}

// PUT
func updateCelebrity(w http.ResponseWriter, r *http.Request) {
	id, err := getIDFromRequest(r)
	if err != nil {
		http.Error(w, "invalid id", 400)
		return
	}

	var c Celebrity
	if err := json.NewDecoder(r.Body).Decode(&c); err != nil {
		http.Error(w, "invalid json", 400)
		return
	}

	res, err := db.Exec(
		"UPDATE celebrities SET fullName=$1, nationality=$2, reqPhotoPath=$3 WHERE id=$4",
		c.FullName, c.Nationality, c.ReqPhotoPath, id)

	if err != nil {
		http.Error(w, err.Error(), 500)
		return
	}

	rows, _ := res.RowsAffected()
	if rows == 0 {
		http.Error(w, "celebrity not found", 404)
		return
	}

	c.Id = id
	writeJSON(w, 200, c)
}

// DELETE
func deleteCelebrity(w http.ResponseWriter, r *http.Request) {
	id, err := getIDFromRequest(r)
	if err != nil {
		http.Error(w, "invalid id", 400)
		return
	}

	res, err := db.Exec("DELETE FROM celebrities WHERE id=$1", id)
	if err != nil {
		http.Error(w, err.Error(), 500)
		return
	}

	rows, _ := res.RowsAffected()
	if rows == 0 {
		http.Error(w, "celebrity not found", 404)
		return
	}

	w.WriteHeader(204)
}

func getIDFromRequest(r *http.Request) (int, error) {
	vars := mux.Vars(r)
	return strconv.Atoi(vars["id"])
}

func writeJSON(w http.ResponseWriter, status int, v any) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(v)
}
