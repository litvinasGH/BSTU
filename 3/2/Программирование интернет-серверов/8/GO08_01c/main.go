package main

import (
	"log"
	"time"

	"github.com/gorilla/websocket"
)

func main() {
	url := "ws://localhost:3000/ws"

	conn, _, err := websocket.DefaultDialer.Dial(url, nil)
	if err != nil {
		log.Fatal("Connection error:", err)
	}
	defer conn.Close()

	log.Println("Connected to server")

	for i := 1; i <= 5; i++ {
		msg := "message " + string(rune('0'+i))

		log.Println("Sending:", msg)

		err := conn.WriteMessage(websocket.TextMessage, []byte(msg))
		if err != nil {
			log.Println("Write error:", err)
			return
		}

		_, response, err := conn.ReadMessage()
		if err != nil {
			log.Println("Read error:", err)
			return
		}

		log.Println("Received:", string(response))

		time.Sleep(1 * time.Second)
	}

	log.Println("Client finished")
}
