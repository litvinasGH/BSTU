package main

import (
	P03_02 "GO03_02/P03_02"
	"fmt"
	"log"
	"net/http"
)

func router(w http.ResponseWriter, r *http.Request) {
	switch r.Method {
	case "GET":
		switch r.URL.Path {
		case "/S":
			P03_02.PlusGet()
		case "/G":
			fmt.Fprint(w, P03_02.GenStr())
		default:
			http.NotFound(w, r)
		}
	case "POST":
		if r.URL.Path == "/S" {
			P03_02.PlusPost()
		} else {
			http.NotFound(w, r)
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
