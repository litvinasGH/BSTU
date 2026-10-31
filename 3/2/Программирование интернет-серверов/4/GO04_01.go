package main

import (
	"encoding/json"
	"errors"
	"log"
	"net/http"
	"os"
	"strconv"
	"sync"

	"github.com/gorilla/mux"
)

type Celebrity struct {
	Id           int    `json:"id"`
	FullName     string `json:"fullName"`
	Nationality  string `json:"nationality"`
	ReqPhotoPath string `json:"reqPhotoPath"`
}

var (
	dataFile = "celebrities.json"
	mu       sync.Mutex
)

func main() {
	r := mux.NewRouter()

	r.HandleFunc("/Celebrities/All", getAllCelebrities).Methods(http.MethodGet)
	r.HandleFunc("/Celebrities/{id}", getCelebrityByID).Methods(http.MethodGet)
	r.HandleFunc("/Celebrities", createCelebrity).Methods(http.MethodPost)
	r.HandleFunc("/Celebrities/{id}", updateCelebrity).Methods(http.MethodPut)
	r.HandleFunc("/Celebrities/{id}", deleteCelebrity).Methods(http.MethodDelete)

	addr := ":3000"
	log.Printf("GO04_01 started on %s", addr)
	if err := http.ListenAndServe(addr, loggingMiddleware(r)); err != nil {
		log.Fatal(err)
	}
}

func loggingMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		log.Printf("%s %s from %s", r.Method, r.URL.Path, r.RemoteAddr)
		next.ServeHTTP(w, r)
	})
}

func getAllCelebrities(w http.ResponseWriter, r *http.Request) {
	mu.Lock()
	defer mu.Unlock()

	items, err := readCelebrities()
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	writeJSON(w, http.StatusOK, items)
}

func getCelebrityByID(w http.ResponseWriter, r *http.Request) {
	id, err := getIDFromRequest(r)
	if err != nil {
		http.Error(w, "invalid id", http.StatusBadRequest)
		return
	}

	mu.Lock()
	defer mu.Unlock()

	items, err := readCelebrities()
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	for _, c := range items {
		if c.Id == id {
			writeJSON(w, http.StatusOK, c)
			return
		}
	}

	http.Error(w, "celebrity not found", http.StatusNotFound)
}

func createCelebrity(w http.ResponseWriter, r *http.Request) {
	var newItem Celebrity
	if err := json.NewDecoder(r.Body).Decode(&newItem); err != nil {
		http.Error(w, "invalid json", http.StatusBadRequest)
		return
	}

	if newItem.Id == 0 {
		http.Error(w, "id is required", http.StatusBadRequest)
		return
	}

	mu.Lock()
	defer mu.Unlock()

	items, err := readCelebrities()
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	for _, c := range items {
		if c.Id == newItem.Id {
			http.Error(w, "duplicate id", http.StatusConflict)
			return
		}
	}

	items = append(items, newItem)
	if err := saveCelebrities(items); err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	writeJSON(w, http.StatusCreated, newItem)
}

func updateCelebrity(w http.ResponseWriter, r *http.Request) {
	id, err := getIDFromRequest(r)
	if err != nil {
		http.Error(w, "invalid id", http.StatusBadRequest)
		return
	}

	var updated Celebrity
	if err := json.NewDecoder(r.Body).Decode(&updated); err != nil {
		http.Error(w, "invalid json", http.StatusBadRequest)
		return
	}

	mu.Lock()
	defer mu.Unlock()

	items, err := readCelebrities()
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	found := false
	for i, c := range items {
		if c.Id == id {
			updated.Id = id
			items[i] = updated
			found = true
			break
		}
	}

	if !found {
		http.Error(w, "celebrity not found", http.StatusNotFound)
		return
	}

	if err := saveCelebrities(items); err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	writeJSON(w, http.StatusOK, updated)
}

func deleteCelebrity(w http.ResponseWriter, r *http.Request) {
	id, err := getIDFromRequest(r)
	if err != nil {
		http.Error(w, "invalid id", http.StatusBadRequest)
		return
	}

	mu.Lock()
	defer mu.Unlock()

	items, err := readCelebrities()
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	index := -1
	for i, c := range items {
		if c.Id == id {
			index = i
			break
		}
	}

	if index == -1 {
		http.Error(w, "celebrity not found", http.StatusNotFound)
		return
	}

	items = append(items[:index], items[index+1:]...)
	if err := saveCelebrities(items); err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	w.WriteHeader(http.StatusNoContent)
}

func getIDFromRequest(r *http.Request) (int, error) {
	vars := mux.Vars(r)
	return strconv.Atoi(vars["id"])
}

func readCelebrities() ([]Celebrity, error) {
	data, err := os.ReadFile(dataFile)
	if err != nil {
		if errors.Is(err, os.ErrNotExist) {
			return []Celebrity{}, nil
		}
		return nil, err
	}

	if len(data) == 0 {
		return []Celebrity{}, nil
	}

	var items []Celebrity
	if err := json.Unmarshal(data, &items); err != nil {
		return nil, err
	}

	return items, nil
}

func saveCelebrities(items []Celebrity) error {
	data, err := json.MarshalIndent(items, "", "  ")
	if err != nil {
		return err
	}

	return os.WriteFile(dataFile, data, 0644)
}

func writeJSON(w http.ResponseWriter, status int, v any) {
	w.Header().Set("Content-Type", "application/json; charset=utf-8")
	w.WriteHeader(status)
	_ = json.NewEncoder(w).Encode(v)
}
