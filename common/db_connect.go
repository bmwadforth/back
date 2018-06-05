package common

import (
	enums "bmwadforth/enums"
	"database/sql"
	"fmt"
	"log"

	_ "github.com/lib/pq"
)

var SqlDB *sql.DB

func DBConnect() *sql.DB {
	connectionString := fmt.Sprintf("postgres://%s:%s@%s:%d/%s?sslmode=%s", enums.DB_Username, enums.DB_Password, enums.DB_Host, enums.DB_Port, enums.DB_Name, enums.DB_SSLMode)
	db, err := sql.Open("postgres", connectionString)
	if err != nil {
		panic(err)
	}

	err = db.Ping()
	if err != nil {
		panic(err)
	}
	log.Println("Database connection successful")
	SqlDB = db
	return db
}

func GetDB() *sql.DB {
	return SqlDB
}
