
package main

import (
    "bufio"
    "bytes"
    "encoding/json"
    "fmt"
    "net/http"
    "os"
    "strconv"
    "strings"
    "time"
)

// JSON-RPC запрос
type RPCRequest struct {
    JSONRPC string `json:"jsonrpc"`
    Method  string `json:"method"`
    Params  any    `json:"params"`
    ID      int    `json:"id"`
}

// JSON-RPC ошибка
type RPCError struct {
    Code    int    `json:"code"`
    Message string `json:"message"`
}

// JSON-RPC ответ
type RPCResponse struct {
    JSONRPC string          `json:"jsonrpc"`
    Result  json.RawMessage `json:"result"`
    Error   *RPCError       `json:"error"`
    ID      json.RawMessage `json:"id"`
}

// HTTP-клиент
var httpClient = &http.Client{
    Timeout: 10 * time.Second,
}

var serverURL = func() string {
    url := os.Getenv("RPC_URL")
    if url == "" {
        url = "http://localhost:5269/rpc"
    }
    return url
}()





func sendRequest(request RPCRequest) (*RPCResponse, error) {
    data, err := json.Marshal(request)
    if err != nil {
        return nil, err
    }

    response, err := httpClient.Post(
        serverURL,
        "application/json",
        bytes.NewBuffer(data),
    )
    if err != nil {
        return nil, err
    }
    defer response.Body.Close()

    if response.StatusCode != http.StatusOK {
        return nil, fmt.Errorf(
            "HTTP error: %s",
            response.Status,
        )
    }

    var result RPCResponse

    if err := json.NewDecoder(response.Body).Decode(&result); err != nil {
        return nil, err
    }

    return &result, nil
}



func printResponse(response *RPCResponse) {
    if response.Error != nil {
        fmt.Printf(
            "Ошибка %d: %s\n",
            response.Error.Code,
            response.Error.Message,
        )
        return
    }

    fmt.Printf("Результат: %s\n", response.Result)
}


func positionalCall(method string, params []float64) {
    request := RPCRequest{
        JSONRPC: "2.0",
        Method:  method,
        Params:  params,
        ID:      1,
    }

    response, err := sendRequest(request)
    if err != nil {
        fmt.Println("Ошибка отправки:", err)
        return
    }

    printResponse(response)
}


func namedCall(method string, x, y float64) {
    request := RPCRequest{
        JSONRPC: "2.0",
        Method:  method,
        Params: map[string]float64{
            "x": x,
            "y": y,
        },
        ID: 2,
    }

    response, err := sendRequest(request)
    if err != nil {
        fmt.Println("Ошибка отправки:", err)
        return
    }

    printResponse(response)
}


func factorialCall(x int, named bool) {
    var params any

    if named {
        params = map[string]int{"x": x}
    } else {
        params = []int{x}
    }

    request := RPCRequest{
        JSONRPC: "2.0",
        Method:  "FACT",
        Params:  params,
        ID:      3,
    }

    response, err := sendRequest(request)
    if err != nil {
        fmt.Println("Ошибка отправки:", err)
        return
    }

    printResponse(response)
}


func batchCall() {
    requests := []RPCRequest{
        {
            JSONRPC: "2.0",
            Method:  "SUM",
            Params:  []float64{10, 5},
            ID:      10,
        },
        {
            JSONRPC: "2.0",
            Method:  "SUB",
            Params:  []float64{10, 5},
            ID:      11,
        },
        {
            JSONRPC: "2.0",
            Method:  "MUL",
            Params:  []float64{10, 5},
            ID:      12,
        },
        {
            JSONRPC: "2.0",
            Method:  "DIV",
            Params:  []float64{10, 2},
            ID:      13,
        },
        {
            JSONRPC: "2.0",
            Method:  "FACT",
            Params:  []int{5},
            ID:      14,
        },
    }

    data, err := json.Marshal(requests)
    if err != nil {
        fmt.Println("Ошибка формирования пакета:", err)
        return
    }

    response, err := httpClient.Post(
        serverURL,
        "application/json",
        bytes.NewBuffer(data),
    )
    if err != nil {
        fmt.Println("Ошибка отправки:", err)
        return
    }
    defer response.Body.Close()

    if response.StatusCode != http.StatusOK {
        fmt.Println("HTTP error:", response.Status)
        return
    }

    var results []RPCResponse

    if err := json.NewDecoder(response.Body).Decode(&results); err != nil {
        fmt.Println("Ошибка чтения ответа:", err)
        return
    }

    fmt.Println("\nРезультаты пакетного запроса:")

    for _, result := range results {
        fmt.Printf("ID %s: ", result.ID)

        if result.Error != nil {
            fmt.Printf(
                "Ошибка %d: %s\n",
                result.Error.Code,
                result.Error.Message,
            )
        } else {
            fmt.Printf("%s\n", result.Result)
        }
    }
}



func runInteractive() {
    reader := bufio.NewReader(os.Stdin)

    for {
        fmt.Println("1 - SUM")
        fmt.Println("2 - SUB")
        fmt.Println("3 - MUL")
        fmt.Println("4 - DIV")
        fmt.Println("5 - FACT")
        fmt.Println("6 - Batch (пакетная обработка)")
        fmt.Println("0 - Выход")

        fmt.Print("Выберите операцию: ")

        choice, _ := reader.ReadString('\n')
        choice = strings.TrimSpace(choice)

        if choice == "0" {
            return
        }

        if choice == "6" {
            batchCall()
            continue
        }

        if choice < "1" || choice > "5" {
            fmt.Println("Неизвестная операция")
            continue
        }

        fmt.Print("Тип параметров (1 - позиционные, 2 - именованные): ")
        mode, _ := reader.ReadString('\n')
        mode = strings.TrimSpace(mode)

        if mode != "1" && mode != "2" {
            fmt.Println("Неизвестный тип параметров")
            continue
        }

        method := map[string]string{
            "1": "SUM",
            "2": "SUB",
            "3": "MUL",
            "4": "DIV",
            "5": "FACT",
        }[choice]

        if method == "FACT" {
            fmt.Print("Введите x: ")
            input, _ := reader.ReadString('\n')

            x, err := strconv.Atoi(strings.TrimSpace(input))
            if err != nil {
                fmt.Println("Некорректное число")
                continue
            }

            factorialCall(x, mode == "2")
            continue
        }

        fmt.Print("Введите x: ")
        inputX, _ := reader.ReadString('\n')

        fmt.Print("Введите y: ")
        inputY, _ := reader.ReadString('\n')

        x, errX := strconv.ParseFloat(
            strings.ReplaceAll(strings.TrimSpace(inputX), ",", "."),
            64,
        )

        y, errY := strconv.ParseFloat(
            strings.ReplaceAll(strings.TrimSpace(inputY), ",", "."),
            64,
        )

        if errX != nil || errY != nil {
            fmt.Println("Некорректные числа")
            continue
        }

        if mode == "1" {
            positionalCall(method, []float64{x, y})
        } else {
            namedCall(method, x, y)
        }
    }
}

func main() {
    fmt.Println("JSON-RPC Go Client")
    fmt.Println("Server:", serverURL)

    runInteractive()
}