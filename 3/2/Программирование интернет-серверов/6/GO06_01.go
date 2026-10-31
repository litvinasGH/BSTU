package main

import (
	"encoding/json"
	"log"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
	"gorm.io/driver/postgres"
	"gorm.io/gorm"
)

type Celebrity struct {
	Id           int    `json:"id" gorm:"primaryKey"`
	FullName     string `json:"fullName"`
	Nationality  string `json:"nationality"`
	ReqPhotoPath string `json:"reqPhotoPath"`
}

var db *gorm.DB

func main() {
	initDB()

	r := mux.NewRouter()

	r.HandleFunc("/Celebrities/All", getAllCelebrities).Methods("GET")
	r.HandleFunc("/Celebrities/{id}", getCelebrityByID).Methods("GET")
	r.HandleFunc("/Celebrities", createCelebrity).Methods("POST")
	r.HandleFunc("/Celebrities/{id}", updateCelebrity).Methods("PUT")
	r.HandleFunc("/Celebrities/{id}", deleteCelebrity).Methods("DELETE")

	log.Println("GO06_01 started on :3000")
	log.Fatal(http.ListenAndServe(":3000", r))
}

func initDB() {
	dsn := "host=localhost user=postgres password=postgres dbname=gisdb port=5432 sslmode=disable"

	var err error
	db, err = gorm.Open(postgres.Open(dsn), &gorm.Config{})
	if err != nil {
		log.Fatal(err)
	}

	db.AutoMigrate(&Celebrity{})
}

// GET ALL
func getAllCelebrities(w http.ResponseWriter, r *http.Request) {
	var items []Celebrity
	db.Find(&items)
	writeJSON(w, 200, items)
}

// GET BY ID
func getCelebrityByID(w http.ResponseWriter, r *http.Request) {
	id, _ := strconv.Atoi(mux.Vars(r)["id"])

	var c Celebrity
	if result := db.First(&c, id); result.Error != nil {
		http.Error(w, "not found", 404)
		return
	}

	writeJSON(w, 200, c)
}

// POST
func createCelebrity(w http.ResponseWriter, r *http.Request) {
	var c Celebrity
	json.NewDecoder(r.Body).Decode(&c)

	if err := db.Create(&c).Error; err != nil {
		http.Error(w, "duplicate id", 409)
		return
	}

	writeJSON(w, 201, c)
}

// PUT
func updateCelebrity(w http.ResponseWriter, r *http.Request) {
	id, _ := strconv.Atoi(mux.Vars(r)["id"])

	var c Celebrity
	if err := db.First(&c, id).Error; err != nil {
		http.Error(w, "not found", 404)
		return
	}

	json.NewDecoder(r.Body).Decode(&c)
	c.Id = id

	db.Save(&c)
	writeJSON(w, 200, c)
}

// DELETE
func deleteCelebrity(w http.ResponseWriter, r *http.Request) {
	id, _ := strconv.Atoi(mux.Vars(r)["id"])

	if err := db.Delete(&Celebrity{}, id).Error; err != nil {
		http.Error(w, "not found", 404)
		return
	}

	w.WriteHeader(204)
}

func writeJSON(w http.ResponseWriter, status int, v any) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	json.NewEncoder(w).Encode(v)
}
