package main

import (
	"encoding/json"
	"fmt"
	"log"
	"math"
	"net/http"

	"github.com/gorilla/mux"
)

var precision = 2

type RPCRequest struct {
	JSONRPC string          `json:"jsonrpc"`
	Method  string          `json:"method"`
	Params  json.RawMessage `json:"params"`
	ID      interface{}     `json:"id"`
}

type RPCResponse struct {
	JSONRPC string      `json:"jsonrpc"`
	Result  interface{} `json:"result,omitempty"`
	Error   interface{} `json:"error,omitempty"`
	ID      interface{} `json:"id"`
}

func rpcHandler(w http.ResponseWriter, r *http.Request) {
	var raw json.RawMessage

	err := json.NewDecoder(r.Body).Decode(&raw)
	if err != nil {
		log.Println("Decode error:", err)
		http.Error(w, err.Error(), 400)
		return
	}

	if len(raw) > 0 && raw[0] == '[' {
		var requests []RPCRequest

		if err := json.Unmarshal(raw, &requests); err != nil {
			http.Error(w, err.Error(), 400)
			return
		}

		var responses []RPCResponse

		for _, req := range requests {
			log.Println("Batch request:", req.Method)

			res, err := handleMethod(req.Method, req.Params)

			if req.ID == nil {
				continue
			}

			resp := RPCResponse{
				JSONRPC: "2.0",
				ID:      req.ID,
			}

			if err != nil {
				resp.Error = err.Error()
			} else {
				resp.Result = res
			}

			responses = append(responses, resp)
		}

		json.NewEncoder(w).Encode(responses)
		return
	}

	var req RPCRequest
	if err := json.Unmarshal(raw, &req); err != nil {
		http.Error(w, err.Error(), 400)
		return
	}

	log.Println("Request:", req.Method)

	res, err := handleMethod(req.Method, req.Params)

	if req.ID == nil {
		w.WriteHeader(http.StatusNoContent)
		return
	}

	resp := RPCResponse{
		JSONRPC: "2.0",
		ID:      req.ID,
	}

	if err != nil {
		resp.Error = err.Error()
	} else {
		resp.Result = res
	}

	json.NewEncoder(w).Encode(resp)
}

func handleMethod(method string, params json.RawMessage) (interface{}, error) {

	switch method {

	case "sum", "sub", "mul", "div":
		x, y, err := parseXY(params)
		if err != nil {
			return nil, err
		}

		var res float64

		switch method {
		case "sum":
			res = x + y
		case "sub":
			res = x - y
		case "mul":
			res = x * y
		case "div":
			if y == 0 {
				return nil, fmt.Errorf("division by zero")
			}
			res = x / y
		}

		return round(res), nil

	case "pre":
		n, err := parseN(params)
		if err != nil {
			return nil, err
		}
		precision = n
		return "ok", nil
	}

	return nil, fmt.Errorf("unknown method")
}

func parseXY(params json.RawMessage) (float64, float64, error) {

	var arr []float64
	if err := json.Unmarshal(params, &arr); err == nil && len(arr) == 2 {
		return arr[0], arr[1], nil
	}

	var obj map[string]float64
	if err := json.Unmarshal(params, &obj); err == nil {
		x, okX := obj["x"]
		y, okY := obj["y"]
		if okX && okY {
			return x, y, nil
		}
	}

	return 0, 0, fmt.Errorf("invalid params")
}

func parseN(params json.RawMessage) (int, error) {
	var obj map[string]int
	if err := json.Unmarshal(params, &obj); err != nil {
		return 0, err
	}
	n, ok := obj["N"]
	if !ok {
		return 0, fmt.Errorf("missing N")
	}
	return n, nil
}

func round(val float64) float64 {
	pow := math.Pow(10, float64(precision))
	return math.Round(val*pow) / pow
}

func main() {
	r := mux.NewRouter()

	r.HandleFunc("/rpc", rpcHandler).Methods("POST")

	log.Println("Server started on :3000")
	log.Fatal(http.ListenAndServe(":3000", r))
}
