package main

import (
	"log"
	"net/http"
)

func router(w http.ResponseWriter, r *http.Request) {
	switch r.Method {
	case "GET":
		handler(w, r)
	case "POST":
		handler(w, r)
	case "PUT":
		handler(w, r)
	default:
		http.Error(w, "Method not Allowed", http.StatusMethodNotAllowed)
	}
}

func main() {
	http.HandleFunc("/", router)
	log.Println("Сервер: http://localhost:3000")
	log.Fatal(http.ListenAndServe(":3000", nil))
}
