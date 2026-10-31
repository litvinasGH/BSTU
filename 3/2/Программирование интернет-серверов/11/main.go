package main

import (
	"database/sql"
	"encoding/json"
	"log"
	"net/http"
	"strconv"

	_ "go11_01/docs"

	"github.com/gorilla/mux"
	_ "github.com/jackc/pgx/v5/stdlib"

	httpSwagger "github.com/swaggo/http-swagger"
)

// @title GO11_01 REST API
// @version 1.0
// @description REST API with Swagger UI
// @host localhost:3000
// @BasePath /

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

	r.HandleFunc("/Celebrities/All", getAllCelebrities).Methods("GET")
	r.HandleFunc("/Celebrities/{id}", getCelebrityByID).Methods("GET")
	r.HandleFunc("/Celebrities", createCelebrity).Methods("POST")
	r.HandleFunc("/Celebrities/{id}", updateCelebrity).Methods("PUT")
	r.HandleFunc("/Celebrities/{id}", deleteCelebrity).Methods("DELETE")

	r.PathPrefix("/swagger/").Handler(httpSwagger.WrapHandler)

	log.Println("GO11_01 started on :3000")
	log.Println("http://localhost:3000/swagger/index.html")
	log.Fatal(http.ListenAndServe(":3000", r))

}

func initDB() {
	var err error

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

// @Summary Get all celebrities
// @Tags Celebrities
// @Produce json
// @Success 200 {array} Celebrity
// @Router /Celebrities/All [get]
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

// @Summary Get celebrity by ID
// @Tags Celebrities
// @Produce json
// @Param id path int true "Celebrity ID"
// @Success 200 {object} Celebrity
// @Router /Celebrities/{id} [get]
func getCelebrityByID(w http.ResponseWriter, r *http.Request) {
	id, _ := strconv.Atoi(mux.Vars(r)["id"])

	var c Celebrity

	err := db.QueryRow(
		"SELECT id, fullName, nationality, reqPhotoPath FROM celebrities WHERE id=$1",
		id,
	).Scan(&c.Id, &c.FullName, &c.Nationality, &c.ReqPhotoPath)

	if err != nil {
		http.Error(w, "not found", 404)
		return
	}

	writeJSON(w, 200, c)
}

// @Summary Create celebrity
// @Tags Celebrities
// @Accept json
// @Produce json
// @Param celebrity body Celebrity true "Celebrity"
// @Success 201 {object} Celebrity
// @Router /Celebrities [post]
func createCelebrity(w http.ResponseWriter, r *http.Request) {
	var c Celebrity

	json.NewDecoder(r.Body).Decode(&c)

	_, err := db.Exec(
		"INSERT INTO celebrities(id, fullName, nationality, reqPhotoPath) VALUES($1,$2,$3,$4)",
		c.Id,
		c.FullName,
		c.Nationality,
		c.ReqPhotoPath,
	)

	if err != nil {
		http.Error(w, "duplicate id", 409)
		return
	}

	writeJSON(w, 201, c)
}

// @Summary Update celebrity
// @Tags Celebrities
// @Accept json
// @Produce json
// @Param id path int true "Celebrity ID"
// @Param celebrity body Celebrity true "Celebrity"
// @Success 200 {object} Celebrity
// @Router /Celebrities/{id} [put]
func updateCelebrity(w http.ResponseWriter, r *http.Request) {
	id, _ := strconv.Atoi(mux.Vars(r)["id"])

	var c Celebrity
	json.NewDecoder(r.Body).Decode(&c)

	_, err := db.Exec(
		"UPDATE celebrities SET fullName=$1, nationality=$2, reqPhotoPath=$3 WHERE id=$4",
		c.FullName,
		c.Nationality,
		c.ReqPhotoPath,
		id,
	)

	if err != nil {
		http.Error(w, err.Error(), 500)
		return
	}

	c.Id = id
	writeJSON(w, 200, c)
}

// @Summary Delete celebrity
// @Tags Celebrities
// @Param id path int true "Celebrity ID"
// @Success 204
// @Router /Celebrities/{id} [delete]
func deleteCelebrity(w http.ResponseWriter, r *http.Request) {
	id, _ := strconv.Atoi(mux.Vars(r)["id"])

	_, err := db.Exec("DELETE FROM celebrities WHERE id=$1", id)

	if err != nil {
		http.Error(w, err.Error(), 500)
		return
	}

	w.WriteHeader(204)
}

func writeJSON(w http.ResponseWriter, status int, v any) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(v)
}
