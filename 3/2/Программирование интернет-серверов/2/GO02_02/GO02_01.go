package main

import (
	"fmt"
	"log"
	"net/http"

	GO02_01lib "GO02_02/GO02_01lib"
)

const A01 = 3

func handler(w http.ResponseWriter, r *http.Request) {
	switch r.Method {
	case "GET":
		fmt.Fprintf(w, "C01 = %d\n"+
			"C02 = %t\nC03 = %s",
			A01, A02, GO02_01lib.A03)
	default:
		http.Error(w, "Method not Allowed", http.StatusMethodNotAllowed)
	}
}

func main() {
	http.HandleFunc("/", handler)
	log.Println("Сервер: http://localhost:4000")
	log.Fatal(http.ListenAndServe(":4000", nil))

}
