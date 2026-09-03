package main

import (
	"database/sql"
	"fmt"
	"log"
	"net/http"
	"os"
	"strconv"
	"strings"

	"github.com/graphql-go/graphql"
	"github.com/graphql-go/handler"
	_ "github.com/jackc/pgx/v5/stdlib"
)

type Celebrity struct {
	Id           int    `json:"id"`
	FullName     string `json:"fullName"`
	Nationality  string `json:"nationality"`
	ReqPhotoPath string `json:"reqPhotoPath"`
}

var db *sql.DB

func main() {
	initDB()

	schema := mustSchema()
	gqlHandler := handler.New(&handler.Config{
		Schema:   &schema,
		Pretty:   true,
		GraphiQL: true,
	})

	mux := http.NewServeMux()
	mux.Handle("/graphql", loggingMiddleware(gqlHandler))
	mux.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "text/plain; charset=utf-8")
		_, _ = w.Write([]byte("GO10_01 GraphQL server is running. Open /graphql\n"))
	})

	port := getEnv("PORT", "3000")
	log.Printf("GO10_01 started on :%s", port)
	log.Fatal(http.ListenAndServe(":"+port, mux))
}

func initDB() {
	var err error

	dsn := os.Getenv("DATABASE_URL")
	if dsn == "" {
		host := getEnv("DB_HOST", "localhost")
		port := getEnv("DB_PORT", "5432")
		user := getEnv("DB_USER", "postgres")
		pass := getEnv("DB_PASSWORD", "postgres")
		name := getEnv("DB_NAME", "gisdb")

		dsn = fmt.Sprintf(
			"host=%s user=%s password=%s dbname=%s port=%s sslmode=disable",
			host, user, pass, name, port,
		)
	}

	db, err = sql.Open("pgx", dsn)
	if err != nil {
		log.Fatal(err)
	}

	if err := db.Ping(); err != nil {
		log.Fatal(err)
	}

	_, err = db.Exec(`
	CREATE TABLE IF NOT EXISTS celebrities (
		id INTEGER PRIMARY KEY,
		fullName TEXT NOT NULL,
		nationality TEXT NOT NULL,
		reqPhotoPath TEXT
	);`)
	if err != nil {
		log.Fatal(err)
	}
}

func mustSchema() graphql.Schema {
	celebrityType := graphql.NewObject(graphql.ObjectConfig{
		Name: "Celebrity",
		Fields: graphql.Fields{
			"id": &graphql.Field{
				Type: graphql.NewNonNull(graphql.Int),
				Resolve: func(p graphql.ResolveParams) (interface{}, error) {
					c, ok := asCelebrity(p.Source)
					if !ok {
						return nil, nil
					}
					return c.Id, nil
				},
			},
			"fullName": &graphql.Field{
				Type: graphql.NewNonNull(graphql.String),
				Resolve: func(p graphql.ResolveParams) (interface{}, error) {
					c, ok := asCelebrity(p.Source)
					if !ok {
						return nil, nil
					}
					return c.FullName, nil
				},
			},
			"nationality": &graphql.Field{
				Type: graphql.NewNonNull(graphql.String),
				Resolve: func(p graphql.ResolveParams) (interface{}, error) {
					c, ok := asCelebrity(p.Source)
					if !ok {
						return nil, nil
					}
					return c.Nationality, nil
				},
			},
			"reqPhotoPath": &graphql.Field{
				Type: graphql.String,
				Resolve: func(p graphql.ResolveParams) (interface{}, error) {
					c, ok := asCelebrity(p.Source)
					if !ok {
						return nil, nil
					}
					return c.ReqPhotoPath, nil
				},
			},
		},
	})

	query := graphql.NewObject(graphql.ObjectConfig{
		Name: "Query",
		Fields: graphql.Fields{
			"celebrities": &graphql.Field{
				Type: graphql.NewNonNull(graphql.NewList(graphql.NewNonNull(celebrityType))),
				Resolve: func(p graphql.ResolveParams) (interface{}, error) {
					return getAllCelebrities()
				},
			},
			"celebrity": &graphql.Field{
				Type: celebrityType,
				Args: graphql.FieldConfigArgument{
					"id": &graphql.ArgumentConfig{Type: graphql.NewNonNull(graphql.Int)},
				},
				Resolve: func(p graphql.ResolveParams) (interface{}, error) {
					id := p.Args["id"].(int)
					return getCelebrityByID(id)
				},
			},
		},
	})

	mutation := graphql.NewObject(graphql.ObjectConfig{
		Name: "Mutation",
		Fields: graphql.Fields{
			"createCelebrity": &graphql.Field{
				Type: celebrityType,
				Args: graphql.FieldConfigArgument{
					"id":           &graphql.ArgumentConfig{Type: graphql.NewNonNull(graphql.Int)},
					"fullName":     &graphql.ArgumentConfig{Type: graphql.NewNonNull(graphql.String)},
					"nationality":  &graphql.ArgumentConfig{Type: graphql.NewNonNull(graphql.String)},
					"reqPhotoPath": &graphql.ArgumentConfig{Type: graphql.String},
				},
				Resolve: func(p graphql.ResolveParams) (interface{}, error) {
					c := Celebrity{
						Id:          p.Args["id"].(int),
						FullName:    p.Args["fullName"].(string),
						Nationality: p.Args["nationality"].(string),
					}
					if v, ok := p.Args["reqPhotoPath"].(string); ok {
						c.ReqPhotoPath = v
					}
					return createCelebrity(c)
				},
			},
			"updateCelebrity": &graphql.Field{
				Type: celebrityType,
				Args: graphql.FieldConfigArgument{
					"id":           &graphql.ArgumentConfig{Type: graphql.NewNonNull(graphql.Int)},
					"fullName":     &graphql.ArgumentConfig{Type: graphql.NewNonNull(graphql.String)},
					"nationality":  &graphql.ArgumentConfig{Type: graphql.NewNonNull(graphql.String)},
					"reqPhotoPath": &graphql.ArgumentConfig{Type: graphql.String},
				},
				Resolve: func(p graphql.ResolveParams) (interface{}, error) {
					id := p.Args["id"].(int)
					c := Celebrity{
						FullName:    p.Args["fullName"].(string),
						Nationality: p.Args["nationality"].(string),
					}
					if v, ok := p.Args["reqPhotoPath"].(string); ok {
						c.ReqPhotoPath = v
					}
					return updateCelebrity(id, c)
				},
			},
			"deleteCelebrity": &graphql.Field{
				Type: graphql.Boolean,
				Args: graphql.FieldConfigArgument{
					"id": &graphql.ArgumentConfig{Type: graphql.NewNonNull(graphql.Int)},
				},
				Resolve: func(p graphql.ResolveParams) (interface{}, error) {
					id := p.Args["id"].(int)
					return true, deleteCelebrity(id)
				},
			},
		},
	})

	schema, err := graphql.NewSchema(graphql.SchemaConfig{
		Query:    query,
		Mutation: mutation,
	})
	if err != nil {
		log.Fatal(err)
	}

	return schema
}

func getAllCelebrities() ([]Celebrity, error) {
	rows, err := db.Query(`SELECT id, fullName, nationality, reqPhotoPath FROM celebrities ORDER BY id`)
	if err != nil {
		return nil, err
	}
	defer rows.Close()

	items := make([]Celebrity, 0)
	for rows.Next() {
		var c Celebrity
		if err := rows.Scan(&c.Id, &c.FullName, &c.Nationality, &c.ReqPhotoPath); err != nil {
			return nil, err
		}
		items = append(items, c)
	}
	return items, rows.Err()
}

func getCelebrityByID(id int) (*Celebrity, error) {
	var c Celebrity
	err := db.QueryRow(
		`SELECT id, fullName, nationality, reqPhotoPath
		 FROM celebrities
		 WHERE id = $1`, id).
		Scan(&c.Id, &c.FullName, &c.Nationality, &c.ReqPhotoPath)

	if err == sql.ErrNoRows {
		return nil, fmt.Errorf("celebrity not found")
	}
	if err != nil {
		return nil, err
	}

	return &c, nil
}

func createCelebrity(c Celebrity) (*Celebrity, error) {
	_, err := db.Exec(
		`INSERT INTO celebrities (id, fullName, nationality, reqPhotoPath)
		 VALUES ($1, $2, $3, $4)`,
		c.Id, c.FullName, c.Nationality, c.ReqPhotoPath,
	)
	if err != nil {
		if strings.Contains(strings.ToLower(err.Error()), "duplicate") {
			return nil, fmt.Errorf("duplicate id")
		}
		return nil, err
	}
	return &c, nil
}

func updateCelebrity(id int, c Celebrity) (*Celebrity, error) {
	res, err := db.Exec(
		`UPDATE celebrities
		 SET fullName = $1, nationality = $2, reqPhotoPath = $3
		 WHERE id = $4`,
		c.FullName, c.Nationality, c.ReqPhotoPath, id,
	)
	if err != nil {
		return nil, err
	}

	rows, _ := res.RowsAffected()
	if rows == 0 {
		return nil, fmt.Errorf("celebrity not found")
	}

	c.Id = id
	return &c, nil
}

func deleteCelebrity(id int) error {
	res, err := db.Exec(`DELETE FROM celebrities WHERE id = $1`, id)
	if err != nil {
		return err
	}

	rows, _ := res.RowsAffected()
	if rows == 0 {
		return fmt.Errorf("celebrity not found")
	}
	return nil
}

func asCelebrity(v interface{}) (Celebrity, bool) {
	switch x := v.(type) {
	case Celebrity:
		return x, true
	case *Celebrity:
		if x == nil {
			return Celebrity{}, false
		}
		return *x, true
	default:
		return Celebrity{}, false
	}
}

func loggingMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		log.Printf("%s %s from %s", r.Method, r.URL.Path, r.RemoteAddr)
		next.ServeHTTP(w, r)
	})
}

func getEnv(key, fallback string) string {
	v := os.Getenv(key)
	if v == "" {
		return fallback
	}
	return v
}

func mustAtoi(s string) int {
	n, err := strconv.Atoi(s)
	if err != nil {
		log.Fatal(err)
	}
	return n
}
