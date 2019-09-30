package database

import (
	"database/sql"
	"fmt"
	_ "github.com/lib/pq"
	"log"
)

type Credentials struct {
	Username string
	Password string
	Database string
	Host     string
	Port     uint32
}

type Instance struct {
	Database *sql.DB
}

func LoadCredentials() Credentials {
	//TODO: Get credentials from the process environment/arguments
	return Credentials{
		Username: "postgres",
		Password: "password",
		Database: "bmwadforth",
		Host:     "localhost",
		Port:     5432,
	}
}

func (c *Credentials) ConnectionString() string {
	return fmt.Sprintf("postgres://%s:%s@%s/%s?sslmode=disable", c.Username, c.Password, c.Host, c.Database)
}

func NewDatabaseConnection() *Instance {
	creds := LoadCredentials()
	db, err := sql.Open("postgres", creds.ConnectionString())
	i := Instance{
		Database: db,
	}
	if err != nil {
		log.Fatal(err)
	}

	return &i
}
