package main

import (
	"log"
	"net/http"
)

func router(w http.ResponseWriter, r *http.Request) {
	switch r.Method {

	case "GET", "POST", "PUT":
		switch r.URL.Path {
		case "/A", "/A/B":
			handler(w, r)
		default:
			handler(w, r)
		}
	default:
		http.Error(w, "Method not Allowed", http.StatusMethodNotAllowed)
	}
}

func main() {
	http.HandleFunc("/", router)
	log.Println("Сервер: http://localhost:3000")
	log.Fatal(http.ListenAndServe(":3000", nil))
}
