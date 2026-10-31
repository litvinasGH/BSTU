package main

import (
	"fmt"
	"log"
	"net/http"

	GO02_01lib "GO02_01/GO02_01lib"
)

const C01 = 3.14

func handler(w http.ResponseWriter, r *http.Request) {
	switch r.Method {
	case "GET":
		fmt.Fprintf(w, "C01 = %e\n"+
			"C02 = %e\nC03 = %e",
			C01, C02, GO02_01lib.C03)
	default:
		http.Error(w, "Method not Allowed", http.StatusMethodNotAllowed)
	}
}

func main() {
	http.HandleFunc("/", handler)
	log.Println("Сервер: http://localhost:3000")
	log.Fatal(http.ListenAndServe(":3000", nil))

}
