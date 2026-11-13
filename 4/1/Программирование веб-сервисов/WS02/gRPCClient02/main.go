package main

import (
	"context"
	"fmt"
	"log"
	"os"
	"time"

	pb "example.com/grpcclient02/calculatorpb"

	"google.golang.org/grpc"
	"google.golang.org/grpc/credentials/insecure"
	"google.golang.org/grpc/status"
)

func main() {
	address := "localhost:5002"


	if len(os.Args) > 1 {
		address = os.Args[1]
	}

	conn, err := grpc.NewClient(
		address,
		grpc.WithTransportCredentials(insecure.NewCredentials()),
	)
	if err != nil {
		log.Fatal(err)
	}
	defer conn.Close()

	client := pb.NewCalculatorClient(conn)

	ctx, cancel := context.WithTimeout(
		context.Background(), 10*time.Second,
	)
	defer cancel()

	fmt.Println("Server:", address)

	// SUM
	sum, err := client.Sum(ctx, &pb.BinaryRequest{X: 10, Y: 5})
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("SUM(10, 5) = %v\n", sum.Result)

	// SUB
	sub, err := client.Sub(ctx, &pb.BinaryRequest{X: 10, Y: 5})
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("SUB(10, 5) = %v\n", sub.Result)

	// MUL
	mul, err := client.Mul(ctx, &pb.BinaryRequest{X: 10, Y: 5})
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("MUL(10, 5) = %v\n", mul.Result)

	// DIV
	div, err := client.Div(ctx, &pb.BinaryRequest{X: 10, Y: 5})
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("DIV(10, 5) = %v\n", div.Result)

	// FACT
	fact, err := client.Fact(ctx, &pb.FactorialRequest{X: 5})
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("FACT(5) = %v\n", fact.Result)

	_, err = client.Div(ctx, &pb.BinaryRequest{X: 10, Y: 0})
	printError("DIV(10, 0)", err)


	_, err = client.Fact(ctx, &pb.FactorialRequest{X: 13})
	printError("FACT(13)", err)

}

func printError(operation string, err error) {
	if err == nil {
		fmt.Printf("%s: expected error, but got none\n", operation)
		return
	}

	st := status.Convert(err)
	fmt.Printf("%s error [%s]: %s\n",
		operation, st.Code(), st.Message())
}